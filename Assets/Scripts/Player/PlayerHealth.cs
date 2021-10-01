﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;

    PlayerShooting playerShooting;

    bool isDead;
    bool damaged;

    void Awake()
    {
        //Mendapatkan reference komponen 
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();

        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }

    void Update()
    {
        //Jika terkena damage
        if (damaged)
        {
            // Merubah warna gamabar menjadi value dari flash Colour
            damageImage.color = flashColour;
        }
        else
        {
            //Fade Out damage Image
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        //Set damage to false
        damaged = false;
    }

    //fungsi untuk mendapatkan damage
    public void TakeDamage (int amount)
    {
        damaged = true;

        //mengurangi health
        currentHealth -= amount;

        //merubah tampilan dari health slider 
        healthSlider.value = currentHealth;

        //Memainkan suara ketika terkena damage 
        playerAudio.Play();

        //Memangggil method Death() jika darahnya kurang dari sama dengan 0 dan belum mati
        if(currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        playerShooting.DisableEffects();

        //mentrigger animasi die
        anim.SetTrigger("Die");

        //Memainkan suara ketika mati
        playerAudio.clip = deathClip;
        playerAudio.Play();

        //mematikan script player movement
        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }

    public void RestartLevel()
    {
        //meng load ulang scene dengan index 0 pada build setting 
        SceneManager.LoadScene(0);
    }
}
