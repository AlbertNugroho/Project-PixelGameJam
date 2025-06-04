using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingCell : MonoBehaviour
{
    public TextMeshProUGUI rottxt;
    public TextMeshProUGUI bonestxt;
    public TextMeshProUGUI eyetxt;
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Description;
    public Item item;
    public Image itemImage;
    public GameObject panel;

    [SerializeField] private Button craftButton;

    private void Awake()
    {
        Title.text = item.itemName;
        Description.text = item.itemDescription;
        rottxt.text = item.rotreq.ToString();
        bonestxt.text = item.bonereq.ToString();
        eyetxt.text = item.eyereq.ToString();
        itemImage.sprite = item.sprite;
        craftButton.onClick.AddListener(Craft);
    }

    private void OnEnable()
    {
        UpdatePanelState();
    }

    private void Update()
    {
        UpdatePanelState();
    }

    private void UpdatePanelState()
    {
        bool notEnoughMaterials = !HasEnoughMaterials();
        if (panel.activeSelf != notEnoughMaterials)
        {
            panel.SetActive(notEnoughMaterials);
        }
    }

    private bool HasEnoughMaterials()
    {
        var mat = MaterialsWork.instance;
        return mat.rottenflesh >= item.rotreq && mat.bones >= item.bonereq && mat.eyes >= item.eyereq;
    }

    private void Craft()
    {
        if (HasEnoughMaterials())
        {
            MaterialsWork.instance.Craft(item.rotreq, item.bonereq, item.eyereq);
            Inventory.Singleton.SpawnInventoryItem(item);
            UpdatePanelState();
        }
    }
}
