using UnityEngine.InputSystem;
using UnityEngine;
using Unity.Cinemachine;

public class Shoot : MonoBehaviour
{
    public InputActionReference shoot;
    public Animator gun;
    public GameObject bulletPrefab;
    public Transform bulletspawn;
    public Transform gunpos;
    public float BaseFireRate = 0.5f;
    public float BaseBulletSpeed = 10f;
    public float spreadAngle;
    public int baseBulletCount = 1;
    private float fireCooldown = 0f;
    public float originalFireRate;
    public int basedamage = 10;
    public int fireRateUpStacks = 0;
    [Range(0f, 10f)] public float inaccuracy = 1f;
    public CinemachineImpulseSource impulseSource;

    private void OnEnable()
    {
        shoot.action.Enable();
    }

    private void OnDisable()
    {
        shoot.action.Disable();
    }
    private void Start()
    {
        spreadAngle = baseBulletCount;
        originalFireRate = BaseFireRate;
    }
    void Update()
    {
        if (shoot != null && shoot.action.IsPressed() && fireCooldown <= 0f)
        {
            if (AmmoWorks.singleton.isReloading)
                return;
            if (AmmoWorks.singleton.getcurAmmo() <= 0)
            {
                AmmoWorks.singleton.reload();
                return;
            }
            AudioManager.instance.PlayClip(AudioManager.instance.shootfx);
            Fire();
            fireCooldown = BaseFireRate;
            AmmoWorks.singleton.useAmmo(baseBulletCount);
        }

        if (fireCooldown > 0f)
            fireCooldown -= Time.deltaTime;
    }

    public void Fire()
    {
        gun.SetTrigger("Shoot");
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 fireDirection = (mouseWorldPos - gunpos.position).normalized;

        GameObject[] bulletSpawns = GameObject.FindGameObjectsWithTag("BulletSpawnPoint");
        int spawnCount = bulletSpawns.Length;

        float angleStep = (baseBulletCount > 1) ? spreadAngle / (baseBulletCount - 1) : 0f;
        float startAngle = -spreadAngle / 2f;

        float baseAngle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;

        for (int i = 0; i < spawnCount; i++)
        {
            for (int j = 0; j < baseBulletCount; j++)
            {
                float angleOffset = (baseBulletCount == 1) ? 0 : startAngle + angleStep * j;
                float randomSpread = Random.Range(-inaccuracy, inaccuracy);
                float angle = baseAngle + angleOffset + randomSpread;

                Quaternion rotation = Quaternion.Euler(0, 0, angle);
                Vector3 direction = rotation * Vector3.right;

                GameObject bullet = Instantiate(bulletPrefab, bulletSpawns[i].transform.position, rotation);
                bullet.transform.localScale = Vector2.one * Mathf.Min(basedamage / 10f, 2f);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.linearVelocity = direction * BaseBulletSpeed;
            }
        }

        if (impulseSource != null)
            impulseSource.GenerateImpulse();
    }


    public void UpdateFireRateFromStacks()
    {
        BaseFireRate = originalFireRate / Mathf.Pow(2, fireRateUpStacks);
    }

    public void AddExtraBullets(int amount)
    {
        baseBulletCount += amount;
        baseBulletCount = Mathf.Max(1, baseBulletCount); // Prevents going below 1
    }
}
