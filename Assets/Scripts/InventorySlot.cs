using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public InventoryItem myItem { get; set; }
    public slotTag myTag;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Inventory.carriedItem == null) return;
            if (myTag != slotTag.None && Inventory.carriedItem.myItem.itemTag != myTag) return;
            SetItem(Inventory.carriedItem);
        }
    }

    public void SetItem(InventoryItem item)
    {
        Inventory.carriedItem = null;

        item.activeSlot.myItem = null;


        myItem = item;
        myItem.activeSlot = this;
        myItem.transform.SetParent(transform);
        myItem.canvasGroup.blocksRaycasts = true;

        if(myTag != slotTag.None)
        {
            Inventory.Singleton.EquipEquipment(myTag, myItem);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
