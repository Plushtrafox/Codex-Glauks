using UnityEngine;

public class ExpandeYDesvanece : MonoBehaviour
{
    private float duracion = 0.3f;
    private float tiempo = 0f;
    private Vector3 escalaInicial;
    private Material miMaterial;
    private Color colorInicial;

    void Start()
    {
        escalaInicial = transform.localScale;
        miMaterial = GetComponent<Renderer>().material;
        colorInicial = miMaterial.color;
    }

    void Update()
    {
        tiempo += Time.deltaTime;

        transform.localScale = escalaInicial * (1 + tiempo * 4f);

        float alpha = Mathf.Lerp(1f, 0f, tiempo / duracion);
        miMaterial.color = new Color(colorInicial.r, colorInicial.g, colorInicial.b, alpha);

        if (tiempo >= duracion)
        {
            Destroy(gameObject);
        }
    }
}