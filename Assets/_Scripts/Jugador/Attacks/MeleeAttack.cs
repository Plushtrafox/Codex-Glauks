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
    [SerializeField] private PlumaManager scriptPlumaAtaque; // Referencia al script de la cámara para invocar eventos de sacudida de cámara



    [Header("Melee Attack Settings")]
    [SerializeField] private Transform AttackController;
    [SerializeField] private float damage;
    //[SerializeField] private float tiempoDeAtaque = 0.2f; // Tiempo total del ataque
    //private float tiempoActual = 0f; // Tiempo actual del ataque, se incrementará en cada frame durante el ataque
    [SerializeField]private bool estaAtacando = false; // Bandera para evitar múltiples ataques simultáneos
    




   


    private void Awake()
    {
        controles.EventoAtaqueEmpieza += Hit;
        scriptPlumaAtaque.EventoReactivarAtaque += ReactivarBoolAtaque; // Suscribirse al evento de reactivación del ataque
    }

    private void Start()
    {
        animator.CrossFade("ArmaBase", 0.5f);//nombre incorrecto pero al cambiar el nombre de la animacion no se cambia el nombre de la referencia NO SE PORQUE XD

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
            animator.CrossFade("AtaqueLigero", 0.08f, 0,0f);//nombre incorrecto pero al cambiar el nombre de la animacion no se cambia el nombre de la referencia NO SE PORQUE XD
            animator.Update(0f); // Asegura que el animador esté actualizado antes de verificar el estado
            estaAtacando = true; // Marca que se está atacando para evitar múltiples ataques simultáneos
        }


    }


    public void ReactivarBoolAtaque()
    {
        estaAtacando = false; // Marca que ya no se está atacando
        animator.CrossFade("ArmaBase", 0.2f,0,0f);//nombre incorrecto pero al cambiar el nombre de la animacion no se cambia el nombre de la referencia NO SE PORQUE XD
        animator.Update(0f);
    }


}





