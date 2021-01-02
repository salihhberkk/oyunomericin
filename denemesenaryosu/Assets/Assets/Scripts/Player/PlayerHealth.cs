using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    private int currenHealth;


    public TMPro.TextMeshProUGUI player_healt_txt;
    // Start is called before the first frame update
    void Start()
    {
        showHealt();
        currenHealth = maxHealth;
    }
    public int GetPlayerHealth()
    {
        return currenHealth;
    }
    private void Update()
    {
        showHealt();
    }

    private void showHealt()
    {
        player_healt_txt.text = "%" + currenHealth;
    }

    public void DeductHealth(int damage)
    {
        currenHealth = currenHealth - damage;
        Debug.Log("can gidiyor aloo");
        if(currenHealth <= 0)
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        print("player öldü");
    }

    public void AddHealth(int value)
    {
        currenHealth = currenHealth + value;
        if(currenHealth > maxHealth)
        {
            currenHealth = maxHealth;
        }
    }
    


}
