using UnityEngine;

public enum slotTag {None, Equipment}
public enum itemTag { None, Scope, Barrel, Magazine, Stock, Grip, Melee}

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public int itemID;
    public Sprite sprite;
    public slotTag itemTag;
    public itemTag itemType;
    public string itemName;
    [TextArea] public string itemDescription;
    public float addFireRate = 0f;
    public float addBulletSpeed = 0f;
    public float addSpreadAngle = 0;
    public int addbullet = 0;
    public int adddamage = 0;
    public float addaccuracy = 0f;
    public int addmaxammo = 0;
    public int rotreq;
    public int bonereq;
    public int eyereq;

    [Header("If the item can be equiped")]
    public GameObject equipmentPrefab;
}
