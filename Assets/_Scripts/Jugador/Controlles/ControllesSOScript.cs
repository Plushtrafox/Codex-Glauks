using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "ControllesSOScript", menuName = "ScriptableObjects/ControllesSOScript", order = 1)]
public class ControllesSOSCript : ScriptableObject, Controles.IJugadorActions
{
    private Controles controlesInput;

    public delegate void tipoEventoAxis2(Vector2 axis);
    public delegate void tipoEventoBotonSimple();
    
    
    public event tipoEventoBotonSimple EventoInteractuar;
    public event tipoEventoAxis2 EventoMovimiento;
    public event tipoEventoBotonSimple EventoDash;
    public event tipoEventoBotonSimple EventoAtaqueEmpieza;
    public event tipoEventoBotonSimple EventoAtaqueTermina;
    public event tipoEventoBotonSimple EventoPoder;


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
        EventoMovimiento?.Invoke(context.ReadValue<Vector2>());
    }

  

    public void OnBotonDash(InputAction.CallbackContext context)
    {
        if(context.phase ==InputActionPhase.Started){ EventoDash?.Invoke(); }
        

    }


    public void OnInteractuar(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            EventoInteractuar?.Invoke();
        }
    }

    public void OnAtaque(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            EventoAtaqueEmpieza?.Invoke();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            EventoAtaqueTermina?.Invoke();
        }
    }

    public void OnProyectil(InputAction.CallbackContext context)
    {

        if (context.phase == InputActionPhase.Started)
        {
            EventoPoder?.Invoke();
        }

    } 
    public void OnLargaDistancia(InputAction.CallbackContext context)
    {
        // Implementar lógica para ataque a distancia si es necesario
        if (context.phase == InputActionPhase.Started)
        {

        }
    }
}
