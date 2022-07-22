using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehavior : MonoBehaviour {

	public static void Seek(ref AIData data)
    {
        Transform t = data.go.transform;
        Vector3 cPos = t.position;
        Vector3 vFor = t.forward;
        Vector3 vec = data.vTarget - cPos;
        vec.y = 0.0f;
        float fDist = vec.magnitude;
        vec.Normalize();
        float fLimit = data.fSpeed * Time.deltaTime + 0.01f;
        if (fDist < fLimit)
        {
            Vector3 tPos = data.vTarget;
            tPos.y = cPos.y;
            t.position = tPos;
            //t.forward = vec;
            return;
        }

        // Rotate degree.
        //float fDot = Vector3.Dot(vec, vFor);
        //if (fDot > 1.0f)
        //{
        //    fDot = 1.0f;
        //    t.forward = vec;
        //}
        //else
        //{
        //    if (fDot < -1.0f) { 
        //        fDot = -1.0f;
        //    }
        //    float fTargetDeg = Mathf.Acos(fDot) * Mathf.Rad2Deg;

        //    if (fTargetDeg <= data.fMaxRotate)
        //    {
        //        //Debug.Log(fTargetDeg + " -:- " + data.fMaxRotate);
        //        t.forward = vec;
        //    }
        //    else
        //    {
        //        float fDot2 = Vector3.Dot(t.right, vec);
        //        float fRot = data.fMaxRotate;
        //        if (fDot2 < 0)
        //        {
        //            fRot = -fRot;
        //        }
        //        t.Rotate(0.0f, fRot, 0.0f);
        //       // Debug.Log(fTargetDeg + " +:+ " + data.fMaxRotate);
        //    }
        //}
        float fDot = Vector3.Dot(vec, vFor);
        if (fDot > 1.0f)
        {
            fDot = 1.0f;
            t.forward = vec;
        }
        else
        {
            float fSinLen = Mathf.Sqrt(1 - fDot * fDot);
            //float fRad = Mathf.Acos(fDot);
            //fSinLen = Mathf.Sin(fRad);
            float fDot2 = Vector3.Dot(t.right, vec);

            float fMag = 0.1f;
            if(fDot < 0)
            {
                fMag = 1.0f;
            }
            if (fDot2 < 0)
            {
                fMag = -fMag;
            }

            vFor = vFor + t.right * fSinLen * fMag;
            vFor.Normalize();
            t.forward = vFor;
        }
       

        // Move.
        float fForForce = fDot;
        float fRatio = 1.0f;
        float fAcc = fForForce* fRatio;
        float fAcc2 = fDist / data.fArriveRange;
        if(fAcc2 > 1.0f)
        {
            fAcc2 = 1.0f;
        } else
        {
            fAcc2 = -(1.0f - fAcc2);
        }
      
        data.fSpeed = data.fSpeed + fAcc * Time.deltaTime + fAcc2* Time.deltaTime;
        if (data.fSpeed > data.fMaxSpeed)
        {
            data.fSpeed = data.fMaxSpeed;
        } else if (data.fSpeed < 0.01f)
        {
            data.fSpeed = 0.01f;
        } 
        t.position = cPos + t.forward * data.fSpeed * Time.deltaTime;

    }
}
