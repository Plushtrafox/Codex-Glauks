using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class EnemigoPersigueJugador : EnemigoBase
{
    // This class represents the enemy state where it chases the player.

    public override void OnEnterState(EnemigoStateManager contexto)
    {
        if (!contexto.EstaEnVista)
        {
            contexto.MostrarUIDetectar();
            contexto.EstaEnVista = true;

        }

    }
    public override void OnUpdateState(EnemigoStateManager contexto)
    {
        if (contexto.Objetivo != null)
        {
            contexto.DistanciaActual = Vector3.Distance(contexto.transform.position, contexto.Objetivo.position);




             if (contexto.DistanciaActual <= contexto.RangoAtaqueLargoAlcance && contexto.DistanciaActual >= contexto.RangoPerseguirCortoAlcance)
            {
                contexto.AgenteMovimiento.isStopped = true;
                contexto.ChangeState(contexto.EnemigoDispara);
            }
            else if (contexto.DistanciaActual > contexto.RangoPerseguirLargoALcance)
            {
                contexto.AgenteMovimiento.isStopped = true;
                contexto.ChangeState(contexto.EnemigoEstatico);
            }
            else if (contexto.DistanciaActual< contexto.RangoAtaqueCortoAlcance)
            {
                contexto.ChangeState(contexto.EnemigoAtaqueCortoAlcance);
            }
            else
            {
                contexto.AgenteMovimiento.isStopped = false;
                contexto.AgenteMovimiento.SetDestination(contexto.Objetivo.position);
            }




        }
    }
    public override void OnExitState(EnemigoStateManager contexto)
    {
        // Logic to execute when exiting the chase state
    }
}
