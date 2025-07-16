using UnityEngine;

public class PlumaManager : MonoBehaviour
{
    [SerializeField] private GameObject plumaCollider;

    public delegate void TipoEventoBasico();
    public event TipoEventoBasico EventoReactivarAtaque;
    public event TipoEventoBasico EventoDispararAtaqueLargoAlcance;
    public event TipoEventoBasico EventoReactivarAtaqueLargoAlcance;
    public event TipoEventoBasico EventoPoderLibroGiratorio;




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

    public void ActivarDisparo()
    {
        EventoDispararAtaqueLargoAlcance?.Invoke();
    }

    public void ReactivarDisparo()
    {
        EventoReactivarAtaqueLargoAlcance?.Invoke();
    }
    public void ActivarPoderLibroGiratorio()
    {
        EventoPoderLibroGiratorio?.Invoke();
    }
}
