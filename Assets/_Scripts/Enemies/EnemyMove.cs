using UnityEngine;
using UnityEngine.AI;

public class enemyMove : MonoBehaviour
{
    [SerializeField] private Transform focus;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private HealthSO healthSO;
    [SerializeField] private float attackRange;

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
            agent.isStopped = false;
            agent.SetDestination(focus.position);
            
        }
        else
        {
            agent.isStopped = true;
        }
    }
    void Attack()
    {
        if ()
        {
            
        }
    }
}
