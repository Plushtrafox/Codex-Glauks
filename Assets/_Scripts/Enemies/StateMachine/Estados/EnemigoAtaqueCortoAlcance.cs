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
                    contexto.IniciarHacerDamage();

                }
                else if (contexto.IsAttacking)
                {
                   //CancelInvoke("Attack");
                    contexto.IsAttacking = false;
                    contexto.DetenerHacerDamage();
                }
            }
            else if(Vector3.Distance(contexto.transform.position, contexto.Objetivo.position) > contexto.AttackRange)
            {
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
