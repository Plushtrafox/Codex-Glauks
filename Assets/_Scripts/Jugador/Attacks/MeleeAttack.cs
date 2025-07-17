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
    [SerializeField] private PlumaManager scriptPlumaAtaque; // Referencia al script de la c�mara para invocar eventos de sacudida de c�mara
    [SerializeField] private AnimatorBrain animatorBrain; // Referencia al script AnimatorBrain para manejar las animaciones del jugador



    [Header("Melee Attack Settings")]

    [SerializeField]private bool estaAtacando = false; // Bandera para evitar m�ltiples ataques simult�neos


    [Header("parametros ATAQUE PESADO")]
    [SerializeField] private float tiempoDeCarga = 1.0f;
    [SerializeField]private float tiempoActual = 0f;
    [SerializeField]private bool estaCargandoAtaque= false;
    





   


    private void Awake()
    {
        controles.EventoAtaqueEmpieza += EmpiezaAtaque;
        controles.EventoAtaqueTermina += TerminaAtaque;
        scriptPlumaAtaque.EventoReactivarAtaque += ReactivarBoolAtaque; // Suscribirse al evento de reactivaci�n del ataque
    }


    private void OnDisable()
    {
        controles.EventoAtaqueEmpieza -= EmpiezaAtaque;
        controles.EventoAtaqueTermina -= TerminaAtaque;

        scriptPlumaAtaque.EventoReactivarAtaque += ReactivarBoolAtaque; // Suscribirse al evento de reactivaci�n del ataque

    }


    private void EmpiezaAtaque()
    {
        if (estaCargandoAtaque || estaAtacando) return;


        animatorBrain.ReproducirAnimacion(AnimacionesJugador.JugadorRecargaAtaqueAnimacion, CapasAnimacion.CapaSuperior, true, true, 0.01f);

        StartCoroutine(CargaDeAtaque());
        estaCargandoAtaque = true;
    


    }

    IEnumerator CargaDeAtaque()
    {
        while (tiempoActual < tiempoDeCarga) 
        {

            tiempoActual += Time.deltaTime;
            yield return null;


        }


        yield return null;
    }
    private void TerminaAtaque()
    {
        StopCoroutine(CargaDeAtaque());
        if (!estaAtacando)
        {
            if (tiempoActual >= tiempoDeCarga)
            {
                animatorBrain.ReproducirAnimacion(AnimacionesJugador.JugadorAtaquePesado, CapasAnimacion.CapaSuperior, true, true, 0.01f);
                estaAtacando = true; // Marca que se est� atacando para evitar m�ltiples ataques simult�neos
                estaCargandoAtaque = false;
                //golpe pesado
            }
            else if (tiempoActual < tiempoDeCarga)
            {
                animatorBrain.ReproducirAnimacion(AnimacionesJugador.JugadorAtaqueCortoAlcanceAnimacion1, CapasAnimacion.CapaSuperior, true, true, 0.01f);
                estaAtacando = true; // Marca que se est� atacando para evitar m�ltiples ataques simult�neos
                estaCargandoAtaque = false;

            }



        }
    }


    public void ReactivarBoolAtaque()
    {
        estaAtacando = false; // Marca que ya no se est� atacando

    }

    


}





