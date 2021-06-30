using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2;
    public int priority = 0;    // что бы на месте не прыгали кто то должен быть приорететнее

    NavMeshAgent agent;

    private void OnEnable()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.avoidancePriority = priority;

        //first point
        agent.SetDestination(GetRandomPoint.Get());
    }

    void Update()
    {
        if (agent.remainingDistance <= 0.15f)
        {
            agent.SetDestination(GetRandomPoint.Get());
        }
    }



}
