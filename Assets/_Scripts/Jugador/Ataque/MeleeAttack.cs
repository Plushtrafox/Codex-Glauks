using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ControllesSOSCript controles;
    [SerializeField] private Animator animator;


    [Header("Melee Attack Settings")]
    [SerializeField] private Transform AttackController;
    [SerializeField] private float attackRange = 1.0f;
    [SerializeField] private float damage;


    private void Awake()
    {
        controles.EventoAtaque += Hit;
    }
    private void OnDisable()
    {
        controles.EventoAtaque -= Hit;
    }


    private void Hit()
    {
        animator.CrossFade("AtaqueLigero", 0.1f);
        Collider[] hitEnemies = Physics.OverlapSphere(AttackController.position, attackRange);
        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }

    private void CambioAnimacion()
    {
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackController.position, attackRange);
    }
}
