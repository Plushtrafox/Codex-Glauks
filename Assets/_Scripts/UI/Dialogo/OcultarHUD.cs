using System;
using System.Collections;
using UnityEngine;

public class OcultarHUD : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private DialogoManager dialogoManager;
    [SerializeField] private GameObject hud; // Referencia al HUD que se desea ocultar
    [SerializeField] private GameObject notificacionPuertaBloqueada; // Referencia al HUD que se desea ocultar


    public Action EventoPuertaBloqueada; // Evento para notificar que la puerta está bloqueada
    private void OnEnable()
    {
        dialogoManager.OcultarHUD += HideHUD;
        dialogoManager.MostrarHUD += MostrarHUD; // Suscribirse al evento para mostrar el HUD
        EventoPuertaBloqueada += ActivarNotificacionPuertaBloqueada; // Suscribirse al evento de puerta bloqueada
    }
    private void OnDisable()
    {
        dialogoManager.OcultarHUD -= HideHUD;
        dialogoManager.MostrarHUD -= MostrarHUD; // Desuscribirse del evento para evitar fugas de memoria
        EventoPuertaBloqueada -= ActivarNotificacionPuertaBloqueada; // Desuscribirse del evento de puerta bloqueada
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

    private void ActivarNotificacionPuertaBloqueada()
    {
        StartCoroutine(CorrutinaNotificacionPuertaBloqueada());
    }

    IEnumerator CorrutinaNotificacionPuertaBloqueada()
    {
        notificacionPuertaBloqueada.SetActive(true); // Activa la notificación de puerta bloqueada
        yield return new WaitForSeconds(2f); // Espera 2 segundos
        notificacionPuertaBloqueada.SetActive(false); // Desactiva la notificación de puerta bloqueada

        yield return null;
    }

}
