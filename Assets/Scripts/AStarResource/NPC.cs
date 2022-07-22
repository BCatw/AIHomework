using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {
    AIData m_Data;
    public int m_iCurrentPathPt;
	// Use this for initialization
	void Start () {
        m_Data = new AIData();
        m_Data.fSpeed = 0.0f;
        m_Data.fArriveRange = 10.0f;
        m_Data.fMaxSpeed = 2.5f;
        m_Data.fMaxRotate = 5.0f;
        m_Data.go = this.gameObject;
        m_Data.vTarget = Vector3.zero;
        m_iCurrentPathPt = -1;
    }
	
	// Update is called once per frame
	void Update () {
        SteeringBehavior.Seek(ref m_Data);
    }

    public void SetTarget(Vector3 v)
    {
        m_Data.vTarget = v;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(this.transform.position, this.transform.position + this.transform.forward * 2.0f);

        if (m_Data != null)
        {
            Gizmos.DrawWireSphere(m_Data.vTarget, 1.0f);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, m_Data.fArriveRange);
            
        }
    }
}
