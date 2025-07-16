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

    private void Shoot()
    {
        if (puedeDisparar == true)
        {
            animatorBrain.ReproducirAnimacion(AnimacionesJugador.JugadorAtaqueLargoAlcance, 0, true, false, 0.1f);
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
    }
    private void OnDisable()
    {
        controles.EventoLargaDistancia -= Shoot;
        ataqueManager.EventoDispararAtaqueLargoAlcance -= DisparoLargaDistancia;
        ataqueManager.EventoReactivarAtaqueLargoAlcance -= ReactivarAtaqueLargoAlcance;


    }

}

