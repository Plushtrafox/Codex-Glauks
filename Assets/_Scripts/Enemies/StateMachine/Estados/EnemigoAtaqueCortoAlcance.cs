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
            if (Vector3.Distance(contexto.transform.position, contexto.Objetivo.position) <= contexto.AttackRange)
            {

                if (!contexto.IsAttacking)
                {
                    //InvokeRepeating("Attack", 0, contexto.Cooldown);
                    contexto.IsAttacking = true;
                    contexto.Animator.CrossFade("AtaqueCortoAlcance", 0.5f);

                }

            }
            else if(Vector3.Distance(contexto.transform.position, contexto.Objetivo.position) > contexto.AttackRange)
            {
                contexto.IsAttacking = false;
                contexto.Animator.CrossFade("Caminando", 0.5f);
                // Si el enemigo se aleja del rango de ataque, vuelve al estado de persecución
                contexto.ChangeState(contexto.EnemigoPersigueJugador);
            }

        }
    }
public override void OnExitState(EnemigoStateManager contexto)
{
    // Logic to execute when exiting the chase state
    Debug.Log("Enemigo ha dejado de perseguir al jugador.");
}

    void Attack()
    {
        //vidaJugador.Damage(contexto.Attackcount);
    }
}
