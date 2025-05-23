using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    public static Inventory Singleton;
    public static InventoryItem carriedItem;
    [SerializeField] public InventorySlot[] inventorySlots;
    [SerializeField] public InventorySlot[] equipSlots;
    [SerializeField] Transform dragableTransform;
    [SerializeField] InventoryItem itemPrefab;
    public Transform Scope;
    public Transform Barrel;
    public Transform Magazine;
    public Transform Stock;
    public Transform Grip;
    public Transform Melee;
    public Transform Weapon;
    public int ToDelete;

    [Header("ItemList")]
    [SerializeField] Item[] Items;

    //[Header("Debug")]
    //[SerializeField] Button giveItemBtn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        Singleton = this;
        //giveItemBtn.onClick.AddListener(delegate { SpawnInventoryItem(); });
    }

    public void SpawnInventoryItem(Item item)
    {
        Item _item = item;

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].myItem == null)
            {
                Instantiate(itemPrefab, inventorySlots[i].transform).Initialize(_item, inventorySlots[i]);
                break;
            }
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (carriedItem == null)
            return;
        carriedItem.transform.position = Input.mousePosition;
    }

    public void SetCarriedItem(InventoryItem item)
    {
        if (carriedItem != null)
        {
            if (item.activeSlot.myTag != slotTag.None && item.activeSlot.myTag != carriedItem.myItem.itemTag) return;
            item.activeSlot.SetItem(carriedItem);
        }
        if (item.activeSlot.myTag != slotTag.None)
        {
            ToDelete = item.myItem.itemID;
            EquipEquipment(item.myItem.itemType, null); 
        }

        carriedItem = item;
        item.activeSlot.myItem = null;
        carriedItem.canvasGroup.blocksRaycasts = false;
        item.transform.SetParent(dragableTransform);
    }


    public void EquipEquipment(itemTag tag, InventoryItem item = null)
    {
        Transform parent = null;
        switch (tag)
        {
            case itemTag.Scope: parent = Scope; break;
            case itemTag.Magazine: parent = Magazine; break;
            case itemTag.Stock: parent = Stock; break;
            case itemTag.Grip: parent = Grip; break;
            case itemTag.Melee: parent = Melee; break;
            case itemTag.Barrel: parent = Barrel; break;
        }

        if (parent == null) return;

        if (item == null)
        {
            AudioManager.instance.PlayClip(AudioManager.instance.unequip);
            RemoveItemRecursively(Weapon, ToDelete);
        }
        else
        {
            AudioManager.instance.PlayClip(AudioManager.instance.equip);
            StartCoroutine(InstantiateNextFrame(item.myItem.equipmentPrefab, parent));
        }
    }
    public bool alreadyEquiped(InventoryItem item)
    {
        foreach (var slots in equipSlots)
        {
            if (slots.myItem != null && slots.myItem.myItem.itemID == item.myItem.itemID)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveItemRecursively(Transform parent, int targetID)
    {
        foreach (Transform child in parent)
        {
            ItemID invItem = child.GetComponent<ItemID>();
            if (invItem != null && invItem.id == targetID)
            {
                Debug.Log($"Destroying item with ID: {invItem.id}");
                Destroy(child.gameObject);
                return;
            }
            RemoveItemRecursively(child, targetID);
        }

        Debug.Log("notfound");
    }


    IEnumerator InstantiateNextFrame(GameObject prefab, Transform parent)
    {
        yield return null; // Wait one frame
        Instantiate(prefab, parent);
    }
}
