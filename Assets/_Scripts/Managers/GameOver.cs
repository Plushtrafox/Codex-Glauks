using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    [SerializeField] private HealthSO viditaSO;
<<<<<<< HEAD:Assets/_Scripts/Game Over.cs

    [SerializeField] private GameObject menuDePerdida;

=======
    [SerializeField] private GameObject menuPerdida;
>>>>>>> main:Assets/_Scripts/Managers/GameOver.cs



    void Awake()
    {
        viditaSO.EventoPerdida+= MostrarMenuPerdida;
    }
    void OnDisable()
    {
        viditaSO.EventoPerdida-= MostrarMenuPerdida;
    }


<<<<<<< HEAD:Assets/_Scripts/Game Over.cs
private void MostrarMenuPerdida(){
        Time.timeScale = 0;
        menuDePerdida.SetActive(true);
        print("estamos en la funcion");
}
=======
    private void MostrarMenuPerdida()
    {
        menuPerdida.SetActive(true);
        Time.timeScale = 0f; // Pausa el juego

    }
>>>>>>> main:Assets/_Scripts/Managers/GameOver.cs

    public void reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
<<<<<<< HEAD:Assets/_Scripts/Game Over.cs
        menuDePerdida.SetActive(false);
        Time.timeScale = 1;
        viditaSO.health = 100;

=======
        Time.timeScale = 1f; // Reinicia el tiempo del juego
        viditaSO.Sanar(viditaSO.MaxHealth);
>>>>>>> main:Assets/_Scripts/Managers/GameOver.cs
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




   
 
