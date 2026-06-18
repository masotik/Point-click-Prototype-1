using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    private ItemData currentItem;
    public Image customCursor;
    DraggableItem draggableItem;

    public slot[] craftingSlots;

    public List<ItemData> itemList;
    public string[] recipes;
    public ItemData[] recipeResults;
    public bool[] recipeIsGood;
    public slot resultSlot;

    [Header("Scene Names")]
    public string goodScene = "GoodRecipe";
    public string badScene = "NotTakenRecipe";

    [Header("Result UI")]
    public GameObject resultPanel;
    public Image resultItemSprite;
    public Button continueButton;
    public Button tryAgainButton;

    private bool _currentResultIsGood;

    private void Start()
    {
        resultPanel.SetActive(false);
        continueButton.onClick.AddListener(OnContinue);
        tryAgainButton.onClick.AddListener(OnTryAgain);
    }

    private void Update()
    {   
        if (Input.GetMouseButtonUp(0))
        {
            if (currentItem != null){
                customCursor.gameObject.SetActive(false);

                slot targetSlot = null;
                //float shortestDistance = float.MaxValue;

                foreach (slot slot in craftingSlots)
                {
                    if (slot.item == null)
                    {
                        targetSlot = slot;
                        break;
                    }
                }
                if (targetSlot != null)
                {
                    targetSlot.GetComponent<Image>().sprite = currentItem.sprite;
                    targetSlot.item = currentItem;
                    itemList[targetSlot.index] = currentItem;

                    draggableItem.ResetPosition();
                    CheckForCreatedRecipes();
                }
                else
                {
                    draggableItem.ResetPosition();
                    Debug.Log("Bowl is full!");
                }

                //itemList[nearestSlot.index] = currentItem;

                // CheckForCreatedRecipes();

                currentItem = null;
            }
        }
    }

    void CheckForCreatedRecipes()
    {
        List<string> itemNames = new List<string>();
        foreach (ItemData item in itemList)
        {
            if (item != null)
                itemNames.Add(item.objectName);
        }

        if (itemNames.Count == 0) return;

        itemNames.Sort();
        string currentRecipeString = string.Join("", itemNames);
        Debug.Log("Current recipe: " + currentRecipeString);

        for(int i = 0; i < recipes.Length; i++)
        {
            List<string> sortedRecipe = new List<string>();
                
            if (recipes[i] == currentRecipeString)
            {
                if (recipeResults[i] != null)
                {
                    resultItemSprite.sprite = recipeResults[i].sprite;
                    resultItemSprite.gameObject.SetActive(true);
                }
                else
                {
                    resultItemSprite.gameObject.SetActive(false);
                }

                _currentResultIsGood = recipeIsGood[i];
                resultPanel.SetActive(true);
                return;
            }
        }
    }

    void OnContinue()
    {
        SceneManager.LoadScene(_currentResultIsGood ? goodScene : badScene);
    }

    void OnTryAgain()
    {
        resultPanel.SetActive(false);
        foreach (slot slot in craftingSlots)
        {
            slot.item = null;
            slot.GetComponent<Image>().sprite = null;
            slot.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
        for (int i = 0; i < itemList.Count; i++)
            itemList[i] = null;

        Debug.Log("Bowl cleared!");
    }

    public void OnClickSlot(slot slot)
    {
        slot.item = null;
        itemList[slot.index] = null;
        slot.gameObject.SetActive(false);
        CheckForCreatedRecipes();
    }

    public void OnMouseDownItem(ItemData item, DraggableItem draggedObject){
        if(currentItem == null)
        {
            currentItem = item;
            draggableItem = draggedObject;
            customCursor.gameObject.SetActive(true);
            customCursor.sprite = currentItem.sprite;
        }
    }
}
