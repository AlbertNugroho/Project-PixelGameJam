using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    public static Inventory Singleton;
    public static InventoryItem carriedItem;
    [SerializeField] InventorySlot[] inventorySlots;
    [SerializeField] Transform dragableTransform;
    [SerializeField] InventoryItem itemPrefab;
    public Transform Scope;
    public Transform Barrel;
    public Transform Magazine;
    public Transform Stock;
    public Transform Grip;
    public Transform Melee;
    public Transform Throwable;

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

    public void SpawnInventoryItem(Item item = null)
    {
        Item _item = item;
        if (_item == null) ;
        {
            int random = UnityEngine.Random.Range(0, Items.Length);
            _item = Items[random];
        }

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
        if(carriedItem != null)
        {
            if (item.activeSlot.myTag != slotTag.None && item.activeSlot.myTag != carriedItem.myItem.itemTag) return;
            item.activeSlot.SetItem(carriedItem);
        }
        if(item.activeSlot.myTag != slotTag.None)
        {
            EquipEquipment(item.activeSlot.myTag, null);
        }
        carriedItem = item;
        carriedItem.canvasGroup.blocksRaycasts = false;
        item.transform.SetParent(dragableTransform);
    }

    public void EquipEquipment(slotTag tag, InventoryItem item = null)
    {
        switch(tag)
        {
            case slotTag.Scope:
                if (item == null)
                {
                    foreach (Transform child in Scope)
                    {
                        Destroy(child.gameObject);
                    }
                }
                else
                {
                    StartCoroutine(InstantiateNextFrame(item.myItem.equipmentPrefab, Scope));
                }
                break;
            case slotTag.Magazine:
                if (item == null)
                {
                    foreach (Transform child in Magazine)
                    {
                        Destroy(child.gameObject);
                    }
                }
                else
                {
                    StartCoroutine(InstantiateNextFrame(item.myItem.equipmentPrefab, Magazine));
                }
                break;
            case slotTag.Stock:
                if (item == null)
                {
                    foreach (Transform child in Stock)
                    {
                        Destroy(child.gameObject);
                    }
                }
                else
                {
                    StartCoroutine(InstantiateNextFrame(item.myItem.equipmentPrefab, Stock));
                }
                break;
            case slotTag.HandGrip:
                if (item == null)
                {
                    foreach (Transform child in Grip)
                    {
                        Destroy(child.gameObject);
                    }
                }
                else
                {
                    StartCoroutine(InstantiateNextFrame(item.myItem.equipmentPrefab, Grip));
                }
                break;
            case slotTag.Melee:
                if (item == null)
                {
                    foreach (Transform child in Melee)
                    {
                        Destroy(child.gameObject);
                    }
                }
                else
                {
                    StartCoroutine(InstantiateNextFrame(item.myItem.equipmentPrefab, Melee));
                }
                break;
            case slotTag.Throwable:
                if (item == null)
                {
                    foreach (Transform child in Throwable)
                    {
                        Destroy(child.gameObject);
                    }
                }
                else
                {
                    StartCoroutine(InstantiateNextFrame(item.myItem.equipmentPrefab, Throwable));
                }
                break;
            case slotTag.Barrel:
                if (item == null)
                {
                    foreach (Transform child in Barrel)
                    {
                        Destroy(child.gameObject);
                    }
                }
                else
                {
                    StartCoroutine(InstantiateNextFrame(item.myItem.equipmentPrefab, Barrel));
                }
                break;
        }

    }
    IEnumerator InstantiateNextFrame(GameObject prefab, Transform parent)
    {
        yield return null; // Wait one frame
        Instantiate(prefab, parent);
    }
}
