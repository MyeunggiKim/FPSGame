using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목적: 마우스 좌클릭을 눌렀을 때 폭탄을 특정 방향으로 발사하고 싶다.
//폭탄 오브젝트,발사 위치, 방향
//마우스 오른쪽 버튼을 누른다
//폭탄 게임 오브젝트를 생성하고 firePosition에 위치시킨다.
//폭탄 오브잭트의 rigidBody를 가져와서 카메라 정면 방향으로 물리력을 가한다.


public class PlayerFire : MonoBehaviour
{
    public GameObject bomb;
    public GameObject firePosition;
    public float power;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))// 좌클릭(0), 우클릭(1), 휠클릭(2)
        {
            GameObject bombGO = Instantiate(bomb);
            bombGO.transform.position = firePosition.transform.position;

            Rigidbody rigidbody =bombGO.GetComponent<Rigidbody>();
            rigidbody.AddForce(Camera.main.transform.forward* power, ForceMode.Impulse);
        }

    }
}
