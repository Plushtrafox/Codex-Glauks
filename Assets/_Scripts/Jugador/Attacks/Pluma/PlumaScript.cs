using Unity.VisualScripting;
using UnityEngine;

public class PlumaScript : MonoBehaviour
{
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private int damage = 15;

    private void OnTriggerEnter(Collider other)
    {
        if (other !=null)
        {
            if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemigo))
            {
                enemigo.TakeDamage(damage);

                cameraManager.EventoShakeCamaraGolpe?.Invoke(); // Invoca el evento de sacudida de cámara al golpear un enemigo
            }

        }
    }
}
