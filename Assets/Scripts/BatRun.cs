using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatRun : StateMachineBehaviour
{
    public float speed = 1.0f;
    public float AttackRange = 5.0f;



    Transform player;
    Rigidbody2D rb;
    Bat bat;

    float distance;


    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
       rb = animator.GetComponent<Rigidbody2D>();
       bat = animator.GetComponent<Bat>();
       Debug.Log("Enter run state!");
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bat.LookAtPlayer();

        distance = Vector2.Distance(player.position, rb.position);

        //if(distance >= StopRange)
        {
            Vector2 target = new Vector2(player.position.x, player.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }

        
        if (distance<AttackRange)
        {
            animator.SetTrigger("Attack");
        }

        

    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.ResetTrigger("Attack");
    }

}
