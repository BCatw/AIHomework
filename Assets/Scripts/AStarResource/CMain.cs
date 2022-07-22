using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMain : MonoBehaviour {
    public GameObject m_Control;
    private NPC m_Npc;
    private bool m_bAstar = false;


	// Use this for initialization
	void Start () {
        WayPointTerrain wpt = new WayPointTerrain();
        wpt.Init();

        AStar astar = new AStar();
        astar.Init(wpt);
        m_Npc = m_Control.GetComponent<NPC>();

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rh;
            if (Physics.Raycast(r, out rh, 1000.0f, 1 << LayerMask.NameToLayer("Terrain")))
            {
                m_bAstar = AStar.instance.PerformAStar(m_Control.transform.position, rh.point);
                m_Npc.m_iCurrentPathPt = 0;
                Debug.Log(m_bAstar);
            }

        }
        if(m_bAstar)
        {
            List<Vector3> path = AStar.instance.GetPath();
            int iFinal = path.Count - 1;
            int i;
            for (i = iFinal; i >= m_Npc.m_iCurrentPathPt; i--)
            {
                Vector3 sPos = path[i];
                Vector3 cPos = m_Control.transform.position;
                if(Physics.Linecast(cPos, sPos, 1 << LayerMask.NameToLayer("Wall")))
                {
                    continue;
                }
                m_Npc.m_iCurrentPathPt = i;
                m_Npc.SetTarget(sPos);
                break;
            }
        }
        //if (Input.GetMouseButton(0))
        //{
        //    Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit rh;
        //    if (Physics.Raycast(r, out rh, 1000.0f, 1 << LayerMask.NameToLayer("Terrain")))
        //    {
        //        m_Npc.SetTarget(rh.point);
        //    }

        //}
    }

    private void OnDrawGizmos()
    {
        if(m_bAstar)
        {
            List<Vector3> path = AStar.instance.GetPath();
            Gizmos.color = Color.blue;
            int iCount = path.Count - 1;
            int i;
            for(i = 0; i < iCount; i++)
            {
                Vector3 sPos = path[i];
                sPos.y += 1.0f;
                Vector3 ePos = path[i + 1];
                ePos.y += 1.0f;
                Gizmos.DrawLine(sPos, ePos);
                
            }
            
        }
    }
}
