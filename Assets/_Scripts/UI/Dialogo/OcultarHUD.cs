using UnityEngine;

public class OcultarHUD : MonoBehaviour
{
    [SerializeField] private DialogoManager dialogoManager;
    [SerializeField] private GameObject hud; // Referencia al HUD que se desea ocultar
    private void OnEnable()
    {
        dialogoManager.OcultarHUD += HideHUD;
        dialogoManager.MostrarHUD += MostrarHUD; // Suscribirse al evento para mostrar el HUD
    }
    private void OnDisable()
    {
        dialogoManager.OcultarHUD -= HideHUD;
        dialogoManager.MostrarHUD -= MostrarHUD; // Desuscribirse del evento para evitar fugas de memoria
    }
    private void HideHUD()
    {
        if (hud != null)
        {
            hud.SetActive(false); // Oculta el HUD
        }
    }

    private void MostrarHUD()
    {
        if (hud != null)
        {
            hud.SetActive(true); // Muestra el HUD
        }
    }

}
