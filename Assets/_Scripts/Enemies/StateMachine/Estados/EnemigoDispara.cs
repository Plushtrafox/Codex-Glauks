using UnityEngine;

public class EnemigoDispara : EnemigoBase
{
    // This class represents the enemy state where it chases the player.

    public override void OnEnterState(EnemigoStateManager contexto)
    {

    }
    public override void OnUpdateState(EnemigoStateManager contexto)
    {
        contexto.DistanciaActual = Vector3.Distance(contexto.transform.position, contexto.Objetivo.position);
        contexto.VerObjetivo();


        if (contexto.DistanciaActual > contexto.RangoAtaqueLargoAlcance)
        {
            contexto.ChangeState(contexto.EnemigoPersigueJugador);
        }
        else if (contexto.DistanciaActual < contexto.RangoAtaqueLargoAlcance && contexto.DistanciaActual > contexto.RangoPerseguirCortoAlcance)
        {

            if (!contexto.IsAttacking)
            {
                contexto.IsAttacking = true;
                contexto.Animator.CrossFade("EnemigoAtaqueLargoAlcanceAnimacion", 0.2f);
                // Aquí podrías llamar a un método para realizar el disparo
            }

        }
        else if(contexto.DistanciaActual < contexto.RangoPerseguirCortoAlcance)
        {
            contexto.ChangeState(contexto.EnemigoPersigueJugador);

        }
    }
    public override void OnExitState(EnemigoStateManager contexto)
    {
        contexto.IsAttacking = false;

    }
}
