using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//적을 FSM 다이어그램에 따라 동작시키고 싶다.
//적 상태

//플레이어와의 거리를 측정해서 특정 상태로 만들어준다.
//플레이어와의 거리, 플레이어 트랜스폼

//적의 상태가 Move라면 적이 플레이어를 따라간다.
//이동속도, 적의 이동을 위한 캐릭터 컨트롤러, 공격범위

//플레이어가 공격범위 내에 들어오면 특정 시간에 한번씩 공격한다.
//현재시간, 공격딜레이
public class EnemyFSM : MonoBehaviour
{
    //적 상태
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damage,
        Die
    }
    //플레이어와의 거리, 플레이어 트랜스폼
    public float findDistance;
    Transform player;

    public float moveSpeed;
    CharacterController characterController;
    public float attackDistance = 10.0f;

    //현제시간, 공격딜레이
    float currentTime = 0.0f;
    public float attackTime = 2.0f;

    EnemyState enemyState;


    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyState.Idle;

        player = GameObject.Find("Player").transform;

        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //적을 FSM 다이어그램에 따라 동작시키고 싶다.
        switch (enemyState)
        {
            case EnemyState.Idle:
                Idle();
                //플레이어와의 거리를 측정해서 특정 상태로 만들어준다.
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damage:
                Damage();
                break;
            case EnemyState.Die:
                Die();
                break;

        }
    }

    

    private void Die()
    {
        throw new NotImplementedException();
    }

    private void Damage()
    {
            throw new NotImplementedException();
    }

    private void Return()
    {
        throw new NotImplementedException();
    }

    private void Attack()
    {

        //플레이어가 공격범위 내에 들어오면 공격한다.
        float distanceToPlayer = (player.position - transform.position).magnitude;
        if (distanceToPlayer < attackDistance)
        {
            currentTime += Time.deltaTime;
            //공격딜레이마다 한번씩 공격한다.
            if(currentTime >= attackTime)
            {
                print("공격");
                currentTime = 0;
            }
        }
        //그렇지 않으면 플레이어를 추적한다.
        if (distanceToPlayer > attackDistance)
        {
            enemyState = EnemyState.Move;
            print("상태 전환: Attack -> Move");
        }
    }

    private void Move()
    {
        //플레이어와의 거리가 공격범위 밖이면 적이 플레이어를 따라간다.
        float distanceToPlayer=(player.position - transform.position).magnitude;
        if (distanceToPlayer > attackDistance)
        {
            Vector3 dir = (player.position - transform.position).normalized;

            characterController.Move(dir*Time.deltaTime*moveSpeed);
        }
        if (distanceToPlayer <= attackDistance)
        {
            enemyState = EnemyState.Attack;
            print("상태 전환: Move ->  Attack");
        }
        else if (distanceToPlayer >= findDistance)
        {
            enemyState = EnemyState.Idle;
            print("상태 전환: Move -> Idle");
        }
    }

    private void Idle()
    {
        float distnaceToPlayer = (player.position - transform.position).magnitude;
        //float distnaceToPlayer = Vector3.Distance(transform.position, player.position);

        //플레이어와의 거리가 특정 범위 내라면 상태를 Move로 바꿔준다.
        if (distnaceToPlayer < findDistance)
        {
            enemyState = EnemyState.Move;
            print("상태전환: Idle->Move");            
        }
    }
}
