using UnityEngine;
using System;
using Utility.Audio;
using Utility.GameFlow;
public class Phase2 : MonoBehaviour
{
    [SerializeField] private Transform posicionJugador;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioClipBank audioClipBank;

    private void OnTriggerExit(Collider other)
    {
        print("entro al trigger");
        if (posicionJugador.position.z > transform.position.z)
        {
            if (gameManager.CurrentPhase == GamePhase.Phase2)
            {
                return;
            }
            gameManager.ChangePhase(GamePhase.Phase2);
            return;
        }
        else if (posicionJugador.position.z < transform.position.z)
        {
            if (gameManager.CurrentPhase == GamePhase.Phase1)
            {
                return;
            }
            gameManager.ChangePhase(GamePhase.Phase1);
            return;
        }
        /*if (gameManager.CurrentPhase == GamePhase.Phase1)
        {
            gameManager.EnterNewPhase(GamePhase.Phase2);
            return;
        }
        else if (gameManager.CurrentPhase == GamePhase.Phase2)
        {
            gameManager.EnterNewPhase(GamePhase.Phase1);
            return;
        }*/
    }
    /*private void OnTriggerEnter(Collider other)
    {
        if (gameManager.CurrentPhase == GamePhase.Phase0)
        {
            gameManager.ChangePhase(GamePhase.Phase1);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (gameManager.CurrentPhase == GamePhase.Phase1)
        {
            gameManager.ChangePhase(GamePhase.Phase0);
        }
        
    }*/

}
