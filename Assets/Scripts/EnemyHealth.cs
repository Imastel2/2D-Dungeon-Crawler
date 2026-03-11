using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 3;

    // Start is called before the first frame update
    public void TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log(gameObject.name + " took damage. Health left: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}

