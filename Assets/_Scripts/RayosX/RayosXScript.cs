using UnityEngine;

public class RayosXScript : MonoBehaviour
{
    public static int idPosicion = Shader.PropertyToID("PosicionJugador");
    public static int idSize = Shader.PropertyToID("Size");



    [SerializeField] private Material materialRayosXPared;
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
            materialRayosXPared.SetFloat(idSize, 1);
        }
        else
        {
            materialRayosXPared.SetFloat(idSize, 0);
        }




        materialRayosXPared.SetVector(idPosicion, direccion);
    }

}
