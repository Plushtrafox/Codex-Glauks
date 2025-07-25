using UnityEngine;

public class OndularTextura : MonoBehaviour
{
    public float velocidadX = 0.02f;
    public float velocidadY = 0.01f;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float offsetX = Time.time * velocidadX;
        float offsetY = Mathf.Sin(Time.time * velocidadY) * 0.05f;
        rend.material.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}
