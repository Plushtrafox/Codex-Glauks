using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class EnemigoPersigueJugador : EnemigoBase
{
    // This class represents the enemy state where it chases the player.

    public override void OnEnterState(EnemigoStateManager contexto)
    {
        // Logic to execute when entering the chase state
        Debug.Log("Enemigo ha comenzado a perseguir al jugador.");

        //colocar animacion de simbolo de exclamacion arriba del enemigo
    }
    public override void OnUpdateState(EnemigoStateManager contexto)
    {
        if (contexto.Objetivo != null)
        {
            contexto.DistanciaActual = Vector3.Distance(contexto.transform.position, contexto.Objetivo.position);
            if (contexto.DistanciaActual > contexto.AttackRange && contexto.DistanciaActual <= contexto.RangoDeVision)
            {
                contexto.AgenteMovimiento.isStopped = false;
                contexto.AgenteMovimiento.SetDestination(contexto.Objetivo.position);

            }
            else if (contexto.DistanciaActual <= contexto.AttackRange)
            {
                contexto.AgenteMovimiento.isStopped = true;


                contexto.ChangeState(contexto.EnemigoAtaqueCortoAlcance);
            }
            else
            {
                contexto.AgenteMovimiento.isStopped = true;
                contexto.ChangeState(contexto.EnemigoEstatico);
            }

        }
    }
    public override void OnExitState(EnemigoStateManager contexto)
    {
        // Logic to execute when exiting the chase state
        Debug.Log("Enemigo ha dejado de perseguir al jugador.");
    }
}
