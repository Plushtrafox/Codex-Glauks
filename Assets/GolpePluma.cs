using UnityEngine;

public class GolpePluma : MonoBehaviour
{
    public GameObject efectoGolpe;
    public int cantidadRayos = 6;
    public float radio = 0.5f;

    void OnTriggerEnter(Collider other)
    {
        // 💡 Solo atacar si el botón de ataque está presionado
        if (!Input.GetButton("Fire1")) return; // Usa "Fire1" para clic izquierdo o botón asignado

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
                    Random.Range(80f, 100f),
                    Random.Range(0f, 360f),
                    Random.Range(-30f, 30f)
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