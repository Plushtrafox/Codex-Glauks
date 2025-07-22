using UnityEngine;

public class JugadorMostrarUI : MonoBehaviour
{
    [SerializeField] private MostrarUISO mostrarUISO;
    [SerializeField] private GameObject uiPanel;
    private void OnEnable()
    {
        if (mostrarUISO != null)
        {
            mostrarUISO.EventoMostrarUI += MostrarUI;
            mostrarUISO.EventoOcultarUI += OcultarUI;
        }
    }
    private void OnDisable()
    {
        if (mostrarUISO != null)
        {
            mostrarUISO.EventoMostrarUI -= MostrarUI;
            mostrarUISO.EventoOcultarUI -= OcultarUI;
        }
    }
    private void MostrarUI()
    {
        uiPanel.SetActive(true);
    }
    private void OcultarUI()
    {
        uiPanel.SetActive(false);
    }
}
