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
    




   


    private void Awake()
    {
        controles.EventoAtaqueEmpieza += Hit;
        scriptPlumaAtaque.EventoReactivarAtaque += ReactivarBoolAtaque; // Suscribirse al evento de reactivación del ataque
    }


    private void OnDisable()
    {
        controles.EventoAtaqueEmpieza -= Hit;
        scriptPlumaAtaque.EventoReactivarAtaque += ReactivarBoolAtaque; // Suscribirse al evento de reactivación del ataque

    }


    private void Hit()
    {

        if (!estaAtacando)
        {
            animatorBrain.ReproducirAnimacion(AnimacionesJugador.JugadorAtaqueCortoAlcanceAnimacion1, 1, true,true);
            animatorBrain.ReproducirAnimacion(AnimacionesJugador.JugadorAtaqueCortoAlcanceAnimacion1, 0, true, true);

            animator.CrossFade("JugadorAtaqueCortoAlcanceAnimacion1", 0.02f, 0,0f, 0);//nombre incorrecto pero al cambiar el nombre de la animacion no se cambia el nombre de la referencia NO SE PORQUE XD
            animator.Update(0f); // Asegura que el animador esté actualizado antes de verificar el estado
            estaAtacando = true; // Marca que se está atacando para evitar múltiples ataques simultáneos
        }


    }


    public void ReactivarBoolAtaque()
    {
        estaAtacando = false; // Marca que ya no se está atacando

    }


}





