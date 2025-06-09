using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        // Logic for enemy death, e.g., play animation, destroy object, etc.
        Destroy(gameObject);
    }
}
