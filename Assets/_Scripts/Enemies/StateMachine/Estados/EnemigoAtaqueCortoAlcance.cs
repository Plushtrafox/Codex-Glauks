using UnityEngine;

public class EnemigoAtaqueCortoAlcance :EnemigoBase
{

    public override void OnEnterState(EnemigoStateManager contexto)
    {

    }
public override void OnUpdateState(EnemigoStateManager contexto)
{
        if (contexto.Objetivo != null)
        {
            contexto.VerObjetivo();
            float distanciaJugador = Vector3.Distance(contexto.transform.position, contexto.Objetivo.position);
            if (distanciaJugador <= contexto.RangoAtaqueCortoAlcance)
            {

                if (!contexto.IsAttacking)
                {
                    //InvokeRepeating("Attack", 0, contexto.Cooldown);
                    contexto.IsAttacking = true;
                    contexto.Animator.CrossFade("EnemigoAtaqueAnimacion", 0.5f);

                }

            }
            else if(distanciaJugador > contexto.RangoAtaqueCortoAlcance)
            {
                contexto.IsAttacking = false;
                // Si el enemigo se aleja del rango de ataque, vuelve al estado de persecución
                contexto.ChangeState(contexto.EnemigoPersigueJugador);
            }


        }
    }
public override void OnExitState(EnemigoStateManager contexto)
{
    // Logic to execute when exiting the chase state
}

}
