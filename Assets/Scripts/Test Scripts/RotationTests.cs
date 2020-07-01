using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WAH.Player;


public class RotationTests : MonoBehaviour
{
    public float RotateSpeed=1f;

    private float timer;

    public Vector3 InputDir, PrevDir, CurrDir;

    public float CurrentAngle, WantedAngle, DynamicAngle;

    public void Update()
    {
        CurrDir = transform.forward;
        InputDir = WAH.Player.Input.MoveDirection();
        PrevDir = CurrDir;
    }

    public void LateUpdate()
    {
        WantedAngle = Vector3.Angle(InputDir, PrevDir);
        if(timer<1)
            timer += Time.fixedDeltaTime * RotateSpeed;
        DynamicAngle = Mathf.Lerp(WantedAngle, 0, timer);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, InputDir * 5);
    }
}
