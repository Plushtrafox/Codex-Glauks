using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] public GameObject LongBullet; // Prefab de la bala
    public Transform SpawnPoint; // Punto de aparici�n de la bala

    [SerializeField] public float bulletSpeed = 20f; // Velocidad de la bala
    [SerializeField] public float shotRate = 0.5f; // Tiempo entre disparos

    private float nextShotTime = 0f; // Tiempo del pr�ximo disparo
    // Update is called once per frame

    void Update()
    {
        
    }

}
