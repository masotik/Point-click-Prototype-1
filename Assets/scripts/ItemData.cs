    using UnityEngine;

public class ItemData : MonoBehaviour
{
    [Header("Setup")]
    public int itemID, requiredItemID;
    public bool addToInventory = true;
    public string objectName;
    public Vector2 nameTagSize = new Vector2(1, 0.5f);
    public string itemName;
    public Vector2 itemTagSize = new Vector2(1, 0.5f);

    [Header("Success")]
    public GameObject[] objectsToRemove;
    public GameObject[] objectToSetActive;
    public Sprite itemSlotSprite;

    public Sprite sprite;
}
