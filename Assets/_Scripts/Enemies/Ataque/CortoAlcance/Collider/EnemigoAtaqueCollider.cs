using System.Collections;
using UnityEngine;

public class EnemigoAtaqueCollider : MonoBehaviour
{
    [SerializeField] private HealthSO vidaJugador;
    [SerializeField] private int cantidadDamage = 5; //daño que hace enemigo a jugador
    [SerializeField] private bool puedeAtacar = true; // Indica si el enemigo puede atacar al jugador



    private void OnTriggerEnter(Collider other)
    {
        if (puedeAtacar)
        {
            vidaJugador.Damage(cantidadDamage); // Reduce player health by 1 when the enemy collider hits the player
            StartCoroutine(ReactivarAtaque()); // Inicia la corrutina para reactivar el ataque después de un tiempo

        }

    }

    IEnumerator ReactivarAtaque() // Método para reactivar el ataque cuando el jugador sale del collider
    {
        puedeAtacar = false; // Desactiva el ataque para evitar múltiples colisiones

        yield return new WaitForSeconds(0.1f); // Espera 1 segundo antes de permitir otro ataque
        puedeAtacar = true; // Reactiva el ataque cuando el jugador sale del collider

        yield return null; // Finaliza la corrutina

    }


}
