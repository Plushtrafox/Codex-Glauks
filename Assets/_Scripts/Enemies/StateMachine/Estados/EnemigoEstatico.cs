using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class EnemigoEstatico : EnemigoBase
{
    // This class represents the enemy state where it chases the player.

    public override void OnEnterState(EnemigoStateManager contexto)
    {
        
        contexto.Animator.CrossFade("Base", 0.5f); // Cambia la animación a Idle
        contexto.AgenteMovimiento.isStopped = true;
        contexto.EstaEnVista = false; // resetea el booleano para que cuando vea al jugador otra vez muestre el UI de detectarlo

    }
    public override void OnUpdateState(EnemigoStateManager contexto)
    {
        if (contexto.Objetivo != null)
        {
            contexto.DistanciaActual = Vector3.Distance(contexto.transform.position, contexto.Objetivo.position);
            if (contexto.DistanciaActual <= contexto.RangoPerseguirLargoALcance)
            {
                contexto.ChangeState(contexto.EnemigoPersigueJugador);
            }

        }

    }
    public override void OnExitState(EnemigoStateManager contexto)
    {
        // Logic to execute when exiting the chase state
    }
}
