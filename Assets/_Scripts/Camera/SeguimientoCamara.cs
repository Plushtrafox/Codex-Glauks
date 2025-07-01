using Unity.VisualScripting;
using UnityEngine;

public class SeguimientoCamara : MonoBehaviour
{
    [SerializeField] Transform Jugador;
    Transform Movimiento;
    Vector3 vector;
    [SerializeField] Transform Camara;
    float player;
    private void Awake()
    {
        
    }
    void Update()
    {
        Vector3 Movimiento = new Vector3(Jugador.position.x, Camara.position.y, Jugador.position.z);
        Camara.position = Movimiento;
    }
}
