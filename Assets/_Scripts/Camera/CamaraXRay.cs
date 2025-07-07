using UnityEngine;

public class CamaraXRay : MonoBehaviour
{
    private RaycastHit hit;
    [SerializeField] private Transform player; // Referencia al transform del jugador


    private void Update()
    {
        if (Physics.Linecast(transform.position, player.position, out hit))
        {
            if (hit.collider.CompareTag("Muro"))
            {
               
                if (hit.collider.TryGetComponent<Renderer>(out Renderer render))
                {
                    print("Renderer found on wall: " + render.name);
                    Material mat = render.material;
                    mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0.3f); // 30% opacidad

                }
            }
        }
    }


}
