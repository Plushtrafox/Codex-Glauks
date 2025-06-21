using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    [SerializeField] private HealthSO viditaSO;

    [SerializeField] private GameObject menuDePerdida;




    void Awake()
    {
        viditaSO.EventoPerdida+= MostrarMenuPerdida;
    }
    void OnDisable()
    {
        viditaSO.EventoPerdida-= MostrarMenuPerdida;
    }


private void MostrarMenuPerdida(){
        Time.timeScale = 0;
        menuDePerdida.SetActive(true);
        print("estamos en la funcion");
}

    public void reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        menuDePerdida.SetActive(false);
        Time.timeScale = 1;
        viditaSO.health = 100;

    }


    public void MenuInicial(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }

    public void Salir()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
        
    }
}




   
 
