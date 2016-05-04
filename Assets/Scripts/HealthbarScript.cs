using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthbarScript : MonoBehaviour {

    public float maxHealth = 100;
    public float healthAdd = 0.1f; 
    private Image healthbarFilling;
    private float health; 

	// Use this for initialization
	void Start () {
        healthbarFilling = this.GetComponent<Image>();
        health = maxHealth;

	
	}

    public void AddHealth(float value)
    {
        health += value;
        if (health > maxHealth)
            health = maxHealth;
        UpdateHealth(); 
    }

    public bool RemoveHealth(float value)
    {
        health -= value; 
        if(health <= 0)
        {
            health = 0;
            UpdateHealth();
            return true;
        }
        UpdateHealth();
        return false; 
    }

    private void UpdateHealth()
    {
        healthbarFilling.fillAmount = health / maxHealth; 
    }
	

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            AddHealth(healthAdd);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            RemoveHealth(healthAdd);
        }

    }

}
