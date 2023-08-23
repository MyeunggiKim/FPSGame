using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목적: 마우스 좌클릭을 눌렀을 때 폭탄을 특정 방향으로 발사하고 싶다.
//폭탄 오브젝트,발사 위치, 방향
//마우스 오른쪽 버튼을 누른다
//폭탄 게임 오브젝트를 생성하고 firePosition에 위치시킨다.
//폭탄 오브잭트의 rigidBody를 가져와서 카메라 정면 방향으로 물리력을 가한다.

//마우스 왼쪽 버튼을 누르면 시선 방향으로 총을 발사하고 싶다.
//마우스 왼쪽 버튼을 누른다.
//레이를 생성하고 발사 위치와 발사 방향을 설정한다.
//레이와 부딪힌 대상의 정보를 저장할 수 있는 변수를 만든다.
//레이를 발사하고 부딪힌 물체가 있으면 그 위치에 피격 효과를 만든다.
//피격 효과 게임 오브젝트 , 이펙트의 파티클 시스템

//레이가 부딪힌 대상이 Enemy라면 Enemy에게 데미지를 주겠다.

//이동 블랜드 트리의 파라미터 값이 0일 경우, Attack Trigger를 시전한다.
//자식 오브젝트의 애니메이션

public class PlayerFire : MonoBehaviour
{
    public GameObject bomb;
    public GameObject firePosition;
    public float power;
    public PlayerFire playerFire;

    private Transform myTransform;

    public GameObject hitEffect;
    new ParticleSystem particleSystem;

    Animator animator;

    private void Awake()
    {
        playerFire = GameObject.Find("Player").GetComponent<PlayerFire>();
    }



    private void Start()
    {
        particleSystem =hitEffect.GetComponent<ParticleSystem>();

        animator = GameObject.GetComponent<Animator>();


        /*int x = 3;
        int y = 4;
        Swap(ref x ,ref y);
        //print(string.Format("x: {0}, Y : {1}", x, y));

        int a = 7;
        int b = 3;
        int quotient;

        quotient = Divide(a, b, out remainder);
        print(string.Format("몫: {0}, 나머지: {1}", quotient, remainder));*/
    }
    //int remainder;
    public int weaponPower = 2;


    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.state != GameManager.GameState.Start)
        {
            return;
        }
        if (Input.GetMouseButtonDown(1))// 좌클릭(0), 우클릭(1), 휠클릭(2)
        {
            GameObject bombGO = Instantiate(bomb);
            bombGO.transform.position = firePosition.transform.position;

            Rigidbody rigidbody =bombGO.GetComponent<Rigidbody>();
            rigidbody.AddForce(Camera.main.transform.forward* power, ForceMode.Impulse);
        }

        //마우스 왼쪽 버튼을 누른다.
        if(Input.GetMouseButtonDown(0))
        {
            if (animator.GetFloat("MoveMotion")==0)
            {
                animator.SetTrigger("Attack");
            }

            //레이를 생성하고 발사위치 발사방향을 설정한다.
            Ray ray =new Ray(Camera.main.transform.position,Camera.main.transform.forward);

            //레이와 부딪힌 대상의 정보를 저장할 수 있는 변수를 만든다.
            RaycastHit hitInfo=new RaycastHit();

            //레이를 발사하고 부딪힌 물체가 있으면 그 위치에 피격 효과를 만든다.
            if(Physics.Raycast(ray, out hitInfo))
            {
                print("충돌체와의 거리는: " + hitInfo.distance);

                //피격효과를 법선 백터 방향으로 만든다.
                hitEffect.transform.position = hitInfo.point;
                hitEffect.transform.forward= hitInfo.normal;

                //피격 이펙트를 재생한다.
                particleSystem.Play();

                //레이가 부딪힌 대상이 Enemy라면 Enemy에게 데미지를 주겠다.
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    EnemyFSM enemyFSM = hitInfo.transform.GetComponent<EnemyFSM>();
                    enemyFSM.DamageAction(weaponPower);
                }
            }
        }
    }
    public void Swap(ref int a,ref int b)
    {
        int temp = a;
        a = b; 
        b = temp;
    }

    /*public int Divide(int a, int b, out int remainder)
    {
        remainder = a % b;

        return a / b;
    }*/
}
