using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySteeringBehavior
{
    static public void Move(AIDataSteering data)
    {
        if (!data.m_bMove)
        {
            return;
        }

        Transform trans = data.m_Go.transform;
        Vector3 currentPosit = data.m_Go.transform.position;
        Vector3 right = trans.right;
        Vector3 originFoward = trans.forward;
        Vector3 forward = data.m_vCurrentVector;

#region rotate
        if (data.m_fTempTurnForce > data.m_fMaxRot) data.m_fTempTurnForce = data.m_fMaxRot;
        else if (data.m_fTempTurnForce < -data.m_fMaxRot) data.m_fTempTurnForce = -data.m_fMaxRot;

        forward = forward + right * data.m_fTempTurnForce;
        forward.Normalize();
        trans.forward = forward;
        #endregion

        data.m_Speed = data.m_Speed + data.m_fMoveForce * Time.deltaTime;
        if (data.m_Speed < 0.01f) data.m_Speed = 0.01f;
        else if (data.m_Speed > data.m_fMaxSpeed) data.m_Speed = data.m_fMaxSpeed;

        if(data.m_Speed < 0.02f)
        {
            if (data.m_fTempTurnForce > 0) trans.forward = right;
            else trans.forward = -right;
        }

        currentPosit += trans.forward * data.m_Speed;
        trans.position = currentPosit;
    }

    static public bool Seek(AIDataSteering data)
    {
        Vector3 currentPosit = data.m_Go.transform.position;
        Vector3 dir = data.m_vTarget - currentPosit;
        dir.y = 0.0f;
        float distance = dir.magnitude;
        float bias = 0.001f;

        Debug.Log($"Distance: {distance}");

        //若距離小於速度，則停止移動
        if(distance < data.m_Speed + bias)
        {
            Vector3 finalPosit = data.m_vTarget;
            finalPosit.y = currentPosit.y;
            data.m_Go.transform.position = finalPosit;
            data.m_fMoveForce = 0.0f;
            data.m_fTempTurnForce = 0.0f;
            data.m_Speed = 0.0f;
            data.m_bMove = false;
            return false;
        }

        Vector3 forward = data.m_Go.transform.forward;
        Vector3 right = data.m_Go.transform.right;
        data.m_vCurrentVector = forward;
        dir.Normalize();
        float dotForward = Vector3.Dot(forward, dir);
        if(dotForward > 0.96f)
        {
            dotForward = 1.0f;
            data.m_vCurrentVector = dir;
            data.m_fTempTurnForce = 0.0f;
            data.m_fRot = 0.0f;
        }
        else
        {
            if (dotForward < -1.0f) dotForward = -1.0f;
            float dotRight = Vector3.Dot(right, dir);

            if (dotForward < 0.0f)
            {
                if (dotRight > 0.0f) dotRight = 1.0f;
                else dotRight = -1.0f;
            }

            if (distance < 3.0f) dotRight *= (distance / 3.0f + 1.0f);
            data.m_fTempTurnForce = dotRight;
        }

        if (distance < 3.0f)
        {
            Debug.Log(data.m_Speed);
            if (data.m_Speed > 0.1f) data.m_fMoveForce = -(1.0f - distance / 3.0f) * 0.5f;
            else data.m_fMoveForce = dotForward * 100.0f;
        }
        else data.m_fMoveForce = 100.0f;

        data.m_bMove = true;
        return true;
    }
}