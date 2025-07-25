using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerMenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GameObject menuConfiguracion;
    [SerializeField] private ControllesSOSCript controllesSOS;


    private void Awake()
    {
        controllesSOS.EventoBotonEscape += InteractuarMenuPausa;
    }

    private void OnDisable()
    {
        controllesSOS.EventoBotonEscape -= InteractuarMenuPausa; // Asegúrate de desuscribirte para evitar fugas de memoria
    }

    private void InteractuarMenuPausa()
    {
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
        print("deberia iniciar juego");
        SceneManager.LoadScene(0);
    }
}
