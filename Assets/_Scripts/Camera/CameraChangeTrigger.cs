using System.Collections;
using UnityEngine;

public class CameraChangeTrigger : MonoBehaviour
{

    [SerializeField] private CameraManager cameraManager; // Referencia al CameraManager
    [SerializeField] private tipoCamara camaraObjetivo; // Tipo de c�mara que se activar� al entrar en el trigger
    [SerializeField] private Transform ubicacionJugador; // Referencia al jugador

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(CalculoCambioCamaraCorrutina()); // Inicia la corrutina para calcular el cambio de c�mara

    }

    IEnumerator CalculoCambioCamaraCorrutina()
    {
        yield return new WaitForSeconds(0.5f);
        if (ubicacionJugador.position.z > transform.position.z)
        {
            cameraManager.EventoCambioCamara?.Invoke(camaraObjetivo); // Invoca el evento de cambio de c�mara
        }
        else if (ubicacionJugador.position.z < transform.position.z)
        {
            cameraManager.EventoCambioCamara?.Invoke(camaraObjetivo==tipoCamara.Normal?tipoCamara.Escaleras:tipoCamara.Normal); // Invoca el evento de cambio de c�mara
        }

        yield return null;
    }


}
