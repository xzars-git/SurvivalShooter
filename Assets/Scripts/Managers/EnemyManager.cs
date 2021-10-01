﻿using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    [SerializeField]
    EnemyFactory factory;
    IFactory Factory { get { return factory as IFactory; } }

    void Start()
    {
        //Mengeksekusi fungsi spawn setiap beberapa detik sesuai dengan nilai spawn time
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        // Jika player telah mati maka tidak membuat enemy baru
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }

        //Mendapatkan nilai random
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        int spawnEnemy = Random.Range(0, 3);

        //Memduplikasi enemy
        Factory.FactoryMethod(spawnEnemy);
    }
}
