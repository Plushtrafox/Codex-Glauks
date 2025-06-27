using UnityEngine;

public class Enemy : MonoBehaviour, IVida
{
    [SerializeField] private float health;
   // [SerializeField] private float porcentajeParaEstunear=0f;
   // [SerializeField] private float tiempoEstuneo = 0f;
   // private float tiempoActualEstuneado = 0f;


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
