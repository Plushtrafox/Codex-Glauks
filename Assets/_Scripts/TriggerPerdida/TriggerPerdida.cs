using UnityEngine;

public class TriggerPerdida : MonoBehaviour
{
    [SerializeField] private HealthSO vidaSO;

    private void OnTriggerEnter(Collider other)
    {
        vidaSO.EventoPerdida?.Invoke();
    }
}
