using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목표: 마우스 입력을 받아 카메라를 회전시킨다.
//필요속성: 마우스 입력 X, Y, 속도
//순서1: 마우스 입력을 받는다.
//순서2: 마우스 입력에 따라 방향을 설정한다.
//순서3: 물체를 회전시킨다.



public class CamRotate : MonoBehaviour
{
    public float speed = 10.0f;
    float mx = 0;
    float my = 0;
    



    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.state != GameManager.GameState.Start)
        {
            return;
        }            
        //마우스의 입력.
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        mx += mouseX * speed * Time.deltaTime;
        my += mouseY * speed * Time.deltaTime;

        my = Mathf.Clamp(my, -90.0f, 90.0f);

        //입력에 따른 회전방향 설정.
        Vector3 dir =  new Vector3(-my, mx, 0);

        //물체를 회전시킨다.
        //r = r0 +vt
        transform.eulerAngles = dir;

    }
}
