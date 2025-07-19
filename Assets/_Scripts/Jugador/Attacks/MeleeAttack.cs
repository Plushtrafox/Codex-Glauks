using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class MeleeAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ControllesSOSCript controles;
    [SerializeField] private Animator animator;
    AnimatorStateInfo animatorStateInfo; // Variable para almacenar el estado actual del animador
    [SerializeField] private PlumaManager scriptPlumaAtaque; // Referencia al script de la cámara para invocar eventos de sacudida de cámara
    [SerializeField] private AnimatorBrain animatorBrain; // Referencia al script AnimatorBrain para manejar las animaciones del jugador



    [Header("Melee Attack Settings")]

    [SerializeField]private bool estaAtacando = false; // Bandera para evitar múltiples ataques simultáneos


    [Header("parametros ATAQUE PESADO")]
    [SerializeField] private float tiempoDeCarga = 1.0f;
    [SerializeField]private float tiempoActual = 0f;
    [SerializeField]private bool estaCargandoAtaque= false;

   [SerializeField] private bool puedeAtacar = true; // Bandera para controlar si el jugador puede atacar

    public Action EventoDesactivarAtaques; // Evento para notificar que se ha realizado un ataque pesado

    private Coroutine CargaDeAtaqueCorrutina;


    private void DesactivarAtaque()
    {
        if (puedeAtacar)
        {
            puedeAtacar = false; // Desactiva la capacidad de atacar

        }
    }





    private void Awake()
    {
        controles.EventoAtaqueEmpieza += EmpiezaAtaque;
        controles.EventoAtaqueTermina += TerminaAtaque;
        EventoDesactivarAtaques += DesactivarAtaque; // Suscribirse al evento de desactivación de ataques
        scriptPlumaAtaque.EventoReactivarAtaque += ReactivarBoolAtaque; // Suscribirse al evento de reactivación del ataque
    }


    private void OnDisable()
    {
        controles.EventoAtaqueEmpieza -= EmpiezaAtaque;
        controles.EventoAtaqueTermina -= TerminaAtaque;
        EventoDesactivarAtaques -= DesactivarAtaque; // Desuscribirse del evento de desactivación de ataques
        scriptPlumaAtaque.EventoReactivarAtaque += ReactivarBoolAtaque; // Suscribirse al evento de reactivación del ataque

    }


    private void EmpiezaAtaque()
    {
        if (estaCargandoAtaque || estaAtacando || !puedeAtacar) return;
        print("se presiono para hacer un ataque a corto alcance");

        animatorBrain.ReproducirAnimacion(AnimacionesJugador.JugadorRecargaAtaqueAnimacion, CapasAnimacion.CapaSuperior, true, true, 0.01f);

        CargaDeAtaqueCorrutina = StartCoroutine(CargaDeAtaque()); // Iniciar la corrutina de carga de ataque al inicio
        estaCargandoAtaque = true;
    


    }

    IEnumerator CargaDeAtaque()
    {
        print("empezo corrutina de ataque");
        tiempoActual = 0f; // Reinicia el tiempo actual al inicio de la carga del ataque
        while (tiempoActual < tiempoDeCarga) 
        {

            tiempoActual += Time.deltaTime;
            yield return null;


        }
        print("termino carga en corrutina");


        yield return null;
    }
    private void TerminaAtaque()
    {
        if (estaAtacando || !puedeAtacar) return;

        StopCoroutine(CargaDeAtaqueCorrutina); // Detiene la corrutina de carga de ataque si está en ejecución


        if (!estaAtacando)
        {
            if (tiempoActual >= tiempoDeCarga)
            {
                animatorBrain.ReproducirAnimacion(AnimacionesJugador.JugadorAtaquePesado, CapasAnimacion.CapaSuperior, true, true, 0f);
                estaAtacando = true; // Marca que se está atacando para evitar múltiples ataques simultáneos
                estaCargandoAtaque = false;
                EventoDesactivarAtaques?.Invoke(); // Invoca el evento para desactivar ataques
                tiempoActual = 0f; // Reinicia el tiempo actual al inicio de la carga del ataque

                //golpe pesado
            }
            else if (tiempoActual < tiempoDeCarga)
            {
                animatorBrain.ReproducirAnimacion(AnimacionesJugador.JugadorAtaqueCortoAlcanceAnimacion1, CapasAnimacion.CapaSuperior, true, true, 0.01f);
                estaAtacando = true; // Marca que se está atacando para evitar múltiples ataques simultáneos
                estaCargandoAtaque = false;
                EventoDesactivarAtaques?.Invoke(); // Invoca el evento para desactivar ataques
                tiempoActual = 0f; // Reinicia el tiempo actual al inicio de la carga del ataque



            }



        }
    }


    public void ReactivarBoolAtaque()
    {
        estaAtacando = false; // Marca que ya no se está atacando
        puedeAtacar = true; // Reactiva la capacidad de atacar
    }

    


}





