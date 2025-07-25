using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerFinalEscenaDungeon : MonoBehaviour
{
    private bool isPlayerInRange;
    private bool notificacionIniciada;
    private bool _puertaEstaAbierta=false; // Indica si la puerta está abierta o no

    [SerializeField] private MostrarUISO mostrarUISO; // ScriptableObject para manejar la visibilidad del HUD
    [SerializeField] private ControllesSOSCript controllesSOSCript; // ScriptableObject para manejar los controles del jugador
    [SerializeField] private OcultarHUD ocultarHUD; // ScriptableObject para manejar la ocultación del HUD
    [SerializeField] private ManagerScriptableObject manager; // ScriptableObject para manejar el final de la escena
    private Material materialInstancia;
    [SerializeField] private Renderer rendererPuerta; // Material que se aplicará a la puerta














    private void Awake()
    {
        manager.PilarScriptableObject.Clear(); // Asegúrate de que la lista esté vacía al iniciar

        controllesSOSCript.EventoInteractuar += InteractuarPuerta; // Suscribe al evento de interacción del jugador
        manager.EventoPilar += AbrirPuerta;
    }
    private void Start()
    {

        // Crea una instancia única del material al iniciar
        materialInstancia = new Material(rendererPuerta.material);
        rendererPuerta.material = materialInstancia;
        CambiarColorSeguro(new Color(0.3f,0.3f,0.3f));
    }
    private void OnDisable()
    {
        controllesSOSCript.EventoInteractuar -= InteractuarPuerta; // Asegúrate de desuscribirte para evitar fugas de memoria
        manager.EventoPilar -= AbrirPuerta;

    }

    private void AbrirPuerta()
    {
        _puertaEstaAbierta = true; // Marca que la puerta está abierta
        CambiarColorSeguro(Color.white); // Cambia el color del material a verde para indicar que la puerta está abierta

    }

    public void CambiarColorSeguro(Color color)
    {
        if (materialInstancia != null)
            materialInstancia.color = color;
    }

    private void InteractuarPuerta()
    {
        if (_puertaEstaAbierta) // Si la puerta está abierta, no hace nada
        {
            SceneManager.LoadScene("Forest1"); // Carga la escena final del dungeon
        }

        if (!isPlayerInRange || notificacionIniciada || _puertaEstaAbierta) return; // Si el jugador no está en rango, no hace nada
        mostrarUISO.EventoOcultarUI?.Invoke(); // Oculta la UI de interacción si se está utilizando un sistema de HUD
        ocultarHUD.EventoPuertaBloqueada?.Invoke(); // Notifica que la puerta está bloqueada
        StartCoroutine(ReactivarNotificacionPuertaBloqueada()); // Inicia la corrutina para mostrar la notificación de puerta bloqueada
    }

    IEnumerator ReactivarNotificacionPuertaBloqueada()
    {
        notificacionIniciada = true; // Marca que la notificación ha sido iniciada
        yield return new WaitForSeconds(2f); // Espera 2 segundos antes de permitir otra interacción
        notificacionIniciada=false; // Permite que la notificación se pueda iniciar nuevamente
        yield return null; // Espera un frame antes de continuar
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {

            isPlayerInRange = true;
            mostrarUISO.EventoMostrarUI?.Invoke(); // Muestra la UI de interacción si se está utilizando un sistema de HUD
            // Aquí puedes iniciar el diálogo o mostrar un mensaje al jugador

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            mostrarUISO.EventoOcultarUI?.Invoke();
            // Aquí puedes finalizar el diálogo o ocultar el mensaje al jugador
        }

    }
}
