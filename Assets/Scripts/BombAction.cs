using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//목적: 폭탄이 물체에 충돌했을 때 이펙트를 생성하고 파괴된다.
//폭발 이펙트


public class BombAction : MonoBehaviour
{
    //폭발 이펙트
    public GameObject bombEffect;
    private void OnCollisionEnter(Collision collision)
    {
        //이펙트를 만든다.
        GameObject bombEffGO = Instantiate(bombEffect);

        //이펙트의 위치를 폭탄의 위치로 설정한다.
        bombEffGO.transform.position = transform.position;

        //폭탄 제거
        Destroy(gameObject);
    }
}
