using UnityEngine;

public abstract class EnemigoBase 
{


    public abstract void OnEnterState(EnemigoStateManager contexto); // Called when the state is entered
    public abstract void OnUpdateState(EnemigoStateManager contexto); // Called every frame while in the state
    public abstract void OnExitState(EnemigoStateManager contexto);  // Called when the state is exited

}
