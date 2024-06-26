using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class TankHealth : MonoBehaviour
    //H = Henry addition
    //J = Jackson addition
{
    // The amount of health each tank starts with
    public float m_StartingHealth = 100f;

    // A prefab that will be instantiated in Awake, then used whenever
    // the tank dies
    public GameObject m_ExplosionPrefab;

    public float m_CurrentHealth;           //H
    private bool m_Dead;
    public Image healthBar;                 //H
    public float healthAmount = 100f;       //H
    public float maxHealth = 100;           //J

    // The particle system that will play when the tank is destroyed
    private ParticleSystem m_ExplosionParticles;

    private void Awake()
    {
        // Instantiate the explosion prefab and get a reference to
        // the particle system on it
        m_ExplosionParticles =
        Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();

        // Disable the prefab so it can be activated when it's required
        m_ExplosionParticles.gameObject.SetActive(false);
    }

    private void OnEnable()
    {

        // when the tank is enabled, reset the tank's health and whether
        // or not it's dead
        m_CurrentHealth = m_StartingHealth;
        if (gameObject.tag == "Player")     //H
        {
            healthBar.fillAmount = m_StartingHealth;        //H
        }
        m_Dead = false;
    }

    public void TakeDamage(float amount)
    {
        // Reduce current health by the amount of damage done
        m_CurrentHealth -= amount;
        if(m_CurrentHealth < 0f)
        {
            m_CurrentHealth = 0f;
        }

        if (gameObject.tag == "Player")     //H
        {
            healthAmount = m_CurrentHealth;     //H
            healthBar.fillAmount = healthAmount / 100f;     //H
        }

        // if the current health is at or below zero and it has not yet
        // been registered, call OnDeath
        if (m_CurrentHealth <= 0f && !m_Dead)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        // Set the flag so that this function is only called once
        m_Dead = true;

        // Move the instantiated explosion prefab to the tank's position
        // and turn it on
        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);

        // Play the particle system of the tank exploding
        m_ExplosionParticles.Play();

        // Turn the tank off
        gameObject.SetActive(false);
    }

    public void Heal(float healAmount) //J
    {
        m_CurrentHealth += healAmount;
       if (m_CurrentHealth > maxHealth)
        {
            m_CurrentHealth = maxHealth;
        }
        healthBar.fillAmount = healthAmount / 100f;
        Debug.Log("Health: " + m_CurrentHealth);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "Player")     //H
        {
            healthAmount = m_CurrentHealth;     //H
            healthBar.fillAmount = healthAmount / 100f;     //H
        }
    }
}
