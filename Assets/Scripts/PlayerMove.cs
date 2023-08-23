using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

//목적4: 플레이어가 공격을 당하면 hp를 damege 만큼 깎는다.
//hp

//현제 플레이어의 hp를 hp슬라이더에 적용한다.
//hp,maxHp,Slider

//적의 공격을 받으면 HitImage를 켰다가 꺼준다.

//플레이어가 죽으면 hitImage의 알파값을 증가시킨다.
//현재시간,hitImage 종료시간

//GameManager가 Ready상태일 때 플레이어,적은 움직일 수 없다.


//플레이어의 자식 중 모델링 오브젝트에 있는 애니메이터


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


    public int hp = 10;

    int maxHP = 10;
    public Slider hpSlider;

    public GameObject hitImage;

    float currentTime;
    public float endTime;


    private void Start()
    {
        playerController = GetComponent<CharacterController>();


    }


    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.state != GameManager.GameState.Start)
        {
            return;
        }            
        //사용자의 입력을 받는다
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");


            //점프 중이라면 점프전 상태로 초기화 하고 싶다.
            if (isJumping && playerController.collisionFlags == CollisionFlags.Below)
            {
                isJumping = false;

            }
            //바닥에 닿아 있을 경우 수직 속도를 초기화 한다.
            else if (playerController.collisionFlags == CollisionFlags.Below)
            {

                yVelocity = 0.0f;
            }


            if (Input.GetButtonDown("Jump") && !isJumping)
            {
                yVelocity = jumpHigh;
                isJumping = true;
            }



            //이동방향을 설정한다.
            Vector3 dir = new Vector3(h, 0, v);
            dir = Camera.main.transform.TransformDirection(dir);


            //플레이어의 수직 속도를 중력에 적용하고 싶다.
            yVelocity = yVelocity + gravity * Time.deltaTime;
            dir.y = yVelocity;


            //플레이어를 이동시킨다
            //transform.position += dir * Time.deltaTime * speed;
            playerController.Move(dir * speed * Time.deltaTime);

            hpSlider.value = (float)hp / maxHP;
        }

        public void DamageAction(int damage)
        {
            hp -= damage;

            if (hp > 0)
            {
                StartCoroutine(PlayHitEffect());
            }
            //플레이어가 죽으면 hitImage의 알파값을 증가시킨다.
            else
            {
                StartCoroutine(DeadEffect());
            }
        }

        IEnumerator DeadEffect()
        {
            hitImage.gameObject.SetActive(true);
            Color hitImageColor = hitImage.GetComponent<Image>().color;


            while (true)
            {
                currentTime += Time.deltaTime;

                yield return null;

                hitImageColor.a = Mathf.Lerp(0, 1, currentTime / endTime);

                hitImage.GetComponent<Image>().color = hitImageColor;


                if (currentTime > endTime)
                {
                    currentTime = 0;
                    break;
                }
            }
        }

        IEnumerator PlayHitEffect()
        {
            hitImage.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.05f);
            hitImage.gameObject.SetActive(false);
        }
    
}
