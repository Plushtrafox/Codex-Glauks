using UnityEngine;

public class BalaEnemigo : MonoBehaviour
{

    [SerializeField] private HealthSO vidaJugador;
    [SerializeField] private int cantidadDamage = 5; //da�o que hace enemigo a jugador



    private void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Player")) // Verifica si el objeto con el que colisiona tiene la etiqueta "Player"
        {
            vidaJugador.Damage(cantidadDamage); // Reduce player health by 1 when the enemy collider hits the player
        }
        Destroy(gameObject); // Destruye la bala despu�s de la colisi�n


    }
}
