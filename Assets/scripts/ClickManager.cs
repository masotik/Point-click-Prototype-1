using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ClickManager : MonoBehaviour
{
    //public GameObject screenIng;
    //public Canvas canvas;
    //public GameObject arrowTablet;
    GameManager gameManager;

    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    public void ClickReaction(ItemData item)
    {
        TryGettingItem(item);
    }

    public void TryGettingItem(ItemData item)
    {
        //Debug.Log("item: " + item.itemID + " required: " + item.requiredItemID + " selected: " + gameManager.selecteItemID);
        bool canGetItem = item.requiredItemID == -1 || gameManager.selecteItemID == item.requiredItemID;
        if (canGetItem)
        {
            if (item.addToInventory)
                GameManager.collectedItems.Add(item);
        }
        StartCoroutine(UpdateSceneAfterAction(item, canGetItem));
    }

    private void SwapSprite(ItemData item)
    {
        if (item.sprite == null) return;

        SpriteRenderer sr = item.GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.sprite = item.sprite;
    }

    private IEnumerator UpdateSceneAfterAction(ItemData item, bool canGetItem)
    {
        if (canGetItem)
        {
            foreach (GameObject g in item.objectsToRemove)
                Destroy(g);
            foreach (GameObject g in item.objectToSetActive)
                g.SetActive(true);
            gameManager.UpdateEquipmentCanvas();
            gameManager.CheckSpecialConditions(item);
        }
        yield return null;
    }

}
