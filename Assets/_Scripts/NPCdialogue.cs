using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class NPCdialogue : MonoBehaviour
{
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;

    private float typingTime= 0.05f;

    [SerializeField,TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel; // Panel de di�logo que se mostrar� al jugador
    [SerializeField] private TMP_Text dialogueText; // Texto del di�logo que se mostrar� en el panel


    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E)) // Cambia la tecla seg�n tu preferencia
        {
            if (!didDialogueStart) // Si el di�logo no ha comenzado, inicia el di�logo
                StartDialogue();
            else if (dialogueText.text == dialogueLines[lineIndex]) // Cambia la tecla seg�n tu preferencia
            {
                nextDialogueLine(); // Avanza a la siguiente l�nea del di�logo
            }
            else
            {
                StopAllCoroutines(); // Detiene todas las corrutinas si no se est� mostrando el di�logo
                dialogueText.text = dialogueLines[lineIndex]; // Muestra la l�nea actual del di�logo sin efecto de escritura
            }
        }
        
    }
    private void StartDialogue()
    {
        didDialogueStart = true; // Marca que el di�logo ha comenzado
        dialoguePanel.SetActive(true); // Activa el panel de di�logo
        dialogueMark.SetActive(false); // Desactiva el marcador de di�logo
        lineIndex = 0; // Reinicia el �ndice de la l�nea del di�logo
        Time.timeScale = 0f;
        StartCoroutine (ShowLine()); // Inicia la corrutina para mostrar la primera l�nea del di�logo
    }
    private void nextDialogueLine()
    {
        lineIndex++; // Incrementa el �ndice de la l�nea del di�logo
        if (lineIndex < dialogueLines.Length) // Verifica si hay m�s l�neas de di�logo
        {
            StartCoroutine(ShowLine()); // Inicia la corrutina para mostrar la siguiente l�nea del di�logo
        }
        else
        {
            didDialogueStart= false; // Si no hay m�s l�neas, finaliza el di�logo
            dialoguePanel.SetActive(false);
            dialogueMark.SetActive(true); // Reactiva el marcador de di�logo
            Time.timeScale = 1f;
        }
    }
    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty; // Limpia el texto del di�logo antes de mostrar una nueva l�nea

        foreach(char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch; // Agrega cada car�cter uno por uno para un efecto de escritura
            yield return new WaitForSecondsRealtime(typingTime); // Ajusta la velocidad del efecto de escritura
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            dialogueMark.SetActive(true); // Activa el marcador de di�logo cuando el jugador entra en rango
            // Aqu� puedes iniciar el di�logo o mostrar un mensaje al jugador

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogueMark.SetActive(false); // Desactiva el marcador de di�logo cuando el jugador sale del rango
            // Aqu� puedes finalizar el di�logo o ocultar el mensaje al jugador
        }

    }
}
