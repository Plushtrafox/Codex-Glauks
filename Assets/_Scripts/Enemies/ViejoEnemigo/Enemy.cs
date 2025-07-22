using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IVida
{
    [SerializeField] private Slider barraVidaUI; // Referencia a la barra de vida en la UI
    private float health;
    [SerializeField] private float maxHealth = 20f; // Salud máxima del enemigo



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        health = maxHealth; // Inicializa la salud del enemigo

    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        float porcentajeDeSalud = health / maxHealth; // Calcula el porcentaje de daño
        barraVidaUI.value = porcentajeDeSalud; // Actualiza la barra de vida en la UI
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
