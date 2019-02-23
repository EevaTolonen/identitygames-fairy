using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    private Vector2 enemyPosition;
    private bool chasing = false;
    private bool patrolling = true;
    private float patrollingSpeed = 1f;
    private GameObject target;
    

    // Start is called before the first frame update
    void Awake()
    {
        /*agent = GetComponent("NavMeshAgent") as UnityEngine.AI.NavMeshAgent;
        agent.speed = patrollingSpeed;
        enemyPosition = this.transform.position;
        InvokeRepeating("Patrol", 1f, 5f);*/
    }

    void Patrol()
    {
        /*agent.SetDestination(new Vector2(enemyPosition.x + 3, enemyPosition.y));*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
