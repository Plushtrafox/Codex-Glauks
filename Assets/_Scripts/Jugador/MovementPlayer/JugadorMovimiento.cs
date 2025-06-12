using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class JugadorMovimiento : MonoBehaviour
{
    [SerializeField] private ControllesSOSCript control;

    public float velocidad = 5f;
    public float velocidadRotacion = 10f;

    //private Controles controles;
    //private Vector2 inputMovimiento;

    [SerializeField] private Rigidbody rigidbodyJugador;
    Vector3 direccion;

    [SerializeField] private Camera camara;
    [SerializeField] private float velocidadActual;
    [SerializeField] private float velocidadDash = 10f;
    [SerializeField] private float tiempoDeDash = 0.5f; // Duración del dash en segundos

    private void Awake()
    {
        //controles = new Controles();

        control.eventoMovimiento+= movimientoBase;

    }

    private void OnEnable()
    {
        //controles.Jugador.Enable();
        //controles.Jugador.Mover.performed += ctx => inputMovimiento = ctx.ReadValue<Vector2>();
        //controles.Jugador.Mover.canceled += ctx => inputMovimiento = Vector2.zero;
    }

    private void OnDisable()
    {
        //controles.Jugador.Disable();
        control.eventoMovimiento -= movimientoBase;
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
        //Time.timeScale = 0.1f; // Asegurarse de que el tiempo no esté pausado
        //Time.fixedDeltaTime = 0.02f * Time.timeScale; // Ajustar el tiempo fijo para que coincida con el tiempo escaladow
        Invoke("ReiniciarVelocidad", tiempoDeDash);

    }
    IEnumerator nombre()
    {
        yield return new WaitForSeconds(tiempoDeDash);
    ReiniciarVelocidad();
    }
    private void ReiniciarVelocidad() 
    {
        
        //Time.timeScale = 1f; // Reiniciar el tiempo a su valor normal
        velocidadActual = velocidad; 
    }
    private void FixedUpdate()
    {
       

        rigidbodyJugador.linearVelocity = direccion*velocidad;


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
