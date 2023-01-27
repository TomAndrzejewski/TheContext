using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private Transform TLCorner;
    [SerializeField] private Transform TRCorner;
    [SerializeField] private Transform BLCorner;
    [SerializeField] private Transform BRCorner;

    [SerializeField] private int difficulty;
    [SerializeField] private float spawnInterval;

    private float timer = 0f;

    private bool isSpawning = false;

    private void Awake()
    {
        if (instance)
            Destroy(this);
        else instance = this;

        difficulty = 1;
        spawnInterval = 0.5f;
    }

    private void Start()
    {
        // BeginSpawningEnemies();
    }

    private void Update()
    {
        if (isSpawning)
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                timer = 0f;
                SpawnEnemy();
                spawnInterval *= 1f - 0.001f * difficulty;
            }
        }
    }

    public void BeginSpawningEnemies()
    {
        isSpawning = true;
    }

    public void StopSpawningEnemies()
    {
        isSpawning = false;
    }

    private void SpawnEnemy()
    {
        float posX;
        float posY;

        bool xConst = Random.value > 0.5;
        bool side = Random.value > 0.5;

        if (xConst && side)
        {
            posX = TLCorner.position.x;
            posY = Random.Range(BLCorner.position.y, TLCorner.position.y);
        }
        else if (xConst && !side)
        {
            posX = TRCorner.position.x;
            posY = Random.Range(BLCorner.position.y, TLCorner.position.y);
        }
        else if (!xConst && side)
        {
            posX = Random.Range(TLCorner.position.x, TRCorner.position.x);
            posY = TLCorner.position.y;
        }
        else
        {
            posX = Random.Range(TLCorner.position.x, TRCorner.position.x);
            posY = BLCorner.position.y;
        }

        Instantiate(enemyPrefab, new Vector3(posX, posY, 0f), Quaternion.identity, transform);
    }
}
