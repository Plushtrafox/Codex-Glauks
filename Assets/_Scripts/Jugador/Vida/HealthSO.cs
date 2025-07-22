using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
[CreateAssetMenu(fileName = "HealthSO", menuName = "ScriptableObjects/HealthSO", order = 1)]
public class HealthSO : ScriptableObject
{
    public delegate void tipoEventoPerdida();
    public event tipoEventoPerdida EventoPerdida;

    public delegate void tipoEventoActualizarBarraUI();
    public event tipoEventoActualizarBarraUI EventoActualizarBarraUI;

    [SerializeField]private int health = 100;
    private int maxHealth = 100;

    public int MaxHealth { get { return maxHealth; } set { maxHealth = value; }}
    public int Health
    {
        get { return health; }

    }

    public void Sanar(int cantidad)
    {
        health += cantidad;

    }

    private void OnEnable()
    {
        health = maxHealth; // Initialize health to 100 at the start
    }
    public void Damage(int quantity)
    {

        health -= quantity;
        EventoActualizarBarraUI?.Invoke(); // Notify that the health bar should be updated

        if (health < 0)
        {
            EventoPerdida?.Invoke();
        }

       
    }
}
