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
    private float spreadAngle;
    public int baseBulletCount = 1;
    private float fireCooldown = 0f;
    public float originalFireRate;
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
        originalFireRate = BaseFireRate;
    }
    void Update()
    {
        if (shoot != null && shoot.action.IsPressed() && fireCooldown <= 0f)
        {
            Fire();
            fireCooldown = BaseFireRate;
        }

        if (fireCooldown > 0f)
            fireCooldown -= Time.deltaTime;
    }

    public void Fire()
    {
        spreadAngle = baseBulletCount;
        gun.SetTrigger("Shoot");
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 fireDirection = (mouseWorldPos - gunpos.position).normalized;

        float angleStep = spreadAngle / (baseBulletCount - 1);
        float startAngle = -spreadAngle / 2;

        float baseAngle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;
        float currentInaccuracy = inaccuracy;

        for (int i = 0; i < baseBulletCount; i++)
        {
            float angleOffset = (baseBulletCount == 1) ? 0 : startAngle + angleStep * i;
            float randomSpread = Random.Range(-currentInaccuracy, currentInaccuracy);
            float angle = baseAngle + angleOffset + randomSpread;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Vector3 direction = rotation * Vector3.right;

            GameObject bullet = Instantiate(bulletPrefab, bulletspawn.position, rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = direction * BaseBulletSpeed;
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
