using Pathfinding;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnWaves : MonoBehaviour
{
    float remainingTime = 0;
    public GameObject[] enemyPrefab;
    public TextMeshProUGUI timeText;
    public Tilemap groundTilemap;
    public Transform player;
    public float spawnRadius = 15f;
    public int maxEnemiesToSpawn = 10;
    private bool waveStarted = false;
    public float minSpawnDistance = 10f;
    public GameObject wall;
    private int maxhealth = 30;
    private int maxDamage = 20;
    private int minhealth = 20;
    private int mindamage = 10;
    private int difficulty = 1;
    public TextMeshProUGUI difficultyText;
    void Start()
    {
        difficultyText.text = difficulty.ToString();
        UpdateTimeText();
    }

    // Update is called once per frame
    void Update()
    {
        if (!waveStarted) return;

        remainingTime = Mathf.Max(remainingTime - Time.deltaTime, 0);
        UpdateTimeText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !waveStarted)
        {
            waveStarted = true;
            remainingTime = 60;
            StartWave();
        }
    }
    private void UpdateTimeText()
    {
        int minutes = (int)(remainingTime / 60) % 60;
        int seconds = (int)(remainingTime) % 60;
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void StartWave()
    {
        AudioManager.instance.PlayMusic(AudioManager.BgAudio.Waves);
        wall.SetActive(true);
        UpdateTimeText();
        SpawnEnemies();
        InvokeRepeating("SpawnEnemies", 5, 5);
    }

    public void SpawnEnemies()
    {
        if (remainingTime <= 0)
        {
            difficulty++;
            difficultyText.text = difficulty.ToString();
            AudioManager.instance.PlayMusic(AudioManager.BgAudio.Stage);
            waveStarted = false;
            CancelInvoke("SpawnEnemies");
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
            Health health = FindFirstObjectByType<Health>();
            health.FullHeal();
            wall.SetActive(false);
            player.position = new Vector2(-6.48f, -0.96f);
            return;
        }
        GridGraph grid = AstarPath.active.data.gridGraph;
        List<Vector3> validSpawnPositions = new List<Vector3>();

        grid.GetNodes(node =>
        {
            if (node.Walkable)
            {
                Vector3 worldPos = (Vector3)node.position;

                if (Vector3.Distance(worldPos, player.position) <= spawnRadius && Vector3.Distance(worldPos, player.position) >= minSpawnDistance)
                {
                    Vector3Int cellPos = groundTilemap.WorldToCell(worldPos);
                    if (groundTilemap.HasTile(cellPos))
                    {
                        validSpawnPositions.Add(worldPos);
                    }
                }
            }
        });
        int enemiesToSpawn = Mathf.Min(maxEnemiesToSpawn + (difficulty - 1) * 2, validSpawnPositions.Count);
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int Health = 0;
            int Damage = 0;
            Health = Random.Range(minhealth + (difficulty - 1) * 40, maxhealth + (difficulty - 1) * 40 + 1);
            Damage = Random.Range(mindamage + (difficulty - 1) * 10, maxDamage + (difficulty - 1) * 10 + 1);
            Vector3 spawnPos = validSpawnPositions[Random.Range(0, validSpawnPositions.Count)];
            GameObject Enemy = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], spawnPos, Quaternion.identity);
            float scale = Mathf.Clamp((Health / 20f + Damage / 10f) / 2f, 0.5f, 2f);
            Enemy.transform.localScale = Vector3.one * scale;
            Enemy.GetComponent<GeneralHealth>().Health = Health;
            Enemy.GetComponent<EnemyAttack>().AttackDamage = Damage;
        }
    }
}
