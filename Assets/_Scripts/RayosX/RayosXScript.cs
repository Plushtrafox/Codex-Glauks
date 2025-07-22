using UnityEngine;

public class RayosXScript : MonoBehaviour
{
    public static int idPosicion = Shader.PropertyToID("PosicionJugador");
    public static int idSize = Shader.PropertyToID("_Size");



    [SerializeField] private Material[] materialRayosXPared;
    [SerializeField] private Camera camara;
    [SerializeField] private LayerMask layerMaskRayosX;
    [SerializeField] private float distanciaRayosX = 100f;


    void Update()
    {
        Vector3 direccion = camara.transform.position - transform.position;
        var rayo = new Ray(transform.position, direccion.normalized);
        var vista = camara.WorldToViewportPoint(transform.position);

        if (Physics.Raycast(rayo, out RaycastHit hit, distanciaRayosX, layerMaskRayosX))
        {
            foreach (var materialRayosXPared in materialRayosXPared)
            {
                materialRayosXPared.SetFloat(idSize, 1.5f);

            }
        }
        else
        {
            foreach (var materialRayosXPared in materialRayosXPared)
            {
                materialRayosXPared.SetFloat(idSize, 0);
            }
        }



        foreach (var materialRayosXPared in materialRayosXPared)
        {
            materialRayosXPared.SetVector(idPosicion, vista);
        }
    }

}
