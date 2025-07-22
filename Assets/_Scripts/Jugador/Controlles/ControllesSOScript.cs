using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "ControllesSOScript", menuName = "ScriptableObjects/ControllesSOScript", order = 1)]
public class ControllesSOSCript : ScriptableObject, Controles.IJugadorActions
{
    private Controles controlesInput;

    public delegate void tipoEventoAxisMove(Vector2 axis, bool esMouseTeclado);
    public delegate void tipoEventoAxis2(Vector2 axis);
    public delegate void tipoEventoBotonSimple();
    
    
    public event tipoEventoBotonSimple EventoInteractuar;
    public event tipoEventoBotonSimple EventoDash;
    public event tipoEventoBotonSimple EventoAtaqueEmpieza;
    public event tipoEventoBotonSimple EventoAtaqueTermina;
    public event tipoEventoBotonSimple EventoPoder;
    public event tipoEventoBotonSimple EventoLargaDistancia;

    public event tipoEventoAxisMove EventoMovimiento;
    public event tipoEventoAxis2 EventoDireccion;




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
        if (context.control.device is Gamepad)
        {
            EventoMovimiento?.Invoke(context.ReadValue<Vector2>(), false);


        }
        else if (context.control.device is Keyboard )
        {
            EventoMovimiento?.Invoke(context.ReadValue<Vector2>(), true);
        }
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

    public void OnLargaDistancia(InputAction.CallbackContext context)
    {
        // Implementar lógica para ataque a distancia si es necesario
        if (context.phase == InputActionPhase.Started)
        {
            EventoLargaDistancia?.Invoke();
        }
    }

    public void OnGiratorio(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            EventoPoder?.Invoke();
        }

    }

    public void OnDireccion(InputAction.CallbackContext context)
    {
        
        EventoDireccion?.Invoke(context.ReadValue<Vector2>());
    }
}
