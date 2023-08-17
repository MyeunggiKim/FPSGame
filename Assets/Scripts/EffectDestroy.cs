using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목표: 이펙트가 일정 시간이 지나면 제거한다.
//특정시간, 현재시간

public class EffectDestroy : MonoBehaviour
{
    //현재시간, 특정시간
    float currentTime = 0;
    float destroyTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        //이팩트가 특정 시간이 지나면 사라진다.
        if (currentTime > destroyTime)
        {
            Destroy(gameObject);
        }
    }
}
