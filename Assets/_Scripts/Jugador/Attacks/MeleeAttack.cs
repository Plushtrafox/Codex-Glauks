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
    




   


    private void Awake()
    {
        controles.EventoAtaqueEmpieza += Hit;
        scriptPlumaAtaque.EventoReactivarAtaque += ReactivarBoolAtaque; // Suscribirse al evento de reactivaci�n del ataque
    }


    private void OnDisable()
    {
        controles.EventoAtaqueEmpieza -= Hit;
        scriptPlumaAtaque.EventoReactivarAtaque += ReactivarBoolAtaque; // Suscribirse al evento de reactivaci�n del ataque

    }


    private void Hit()
    {

        if (!estaAtacando)
        {
            animatorBrain.ReproducirAnimacion(AnimacionesJugador.JugadorAtaqueCortoAlcanceAnimacion1, 1, true,true);
            animatorBrain.ReproducirAnimacion(AnimacionesJugador.JugadorAtaqueCortoAlcanceAnimacion1, 0, true, true);

            animator.CrossFade("JugadorAtaqueCortoAlcanceAnimacion1", 0.02f, 0,0f, 0);//nombre incorrecto pero al cambiar el nombre de la animacion no se cambia el nombre de la referencia NO SE PORQUE XD
            animator.Update(0f); // Asegura que el animador est� actualizado antes de verificar el estado
            estaAtacando = true; // Marca que se est� atacando para evitar m�ltiples ataques simult�neos
        }


    }


    public void ReactivarBoolAtaque()
    {
        estaAtacando = false; // Marca que ya no se est� atacando

    }


}





