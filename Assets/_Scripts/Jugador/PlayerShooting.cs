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


    void Shoot()
    {
        GameObject bullet = Instantiate(Proyectile); // Creo la bala y la ubico con una rotación
                                   // El prefab de la bala debe tener rb
        if (bullet.TryGetComponent<Rotacion>(out Rotacion rb)) // Si el prefab tiene un rigidbody
        {
            rb.EjeTransform = ejeTransform; // Asigno el eje de rotación a la bala
        }

    }
    private void Awake()
    {
        controlesInput.EventoProyectil += Shoot; // Asigno el evento de disparo del scriptable object
        
    }
    private void OnDisable()
    {
        controlesInput.EventoProyectil -= Shoot; // Desasigno el evento de disparo del scriptable object
    }
    }

