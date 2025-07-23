using UnityEngine;
using Utility.GameFlow;


public enum TipoLlamadaAudio
{
    unica,
    repetitiva
}
public class AudioCaller : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;

    [SerializeField] private float tiempoEntreLLamadas = 1f;

    [SerializeField] private TipoLlamadaAudio tipoLlamadaAudio=TipoLlamadaAudio.unica;

    [SerializeField] private string nombreCodigoAudio;


    private float tiempoUltimaLlamada = 0f;


    private void Start()
    {
        //audioManager.PlayClipOneShot();


    }




}
