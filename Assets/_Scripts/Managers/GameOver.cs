using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    [SerializeField] private HealthSO viditaSO;

    [SerializeField] private GameObject menuPerdida;

    [SerializeField] private DialogoManager managerHUD;






    void Awake()
    {
        viditaSO.EventoPerdida+= MostrarMenuPerdida;
    }
    void OnDisable()
    {
        viditaSO.EventoPerdida-= MostrarMenuPerdida;
    }



    private void MostrarMenuPerdida()
    {
        managerHUD.OcultarHUD?.Invoke(); // Oculta el HUD del jugador
        menuPerdida.SetActive(true);
        Time.timeScale = 0f; // Pausa el juego

    }

    public void reiniciar()
    {
        SceneManager.LoadScene("CALABOXO1");

        menuPerdida.SetActive(false);
        Time.timeScale = 1f; // Reinicia el tiempo del juego
        viditaSO.Sanar(viditaSO.MaxHealth);
    }


    public void MenuInicial(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }

    public void Salir()
    {
        Application.Quit();
        
    }
}




   
 
