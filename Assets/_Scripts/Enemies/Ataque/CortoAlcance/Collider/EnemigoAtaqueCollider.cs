using UnityEngine;

public class EnemigoAtaqueCollider : MonoBehaviour
{
    [SerializeField] private HealthSO vidaJugador;
    [SerializeField] private int cantidadDamage = 5; //daño que hace enemigo a jugador

    private bool puedeAtacar = true; // Indica si el enemigo puede atacar al jugador
    private void OnTriggerEnter(Collider other)
    {
        if (puedeAtacar)
        {
            vidaJugador.Damage(cantidadDamage); // Reduce player health by 1 when the enemy collider hits the player
            puedeAtacar = false; // Desactiva el ataque para evitar múltiples colisiones

        }

    }

}
