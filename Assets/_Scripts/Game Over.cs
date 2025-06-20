using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    [SerializeField] private HealthSO viditaSO;




    void Awake()
    {
        viditaSO.EventoPerdida+= MostrarMenuPerdida;
    }
    void OnDisable()
    {
        viditaSO.EventoPerdida-= MostrarMenuPerdida;
    }


private void MostrarMenuPerdida(){
    
}


    public void reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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




   
 
