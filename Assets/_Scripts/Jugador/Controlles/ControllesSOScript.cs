using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "ControllesSOScript", menuName = "ScriptableObjects/ControllesSOScript", order = 1)]
public class ControllesSOSCript : ScriptableObject, Controles.IJugadorActions
{
    private Controles controlesInput;

    public delegate void tipoEventoAxis2(Vector2 axis);
    public delegate void tipoEventoBotonSimple();



    public event tipoEventoAxis2 eventoMovimiento;
    public event tipoEventoBotonSimple EventoDash;


    private void OnEnable()
    {
        controlesInput = new Controles();
        controlesInput.Jugador.Enable();

        controlesInput.Jugador.AddCallbacks(this);
    }

    private void OnDisable()
    {
        controlesInput.Jugador.Disable();

    }

    public void OnMover(InputAction.CallbackContext context)
    {
        eventoMovimiento?.Invoke(context.ReadValue<Vector2>());
    }

  

    public void OnBotonDash(InputAction.CallbackContext context)
    {
        if(context.phase ==InputActionPhase.Started){ EventoDash?.Invoke(); }
        

    }
}
