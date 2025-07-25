using UnityEngine;

public class ManagerMenuConfiguracion : MonoBehaviour
{
    [SerializeField]private GameObject menuConfiguracion;
   public void CerrarMenu()
    {
        print("Deberia cerrrar el menu");
        menuConfiguracion.SetActive (false);
    }
}
