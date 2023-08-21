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
//����ð�, ���ݵ�����, attackPower

//�÷��̾ ���󰡴� Ư�� ������ ����� ��� �ʱ� ��ġ�� ���ư���.
//�ʱ� ��ġ, Ư�� ����

//�ʱ� ��ġ�� ���ƿ´�, �̵� ���� �̳���  Idle ���·� ��ȯ�Ѵ�.

//�÷��̾��� ������ ������ �÷��̾��� hitDamage��ŭ ���� hp�� ���ҽ�Ų��.
//hp

//�׾��� ��� 2�� �Ŀ� �ڽ��� �����Ѵ�.
public class EnemyFSM : MonoBehaviour
{
    //�� ����
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
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

    //2�� �� �ڽ��� �����Ѵ�.
    IEnumerator DieProcess()
    {
        yield return new WaitForSeconds(2);

        print("�� ���");
        Destroy(gameObject);
    }


    //�÷��̾��� ������ ������ damage��ŭ Enemy�� ü���� ���ҽ�Ų��.
    //Enemy�� ü���� 0���� ũ�� Damaged ���·� ��ȯ
    //�׷��� �ʴٸ� Die ���·� ��ȯ

    public void DamageAction(int damage)
    {
        if (enemyState == EnemyState.Damaged || enemyState == EnemyState.Die)
        {
            return;
        }


        //�÷��̾��� ���ݷ� ��ŭ hp�� ���ҽ�Ų��.
        hp -= damage;
        if (hp > 0)
        {
            enemyState = EnemyState.Damaged;
            print("���� ��ȯ: Any State -> Damaged");
            Damaged();
        }
        else
        {
            enemyState = EnemyState.Die;
            print("���� ��ȯ: Any State -> Die");
            Die();
        }
    }

    private void Damaged()
    {
        StartCoroutine(DamageProcess());

    }

    IEnumerator DamageProcess()
    {
        //�ǰ� ��� �ð���ŭ ��ٸ���.
        yield return new WaitForSeconds(0.5f);

        //���� ���¸� Move�� ��ȯ�Ѵ�.

        enemyState = EnemyState.Move;
        print("���� ��ȯ: Damaged -> Move");
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
            print("���� ��ȯ:Retrun -> Idel");
        }
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
                player.GetComponent<PlayerMove>().DamageAction(attackPower);
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

        //�÷��̾ ���󰡴� �̵������� ����� �ʱ� ��ġ�� ���ƿ´�.
        float distanceToSponPoint = (sponPoint - transform.position).magnitude;

        if (distanceToSponPoint > moveArea)
        {
            enemyState = EnemyState.Return;
            print("���� ��ȯ: Move -> Retrun");
        }
        if (distanceToPlayer > attackDistance)
        {
            Vector3 dir = (player.position - transform.position).normalized;

            characterController.Move(dir*Time.deltaTime*moveSpeed);
        }
        if (distanceToPlayer <= attackDistance)
        {
            enemyState = EnemyState.Attack;
            print("���� ��ȯ: Move ->  Attack");
            currentTime = attackTime;
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
