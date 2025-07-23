using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerMenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GameObject menuConfiguracion;
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
