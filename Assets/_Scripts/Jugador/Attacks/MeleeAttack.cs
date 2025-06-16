using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private Transform AttackController;
    [SerializeField] private float attackRange = 1.0f;
    [SerializeField] private float damage;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) // Left mouse button
        {
            Hit();
        }
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackController.position, attackRange);
    }
}
