using System;
using UnityEngine;
public enum GamePhase
{
    Phase0,
    Phase1,
    Phase2,
    Phase3,
}
public class GameManager : MonoBehaviour
{
    
    // Singleton pattern para acceso fácil
    public static GameManager Instance { get; private set; }
    public static Action<GamePhase> OnPhaseChanged { get; internal set; }

    // Enum para definir las fases del juego
    

    [SerializeField] private GamePhase currentPhase = GamePhase.Phase1;
    public GamePhase CurrentPhase => currentPhase;
    private void Awake()
    {
        // Implementación del singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        currentPhase = GamePhase.Phase1; // Inicializar la fase actual
        EnterNewPhase(currentPhase); // Entrar en la fase inicial
        OnPhaseChanged?.Invoke(currentPhase); // Notificar el cambio de fase inicial
    }
    public GamePhase GetCurrentPhase()
    {
        return currentPhase;
    }

    public void ChangePhase(GamePhase newPhase)
    {
        // Salir de la fase actual
        ExitCurrentPhase();

        // Cambiar a la nueva fase
        currentPhase = newPhase;

        // Entrar en la nueva fase
        EnterNewPhase(newPhase);
        OnPhaseChanged?.Invoke(newPhase);
    }

    private void ExitCurrentPhase()
    {
        switch (currentPhase)
        {
            case GamePhase.Phase0:
                // Lógica para salir de la fase 0, si es necesario
                break;
            case GamePhase.Phase1:
                
                break;
            case GamePhase.Phase2:
                
                break;
            case GamePhase.Phase3:
                break;
        }
    }

    public void EnterNewPhase(GamePhase newPhase)
    {
        switch (newPhase)
        {
            case GamePhase.Phase0:
                currentPhase = GamePhase.Phase0;
                break;
            case GamePhase.Phase1:
                currentPhase = GamePhase.Phase1;
                break;
            case GamePhase.Phase2:
                currentPhase = GamePhase.Phase2;
                break;
            case GamePhase.Phase3:
                currentPhase = GamePhase.Phase3;


                break;
                // ... otros casos
        }

    }
}
