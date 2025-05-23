using UnityEngine;

[RequireComponent(typeof(ItemID))]
public class ChangeAtribute : MonoBehaviour
{
    Shoot shoot;
    public Item item;
    private void Awake()
    {
        shoot = FindFirstObjectByType<Shoot>();
        shoot.BaseFireRate -= item.addFireRate;
        shoot.BaseBulletSpeed += item.addBulletSpeed;
        shoot.spreadAngle += item.addSpreadAngle;
        shoot.baseBulletCount += item.addbullet;
        shoot.basedamage += item.adddamage;
        shoot.inaccuracy -= item.addaccuracy;
        if(shoot.BaseFireRate<0)
            shoot.BaseFireRate = 0.01f;
        if(shoot.inaccuracy < 0)
            shoot.inaccuracy = 0f;
        AmmoWorks.singleton.setMaxAmmo(AmmoWorks.singleton.maxAmmo + item.addmaxammo);

    }
    private void OnDestroy()
    {
        shoot.BaseFireRate += item.addFireRate;
        shoot.BaseBulletSpeed -= item.addBulletSpeed;
        shoot.spreadAngle -= item.addSpreadAngle;
        shoot.baseBulletCount -= item.addbullet;
        shoot.basedamage -= item.adddamage;
        shoot.inaccuracy += item.addaccuracy;
        AmmoWorks.singleton.setMaxAmmo(AmmoWorks.singleton.maxAmmo - item.addmaxammo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
