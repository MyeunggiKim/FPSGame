using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public float speed = 10.0f;


    // Update is called once per frame
    void Update()
    {        

        //마우스의 입력.
        float mouseX = Input.GetAxis("Mouse X");

        //입력에 따른 회전방향 설정.
        Vector3 dir = new Vector3(0, mouseX, 0);

        //물체를 회전시킨다.
        //r = r0 +vt
        transform.eulerAngles = transform.eulerAngles + dir * speed * Time.deltaTime;
    }
}
