using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class JugadorMovimiento : MonoBehaviour
{
    [SerializeField] private ControllesSOSCript control;

    public float velocidad = 5f;
    public float velocidadRotacion = 10f;
    public float velocidadDash = 30f;
    private float velocidadActual;
    public float tiempoDeDash = 0.15f;
    //private Controles controles;
    //private Vector2 inputMovimiento;

    [SerializeField] private Rigidbody rigidbodyJugador;
    Vector3 direccion;

    [SerializeField] private Camera camara;

    private void Awake()
    {
        //controles = new Controles();

        control.EventoMovimiento+= movimientoBase;
        control.EventoDash += MovimientoDash;


    }

    private void OnEnable()
    {
        //controles.Jugador.Enable();
        //controles.Jugador.Mover.performed += ctx => inputMovimiento = ctx.ReadValue<Vector2>();
        //controles.Jugador.Mover.canceled += ctx => inputMovimiento = Vector2.zero;
    }
    private void Start()
    {
        velocidadActual = velocidad;
    }
    private void OnDisable()
    {
        //controles.Jugador.Disable();
        control.EventoMovimiento -= movimientoBase;
        control.EventoDash -= MovimientoDash;
    }

    private void movimientoBase(Vector2 axis)
    {
        direccion = new Vector3(axis.x, 0, axis.y);
        direccion = camara.transform.TransformDirection(direccion); // Convertir a espacio de la cámara
        direccion.y = 0;// Proyectar en el plano horizontal


    }
    private void MovimientoDash() 
    { 
        velocidadActual = velocidadDash;
        Invoke("ReiniciarVelocidad", tiempoDeDash);

    }
    private void ReiniciarVelocidad() { velocidadActual = velocidad; }
    private void FixedUpdate()
    {
       

        rigidbodyJugador.linearVelocity = direccion*velocidadActual;


        // Movimiento
        //transform.Translate(direccion * velocidad * Time.fixedDeltaTime, Space.World);

        // Rotación hacia dirección de movimiento
        if (direccion != Vector3.zero)
        {
            Quaternion rotacionDeseada = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada, velocidadRotacion * Time.fixedDeltaTime);
        }
    }
}
