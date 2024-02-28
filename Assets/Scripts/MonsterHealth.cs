using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    // Define a delegate type
    public delegate void DeathDelegate();

    // Declare a public variable of the delegate type
    public DeathDelegate onDeath;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //takeDamage(20);
            //Die();
        }   
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Monster hurts!");
        if(currentHealth <= 0)
        {
            Debug.Log("Monster died!");
            if (onDeath != null)
            {
                // Invoke the delegate, triggering the method in bat.cs
                onDeath();
            }
        }
    }
}
