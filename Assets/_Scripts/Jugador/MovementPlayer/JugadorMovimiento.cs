using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility.Audio;
using Utility.GameFlow;

public class JugadorMovimiento : MonoBehaviour
{
   
    [Header("REFERENCIAS")]
    [SerializeField] private ControllesSOSCript control;
    [SerializeField] private Rigidbody rigidbodyJugador;
    [SerializeField] private Camera camara;
    [SerializeField] private audioManager audioManager; // Asegúrate de tener un AudioManager en tu escena
    [SerializeField] private Animator animatorJugador; // Referencia al Animator del jugador
    [SerializeField] private PlayerInput playerInput;

    [Header("Movimiento Jugador")]
    [SerializeField] private float velocidad = 5f;
    [SerializeField] private float velocidadRotacion = 10f;
    private float velocidadActual;    
    private Vector3 direccion;
    private Vector3 _direccionVista;

    [Header("Dash Jugador")]
    [SerializeField] private float velocidadDash = 30f;
    [SerializeField] private float tiempoDeDash = 0.15f; // Duración del dash en segundos


    [Header("Gravedad Jugador")]
    [SerializeField] private float aumentoGravedad = 1f; // Aumento de la gravedad al moverse



    [Header("direccion Jugador")]
    [SerializeField] private float sensibilidadDeRotacion = 0.5f; // Sensibilidad de rotación del jugador
    [SerializeField] private float magnitudDeSensibilidadVision = 0.1f; // Magnitud mínima para considerar la dirección de visión


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        control.EventoMovimiento+= movimientoBase;
        control.EventoDash += MovimientoDash;
        control.EventoDireccion += DireccionVista;
    }



    private void Start()
    {

        velocidadActual = velocidad; // Inicializar la velocidad actual al valor de velocidad normal
        Cursor.visible = false; // Hacer invisible el cursor
        Cursor.lockState=CursorLockMode.Confined; // Bloquear el cursor dentro de la ventana del juego

        //animatorJugador.Play("");
    }

    private void OnDisable()
    {
        //controles.Jugador.Disable();
        control.EventoMovimiento -= movimientoBase;
        control.EventoDash -= MovimientoDash;
        control.EventoDireccion -= DireccionVista;
    }

    private void movimientoBase(Vector2 axis)
    {
        direccion = camara.transform.forward * axis.y + camara.transform.right * axis.x;
        direccion.y = 0;
    }

    private void MovimientoDash() 
    { 
        velocidadActual = velocidadDash;

        //Time.timeScale = 0.1f; // Asegurarse de que el tiempo no esté pausado
        //Time.fixedDeltaTime = 0.02f * Time.timeScale; // Ajustar el tiempo fijo para que coincida con el tiempo escaladow
        Invoke("ReiniciarVelocidad", tiempoDeDash);
        ClipData clipData = audioManager.GetClipData("Dash"); // Obtener datos del clip de sonido de dash
        audioManager.PlayClipOneShot(clipData); // Reproducir sonido de dash
    }

    private void ReiniciarVelocidad() 
    {
        
        //Time.timeScale = 1f; // Reiniciar el tiempo a su valor normal
        velocidadActual = velocidad; 
    }

    private void DireccionVista(Vector2 direccionInput)
    {
        if (playerInput.currentControlScheme != "Keyboard&Mouse")
        {
            return; // No hacer nada si no es teclado y mouse
        }

        direccionInput *= sensibilidadDeRotacion; // Aplicar sensibilidad de rotación

        _direccionVista = (camara.transform.forward * direccionInput.y + camara.transform.right * direccionInput.x).normalized;
        _direccionVista = new Vector3(_direccionVista.x, 0, _direccionVista.z).normalized;

        if (_direccionVista!=Vector3.zero || _direccionVista.magnitude>magnitudDeSensibilidadVision)
        {            

            Quaternion rotacionDeseada = Quaternion.LookRotation(_direccionVista);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada, velocidadRotacion*Time.deltaTime);

            

        }



    }

    private void FixedUpdate()
    {
        Vector3 planeVelocity = direccion.normalized * velocidadActual;

        rigidbodyJugador.linearVelocity = new Vector3(planeVelocity.x, rigidbodyJugador.linearVelocity.y+aumentoGravedad, planeVelocity.z);


        if (_direccionVista != Vector3.zero)
        {



            Quaternion rotacionDeseada = Quaternion.LookRotation(new Vector3(_direccionVista.x, 0, _direccionVista.z));

            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada.normalized, velocidadRotacion);



        }
        if (playerInput.currentControlScheme != "Keyboard&Mouse")
        {
            if (direccion != Vector3.zero)
            {
                Quaternion rotacionDeseada = Quaternion.LookRotation(new Vector3(direccion.x, 0, direccion.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada, velocidadRotacion * Time.fixedDeltaTime);
            }
        }

        // Movimiento
        //transform.Translate(direccion * velocidad * Time.fixedDeltaTime, Space.World);

        // Rotación hacia dirección de movimiento

    }
}
