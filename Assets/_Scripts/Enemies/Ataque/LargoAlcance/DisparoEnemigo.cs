using UnityEngine;

public class DisparoEnemigo : MonoBehaviour
{
    [SerializeField] private GameObject prefabDisparo;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private float velocidadDisparo = 10f;

    public void DisparoEnemigoBase()
    {
        GameObject balaNueva = Instantiate(prefabDisparo, puntoDisparo.position, puntoDisparo.rotation);

        if (balaNueva.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.AddForce(puntoDisparo.right * velocidadDisparo, ForceMode.Impulse);

        }
    }
}
