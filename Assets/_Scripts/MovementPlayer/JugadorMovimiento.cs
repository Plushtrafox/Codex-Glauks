using UnityEngine;
using UnityEngine.InputSystem;

public class JugadorMovimiento : MonoBehaviour
{
    public float velocidad = 5f;
    public float velocidadRotacion = 10f;

    private Controles controles;
    private Vector2 inputMovimiento;

    private void Awake()
    {
        controles = new Controles();
    }

    private void OnEnable()
    {
        controles.Jugador.Enable();
        controles.Jugador.Mover.performed += ctx => inputMovimiento = ctx.ReadValue<Vector2>();
        controles.Jugador.Mover.canceled += ctx => inputMovimiento = Vector2.zero;
    }

    private void OnDisable()
    {
        controles.Jugador.Disable();
    }

    private void Update()
    {
        Vector3 direccion = new Vector3(inputMovimiento.x, 0, inputMovimiento.y);

        // Movimiento
        transform.Translate(direccion * velocidad * Time.deltaTime, Space.World);

        // Rotación hacia dirección de movimiento
        if (direccion != Vector3.zero)
        {
            Quaternion rotacionDeseada = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada, velocidadRotacion * Time.deltaTime);
        }
    }
}
