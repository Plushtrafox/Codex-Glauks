using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "ManagerScriptableObject", menuName = "ScriptableObjects/ManagerScriptableObject", order = 1)]
public class ManagerScriptableObject : ScriptableObject
{
    [SerializeField] private List<bool> pilarScriptableObject = new List<bool>();
    public List<bool> PilarScriptableObject
    {
        get { return pilarScriptableObject; }
        private set { pilarScriptableObject = value; }
    }
    public delegate void tipoEventoPilar();

    public event tipoEventoPilar EventoPilar;
    public event tipoEventoPilar EventoIniciarAcertijo;

    


    public void ActualizarValores(bool NuevoValor, bool ViejoValor)
    {
        PilarScriptableObject.Add(NuevoValor);
        PilarScriptableObject.Remove(ViejoValor);
        if (!PilarScriptableObject.Contains(false))
        {
            Debug.Log("se resolvio el acertijo");
            EventoPilar?.Invoke();
        }
    }
   public void AddPilar()
    {
        PilarScriptableObject.Add(false);
    }
    
}
