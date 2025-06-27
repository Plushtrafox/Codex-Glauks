using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineBasicMultiChannelPerlin camaraShake;

    [SerializeField] private float duracionEfectoShakeGolpe = 0.5f;

    [SerializeField] private float intensidadEfectoShakeGolpe = 2f;

    [SerializeField] private float frecuenciaEfectoShakeGolpe = 2f;
    [SerializeField] private bool estaEnGolpe = false;

    [ SerializeField ] private MeleeAttack meleeAttack;




    private void Awake()
    {
        meleeAttack.EventoShakeCamaraGolpe += ShakeCameraGolpe;
    }
    private void OnDisable()
    {
        meleeAttack.EventoShakeCamaraGolpe -= ShakeCameraGolpe;
    }



    private void ShakeCameraGolpe()
    {
        if (! estaEnGolpe)
        {
            estaEnGolpe = true;
            StartCoroutine(GolpeShake());
        }


    }
    IEnumerator GolpeShake()
    {
        camaraShake.FrequencyGain = frecuenciaEfectoShakeGolpe;
        camaraShake.AmplitudeGain = intensidadEfectoShakeGolpe;
        yield return new WaitForSeconds(duracionEfectoShakeGolpe);
        camaraShake.FrequencyGain = 0f;
        camaraShake.AmplitudeGain = 0f;
        estaEnGolpe = false;
        yield return null;
    }
}
