using UnityEngine;

public class GolpePluma : MonoBehaviour
{
    public GameObject efectoGolpe;
    public int cantidadRayos = 6;
    public float radio = 0.5f;

    void OnTriggerEnter(Collider other)
    {
        // ✅ Verifica si estás atacando
        if (!Input.GetButton("Fire1")) return;

        // ✅ Verifica si el objeto tocado es un enemigo
        if (!other.CompareTag("Enemigo")) return;

        // 🎯 Instancia rayos con ubicación cercana al impacto
        for (int i = 0; i < cantidadRayos; i++)
        {
            Vector3 posicion = other.transform.position + new Vector3(
                Random.Range(-radio, radio),
                Random.Range(0f, radio),
                Random.Range(-radio, radio)
            );

            Quaternion rotacion = Quaternion.Euler(
                Random.Range(85f, 95f),
                Random.Range(0f, 360f),
                Random.Range(-25f, 25f)
            );

            GameObject rayo = Instantiate(efectoGolpe, posicion, rotacion);
            Destroy(rayo, 0.3f);
        }

        // 💥 Aplica daño si el enemigo tiene el componente
        Enemigo enemigo = other.GetComponent<Enemigo>();
        if (enemigo != null)
        {
            enemigo.RecibirGolpe();
        }
    }
}