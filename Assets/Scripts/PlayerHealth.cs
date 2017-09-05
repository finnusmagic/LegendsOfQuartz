﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    [SerializeField] int startingHealth = 100;
    [SerializeField] float timeSinceLastHit = 2f;
    [SerializeField] Slider healthSlider;

    private float timer = 0f;
    private CharacterController characterController;
    private Animator anim;
    [SerializeField] private int currentHealth;
    private AudioSource audio;

    private void Awake()
    {
        Assert.IsNotNull(healthSlider);
    }

    void Start ()
    {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
        audio = GetComponent<AudioSource>();
	}
	
	void Update ()
    {
        timer += Time.deltaTime;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            if (other.tag == "Weapon")
            {
                TakeHit();
                timer = 0;
            }
        }
    }

    void TakeHit()
    {
        if (currentHealth > 0)
        {
            GameManager.instance.PlayerHit(currentHealth);
            anim.Play("Hurt");
            currentHealth -= 10;
            healthSlider.value = currentHealth;
            audio.PlayOneShot(audio.clip);
        }

        if (currentHealth <= 0)
        {
            KillPlayer();
        }
    }

    void KillPlayer()
    {
        GameManager.instance.PlayerHit(currentHealth);
        anim.SetTrigger("HeroDie");
        characterController.enabled = false;
    }
}
