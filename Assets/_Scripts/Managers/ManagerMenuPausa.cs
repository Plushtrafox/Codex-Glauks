using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerMenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GameObject menuConfiguracion;
    [SerializeField] private ControllesSOSCript controllesSOS;
    [SerializeField] private HealthSO vidaSO;

    private bool _puedoPausar = true; // Variable para controlar si se puede pausar el juego


    private void Awake()
    {
        controllesSOS.EventoBotonEscape += InteractuarMenuPausa;
        vidaSO.EventoPerdida += DesactivarPausa; // Desactiva la pausa cuando el jugador pierde toda su vida
    }

    private void Start()
    {
        menuPausa.SetActive(false); // Aseg�rate de que el men� de pausa est� desactivado al inicio
        menuConfiguracion.SetActive(false); // Aseg�rate de que el men� de configuraci�n est� desactivado al inicio
        _puedoPausar = true; // Permite pausar el juego al inicio

    }

    private void OnDisable()
    {
        controllesSOS.EventoBotonEscape -= InteractuarMenuPausa; // Aseg�rate de desuscribirte para evitar fugas de memoria
        vidaSO.EventoPerdida -= DesactivarPausa; // Desuscribirse del evento de p�rdida de vida
    }

    private void DesactivarPausa()
    {
        _puedoPausar = false;
    }

    private void InteractuarMenuPausa()
    {
        if (!_puedoPausar) return; // Si no se puede pausar, no hacemos nada

        if (menuPausa.activeSelf)
        {
            CerrarMenu();
        }
        else
        {
            menuPausa.SetActive(true);
        }
    }



    public void CerrarMenu()
    {
        menuPausa.SetActive(false);
    }
    public void IrConfiguracion()
    {
       menuConfiguracion.SetActive(true);
    }

    public void EnviarMenuDeInicio()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
