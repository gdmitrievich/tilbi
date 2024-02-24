using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _range;

    void Update()
    {
        if(_agent.remainingDistance <= _agent.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(transform.position, _range, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                _agent.SetDestination(point);
            }
        }
    }
    bool RandomPoint(Vector3 center, float _range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * _range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}