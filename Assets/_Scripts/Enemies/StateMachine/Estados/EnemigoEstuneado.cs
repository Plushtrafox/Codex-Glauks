using UnityEngine;

public class EnemigoEstuneado : EnemigoBase
{
    // This class represents the enemy state where it chases the player.

    public override void OnEnterState(EnemigoStateManager contexto)
    {
        // Logic to execute when entering the chase state
        Debug.Log("Enemigo ha comenzado a perseguir al jugador.");
    }
    public override void OnUpdateState(EnemigoStateManager contexto)
    {
        // Logic to execute every frame while in the chase state
        Debug.Log("Enemigo persigue al jugador.");
    }
    public override void OnExitState(EnemigoStateManager contexto)
    {
        // Logic to execute when exiting the chase state
        Debug.Log("Enemigo ha dejado de perseguir al jugador.");
    }
}
