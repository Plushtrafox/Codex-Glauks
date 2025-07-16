using System;
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

    public Action EventoShakeCamaraGolpe;


    [Header("parametros HITSTOP")]
    [SerializeField] private float tiempoDeHitstop=0.1f;
    private bool estaEnHitstop = false;
    public Action EventoHitStop;




    private void Awake()
    {
        EventoHitStop += HitStop;
        EventoShakeCamaraGolpe += ShakeCameraGolpe;
    }
    private void OnDisable()
    {
        EventoHitStop -= HitStop;
        EventoShakeCamaraGolpe -= ShakeCameraGolpe;
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



    private void HitStop()
    {
        if (!estaEnHitstop)
        {
            estaEnHitstop = true;
            StartCoroutine(HitStopCorrutina());

        }
    }

    IEnumerator HitStopCorrutina()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(tiempoDeHitstop);
        Time.timeScale = 1;
        estaEnHitstop = false;
        yield return null;
    }
}
