using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
[CreateAssetMenu(fileName = "HealthSO", menuName = "ScriptableObjects/HealthSO", order = 1)]
public class HealthSO : ScriptableObject
{
    public delegate void tipoEventoPerdida();
    public event tipoEventoPerdida EventoPerdida;
<<<<<<< HEAD
    public int health = 100;
=======
    [SerializeField]private int health = 100;
>>>>>>> main
    private int maxHealth = 100;

    public int MaxHealth { get { return maxHealth; } set { maxHealth = value; }}

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
        
        if (health < 0)
        {
            EventoPerdida?.Invoke();
            Debug.Log("estamos sin vida");
        }
       
    }
}
