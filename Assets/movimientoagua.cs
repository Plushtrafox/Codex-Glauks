using UnityEngine;

public class MovimientoAgua : MonoBehaviour
{
    public float velocidadX = 0.05f;
    Renderer rend;

    void Start() { rend = GetComponent<Renderer>(); }

    void Update()
    {
        float offsetX = Time.time * velocidadX;
        rend.material.mainTextureOffset = new Vector2(offsetX, 0);
    }
}