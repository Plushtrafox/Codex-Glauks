using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

// Script unido al player
// SCRIPT DE ATAQUE ESPECIAL, LIBRO GIRATORIO IGNORAR NOMBRE DE CLASE DE SCRIPT

public class PlayerShooting : MonoBehaviour
{
    //Variables
    public GameObject Proyectile;  // Referencia de la bala
    [SerializeField] private ControllesSOSCript controlesInput; // Referencia al scriptable object de controles
    [SerializeField] private Transform ejeTransform;
    [SerializeField] private bool puedeUsarHabilidad = false;
    [SerializeField] private float abilityCooldown = 3;
    [SerializeField] private PlumaManager scriptPlumaAtaque; // Referencia al script de la cámara para invocar eventos de sacudida de cámara
    [SerializeField] private AnimatorBrain animatorBrain; // Referencia al script AnimatorBrain para manejar las animaciones del jugador
    [SerializeField] private MeleeAttack meleeAttackScript;

    [SerializeField] private bool puedeUsar = true; // Variable para evitar problema con los otros ataques y que no se puedan usar todos al mismo tiempo

    private void DesactivarAtaques()
    {
        if (puedeUsar)
        {
            puedeUsar = false; // Desactiva la capacidad de atacar

        }
    } 

    private void ShootAnimation()
    {
        if (puedeUsarHabilidad && puedeUsar)
        {
            animatorBrain.ReproducirAnimacion(AnimacionesJugador.JugadorHabilidadGiratoria, 0, true, false);
            meleeAttackScript.EventoDesactivarAtaques?.Invoke(); // Invoca el evento para desactivar ataques cuerpo a cuerpo

            puedeUsarHabilidad = false;
            
        }

    }

    private void UsarPoderLibroGiratorio()
    {
        GameObject bullet = Instantiate(Proyectile);// Creo la bala y la ubico con una rotación
                                                    // El prefab de la bala debe tener rb

        if (bullet.TryGetComponent<Proyectile>(out Proyectile rb)) // Si el prefab tiene un rigidbody
        {
            rb.EjeTransform = ejeTransform; // Asigno el eje de rotación a la bala

            StartCoroutine(ReactivarPoderGiratorio());
        }
    }
    private void Awake()
    {
        controlesInput.EventoPoder += ShootAnimation; // Asigno el evento de disparo del scriptable object
        scriptPlumaAtaque.EventoPoderLibroGiratorio += UsarPoderLibroGiratorio;
        meleeAttackScript.EventoDesactivarAtaques += DesactivarAtaques; // Suscribirse al evento de desactivación de ataques
        scriptPlumaAtaque.EventoReactivarPoderLibroGiratorio += ReactivarPoder; // Suscribirse al evento de reactivación del poder giratorio

    }

    private void OnDisable()
    {
        controlesInput.EventoPoder -= ShootAnimation; // Desasigno el evento de disparo del scriptable object
        meleeAttackScript.EventoDesactivarAtaques -= DesactivarAtaques;
        meleeAttackScript.EventoDesactivarAtaques -= DesactivarAtaques; // Suscribirse al evento de desactivación de ataques
        scriptPlumaAtaque.EventoPoderLibroGiratorio -= UsarPoderLibroGiratorio;

    }

    IEnumerator ReactivarPoderGiratorio()
    {
        yield return new WaitForSeconds(abilityCooldown); 
        puedeUsarHabilidad = true;
        yield return null;
    }

    private void ReactivarPoder() //reactivar la posibilidad de poder luego de animacion de otros ataques
    {
        puedeUsar = true; // Reactiva la capacidad de atacar


    }
}

