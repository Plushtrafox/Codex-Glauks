using System.Collections;
using UnityEngine;

public class BalaEnemigo : MonoBehaviour
{

    [SerializeField] private HealthSO vidaJugador;
    [SerializeField] private int cantidadDamage = 5; //da�o que hace enemigo a jugador


    private void Start()
    {
        StartCoroutine(DestruirBala()); // Inicia la corrutina para destruir la bala despu�s de un tiempo
    }
    private void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Player")) // Verifica si el objeto con el que colisiona tiene la etiqueta "Player"
        {
            vidaJugador.Damage(cantidadDamage); // Reduce player health by 1 when the enemy collider hits the player
        }
        Destroy(gameObject); // Destruye la bala despu�s de la colisi�n


    }
    IEnumerator DestruirBala() // M�todo para destruir la bala despu�s de un tiempo
    {
        yield return new WaitForSeconds(2f); // Espera 2 segundos antes de destruir la bala
        Destroy(gameObject); // Destruye la bala
        yield return null; // Finaliza la corrutina
    }
}
