using System.Collections;
using UnityEngine;

public class LongShoot : MonoBehaviour
{
    [SerializeField] private ControllesSOSCript controles;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnPoint; // Punto de origen del proyectil
    [SerializeField] private float speed = 20f; // Velocidad del proyectil
    [SerializeField] private float shootTime = 0.5f; // Tiempo entre disparos
    [SerializeField] private CameraManager cameraManager;
    private bool activateShoot= true;


    private void Shoot()
    {
        if (activateShoot == true)
        {
            GameObject bulletInstantiate= Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
            if (bulletInstantiate.TryGetComponent<Bullet>(out Bullet bulletScript))
            {
                bulletScript.CameraManager1 = cameraManager;
            }
            print(bulletInstantiate.name);
            if (bulletInstantiate.TryGetComponent<Rigidbody>(out Rigidbody bulletRigidbody))
            {
                bulletRigidbody.AddForce(spawnPoint.forward *100* speed); // Aplica fuerza al proyectil
            }
            StartCoroutine(cooldown());
        }

    }
    private void Awake()
    {
        controles.EventoLargaDistancia += Shoot;
    }
    private void OnDisable()
    {
        controles.EventoLargaDistancia -= Shoot;
    }
    IEnumerator cooldown()
    {
        activateShoot = false;
        yield return new WaitForSeconds(shootTime);
        activateShoot=true;
        yield return null;
    }
}

