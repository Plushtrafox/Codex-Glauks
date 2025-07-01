using UnityEngine;

public class PlumaManager : MonoBehaviour
{
    [SerializeField] private GameObject plumaCollider;

    public delegate void TipoAtaqueNormal();
    public event TipoAtaqueNormal EventoReactivarAtaque;


    public void ActivarColliderAtaque()
    {
        plumaCollider.SetActive(true); // Desactiva el collider del ataque corto alcance al inicio del ataque

    }
    public void DesactivarColliderAtaque()
    {
        plumaCollider.SetActive(false); // Desactiva el collider del ataque corto alcance al finalizar el ataque
    }
    public void ReactivarBoolAtaque()
    {
        EventoReactivarAtaque?.Invoke(); // Invoca el evento para reactivar el booleano de ataque
    }
}
