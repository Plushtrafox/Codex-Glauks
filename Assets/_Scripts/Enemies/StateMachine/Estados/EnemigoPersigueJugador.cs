using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class EnemigoPersigueJugador : EnemigoBase
{
    // This class represents the enemy state where it chases the player.
    private bool _estaPersiguiendo = false; // Variable para controlar si el enemigo está persiguiendo al jugador

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


            contexto.VerObjetivo();

            if (contexto.DistanciaActual <= contexto.RangoAtaqueLargoAlcance && contexto.DistanciaActual >= contexto.RangoPerseguirCortoAlcance)
            {

                contexto.AgenteMovimiento.isStopped = true;
                contexto.ChangeState(contexto.EnemigoDispara);
            }
            else if (contexto.DistanciaActual >= contexto.RangoPerseguirLargoALcance)
            {

                contexto.AgenteMovimiento.isStopped = true;
                contexto.ChangeState(contexto.EnemigoEstatico);
            }
            else if (contexto.DistanciaActual<= contexto.RangoAtaqueCortoAlcance)
            {
                contexto.ChangeState(contexto.EnemigoAtaqueCortoAlcance);
            }
            else
            {
                contexto.AgenteMovimiento.isStopped = false;
                contexto.AgenteMovimiento.SetDestination(contexto.Objetivo.position);

                if (_estaPersiguiendo) return; // Si ya está persiguiendo, no hacemos nada más
                //falta condicional para no mandar a reproducir animacion multiples veces

                contexto.Animator.CrossFade("EnemigoMovimientoAnimacion",0.2f); // Cambia la animación a caminar
                _estaPersiguiendo = true; // Marca que el enemigo está persiguiendo al jugador
            }




        }
    }
    public override void OnExitState(EnemigoStateManager contexto)
    {
        _estaPersiguiendo = false; // Resetea la variable al salir del estado de persecución
        // Logic to execute when exiting the chase state
    }
}
