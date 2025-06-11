using UnityEngine;

public class FinalPilar: MonoBehaviour
{
    [SerializeField] private ManagerScriptableObject Manager;

    private void EventoCompletado()
    {
        transform.Translate(Vector3.down * 10f, Space.World);

    }
    private void Awake()
    {
        Manager.EventoPilar += EventoCompletado;
    }
    private void OnDisable()
    {
        Manager.EventoPilar -= EventoCompletado;
    }
}
