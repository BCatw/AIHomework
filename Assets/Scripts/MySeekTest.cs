using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySeekTest : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] AIDataSteering data;
    // Update is called once per frame
    void Update()
    {
        data.m_vTarget = target.transform.position;
        MySteeringBehavior.Seek(data);
        MySteeringBehavior.Move(data);
    }
}
