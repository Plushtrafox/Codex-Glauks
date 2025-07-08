using UnityEngine;

public class Bullet : MonoBehaviour
{
    //[SerializeField] private float lifetime = 5f; // Duraci�n del proyectil en segundos
    [SerializeField] private float damage;
    [SerializeField] private CameraManager cameraManager1;
    public CameraManager CameraManager1
    {
        get => cameraManager1;
        set => cameraManager1 = value;
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision == null) return;
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemigo))
        {
            enemigo.TakeDamage(damage);

            cameraManager1.EventoShakeCamaraGolpe?.Invoke(); // Invoca el evento de sacudida de c�mara al golpear un enemigo
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
