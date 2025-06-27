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
    public event tipoEventoBotonSimple EventoAtaque;
    public event tipoEventoBotonSimple EventoProyectil;


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
            EventoAtaque?.Invoke();
        }
    }

    public void OnProyectil(InputAction.CallbackContext context)
    {

        if (context.phase == InputActionPhase.Started)
        {
            EventoProyectil?.Invoke();
        }

    } 
    public void OnLongDistance(InputAction.CallbackContext context)
    {
        // Implementar l�gica para ataque a distancia si es necesario
        if (context.phase == InputActionPhase.Started)
        {

        }
    }
}
