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
//현재시간, 공격딜레이, attackPower

//플레이어를 따라가다 특정 범위를 벗어났을 경우 초기 위치로 돌아간다.
//초기 위치, 특정 범위

//초기 위치로 돌아온다, 이동 범위 이내면  Idle 상태로 전환한다.

//플레이어의 공격을 받으면 플레이어의 hitDamage만큼 적의 hp를 감소시킨다.
//hp

//죽었을 경우 2초 후에 자신을 제거한다.
public class EnemyFSM : MonoBehaviour
{
    //적 상태
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
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

    public int attackPower = 3;

    EnemyState enemyState;



    Vector3 sponPoint;
    public float moveArea = 20.0f;

    float retrunDistance = 0.3f;

    public int hp = 3;

    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyState.Idle;

        player = GameObject.Find("Player").transform;

        characterController = GetComponent<CharacterController>();

        sponPoint = transform.position;
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
            case EnemyState.Damaged:
                //Damaged();
                break;
            case EnemyState.Die:
                //Die();
                break;

        }
    }

    

    private void Die()
    {
        StopAllCoroutines();

        StartCoroutine(DieProcess());
    }

    //2초 후 자신을 제거한다.
    IEnumerator DieProcess()
    {
        yield return new WaitForSeconds(2);

        print("적 사망");
        Destroy(gameObject);
    }


    //플레이어의 공격을 받으면 damage만큼 Enemy의 체력을 감소시킨다.
    //Enemy의 체력이 0보다 크면 Damaged 상태로 전환
    //그렇지 않다면 Die 상태로 전환

    public void DamageAction(int damage)
    {
        if (enemyState == EnemyState.Damaged || enemyState == EnemyState.Die)
        {
            return;
        }


        //플레이어의 공격력 만큼 hp를 감소시킨다.
        hp -= damage;
        if (hp > 0)
        {
            enemyState = EnemyState.Damaged;
            print("상태 전환: Any State -> Damaged");
            Damaged();
        }
        else
        {
            enemyState = EnemyState.Die;
            print("상태 전환: Any State -> Die");
            Die();
        }
    }

    private void Damaged()
    {
        StartCoroutine(DamageProcess());

    }

    IEnumerator DamageProcess()
    {
        //피격 모션 시간만큼 기다린다.
        yield return new WaitForSeconds(0.5f);

        //현제 상태를 Move로 전환한다.

        enemyState = EnemyState.Move;
        print("상태 전환: Damaged -> Move");
    }

    private void Return()
    {
        float distanceToSponPoint = (sponPoint - transform.position).magnitude;

        if (distanceToSponPoint > retrunDistance)
        {
            Vector3 dir = ( sponPoint - transform.position).normalized;
            characterController.Move( dir*moveSpeed*Time.deltaTime );
        }
        else
        {
            enemyState = EnemyState.Idle;
            print("상태 전환:Retrun -> Idel");
        }
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
                player.GetComponent<PlayerMove>().DamageAction(attackPower);
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

        //플레이어를 따라가다 이동범위를 벗어나면 초기 위치로 돌아온다.
        float distanceToSponPoint = (sponPoint - transform.position).magnitude;

        if (distanceToSponPoint > moveArea)
        {
            enemyState = EnemyState.Return;
            print("상태 전환: Move -> Retrun");
        }
        if (distanceToPlayer > attackDistance)
        {
            Vector3 dir = (player.position - transform.position).normalized;

            characterController.Move(dir*Time.deltaTime*moveSpeed);
        }
        if (distanceToPlayer <= attackDistance)
        {
            enemyState = EnemyState.Attack;
            print("상태 전환: Move ->  Attack");
            currentTime = attackTime;
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
