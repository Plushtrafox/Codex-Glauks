using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public enum tipoCamara
{
    Normal,
    Escaleras
}
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


    [Header("Cinemachine")]

    [SerializeField] private CinemachineCamera camaraNormal; // Referencia a la cámara normal
    [SerializeField] private CinemachineCamera camaraEscaleras; // Referencia a la cámara de escaleras
    private tipoCamara camaraActual = tipoCamara.Normal; // Variable para almacenar el tipo de cámara actual

    public Action<tipoCamara> EventoCambioCamara;// Evento para notificar el cambio de cámara, esto sera usado dentro de los triggers que son box colliders en el escenario



    private void Awake()
    {
        EventoCambioCamara += CambioCamara; // Suscribirse al evento de cambio de cámara
        EventoHitStop += HitStop;
        EventoShakeCamaraGolpe += ShakeCameraGolpe;
    }
    private void OnDisable()
    {
        EventoCambioCamara -= CambioCamara; // Desuscribirse del evento de cambio de cámara
        EventoHitStop -= HitStop;
        EventoShakeCamaraGolpe -= ShakeCameraGolpe;
    }


    private void CambioCamara(tipoCamara camaraNueva)
    {
        camaraActual = camaraNueva;

        if (camaraActual == tipoCamara.Normal)
        {
            camaraNormal.Priority.Value = 10; // Usa .Value en Cinemachine 3
            camaraEscaleras.Priority.Value = 0;
        }
        else if(camaraActual==tipoCamara.Escaleras)
        {
            camaraNormal.Priority.Value = 0;
            camaraEscaleras.Priority.Value = 10; // Usa .Value en Cinemachine 3
        }
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
