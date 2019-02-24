using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{
    private float distance = 10f;
    private float speed = 10f;

    private bool movingRight = true;

    private Transform detectGround;
    private Transform playerPos;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        detectGround = animator.transform.GetChild(0);
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.Translate(Vector2.right * speed * Time.deltaTime);
        IsEnemyOnTheEdge(animator);

        float enemyPlayerDistance = Vector3.Distance(animator.transform.position, playerPos.position);
        if (enemyPlayerDistance <= 10f)
        {
            animator.SetBool("isPatrolling", false);
        }
        else animator.SetBool("isPatrolling", true);
    }

    void IsEnemyOnTheEdge(Animator animator)
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(detectGround.position, Vector2.down, distance);
        if (groundInfo.collider == false)
        {
            if (movingRight)
            {
                animator.transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                animator.transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
