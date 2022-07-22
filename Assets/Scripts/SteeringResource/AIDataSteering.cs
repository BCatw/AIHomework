using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIDataSteering
{
    public float m_fRadius;
    public float m_fProbeLength;
    public float m_Speed;
    public float m_fMaxSpeed;
    public float m_fRot;
    public float m_fMaxRot;
    public GameObject m_Go;


    public float m_fSight;
    public float m_fAttackRange;

    public float m_fAttackTime;
    public float m_fHp;

    [Header("For Debug")]
    public GameObject m_TargetObject;
    public Vector3 m_vTarget;
    public Vector3 m_vCurrentVector;
    public float m_fTempTurnForce;
    public float m_fMoveForce;
    public bool m_bMove;

    public bool m_bCol;

    //[HideInInspector]
    //public FSMSystem m_FSMSystem;
    //public BT.cBTSystem m_BTSystem;
}
