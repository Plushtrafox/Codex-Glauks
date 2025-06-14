using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class JugadorMovimiento : MonoBehaviour
{
    [Header("REFERENCIAS")]
    [SerializeField] private ControllesSOSCript control;
    [SerializeField] private Rigidbody rigidbodyJugador;
    [SerializeField] private Camera camara;

    [Header("Movimiento Jugador")]
    [SerializeField] private float velocidad = 5f;
    [SerializeField] private float velocidadRotacion = 10f;
    private float velocidadActual;    
    private Vector3 direccion;

    [Header("Dash Jugador")]
    [SerializeField] private float velocidadDash = 30f;
    [SerializeField] private float tiempoDeDash = 0.15f; // Duración del dash en segundos


    [Header("Gravedad Jugador")]
    [SerializeField] private float aumentoGravedad = 1f; // Aumento de la gravedad al moverse

    private void Awake()
    {
        control.EventoMovimiento+= movimientoBase;
        control.EventoDash += MovimientoDash;
    }


    private void Start()
    {

        velocidadActual = velocidad; // Inicializar la velocidad actual al valor de velocidad normal
    }

    private void OnDisable()
    {
        //controles.Jugador.Disable();
        control.EventoMovimiento -= movimientoBase;
        control.EventoDash -= MovimientoDash;
    }

    private void movimientoBase(Vector2 axis)
    {
        /*direccion = new Vector3(axis.x, 0, axis.y);
        direccion = camara.transform.TransformDirection(direccion); // Convertir a espacio de la cámara
        direccion.y = 0;// Proyectar en el plano horizontal

        //controlJugador.Move(direccion * velocidad * Time.deltaTime);*/

        direccion = camara.transform.forward * axis.y + camara.transform.right * axis.x;

        direccion.y = 0;
    }

    private void MovimientoDash() 
    { 
        velocidadActual = velocidadDash;

        //Time.timeScale = 0.1f; // Asegurarse de que el tiempo no esté pausado
        //Time.fixedDeltaTime = 0.02f * Time.timeScale; // Ajustar el tiempo fijo para que coincida con el tiempo escaladow
        Invoke("ReiniciarVelocidad", tiempoDeDash);

    }

    private void ReiniciarVelocidad() 
    {
        
        //Time.timeScale = 1f; // Reiniciar el tiempo a su valor normal
        velocidadActual = velocidad; 
    }

    private void FixedUpdate()
    {
        Vector3 planeVelocity = direccion.normalized * velocidadActual;

        rigidbodyJugador.linearVelocity = new Vector3(planeVelocity.x, rigidbodyJugador.linearVelocity.y+aumentoGravedad, planeVelocity.z);

     

        // Movimiento
        //transform.Translate(direccion * velocidad * Time.fixedDeltaTime, Space.World);

        // Rotación hacia dirección de movimiento
        if (direccion != Vector3.zero)
        {
            Quaternion rotacionDeseada = Quaternion.LookRotation(new Vector3(direccion.x, 0, direccion.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada, velocidadRotacion * Time.fixedDeltaTime);
        }
    }
}
