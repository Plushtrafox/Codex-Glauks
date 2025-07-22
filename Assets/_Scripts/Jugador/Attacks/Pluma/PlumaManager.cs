using UnityEngine;

public class PlumaManager : MonoBehaviour
{
    [SerializeField] private GameObject plumaCollider;

    public delegate void TipoEventoBasico();
    public event TipoEventoBasico EventoReactivarAtaque;
    public event TipoEventoBasico EventoDispararAtaqueLargoAlcance;
    public event TipoEventoBasico EventoReactivarAtaqueLargoAlcance;
    public event TipoEventoBasico EventoPoderLibroGiratorio;
    public event TipoEventoBasico EventoReactivarPoderLibroGiratorio;





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
        EventoReactivarAtaqueLargoAlcance?.Invoke();
        EventoReactivarPoderLibroGiratorio?.Invoke(); // Invoca el evento para reactivar el poder del libro giratorio


    }

    public void ActivarDisparo()
    {
        EventoDispararAtaqueLargoAlcance?.Invoke();

    }

    public void ReactivarDisparo()
    {
        EventoReactivarAtaqueLargoAlcance?.Invoke();
        EventoReactivarAtaque?.Invoke(); // Invoca el evento para reactivar el booleano de ataque
        EventoReactivarPoderLibroGiratorio?.Invoke(); // Invoca el evento para reactivar el poder del libro giratorio

    }
    public void ReactivarAtaquesGiratorio() //para reactivar los ataques luego de terminar la animacion de ataque especial
    {
        EventoReactivarAtaqueLargoAlcance?.Invoke();
        EventoReactivarAtaque?.Invoke(); // Invoca el evento para reactivar el booleano de ataque
        EventoReactivarPoderLibroGiratorio?.Invoke(); // Invoca el evento para reactivar el poder del libro giratorio
    }
    public void ActivarPoderLibroGiratorio()
    {
        EventoPoderLibroGiratorio?.Invoke();
    }
}
