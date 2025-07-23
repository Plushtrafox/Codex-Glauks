using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Utility.Audio;
using Utility.GameFlow;

public class JugadorMovimiento : MonoBehaviour
{
   
    [Header("REFERENCIAS")]
    [SerializeField] private ControllesSOSCript control;
    [SerializeField] private Rigidbody rigidbodyJugador;
    [SerializeField] private Camera camara;
    [SerializeField] private AudioManager audioManager; // Asegúrate de tener un AudioManager en tu escena
    [SerializeField] private Animator animatorJugador; // Referencia al Animator del jugador
    [SerializeField] private AnimatorBrain animatorBrain; // Referencia al AnimatorBrain del jugador
    [SerializeField] private Slider sliderDash; // Slider para mostrar el cooldown del dash (opcional, si lo necesitas en la UI)

    [Header("Movimiento Jugador")]
    [SerializeField] private float velocidad = 5f;
    [SerializeField] private float velocidadRotacion = 10f;
    private float velocidadActual;    
    private Vector3 direccion;
    private Vector3 _direccionVista;

    [Header("MOVIMIENTO AVANZADO")]
    [SerializeField]private float velocidadMaxima = 8f; // Velocidad máxima del jugador
    [SerializeField] private float velocidadMaximaAdelante = 15f; // Velocidad máxima al moverse hacia adelante
    [SerializeField] private float velocidadMaximaOtraDireccion = 8f; // Velocidad máxima al moverse hacia atrás
    private float _deadzone = 0.1f; // Ajusta este valor según tus necesidades



    [SerializeField] private float aceleracion = 15f; // Tiempo en segundos para alcanzar la velocidad máxima
    [SerializeField] private float desaceleracion = 15f; // Tiempo en segundos para desacelerar a cero
    private Vector3 _velocidadObjetivo;
    private float _umbralParaPausarMovimiento = 1f; // Umbral para pausar el movimiento horizontal si la velocidad es muy baja




    [Header("Dash Jugador")]
    [SerializeField] private float velocidadDash = 30f;
    [SerializeField] private float tiempoDeDash = 0.15f; // Duración del dash en segundos
    [SerializeField] private float cooldownDash = 1f; // Tiempo de espera entre dashes
    private bool dashActivo = false; // Bandera para verificar si el dash está activo
    private bool puedeDash = true; // Bandera para verificar si se puede realizar un dash


    [Header("Gravedad Jugador")]
    [SerializeField] private float aumentoGravedad = 1f; // Aumento de la gravedad al moverse



    [Header("direccion Jugador")]

    private bool _estaUsandoTecladoMouse; // Variable para verificar si se está usando teclado y mouse

    //info para sistema de animaciones
    private const int capaCuerpoSuperior = 0; // capa de torso para arriba
    private const int capaCuerpoInferior = 1; // capa de torso para abajo


    #region "funciones de logica"

    private void movimientoBase(Vector2 axis, bool esMouseTeclado)
    {
        if (dashActivo) return; // Si el dash está activo, no procesar más movimiento

        _estaUsandoTecladoMouse = esMouseTeclado;

        // DEADZONE PARA INPUT - Crucial para evitar micro-movimientos

        if (axis.magnitude < _deadzone)
        {
            // Input muy pequeño = sin movimiento
            axis = Vector2.zero;
        }

        direccion = camara.transform.forward * axis.y + camara.transform.right * axis.x;
        direccion.y = 0;
        _velocidadObjetivo = direccion * velocidadMaxima;


    }

    private void MovimientoDash()
    {
        if (dashActivo || !puedeDash) return; // Si el dash ya está activo, no hacer nada

        Vector3 velocidadOriginal = rigidbodyJugador.linearVelocity; // Guardar la velocidad original del jugador
        Vector3 velocidadDashNueva = velocidadOriginal.normalized * velocidadDash; // Calcular la nueva velocidad del dash
        if (velocidadDashNueva.magnitude < 0.1f) return; // Si la velocidad del dash es muy baja, no hacer nada
        dashActivo = true; // Activar el dash
        puedeDash = false; // Desactivar la posibilidad de hacer otro dash inmediatamente
        StartCoroutine(Dash(velocidadDashNueva, velocidadOriginal)); // Iniciar la coroutine para el dash
        


    }
    IEnumerator Dash(Vector3 velocidadDashNueva, Vector3 velocidadOriginal)
    {
       
        rigidbodyJugador.linearVelocity = velocidadDashNueva; // Aplicar la nueva velocidad del dash
        ClipData clipData = AudioManager.GetClipData("Dash"); // Obtener datos del clip de sonido de dash
        AudioManager.PlayClipOneShot(clipData); // Reproducir sonido de dash

        yield return new WaitForSeconds(tiempoDeDash); // Esperar el tiempo del dash

        rigidbodyJugador.linearVelocity = velocidadOriginal; // Restaurar la velocidad original del jugador
        dashActivo = false; // Desactivar el dash
        if (sliderDash != null)
        {
            sliderDash.value = 0f; // Reiniciar el slider del cooldown del dash
        }
        StartCoroutine(CooldownDash()); // Iniciar el cooldown del dash


        yield return null; // Esperar un frame para asegurar que la coroutine se complete correctamente

    }
    IEnumerator CooldownDash()
    {

        float tiempoTranscurrido = 0f; // Inicializar el tiempo transcurrido
        while (tiempoTranscurrido < cooldownDash)
        {
            tiempoTranscurrido += Time.deltaTime; // Incrementar el tiempo transcurrido

            sliderDash.value = tiempoTranscurrido / cooldownDash; // Actualizar el valor del slider
            
            yield return null; // Esperar un frame antes de continuar
        }
        sliderDash.value = 1f; // Asegurarse de que el slider esté completamente lleno al final del cooldown
        puedeDash = true; // Permitir que se pueda realizar otro dash
    }

    private void DireccionVista(Vector2 mousePosition)
    {

        if (dashActivo) return;

        _estaUsandoTecladoMouse = true;


        Plane plano = new Plane(transform.up, transform.position);

        // Crear un rayo desde la posición del mouse
        Ray ray = camara.ScreenPointToRay(mousePosition);

        // Calcular el punto de intersección
        if (plano.Raycast(ray, out float distancia))
        {
            Vector3 puntoEnPlano = ray.GetPoint(distancia);
            Vector3 direccion = puntoEnPlano - transform.position;
            direccion.y = 0f;

            transform.forward = direccion.normalized; // Actualizar la dirección de la vista del jugador
        }


    }

    private void RevisarMovimiento()
    {
        float smoothVelocity = 0f;
        float smoothTime = 0.1f; // Tiempo de transición en segundos

        Vector3 velocidadGlobal = rigidbodyJugador.linearVelocity;

        // Convertir a velocidad local (relativa al objeto)
        Vector3 velocidadLocal = transform.InverseTransformDirection(velocidadGlobal);

        //CODIGO PARA VERIFICAR DIRECCION Y LLAMAR A REPRODUCIR
        float currentSpeedX = animatorJugador.GetFloat("X");
        float currentSpeedZ = animatorJugador.GetFloat("Z");

        float velocidadFinalX = Mathf.Clamp(velocidadLocal.x, -1f, 1f);
        float velocidadFinalZ = Mathf.Clamp(velocidadLocal.z, -1f, 1f);



        float newSpeedX = Mathf.SmoothDamp(
        currentSpeedX,
        velocidadFinalX,
        ref smoothVelocity,
        smoothTime
        );

        float newSpeedZ = Mathf.SmoothDamp(
        currentSpeedZ,
        velocidadFinalZ,
        ref smoothVelocity,
        smoothTime
        );

        if (velocidadLocal.z > 0.5f)
        {
            velocidadMaxima = velocidadMaximaAdelante; // Si se mueve hacia adelante, usar velocidad máxima hacia adelante

        }
        else if (velocidadLocal.z < -0.5f)
        {
            velocidadMaxima = velocidadMaximaOtraDireccion; // Si se mueve hacia atrás, usar velocidad máxima hacia atrás
        }


            animatorJugador.SetFloat("X", newSpeedX);
        animatorJugador.SetFloat("Z", newSpeedZ);





        //velocidadLocal.z = Mathf.Clamp(velocidadLocal.z, -1f, 1f);
        //velocidadLocal.x = Mathf.Clamp(velocidadLocal.x, -1f, 1f);
        //if (_estaUsandoTecladoMouse)
        //{
        //    if (velocidadLocal.z > 0.8)
        //    {
        //        print("adelante");

        //        animatorBrain.ReproducirAnimacion(AnimacionesJugador.JugadorMovimientoAdelanteAnimacion, capa, false, false, 0.2f); // Reproducir animación de movimiento hacia adelante
        //    }
        //    else if (velocidadLocal.x > 0.5)
        //    {
        //        print("derecha");
        //        animatorBrain.ReproducirAnimacion(AnimacionesJugador.JugadorMovimientoDerechaAnimacion, capa, false, false, 0.2f); // Reproducir animación de movimiento hacia la derecha
        //    }
        //    else if (velocidadLocal.z < -0.8)
        //    {
        //        print("atras");
        //        animatorBrain.ReproducirAnimacion(AnimacionesJugador.JugadorMovimientoAtrasAnimacion, capa, false, false, 0.2f); // Reproducir animación de movimiento hacia atrás
        //    }

        //    else if (velocidadLocal.x < -0.5)
        //    {
        //        print("izquierda");
        //        animatorBrain.ReproducirAnimacion(AnimacionesJugador.JugadorMovimientoIzquierdaAnimacion, capa, false, false, 0.2f); // Reproducir animación de movimiento hacia la izquierda

        //    }
        //    else
        //    {
        //        print("de pie");
        //        animatorBrain.ReproducirAnimacion(AnimacionesJugador.JugadorDePieAnimacion, capa, false, false); // Reproducir animación de estar de pie
        //    }

        //}
    }

    // MÉTODO MOVIMIENTO HORIZONTAL - Versión mejorada
    private void MovimientoHorizontal()
    {
        if (dashActivo) return;

        Vector3 tempoVelocidadActual = new Vector3(rigidbodyJugador.linearVelocity.x, 0, rigidbodyJugador.linearVelocity.z);
        Vector3 deltaVelocidad = _velocidadObjetivo - tempoVelocidadActual;

        // Determinar si hay input real (no solo velocidad objetivo > 0)
        bool hayInput = _velocidadObjetivo.magnitude > 0.01f;


        // PARADA INMEDIATA - Si no hay input y velocidad es baja
        if (!hayInput && tempoVelocidadActual.magnitude <= _umbralParaPausarMovimiento)
        {
            rigidbodyJugador.linearVelocity = new Vector3(0, rigidbodyJugador.linearVelocity.y, 0);
            return;
        }

        // PARADA AGRESIVA - Si velocidad es extremadamente baja
        if (!hayInput && tempoVelocidadActual.magnitude <= 0.05f)
        {
            rigidbodyJugador.linearVelocity = new Vector3(0, rigidbodyJugador.linearVelocity.y + aumentoGravedad, 0);
            return;
        }

        // Calcular aceleración/desaceleración
        float cambioVelocidad = hayInput ? aceleracion : desaceleracion;

        // Solo aplicar fuerza si hay diferencia significativa
        if (deltaVelocidad.magnitude > 0.05f)
        {
            Vector3 fuerza = deltaVelocidad.normalized * cambioVelocidad;
            float fuerzaMaxima = cambioVelocidad * rigidbodyJugador.mass * Time.fixedDeltaTime;

            if (fuerza.magnitude > fuerzaMaxima)
            {
                fuerza = fuerza.normalized * fuerzaMaxima;
            }

            fuerza.y = 0;
            rigidbodyJugador.AddForce(fuerza, ForceMode.Impulse);
        }

        // Limitar velocidad máxima
        Vector3 nuevaVelocidadHorizontal = new Vector3(rigidbodyJugador.linearVelocity.x, 0, rigidbodyJugador.linearVelocity.z);

        if (nuevaVelocidadHorizontal.magnitude > velocidadMaxima)
        {
            nuevaVelocidadHorizontal = nuevaVelocidadHorizontal.normalized * velocidadMaxima;
            rigidbodyJugador.linearVelocity = new Vector3(nuevaVelocidadHorizontal.x, rigidbodyJugador.linearVelocity.y + aumentoGravedad, nuevaVelocidadHorizontal.z);
        }

        // Rotación del personaje
        if (direccion != Vector3.zero && !_estaUsandoTecladoMouse)
        {
            Quaternion rotacionDeseada = Quaternion.LookRotation(new Vector3(direccion.x, 0, direccion.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada.normalized, velocidadRotacion);
        }
    }

    //private void SetAnimacionDefecto(int capa)
    //{
    //    if (capa == capaCuerpoSuperior)
    //    {
    //        RevisarAnimacionSuperior();
    //    }
    //    else if (capa == capaCuerpoInferior)
    //    {
    //        RevisarAnimacionInferior();
    //    }

    //}

    //private void RevisarAnimacionSuperior()
    //{
    //    RevisarMovimiento(capaCuerpoSuperior);

    //}
    //private void RevisarAnimacionInferior()
    //{
    //    RevisarMovimiento(capaCuerpoInferior);
    //}



    #endregion





    #region "Funciones Flujo Unity"






    private void Awake()
    {
        control.EventoMovimiento+= movimientoBase;
        control.EventoDash += MovimientoDash;
        control.EventoDireccion += DireccionVista;
    }





    private void Start()
    {

        velocidadActual = velocidad; // Inicializar la velocidad actual al valor de velocidad normal
        Cursor.lockState = CursorLockMode.Confined; // Bloquear el cursor dentro de la ventana del juego

        //animatorJugador.Play("");
    }



    
    private void Update()
    {
        //AnimacionMovimiento();
    }

    private void FixedUpdate()
    {
        MovimientoHorizontal();

        RevisarMovimiento();
        //RevisarAnimacionSuperior();
        //RevisarAnimacionInferior();


    }

    private void OnDisable()
    {
        //controles.Jugador.Disable();
        control.EventoMovimiento -= movimientoBase;
        control.EventoDash -= MovimientoDash;
        control.EventoDireccion -= DireccionVista;
    }
    #endregion
}
