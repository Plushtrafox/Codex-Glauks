using UnityEngine;
using UnityEngine.AI;
public class enemyMove : MonoBehaviour
{
    [SerializeField] private Transform focus;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private HealthSO healthCharacter;
    [SerializeField] private float attackRange=1f;
    private bool isAttacking= false;
    [SerializeField] private float cooldown=1f; 
    [SerializeField] private int attackcount=20;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (focus != null)
        {
            if (Vector3.Distance(gameObject.transform.position, focus.position) > attackRange)
            {
                agent.isStopped = false;
                agent.SetDestination(focus.position);
            }
            else
            {
                agent.isStopped = true;
                if (!isAttacking)
                {
                    InvokeRepeating("Attack",0, cooldown);
                    isAttacking = true;
                }
            }
            
        }
        else
        {
            agent.isStopped = true;
        }
    }
    void Attack()
    {
        healthCharacter.Damage(attackcount);
    }
}
