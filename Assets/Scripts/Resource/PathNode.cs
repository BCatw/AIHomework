using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ePathNodeState
{
    NODE_NONE = -1,
    NODE_OPENED,
    NODE_CLOSED
};

public class PathNode {
    public GameObject m_Go;
    public List<PathNode> m_Neibors;
    public int m_iFloor;
    public bool m_bLink;

    public Vector3 m_Pos;
    public PathNode m_Parent;
    public float m_fG;
    public float m_fH;
    public float m_fF;

    public ePathNodeState m_eNodeState;
}
