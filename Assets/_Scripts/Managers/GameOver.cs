using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    [SerializeField] private HealthSO viditaSO;
    [SerializeField] private GameObject menuPerdida;



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
        menuPerdida.SetActive(true);
        Time.timeScale = 0f; // Pausa el juego

    }


    public void reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f; // Reinicia el tiempo del juego
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




   
 
