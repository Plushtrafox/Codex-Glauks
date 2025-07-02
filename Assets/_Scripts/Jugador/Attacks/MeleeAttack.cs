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
    [SerializeField] private float attackRange = 1.0f;
    [SerializeField] private float damage;
    private HashSet<GameObject> EnemigosGolpeados = new HashSet<GameObject>(); // Usar un HashSet para evitar duplicados en los enemigos golpeados
    //[SerializeField] private float tiempoDeAtaque = 0.2f; // Tiempo total del ataque
    //private float tiempoActual = 0f; // Tiempo actual del ataque, se incrementar� en cada frame durante el ataque
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
        print("se presiono el ataque");
        print("el estado del ataque es: " + estaAtacando);
        if (!estaAtacando)
        {
            animator.CrossFade("AtaqueLigero", 0.5f);//nombre incorrecto pero al cambiar el nombre de la animacion no se cambia el nombre de la referencia NO SE PORQUE XD


            estaAtacando = true; // Marca que se est� atacando para evitar m�ltiples ataques simult�neos
        }


    }


    public void ReactivarBoolAtaque()
    {
        estaAtacando = false; // Marca que ya no se est� atacando
      
    }


    IEnumerator Golpe()
    {
        animator.CrossFade("AtaqueLigero", 0.5f);

        animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0); // Obtiene el estado actual del animador
        float porcentajeDeAnimacion = animatorStateInfo.normalizedTime%1; // Obtiene el porcentaje de la animaci�n actual

        print("empezo la corrutina");

        while (porcentajeDeAnimacion>0 && porcentajeDeAnimacion<0.8)
        {
            porcentajeDeAnimacion = animatorStateInfo.normalizedTime % 1;
            print(animatorStateInfo.length);
            print(animatorStateInfo.normalizedTime);
            
            print("entro en while");
            print("porcentaje de animacion: " + porcentajeDeAnimacion);
            ataqueCortoAlcanceCollider.SetActive(true); // Desactiva el collider del ataque corto alcance al inicio del ataque
            yield return null; // Espera un frame antes de continuar
        }
        if (porcentajeDeAnimacion >= 0.8f)
        {
            print("entro en if");
            ataqueCortoAlcanceCollider.SetActive(false); // Desactiva el collider del ataque corto alcance al finalizar el ataque
        }

        estaAtacando = false; // Marca que ya no se est� atacando
        print("termino la corrutina y el esta atacando es: "+estaAtacando);
        yield return null;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackController.position, attackRange);
    }
}





