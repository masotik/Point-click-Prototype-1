using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static List<ItemData> collectedItems = new List<ItemData>();
    public RectTransform nameTag;
    public GameObject[] localScenes;
    public Image blockingImage;
    int activeLocalScene = 0;
    public AnimationData1 mamaAnimations;

    [Header("Equipment")]
    public GameObject equipmentCanvas;
    public Image[] equipmentSlots,equipmentImages;
    public Sprite emptyItemSlotSprite;
    public Color selectedItemColor;
    public int selectedCanvasSlotsID =0, selecteItemID = -1;

    public void SelectItem(int equipmentCanvasID)
    {
        Color c = Color.white;
        c.a = 0;
        equipmentSlots[selectedCanvasSlotsID].color = c;

        if (equipmentCanvasID >= collectedItems.Count || equipmentCanvasID < 0)
        {
            selecteItemID = -1;
            selectedCanvasSlotsID = 0;
            return;
        }

        selectedCanvasSlotsID = equipmentCanvasID;
        selecteItemID = collectedItems[selectedCanvasSlotsID].itemID;

        equipmentSlots[selectedCanvasSlotsID].color = selectedItemColor;
    }

    public void ShowItemName(int equipmentCanvasID)
    {
        if(equipmentCanvasID<collectedItems.Count)
            UpdateNameTag(collectedItems[equipmentCanvasID]);
    }

    public void UpdateEquipmentCanvas()
    {
        //find how many items we have
        int itemsAmount = collectedItems.Count, itemSlotsAmount = equipmentSlots.Length;
        //replace no items with collected items
        for (int i = 0; i < itemSlotsAmount; i++)
        {
            if (i < itemsAmount)
                equipmentImages[i].sprite = collectedItems[i].itemSlotSprite;
            else
                equipmentImages[i].sprite = emptyItemSlotSprite;
        }
        //special comdition for selecting
        if (itemsAmount == 0)
            SelectItem(-1);
        else if (itemsAmount == 1)
            SelectItem(0);
    }

    public void UpdateNameTag(ItemData item)
    {
        //name
        if (item == null)
        {
            nameTag.gameObject.SetActive(false);
            return;
        }

        //nameTag.parent.gameObject.SetActive(true);

        string nameText = item.objectName;
        Vector2 size = item.nameTagSize;

        if(collectedItems.Contains(item))
        {
            nameText = item.itemName;
            size = item.itemTagSize;
        }
        
        nameTag.GetComponentInChildren<TextMeshProUGUI>().text = nameText;
            //size
        nameTag.sizeDelta = size;
        nameTag.anchoredPosition = new Vector2(item.nameTagSize.x / 2, -0.5f);
    }

    public void CheckSpecialConditions(ItemData item)
    {
        switch (item.itemID)
        {
            case -11:
                StartCoroutine(ChangeScene(0, 0));
                break;
            case -12:
                StartCoroutine(ChangeScene(1, 0));
                break;
            case -13:
                equipmentCanvas.SetActive(false);
                StartCoroutine(ChangeScene(2, 1));
                break;
        }
    }

    public IEnumerator ChangeScene(int sceneNumber,float delay)
    {
        foreach (spriteAnimator sa in FindObjectsOfType<spriteAnimator>())
        {
            sa.StopAnimation();
        }

        //Debug.Log("test");  
        Color c = blockingImage.color;
        blockingImage.enabled = true;

        while (blockingImage.color.a<1)
        {
            c.a += Time.deltaTime;
            blockingImage.color = c;
            //yield return null;
        }
        //localScenes[activeLocalScene].SetActive(false);
        nameTag.gameObject.SetActive(false);
        localScenes[activeLocalScene].gameObject.SetActive(false);
        localScenes[sceneNumber].gameObject.SetActive(true);
        activeLocalScene = sceneNumber;
        
        while (blockingImage.color.a > 0)
        {
            c.a -= Time.deltaTime;
            blockingImage.color = c;
           // yield return null;
        }
        blockingImage.enabled = false;
        
        yield return null;
    }
    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}