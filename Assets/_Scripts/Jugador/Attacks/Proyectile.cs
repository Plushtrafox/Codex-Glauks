using Unity.VisualScripting;
using UnityEngine;

public class Rotacion : MonoBehaviour
{
    [SerializeField] private Transform AttackController;
    [SerializeField] private float attackRange = 1.0f;
    [SerializeField] private float damage;
    [SerializeField] private float lifetime = 5f; // Duración del proyectil en segundos
    [SerializeField] private GameObject projectilePrefab; // Prefab del proyectil


    [Header("Movimiento orbital")]
    public float frecuencia = 1f;  // Vueltas por segundo
    public float amplitud = 1f;    // Radio de la órbita
    [SerializeField] private Transform ejeTransform; // Transform del eje de rotación


    private Vector3 planoX;
    private Vector3 planoY;


    //accesors 
    public Transform EjeTransform { get { return ejeTransform; } set { ejeTransform = value; } }

    void Start()
    {
        if (ejeTransform != null)
        {
            // Calcula dos vectores ortogonales al eje de rotación
            planoX = Vector3.Cross(ejeTransform.up, Vector3.right);
            if (planoX == Vector3.zero)
                planoX = Vector3.Cross(ejeTransform.up, Vector3.forward);
            planoX.Normalize();
            planoY = Vector3.Cross(ejeTransform.up, planoX).normalized;
        }
    }

    void Update()
    {
        if (ejeTransform == null) return;

        float angulo = Time.time * frecuencia * 2f * Mathf.PI; // Radianes
        Vector3 orbita = Mathf.Cos(angulo) * planoX + Mathf.Sin(angulo) * planoY;
        transform.position = ejeTransform.position + orbita * amplitud;

        Destroy(gameObject, lifetime); // Destruye el proyectil después de su vida útil
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Hit();
        print("Hit detected: " + other.name);
    }

    private void Hit()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(AttackController.position, attackRange);
        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(AttackController.position, attackRange);
    }
}


