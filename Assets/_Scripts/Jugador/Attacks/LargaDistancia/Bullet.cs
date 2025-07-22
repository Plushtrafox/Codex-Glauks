using UnityEngine;

public class Bullet : MonoBehaviour
{
    //[SerializeField] private float lifetime = 5f; // Duración del proyectil en segundos
    [SerializeField] private float damage;
    [SerializeField] private CameraManager cameraManager1;
    public CameraManager CameraManager1
    {
        get => cameraManager1;
        set => cameraManager1 = value;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;
        if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemigo))
        {
            enemigo.TakeDamage(damage);

            cameraManager1.EventoShakeCamaraGolpe?.Invoke(); // Invoca el evento de sacudida de cámara al golpear un enemigo
        }
        Destroy(gameObject);

    }


}
