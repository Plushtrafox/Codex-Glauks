using UnityEngine;

public class ManagerMenuConfiguracion : MonoBehaviour
{
    [SerializeField]private GameObject menuConfiguracion;
   public void CerrarMenu()
    {
        menuConfiguracion.SetActive (false);
    }
}
