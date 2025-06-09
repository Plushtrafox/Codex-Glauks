using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
[CreateAssetMenu(fileName = "HealthSO", menuName = "ScriptableObjects/HealthSO", order = 1)]
public class HealthSO : ScriptableObject
{
    private int health = 100;
    private int maxHealth = 100;

    void Start()
    {
        health = maxHealth; // Initialize health to 100 at the start
    }
    void Damage(int quantity)
    {
        health -= quantity;
       
    }
}
