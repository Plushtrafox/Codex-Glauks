using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

// Script unido al player
// Debo añadir sistema de conteo de balas para recargar

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


    private void ShootAnimation()
    {
        if (puedeUsarHabilidad)
        {
            animatorBrain.ReproducirAnimacion(AnimacionesJugador.JugadorHabilidadGiratoria, 0, true, false);


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
        
    }

    private void OnDisable()
    {
        controlesInput.EventoPoder -= ShootAnimation; // Desasigno el evento de disparo del scriptable object
    }

    IEnumerator ReactivarPoderGiratorio()
    {
        yield return new WaitForSeconds(abilityCooldown); 
        puedeUsarHabilidad = true;
        yield return null;
    }

}

