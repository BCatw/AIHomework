using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentTest : MonoBehaviour {

    NavMeshAgent nma;
    NavMeshPath nmp;
    Vector3 vTarget;
    bool bMove = false;
    bool bGetPath = false;
	// Use this for initialization
	void Start () {
        nma = GetComponent<NavMeshAgent>();
        nmp = new NavMeshPath();

    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rh;
            if(Physics.Raycast(r, out rh, 1000.0f))
            {
                if(rh.collider.gameObject != this.gameObject)
                {
                    bMove = true;
                    vTarget = rh.point;
                    //  nma.SetDestination(vTarget);
                    //    nma.destination = vTarget;

                    bGetPath = nma.CalculatePath(vTarget, nmp);
                }
            }
        }
      
        Debug.Log(nma.remainingDistance);
        if(bMove)
        {

            OffMeshLinkData omld =  nma.currentOffMeshLinkData;
            if(omld.activated)
            {
                this.transform.position = omld.endPos;
               nma.velocity = Vector3.zero;
                nma.CompleteOffMeshLink();
            }
        }
       

    }
    private void OnDrawGizmos()
    {

        if (bGetPath)
        {
            Gizmos.color = Color.green;
            Vector3[] vpts = nmp.corners;
            for (int i = 0; i < vpts.Length - 1; i++)
            {
                Gizmos.DrawLine(vpts[i], vpts[i + 1]);
            }

        }
    }
}
