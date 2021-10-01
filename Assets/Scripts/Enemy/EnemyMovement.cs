﻿using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;


    void Awake ()
    {
        //Cari game object dengan tag player
        player = GameObject.FindGameObjectWithTag ("Player").transform;

        //Mendapatkan Reference Component
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
    }


    void Update ()
    {
        //Memindakhkan posisi player

        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination (player.position);
        }

        //Hentikan moving
        else
        {
            nav.enabled = false;
        }
    }
}
