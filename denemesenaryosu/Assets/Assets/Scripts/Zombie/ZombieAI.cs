using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum ZombieState
{
    Idle = 0,
    Walk = 1,
    Dead = 2,
    Attack =3
}
public class ZombieAI : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    ZombieState zombieState;
    GameObject playerObject;
    PlayerHealth playerHealth;
    ZombieHealt zombieHealt;

    AudioSource zombieSource;
    public AudioClip zombieFx;
    // Start is called before the first frame update
    void Start()
    {
        zombieHealt = GetComponent<ZombieHealt>();
        playerObject = GameObject.FindWithTag("Player");
        playerHealth = playerObject.GetComponent<PlayerHealth>();
        zombieState = ZombieState.Idle;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        zombieSource = GetComponent<AudioSource>();

        zombieSource.clip = zombieFx;
        zombieSource.Play();

        //Debug.Log("zombi sesi");
    }

    // Update is called once per frame
    void Update()
    {
        

        if (zombieHealt.GetHealth() <= 0)
        {
            SetState(ZombieState.Dead);
        }
        switch (zombieState)
        {
            case ZombieState.Dead:
                KillZombie();
                break;
            case ZombieState.Attack:
                Attack();
                break;
            case ZombieState.Walk:
                SearchForTarget();
                break;
            case ZombieState.Idle:
                SearchForTarget();
                break;    
            default:
                break;
        }
    }

    private void Attack()
    {
        
        SetState(ZombieState.Attack);
        agent.isStopped = true;
    }
    void MakeAttack()
    {
        playerHealth.DeductHealth(10);
        SearchForTarget();
    }

    private void SearchForTarget()
    {
        
        float distance = Vector3.Distance(transform.position, playerObject.transform.position);
        if (distance < 1.5f)
        {
            Attack();
        }
        else if (distance < 30)
        {
            MoveToPlayer();
        }
        else
        {
            SetState(ZombieState.Idle);
            agent.isStopped = true;
        }
    }

    private void SetState(ZombieState state)
    {
        zombieState = state;

        animator.SetInteger("state", (int)state);
    }

    private void MoveToPlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(playerObject.transform.position);
        SetState(ZombieState.Walk);
    }

    private void KillZombie()
    {
        SetState(ZombieState.Dead);
        agent.isStopped = true;
        zombieSource.Stop();
        Destroy(gameObject, 2);
    }
}