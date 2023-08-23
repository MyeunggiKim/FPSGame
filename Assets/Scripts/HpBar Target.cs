using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Hp Bar의 앞 방향의 타겟의 앞 방향으로 저장한다,
//타겟
public class HpBarTaget : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.forward = target.forward;
    }
}
