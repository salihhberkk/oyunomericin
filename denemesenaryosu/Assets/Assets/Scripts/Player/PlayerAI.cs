using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum PlayerState
{
    Idle = 0,
    Walk = 1,
    Dead = 2,
    Fire = 3
}
public class PlayerAI : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    PlayerState playerState;
    GameObject playerObject;
    PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        
        playerObject = GameObject.FindWithTag("Player");
        playerHealth = playerObject.GetComponent<PlayerHealth>();
        playerState = PlayerState.Idle;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.GetPlayerHealth() <= 0)
        {
            SetState(PlayerState.Dead,0);
        }
        switch (playerState)
        {
            case PlayerState.Dead:
                Die();
                break;
            case PlayerState.Fire:
                Fire();
                break;
            case PlayerState.Walk:
                Walk();
                break;
            case PlayerState.Idle:
                Idle();
                break;
            default:
                break;
        }
    }

    private void Idle()
    {
        SetState(PlayerState.Idle,0);
        agent.isStopped = true;
    }

    private void Walk()
    {
        SetState(PlayerState.Walk, 2);
        agent.isStopped = true;
    }

    private void Fire()
    {
        SetState(PlayerState.Fire, 1);
        agent.isStopped = true;
    }

    private void Die()
    {
        SetState(PlayerState.Dead, 0);
        agent.isStopped = true;
        //Destroy(gameObject, 5);
    }

    private void SetState(PlayerState state,int speed)
    {
        playerState = state;

        animator.SetInteger("state", (int)state);
        animator.SetInteger("speed", speed);
    }
}
