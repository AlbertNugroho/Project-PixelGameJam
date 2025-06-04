using Unity.Cinemachine;
using UnityEngine;

public class BossCannon : MonoBehaviour
{
    public enum FireMode { RapidFire, Shotgun, Sniper }

    public GameObject Target;
    public GameObject bulletPrefab;
    public Transform bulletspawn;
    public CinemachineImpulseSource impulseSource;
    public Transform armOrigin;

    [Header("Rapid Fire Settings")]
    public int rapidFireBulletCount = 1;
    public int rapidFireSpreadAngle = 0;
    public float rapidFireRate = 0.1f;
    public float rapidFireBulletSpeed = 15f;
    public float rapidFireInaccuracy = 1f;
    public int rapidFireDamage = 5;

    [Header("Shotgun Settings")]
    public int shotgunBulletCount = 6;
    public int shotgunSpreadAngle = 45;
    public float shotgunFireRate = 0.8f;
    public float shotgunBulletSpeed = 10f;
    public float shotgunInaccuracy = 4f;
    public int shotgunDamage = 8;

    [Header("Sniper Settings")]
    public int sniperBulletCount = 1;
    public int sniperSpreadAngle = 0;
    public float sniperFireRate = 2f;
    public float sniperBulletSpeed = 25f;
    public float sniperInaccuracy = 0.2f;
    public int sniperDamage = 15;

    public FireMode currentMode = FireMode.Shotgun;

    private float nextFireTime = 0f;
    private float nextModeChangeTime = 0f;
    private float modeChangeInterval = 5f;

    void Update()
    {
        // Auto-fire
        if (Time.time >= nextFireTime)
        {
            AudioManager.instance.PlayClip(AudioManager.instance.BossAtk);
            Fire();
            nextFireTime = Time.time + GetCurrentFireRate();
        }

        // Randomize fire mode every 5 seconds
        if (Time.time >= nextModeChangeTime)
        {
            AudioManager.instance.PlayClip(AudioManager.instance.bossChange);
            RandomizeFireMode();
            nextModeChangeTime = Time.time + modeChangeInterval;
        }
    }


    void RandomizeFireMode()
    {
        int random = Random.Range(0, System.Enum.GetNames(typeof(FireMode)).Length);
        currentMode = (FireMode)random;
    }

    float GetCurrentFireRate()
    {
        return currentMode switch
        {
            FireMode.RapidFire => rapidFireRate,
            FireMode.Shotgun => shotgunFireRate,
            FireMode.Sniper => sniperFireRate,
            _ => 1f,
        };
    }

    int GetCurrentBulletCount()
    {
        return currentMode switch
        {
            FireMode.RapidFire => rapidFireBulletCount,
            FireMode.Shotgun => shotgunBulletCount,
            FireMode.Sniper => sniperBulletCount,
            _ => 1,
        };
    }

    int GetCurrentSpreadAngle()
    {
        return currentMode switch
        {
            FireMode.RapidFire => rapidFireSpreadAngle,
            FireMode.Shotgun => shotgunSpreadAngle,
            FireMode.Sniper => sniperSpreadAngle,
            _ => 0,
        };
    }

    float GetCurrentBulletSpeed()
    {
        return currentMode switch
        {
            FireMode.RapidFire => rapidFireBulletSpeed,
            FireMode.Shotgun => shotgunBulletSpeed,
            FireMode.Sniper => sniperBulletSpeed,
            _ => 10f,
        };
    }

    float GetCurrentInaccuracy()
    {
        return currentMode switch
        {
            FireMode.RapidFire => rapidFireInaccuracy,
            FireMode.Shotgun => shotgunInaccuracy,
            FireMode.Sniper => sniperInaccuracy,
            _ => 1f,
        };
    }

    public int GetCurrentDamage()
    {
        return currentMode switch
        {
            FireMode.RapidFire => rapidFireDamage,
            FireMode.Shotgun => shotgunDamage,
            FireMode.Sniper => sniperDamage,
            _ => 10,
        };
    }

    public void Fire()
    {
        Vector2 fireDirection = (Target.transform.position - transform.position).normalized;

        int bulletCount = GetCurrentBulletCount();
        int spreadAngle = GetCurrentSpreadAngle();
        float bulletSpeed = GetCurrentBulletSpeed();
        float currentInaccuracy = GetCurrentInaccuracy();
        int currentDamage = GetCurrentDamage();

        float angleStep = (bulletCount > 1) ? spreadAngle / (bulletCount - 1) : 0f;
        float startAngle = -spreadAngle / 2;
        float baseAngle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;

        for (int i = 0; i < bulletCount; i++)
        {
            float angleOffset = (bulletCount == 1) ? 0 : startAngle + angleStep * i;
            float randomSpread = Random.Range(-currentInaccuracy, currentInaccuracy);
            float angle = baseAngle + angleOffset + randomSpread;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Vector3 direction = rotation * Vector3.right;

            GameObject bullet = Instantiate(bulletPrefab, bulletspawn.position, rotation);
            bullet.transform.localScale = Vector3.one * (currentDamage / 7f); // Adjust size by damage
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = direction * bulletSpeed;
        }

        impulseSource?.GenerateImpulse();
    }
}
