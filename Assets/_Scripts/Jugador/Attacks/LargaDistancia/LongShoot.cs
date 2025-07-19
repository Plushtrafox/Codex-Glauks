using System.Collections;
using UnityEngine;

public class LongShoot : MonoBehaviour
{
    [SerializeField] private ControllesSOSCript controles;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnPoint; // Punto de origen del proyectil
    [SerializeField] private float speed = 20f; // Velocidad del proyectil
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private PlumaManager ataqueManager;
    [SerializeField] private AnimatorBrain animatorBrain;

    [SerializeField]private bool puedeDisparar= true;

    [SerializeField] private MeleeAttack meleeAtackScript; // Referencia al script de ataque cuerpo a cuerpo

    private void DesactivarAtaque()
    {
        if (puedeDisparar)
        {
            puedeDisparar = false; // Desactiva la capacidad de disparar

        }
    }

    private void Shoot()
    {
        if (puedeDisparar)
        {
            animatorBrain.ReproducirAnimacion(AnimacionesJugador.JugadorAtaqueLargoAlcance, 0, true, false, 0.1f);
            meleeAtackScript.EventoDesactivarAtaques?.Invoke(); // Invoca el evento para desactivar ataques cuerpo a cuerpo
            puedeDisparar = false;
        }
    }

    private void DisparoLargaDistancia()
    {

            GameObject bulletInstantiate = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
            if (bulletInstantiate.TryGetComponent<Bullet>(out Bullet bulletScript))
            {
                bulletScript.CameraManager1 = cameraManager;
            }
            if (bulletInstantiate.TryGetComponent<Rigidbody>(out Rigidbody bulletRigidbody))
            {
                bulletRigidbody.AddForce(transform.forward * speed); // Aplica fuerza al proyectil
            }
        
    }

    private void ReactivarAtaqueLargoAlcance()
    {
        puedeDisparar = true;   
    }
    private void Awake()
    {
        controles.EventoLargaDistancia += Shoot;
        ataqueManager.EventoDispararAtaqueLargoAlcance += DisparoLargaDistancia;
        ataqueManager.EventoReactivarAtaqueLargoAlcance += ReactivarAtaqueLargoAlcance;
        meleeAtackScript.EventoDesactivarAtaques += DesactivarAtaque; // Suscribirse al evento de desactivación de ataques
    }
    private void OnDisable()
    {
        controles.EventoLargaDistancia -= Shoot;
        ataqueManager.EventoDispararAtaqueLargoAlcance -= DisparoLargaDistancia;
        ataqueManager.EventoReactivarAtaqueLargoAlcance -= ReactivarAtaqueLargoAlcance;
        meleeAtackScript.EventoDesactivarAtaques -= DesactivarAtaque; // Desuscribirse del evento de desactivación de ataques


    }

}

