using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ControllesSOSCript controles;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject ataqueCortoAlcanceCollider;
    AnimatorStateInfo animatorStateInfo; // Variable para almacenar el estado actual del animador
    [SerializeField] private PlumaManager scriptPlumaAtaque; // Referencia al script de la c�mara para invocar eventos de sacudida de c�mara



    [Header("Melee Attack Settings")]
    [SerializeField] private Transform AttackController;
    [SerializeField] private float damage;
    //[SerializeField] private float tiempoDeAtaque = 0.2f; // Tiempo total del ataque
    //private float tiempoActual = 0f; // Tiempo actual del ataque, se incrementar� en cada frame durante el ataque
    [SerializeField]private bool estaAtacando = false; // Bandera para evitar m�ltiples ataques simult�neos
    




   


    private void Awake()
    {
        controles.EventoAtaqueEmpieza += Hit;
        scriptPlumaAtaque.EventoReactivarAtaque += ReactivarBoolAtaque; // Suscribirse al evento de reactivaci�n del ataque
    }

    private void Start()
    {
        animator.CrossFade("ArmaBase", 0.5f);//nombre incorrecto pero al cambiar el nombre de la animacion no se cambia el nombre de la referencia NO SE PORQUE XD

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
            animator.CrossFade("AtaqueLigero", 0.08f, 0,0f);//nombre incorrecto pero al cambiar el nombre de la animacion no se cambia el nombre de la referencia NO SE PORQUE XD
            animator.Update(0f); // Asegura que el animador est� actualizado antes de verificar el estado
            estaAtacando = true; // Marca que se est� atacando para evitar m�ltiples ataques simult�neos
        }


    }


    public void ReactivarBoolAtaque()
    {
        estaAtacando = false; // Marca que ya no se est� atacando
        animator.CrossFade("ArmaBase", 0.2f,0,0f);//nombre incorrecto pero al cambiar el nombre de la animacion no se cambia el nombre de la referencia NO SE PORQUE XD
        animator.Update(0f);
    }


}





