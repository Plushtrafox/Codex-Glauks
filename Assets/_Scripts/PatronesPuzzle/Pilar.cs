using System;
using System.Collections;
using UnityEngine;

public class Pilar : MonoBehaviour
{
    private float tiempoActual = 0f; // Tiempo actual del giro
    [SerializeField] private float TiempoGiro = 2f; // Duración del giro en segundos, ajusta según sea necesario
    Quaternion rotacionInicial;
    Quaternion rotacionFinal;
    public bool girando = false;
    [SerializeField] private ControllesSOSCript Controles;
    [SerializeField] private float distanciaInteraccion = 2.1f; // Distancia máxima para interactuar con el pilar
    [SerializeField] private Transform jugadorTransform; // Referencia al transform del jugador para calcular la distancia
    [SerializeField] private float distanciaActual;
    [SerializeField] private int ladoCorrecto = 4;
    [SerializeField] private int ladoActual = 1; // Lado actual del pilar, 0 para izquierda, 1 para derecha, etc.
    //[SerializeField] private Light luzPilar; // Referencia a la luz del pilar, si es necesario para efectos visuales
    [SerializeField] private ManagerScriptableObject Manager;
    
    //tenemos 4 lados 1,2,3,4
    private void Awake()
    {
        Controles.EventoInteractuar += Girar;
    }
    private void OnDisable()
    {
        Controles.EventoInteractuar -= Girar; // Asegúrate de desuscribirte para evitar fugas de memoria
    }
    private IEnumerator GirarCoroutine()
    {
        
        //tiempoActual += Time.deltaTime;
        while (tiempoActual < TiempoGiro)
        {
            //print("transform" + transform.rotation.y);
            //print("rotacionfinal" + rotacionFinal.y);
            
            tiempoActual += Time.deltaTime;
            float porcentaje = tiempoActual / TiempoGiro;
            transform.localRotation = Quaternion.Slerp(rotacionInicial, rotacionFinal, porcentaje);
            //print(porcentaje);
            yield return null; // Espera un frame antes de continuar
           
        }
        if (tiempoActual >= TiempoGiro)
        {      
            if (ladoActual == 1 || ladoActual == 2 || ladoActual == 3 || ladoActual == 4) // Asegúrate de que el lado actual sea válido
            {    
                ladoActual++; // Incrementa el lado actual
                if (ladoActual > 4) // Si supera el máximo, reinicia a 1
                {                   
                    ladoActual = 1;
                }
            }if (ladoActual == ladoCorrecto) // Verifica si el lado actual es el correcto
            {
                //luzPilar.intensity = 4f;
                Manager.ActualizarValores(true, false); // Actualiza el estado del pilar en el manager
                // Aquí puedes agregar la lógica que deseas ejecutar cuando se alcanza el lado correcto
            }
            else
            {
                //luzPilar.intensity = 0f; // Ajusta la intensidad de la luz si no es el lado correcto
                if ((ladoActual == ladoCorrecto +1)||(ladoActual == 1 && ladoCorrecto == 4 ))
                {
                    Manager.ActualizarValores(false, true); // Actualiza el estado del pilar en el manager
                }
            }
            transform.localRotation = rotacionFinal; // Asegura que la rotación final se aplique
            girando = false; // Si ya está en la rotación final, no hace nada
            tiempoActual = 0f; // Reinicia el tiempo actual para futuros giros
            yield break;

        }
        yield break;
        }

    private void Girar()
    {
        distanciaActual = Vector3.Distance(jugadorTransform.position, transform.position);
        if (girando||distanciaActual>=distanciaInteraccion)  return; // Si ya está girando, no hace nada
        rotacionInicial = transform.localRotation; // Guarda la rotación inicial
        rotacionFinal = Quaternion.Euler(0,90, 0) * transform.localRotation;
        StartCoroutine("GirarCoroutine"); // Cambia la duración según sea necesario
        girando = true;
        
        //print("rotacionfinal" + rotacionFinal.y);
        //print("rotacioninicial" + rotacionInicial.y);
    }
    private void Start()
    {
        Manager.AddPilar(); // Añade un nuevo pilar al manager al iniciar
    }

}

