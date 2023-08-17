using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목표: W,A,S,D를 누르면 캐릭터를 그 방향으로 이동시키고 싶다.
//필요속성: 이동속도
//사용자의 입력을 받는다.
//이동 방향을 설정한다.
//이동 속도에 따라 플레이어를 이동시킨다

//목적2: 스페이스를 누르면 수직으로 점프하고 싶다.
//필요속성: 중력 변수, 수직 속력 변수, 캐릭터 컨트롤러, 점프 높이
//플레이어의 수직 속도에 중력을 적용하고 싶다.
//캐릭터 컨트롤러로 플레이어를  이동시키고 싶다.
//스페이스 키를 누르면 수직 속도에 점프 높이를 적용하 싶다.

//목적3: 점프 중인지 확인하고, 점프 중이라면 점프전 상태로 초기화 하고 싶다.


public class PlayerMove : MonoBehaviour
{
    //이동속도
    public float speed = 10;

    //캐릭터 컨트롤러, 중력 변수, 수직 속력 변수, 점프 높이
    CharacterController playerController;
    float gravity = -20.0f;
    public float yVelocity = 0.0f;
    public float jumpHigh= 10.0f;
    public bool isJumping = false;



    private void Start()
    {
        playerController = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        //사용자의 입력을 받는다
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");


        //점프 중이라면 점프전 상태로 초기화 하고 싶다.
        if(isJumping && playerController.collisionFlags == CollisionFlags.Below)
        {
            isJumping = false;

        }
        //바닥에 닿아 있을 경우 수직 속도를 초기화 한다.
        else if (playerController.collisionFlags ==  CollisionFlags.Below)
        {

            yVelocity = 0.0f;
        }


        if (Input.GetButtonDown("Jump")&& !isJumping)
        {
            yVelocity = jumpHigh;
            isJumping = true;
        }

        

        //이동방향을 설정한다.
        Vector3 dir = new Vector3(h, 0, v);
        dir = Camera.main.transform.TransformDirection(dir);
        

        //플레이어의 수직 속도를 중력에 적용하고 싶다.
        yVelocity = yVelocity + gravity *Time.deltaTime;
        dir.y = yVelocity;


        //플레이어를 이동시킨다
        //transform.position += dir * Time.deltaTime * speed;
        playerController.Move(dir*speed*Time.deltaTime);


    }
}
