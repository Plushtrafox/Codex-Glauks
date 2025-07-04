using UnityEngine;

public class LongShoot : MonoBehaviour
{
    [SerializeField] private ControllesSOSCript controles;
    [SerializeField] private Rigidbody bulletPrefab;
    [SerializeField] private Transform spawnPoint; // Punto de origen del proyectil
    [SerializeField] private float speed = 20f; // Velocidad del proyectil
    [SerializeField] private float shootTime = 0.5f; // Tiempo entre disparos
    [SerializeField] private float lifetime = 5f; // Duración del proyectil en segundos
    private float shootStart; // Referencia al script de controles

    private void Update()
    {
        if (Input.GetButtonDown("Fire2") && Time.time >= shootStart + shootTime)
        {
            shootStart = Time.time + shootTime; // Actualiza el tiempo del último disparo
            Rigidbody  bulletPrefabInstanciate; // Variable para almacenar la instancia del proyectil

            bulletPrefabInstanciate = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity) as Rigidbody; // Crea el proyectil

            bulletPrefabInstanciate.AddForce(spawnPoint.forward *100* speed); // Aplica fuerza al proyectil
        }
        Destroy(bulletPrefab, lifetime);
    }
}

