using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Hp Bar�� �� ������ Ÿ���� �� �������� �����Ѵ�,
//Ÿ��
public class HpBarTaget : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.forward = target.forward;
    }
}
