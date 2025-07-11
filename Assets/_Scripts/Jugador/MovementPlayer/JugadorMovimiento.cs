using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
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
    [SerializeField] private AnimatorBrain animatorBrain; // Referencia al AnimatorBrain del jugador

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
    private bool _estaUsandoTecladoMouse; // Variable para verificar si se está usando teclado y mouse

    //info para sistema de animaciones
    private const int capaCuerpoSuperior = 0; // capa de torso para arriba
    private const int capaCuerpoInferior = 1; // capa de torso para abajo


    private void Awake()
    {
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

    private void movimientoBase(Vector2 axis, bool esMouseTeclado)
    {
        _estaUsandoTecladoMouse = esMouseTeclado; // Actualizar la variable según el tipo de entrada

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
        _estaUsandoTecladoMouse = true;

        direccionInput *= sensibilidadDeRotacion; // Aplicar sensibilidad de rotación

        _direccionVista = (camara.transform.forward * direccionInput.y + camara.transform.right * direccionInput.x).normalized;
        _direccionVista = new Vector3(_direccionVista.x, 0, _direccionVista.z).normalized;

        if (_direccionVista!=Vector3.zero || _direccionVista.magnitude>magnitudDeSensibilidadVision)
        {            

            Quaternion rotacionDeseada = Quaternion.LookRotation(_direccionVista);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada, velocidadRotacion*Time.deltaTime);
        }

    }

    private void SetAnimacionDefecto(int capa)
    {
        if (capa == capaCuerpoSuperior)
        {
            RevisarAnimacionSuperior();       
        }
        else if (capa == capaCuerpoInferior)
        {
            RevisarAnimacionInferior();
        }

    }

    private void RevisarAnimacionSuperior()
    {
        RevisarMovimiento(capaCuerpoSuperior);

    }
    private void RevisarAnimacionInferior()
    {
        RevisarMovimiento(capaCuerpoInferior);
    }

    private void RevisarMovimiento(int capa)
    {

        if ( _estaUsandoTecladoMouse)
        {
            Vector3 velocidadGlobal = rigidbodyJugador.linearVelocity;

            // Convertir a velocidad local (relativa al objeto)
            Vector3 velocidadLocal = transform.InverseTransformDirection(velocidadGlobal);

            //CODIGO PARA VERIFICAR DIRECCION Y LLAMAR A REPRODUCIR

            if (velocidadLocal.z > 0)
            {
                animatorBrain.ReproducirAnimacion(AnimacionesJugador.JugadorMovimientoAdelanteAnimacion, capa, false, false, 0.1f); // Reproducir animación de movimiento hacia adelante
            }
            else if (velocidadLocal.z < 0)
            {
                animatorBrain.ReproducirAnimacion(AnimacionesJugador.JugadorMovimientoAtrasAnimacion, capa, false, false, 0.1f); // Reproducir animación de movimiento hacia atrás
            }
            else if (velocidadLocal.x > 0)
            {
                animatorBrain.ReproducirAnimacion(AnimacionesJugador.JugadorMovimientoDerechaAnimacion, capa, false, false, 0.1f); // Reproducir animación de movimiento hacia la derecha
            }
            else if (velocidadLocal.x < 0)
            {
                animatorBrain.ReproducirAnimacion(AnimacionesJugador.JugadorMovimientoIzquierdaAnimacion, capa, false, false, 0.1f); // Reproducir animación de movimiento hacia la izquierda

            }
            else
            {
                animatorBrain.ReproducirAnimacion(AnimacionesJugador.JugadorDePieAnimacion, capa, false, false); // Reproducir animación de estar de pie
            }



        }
    }


    private void FixedUpdate()
    {
        Vector3 planeVelocity = direccion.normalized * velocidadActual;

        rigidbodyJugador.linearVelocity = new Vector3(planeVelocity.x, rigidbodyJugador.linearVelocity.y+aumentoGravedad, planeVelocity.z);


        RevisarAnimacionSuperior();
        RevisarAnimacionInferior();


        if (direccion != Vector3.zero && !_estaUsandoTecladoMouse)
        {


            Quaternion rotacionDeseada = Quaternion.LookRotation(new Vector3(direccion.x, 0, direccion.z));

            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada.normalized, velocidadRotacion);



        }


    }
}
