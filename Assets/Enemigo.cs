using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public int vida = 5;
    public Color colorOriginal = Color.white;
    public Color colorHerido = Color.red;
    public GameObject efectoRayo;

    private Renderer miRenderer;

    private void Start()
    {
        miRenderer = GetComponent<Renderer>();
        miRenderer.material.color = colorOriginal;
    }

    public void RecibirGolpe()
    {
        // ⚡ PRIMERO mostramos el rayo, antes de tocar la vida
        if (efectoRayo != null)
        {
            GameObject rayo = Instantiate(efectoRayo, transform.position, Quaternion.Euler(90, 0, 0));
            Destroy(rayo, 0.5f);
        }

        // ⚔️ Luego aplicamos el daño
        vida--;

        // 🎨 Cambia de color al segundo golpe
        if (vida == 3)
        {
            miRenderer.material.color = colorHerido;
        }

        // ☠️ Se destruye si ya no tiene vida
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }
}