using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehaviour : StateMachineBehaviour
{
    private Transform detectGround;
    private Transform playerPos;
    private bool movingRight = true;

    private float speed = 10f;
    private float distance = 10f;
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        detectGround = animator.transform.GetChild(0);
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // animator.transform.Translate(new Vector2(playerPos.position.x, playerPos.position.y) * speed * Time.deltaTime);
        IsEnemyOnTheEdge(animator);
        float enemyPlayerDistance = Vector3.Distance(animator.transform.position, playerPos.position);
        if (enemyPlayerDistance > 10f)
        {
            animator.SetBool("isPatrolling", true);
        }
        else animator.SetBool("isPatrolling", false);
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

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    //override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}
}
