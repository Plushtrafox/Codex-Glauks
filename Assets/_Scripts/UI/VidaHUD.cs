using UnityEngine;
using UnityEngine.UI;

public class VidaHUD : MonoBehaviour
{

    [SerializeField] private HealthSO vidaSO; // ScriptableObject que contiene la vida del jugador
    [SerializeField] private Slider barraVida; // Slider que representa la barra de vida en la UI

    private void Awake()
    {
        vidaSO.EventoActualizarBarraUI += ActualizarBarraVida; // Suscribe al evento de actualización de la barra de vida
    }
    private void OnDisable()
    {
        vidaSO.EventoActualizarBarraUI -= ActualizarBarraVida; // Desuscribe del evento de actualización de la barra de vida
    }

    private void Start()
    {
        ActualizarBarraVida(); // Actualiza la barra de vida al inicio
    }

    private void ActualizarBarraVida()
    {

        barraVida.value = vidaSO.Health; // Actualiza el valor del slider con la vida actual del jugador
    
    }
}
