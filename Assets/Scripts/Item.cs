using UnityEngine;

public enum slotTag {None, Scope, Stock, Barrel, Magazine, Melee, HandGrip,Throwable}

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public Sprite sprite;
    public slotTag itemTag;

    [Header("If the item can be equiped")]
    public GameObject equipmentPrefab;
}
