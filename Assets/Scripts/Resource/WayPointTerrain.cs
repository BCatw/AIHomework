using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WayPointTerrain {
    public List<PathNode> m_NodeList;
    public GameObject[] m_Walls;
    public void Init()
    {
        m_Walls = GameObject.FindGameObjectsWithTag("Wall");
        m_NodeList = new List<PathNode>();
        GameObject [] gos = GameObject.FindGameObjectsWithTag("WP");
        foreach(GameObject g in gos)
        {
            PathNode p = new PathNode();
            p.m_bLink = false;
            p.m_iFloor = 0;
            p.m_fF = p.m_fG = p.m_fH = 0.0f;
            p.m_Parent = null;
            p.m_Neibors = new List<PathNode>();
            p.m_Pos = g.transform.position;
            p.m_Go = g;
            m_NodeList.Add(p);
        }

        LoadWP();
    }

    public void ClearAStarInfo()
    {
        foreach(PathNode node in m_NodeList)
        {
            node.m_Parent = null;
            node.m_fF = 0.0f;
            node.m_fG = 0.0f;
            node.m_fH = 0.0f;
            node.m_eNodeState = ePathNodeState.NODE_NONE;
        }
    }

    public PathNode GetNodeFromPosition(Vector3 pos, int iFloor)
    {
        PathNode rnode = null;
        PathNode node;
        int iC = m_NodeList.Count;
        float max = 100000.0f;
        for(int i = 0; i < iC; i++)
        {
            node = m_NodeList[i];
            if(node.m_iFloor != iFloor)
            {
                continue;
            }
            if (Physics.Linecast(pos, node.m_Pos, 1 << LayerMask.NameToLayer("Wall"))) {
                continue;
            }
            Vector3 vec = node.m_Pos - pos;
            vec.y = 0.0f; // Optional
            float mag = vec.magnitude;
            if(mag < max)
            {
                max = mag;
                rnode = node;
            }
        }

        return rnode;
    }

    public void LoadWP()
    {
        //FileStream fs = new FileStream("C:/Users/Student/Desktop/AITest/Assets/abc.txt", FileMode.Open);
        //if (fs == null)
        //{
        //    Debug.Log("no file");
        //   // return;
        //}
        StreamReader sr = new StreamReader("Assets/abc.txt");
        if (sr == null)
        {
            return;
        }
        Debug.Log(sr.ReadLine());
        sr.Close();

        TextAsset ta = Resources.Load("abc") as TextAsset;
        string sAll = ta.text;
        string [] sLines = sAll.Split('\n');
        int iLine = sLines.Length;
        int iLineIndex = 0;
       
        while(iLineIndex < iLine)
        {
            string s = sLines[iLineIndex];
            iLineIndex++;
            string [] ss = s.Split(' ');
            PathNode pCurrent = null;
            for(int i = 0; i < m_NodeList.Count; i++)
            {
                if(m_NodeList[i].m_Go.name.Equals(ss[0]))
                {
                    pCurrent = m_NodeList[i];
                    break;
                }
            }
            if(pCurrent == null)
            {
                continue;
            }
            pCurrent.m_iFloor = int.Parse(ss[1]);
            if(ss[2].Equals("True"))
            {
                pCurrent.m_bLink = true;
            }
            int inNei = int.Parse(ss[3]);
            int iIndex = 4;
            for (int i = 0; i < inNei; i++)
            {
                string sName = ss[iIndex + i];
                for (int j = 0; j < m_NodeList.Count; j++)
                {
                    if (m_NodeList[j].m_Go.name.Equals(sName))
                    {
                        pCurrent.m_Neibors.Add(m_NodeList[j]);
                        break;
                    }
                }
            }

           // s = sr.ReadLine();
        }

       // sr.Close();

       /* for (int i = 0; i < m_NodeList.Count; i++)
        {
            string sNode = m_NodeList[i].m_Go.name + " " +
                           m_NodeList[i].m_iFloor + " " +
                           m_NodeList[i].m_bLink + " " +
                           m_NodeList[i].m_Neibors.Count;

            for(int j = 0; j < m_NodeList[i].m_Neibors.Count; j++)
            {
                sNode += " ";
                sNode += m_NodeList[i].m_Neibors[j].m_Go.name;
            }
            Debug.Log(sNode);
        }*/
    }

    

}
