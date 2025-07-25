using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private GameObject menuConfiguracion;

    public void AbrirMenuConfiguracion()
    {
        menuConfiguracion.SetActive(true);
    }
    public void IniciarJuego()
    {
        SceneManager.LoadScene(1);
    }
    public void SalirJuego()
    {
        Application.Quit();
    }
}



