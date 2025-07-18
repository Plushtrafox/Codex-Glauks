using UnityEngine;

public class GolpePluma : MonoBehaviour
{
    public GameObject efectoGolpe;
    public int cantidadRayos = 6;
    public float radio = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemigo"))
        {
            for (int i = 0; i < cantidadRayos; i++)
            {
                Vector3 posicion = other.transform.position + new Vector3(
                    Random.Range(-radio, radio),
                    Random.Range(0f, radio),
                    Random.Range(-radio, radio)
                );

                Quaternion rotacion = Quaternion.Euler(
                    Random.Range(80f, 100f),    // vertical
                    Random.Range(0f, 360f),     // giro horizontal
                    Random.Range(-30f, 30f)     // inclinación diagonal
                );

                GameObject rayo = Instantiate(efectoGolpe, posicion, rotacion);
                Destroy(rayo, 0.3f);
            }

            Enemigo enemigo = other.GetComponent<Enemigo>();
            if (enemigo != null)
            {
                enemigo.RecibirGolpe();
            }
        }
    }
}