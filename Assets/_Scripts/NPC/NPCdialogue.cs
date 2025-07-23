using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.UI;

public class NPCdialogue : MonoBehaviour
{
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;

    private float typingTime= 0.05f;

    [SerializeField,TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel; // Panel de diálogo que se mostrará al jugador
    [SerializeField] private TMP_Text dialogueText; // Texto del diálogo que se mostrará en el panel


    [Header("update NPC UI")]
    [SerializeField] private Sprite imagenNPCSprite; // Imagen del NPC que se mostrará en el panel de diálogo
    [SerializeField] private Image imagenNPCUI;
    [SerializeField] private string nombreNPC;
    [SerializeField] private TMP_Text nombreNPCUI;

    [SerializeField] private DialogoManager dialogoManager;


    [Header("Interactuar UI")]
    [SerializeField] private MostrarUISO mostrarUISO; // ScriptableObject para manejar la visibilidad del HUD

    [Header("referencia input system")]
    [SerializeField] private ControllesSOSCript ControllesSOSCript; // ScriptableObject para manejar los controles del jugador


    private void Awake()
    {
        ControllesSOSCript.EventoInteractuar += OnInteractuar; // Suscribe al evento de interacción del jugador
    }

    private void OnInteractuar()
    {
        if (isPlayerInRange) // Cambia la tecla según tu preferencia
        {
            if (!didDialogueStart) // Si el diálogo no ha comenzado, inicia el diálogo
            { 
            StartDialogue();
            mostrarUISO.EventoOcultarUI?.Invoke(); // Oculta la UI de interacción si se está utilizando un sistema de HUD
            }
        else if (dialogueText.text == dialogueLines[lineIndex]) // Cambia la tecla según tu preferencia
        {
            nextDialogueLine(); // Avanza a la siguiente línea del diálogo
        }
        else
        {
            StopAllCoroutines(); // Detiene todas las corrutinas si no se está mostrando el diálogo
            dialogueText.text = dialogueLines[lineIndex]; // Muestra la línea actual del diálogo sin efecto de escritura
        }
        }
    }




    private void OnDisable()
    {
        ControllesSOSCript.EventoInteractuar -= OnInteractuar; // Suscribe al evento de interacción del jugador

    }
    private void StartDialogue()
    {
        dialogoManager.OcultarHUD?.Invoke(); // Oculta el HUD si se está utilizando un sistema de HUD

        didDialogueStart = true; // Marca que el diálogo ha comenzado
        dialoguePanel.SetActive(true); // Activa el panel de diálogo
        dialogueMark.SetActive(false); // Desactiva el marcador de diálogo

        imagenNPCUI.sprite = imagenNPCSprite; // Actualiza la imagen del NPC en el panel de diálogo
        nombreNPCUI.text = nombreNPC; // Actualiza el nombre del NPC en el panel de diálogo

        lineIndex = 0; // Reinicia el índice de la línea del diálogo
        Time.timeScale = 0f;
        StartCoroutine (ShowLine()); // Inicia la corrutina para mostrar la primera línea del diálogo
    }
    private void nextDialogueLine()
    {
        lineIndex++; // Incrementa el índice de la línea del diálogo
        if (lineIndex < dialogueLines.Length) // Verifica si hay más líneas de diálogo
        {
            StartCoroutine(ShowLine()); // Inicia la corrutina para mostrar la siguiente línea del diálogo
        }
        else
        {
            didDialogueStart= false; // Si no hay más líneas, finaliza el diálogo
            dialoguePanel.SetActive(false);
            dialogueMark.SetActive(true); // Reactiva el marcador de diálogo
            Time.timeScale = 1f;
            mostrarUISO.EventoMostrarUI?.Invoke();
            dialogoManager.MostrarHUD?.Invoke(); // Muestra el HUD si se está utilizando un sistema de HUD
        }
    }
    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty; // Limpia el texto del diálogo antes de mostrar una nueva línea

        foreach(char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch; // Agrega cada carácter uno por uno para un efecto de escritura
            yield return new WaitForSecondsRealtime(typingTime); // Ajusta la velocidad del efecto de escritura
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {

            isPlayerInRange = true;
            dialogueMark.SetActive(true); // Activa el marcador de diálogo cuando el jugador entra en rango
            mostrarUISO.EventoMostrarUI?.Invoke(); // Muestra la UI de interacción si se está utilizando un sistema de HUD
            // Aquí puedes iniciar el diálogo o mostrar un mensaje al jugador

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogueMark.SetActive(false); // Desactiva el marcador de diálogo cuando el jugador sale del rango
            mostrarUISO.EventoOcultarUI?.Invoke();
            // Aquí puedes finalizar el diálogo o ocultar el mensaje al jugador
        }

    }
}
