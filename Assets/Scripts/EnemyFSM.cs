using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� FSM ���̾�׷��� ���� ���۽�Ű�� �ʹ�.
//�� ����

//�÷��̾���� �Ÿ��� �����ؼ� Ư�� ���·� ������ش�.
//�÷��̾���� �Ÿ�, �÷��̾� Ʈ������

//���� ���°� Move��� ���� �÷��̾ ���󰣴�.
//�̵��ӵ�, ���� �̵��� ���� ĳ���� ��Ʈ�ѷ�, ���ݹ���

//�÷��̾ ���ݹ��� ���� ������ Ư�� �ð��� �ѹ��� �����Ѵ�.
//����ð�, ���ݵ�����
public class EnemyFSM : MonoBehaviour
{
    //�� ����
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damage,
        Die
    }
    //�÷��̾���� �Ÿ�, �÷��̾� Ʈ������
    public float findDistance;
    Transform player;

    public float moveSpeed;
    CharacterController characterController;
    public float attackDistance = 10.0f;

    //�����ð�, ���ݵ�����
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
        //���� FSM ���̾�׷��� ���� ���۽�Ű�� �ʹ�.
        switch (enemyState)
        {
            case EnemyState.Idle:
                Idle();
                //�÷��̾���� �Ÿ��� �����ؼ� Ư�� ���·� ������ش�.
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

        //�÷��̾ ���ݹ��� ���� ������ �����Ѵ�.
        float distanceToPlayer = (player.position - transform.position).magnitude;
        if (distanceToPlayer < attackDistance)
        {
            currentTime += Time.deltaTime;
            //���ݵ����̸��� �ѹ��� �����Ѵ�.
            if(currentTime >= attackTime)
            {
                print("����");
                currentTime = 0;
            }
        }
        //�׷��� ������ �÷��̾ �����Ѵ�.
        if (distanceToPlayer > attackDistance)
        {
            enemyState = EnemyState.Move;
            print("���� ��ȯ: Attack -> Move");
        }
    }

    private void Move()
    {
        //�÷��̾���� �Ÿ��� ���ݹ��� ���̸� ���� �÷��̾ ���󰣴�.
        float distanceToPlayer=(player.position - transform.position).magnitude;
        if (distanceToPlayer > attackDistance)
        {
            Vector3 dir = (player.position - transform.position).normalized;

            characterController.Move(dir*Time.deltaTime*moveSpeed);
        }
        if (distanceToPlayer <= attackDistance)
        {
            enemyState = EnemyState.Attack;
            print("���� ��ȯ: Move ->  Attack");
        }
        else if (distanceToPlayer >= findDistance)
        {
            enemyState = EnemyState.Idle;
            print("���� ��ȯ: Move -> Idle");
        }
    }

    private void Idle()
    {
        float distnaceToPlayer = (player.position - transform.position).magnitude;
        //float distnaceToPlayer = Vector3.Distance(transform.position, player.position);

        //�÷��̾���� �Ÿ��� Ư�� ���� ����� ���¸� Move�� �ٲ��ش�.
        if (distnaceToPlayer < findDistance)
        {
            enemyState = EnemyState.Move;
            print("������ȯ: Idle->Move");            
        }
    }
}
