using UnityEngine;

public class EnemigoAtaqueCortoAlcance :EnemigoBase
{
    // This class represents the enemy state where it chases the player.
    private HealthSO vidaJugador;
    private int cantidadDamage;

    public override void OnEnterState(EnemigoStateManager contexto)
    {
        vidaJugador = contexto.HealthCharacter;
        cantidadDamage = contexto.AttackCount;
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
                }
                else if (contexto.IsAttacking)
                {
                   //CancelInvoke("Attack");
                    contexto.IsAttacking = false;
                }
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
