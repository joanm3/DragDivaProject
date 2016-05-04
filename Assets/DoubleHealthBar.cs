using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DoubleHealthBar : MonoBehaviour {

    public Image linearBar;
    public Image radialBar;

    public int maxHealth = 1000;
    public int healthAdd = 50; 
    private int health;



	// Use this for initialization
	void Start () {
        health = maxHealth; 
	
	}

    public void AddHealth(int value)
    {
        health += value;
        if (health > maxHealth)
            health = maxHealth;
        UpdateHealthBar();
    }

    public bool RemoveHealth(int value)
    {
        health -= value;
        if (health <= 0)
        {
            health = 0;
            UpdateHealthBar();
            return true;
        }
        UpdateHealthBar();
        return false;
    }



    private void UpdateHealthBar()
    {
        float ratio = health * 1f / maxHealth;
        Debug.Log(ratio);
        //change bars when arriving at 3/5 = 0.6f 
        if (ratio > 0.6)
        {
            linearBar.fillAmount = (ratio - 0.6f) * 2.5f;
            radialBar.fillAmount = 0.75f; 
        } else
        {
            linearBar.fillAmount = 0;
            //multiply to 0.75 which is the maximum ratio. 
            radialBar.fillAmount = 0.75f * ratio * 10f / 6f; 
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddHealth(healthAdd);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            RemoveHealth(healthAdd);
        }

    }
}
