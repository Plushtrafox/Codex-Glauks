using UnityEngine;

public class EnemigoAtaqueCollider : MonoBehaviour
{
    [SerializeField] private HealthSO vidaJugador;
    [SerializeField] private int cantidadDamage = 5; //da�o que hace enemigo a jugador
    [SerializeField] private bool puedeAtacar = true; // Indica si el enemigo puede atacar al jugador



    private void OnTriggerEnter(Collider other)
    {
        if (puedeAtacar)
        {
            print("Enemigo ha golpeado al jugador"); // Mensaje de depuraci�n para verificar la colisi�n
            vidaJugador.Damage(cantidadDamage); // Reduce player health by 1 when the enemy collider hits the player
            puedeAtacar = false; // Desactiva el ataque para evitar m�ltiples colisiones

        }

    }


}
