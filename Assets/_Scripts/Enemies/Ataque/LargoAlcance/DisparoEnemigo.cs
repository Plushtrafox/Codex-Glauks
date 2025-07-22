using UnityEngine;

public class DisparoEnemigo : MonoBehaviour
{
    [SerializeField] private GameObject prefabDisparo;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private float velocidadDisparo = 10f;
    [SerializeField] private Transform objetivo;

    public void DisparoEnemigoBase()
    {
        GameObject balaNueva = Instantiate(prefabDisparo, puntoDisparo.position, puntoDisparo.rotation);

        if (balaNueva.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            Vector3 direccionDisparo = (objetivo.position - puntoDisparo.position).normalized;
            direccionDisparo.y = 0; // Asegura que el disparo sea horizontal
            rb.AddForce(direccionDisparo * velocidadDisparo, ForceMode.Impulse);

        }
    }
}
