using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class EnemigoEstatico : EnemigoBase
{
    // This class represents the enemy state where it chases the player.

    public override void OnEnterState(EnemigoStateManager contexto)
    {

            contexto.AgenteMovimiento.isStopped = true;
        
    }
    public override void OnUpdateState(EnemigoStateManager contexto)
    {
        if (contexto.Objetivo != null)
        {
            contexto.DistanciaActual = Vector3.Distance(contexto.transform.position, contexto.Objetivo.position);
            if (contexto.DistanciaActual <= contexto.RangoDeVision)
            {
                contexto.ChangeState(contexto.EnemigoPersigueJugador);
            }
        }

    }
    public override void OnExitState(EnemigoStateManager contexto)
    {
        // Logic to execute when exiting the chase state
        Debug.Log("Enemigo ha dejado de estar quieto");
    }
}
