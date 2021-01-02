using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealt : MonoBehaviour
{
    public int startHealth = 100;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startHealth;
    }
    public int GetHealth()
    {
        return currentHealth;
    }
    public void Hit(int damage)
    {
        currentHealth -= damage;
        if(currentHealth < 0)
        {
            currentHealth = 0;
            Debug.Log("zombi öldü gardaş");
        }
        Debug.Log("zombi hasar aldı" + currentHealth);
    }
 
}
