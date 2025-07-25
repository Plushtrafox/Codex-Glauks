using System.Collections;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public Color colorOriginal = Color.white;
    public Color colorHerido = Color.red;
    public GameObject efectoRayo;

    [SerializeField] private Renderer miRenderer;
    
    private void Start()
    {

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


        StartCoroutine(RecibirGolpeCoroutine());


    }

    IEnumerator RecibirGolpeCoroutine()
    {
        print("entro en corrutina de recibir golpe");
        // Cambia el color a rojo
        miRenderer.material.color = colorHerido;
        // Espera un segundo
        yield return new WaitForSeconds(2f);
        // Vuelve al color original
        miRenderer.material.color = colorOriginal;

        yield return null;
    }



}