using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "ManagerScriptableObject", menuName = "ScriptableObjects/ManagerScriptableObject", order = 1)]
public class ManagerScriptableObject : ScriptableObject
{
    [SerializeField] private List<bool> PilarScriptableObject = new List<bool>();
    public delegate void tipoEventoPilar();

    public event tipoEventoPilar EventoPilar;

    public void ActualizarValores(bool NuevoValor, bool ViejoValor)
    {
        PilarScriptableObject.Add(NuevoValor);
        PilarScriptableObject.Remove(ViejoValor);
        if (!PilarScriptableObject.Contains(false))
        {
            EventoPilar?.Invoke();
        }
    }
   public void AddPilar()
    {
        PilarScriptableObject.Add(false);
    }
    
}
