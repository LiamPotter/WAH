using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WAH.Types;
using WAH.Player;
using System;

namespace WAH.Player
{
    /// <summary>
    /// The class responsible for controlling and calculating player movement
    /// </summary>
    [CreateAssetMenu(fileName = "Movement", menuName = "WAH Scriptables/Movement Object", order = 0)]
    public class Movement : BehaviourObject
    {
        #region Editor Exposed Variables
      
        [Tooltip("Maximum speed the Player can reach.")]
        public float SpeedCap = 0.5f;

        [Tooltip("How quickly the Player accelerates towards the Speed Cap.")]
        public float AccelerationRamp = 0.01f;

        [Tooltip("How quickly the Player decelerates towards 0 when no input is detected.")]
        public float DecelerationRamp = 0.03f;

        [Tooltip("How quickly the Player's velocity changes when moving in a new direction.")]
        public float VelocityChangeSpeed=2f;

        #endregion

        protected float currentSpeed = 0f;

        protected Vector3 lastMovementDirection;

        protected Vector3 tweenDirection;

        private MasterController master;

        private Transform transform;

        private Rigidbody playerBody;

        public override bool Initialize<T>(T mController)
        {
            master = mController as MasterController;
            transform = master.PTransform;
            playerBody = master.PRigidbody;
            lastMovementDirection = Vector3.zero;
            tweenDirection = Vector3.zero;
            if (!master||!playerBody||!transform)
            {
                Debug.LogError("The player Movement cannot initialize!");
                Debug.Log("Movement Stats: " + master +" "+ playerBody+ " " + transform);
                return false;
            }
            return true;
        }

        public override void Update()
        {
            CalculateVelocity();          
        }

        public override void LateUpdate()
        {
            ApplySpeed();
        }

        private Vector3 InputOrFacingDirection()
        {
            if (Input.IsMoving())
                return Input.MoveDirection();
            else return lastMovementDirection;
        }

        protected void CalculateVelocity()
        {
            if(Input.IsMoving())
            {
                currentSpeed += Time.deltaTime * AccelerationRamp;
                if (Input.MoveDirection().normalized.magnitude >= 0.1f)
                    lastMovementDirection = Input.MoveDirection();
            }
            else
            {
                currentSpeed -= Time.deltaTime * DecelerationRamp;
            } 

          
            currentSpeed=Mathf.Clamp(currentSpeed, 0, SpeedCap);
        }

        protected void ApplySpeed()
        {
            float time = Time.fixedDeltaTime * VelocityChangeSpeed;
            tweenDirection = Vector3.Lerp(tweenDirection, InputOrFacingDirection(), time);
            playerBody.velocity = tweenDirection * currentSpeed;
        }

        private float CurrentVsWantedVelocity()
        {
            float _magnitude=0;

            Vector3 _cur = tweenDirection;

            Vector3 _wanted = InputOrFacingDirection();

            return _magnitude;
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Ray inputRay = new Ray();
            inputRay.origin = transform.position;
            inputRay.direction = InputOrFacingDirection() * 2;
            Gizmos.DrawRay(inputRay);
            if (playerBody)
            {
                Gizmos.color = Color.green;
                Ray velocityRay = new Ray();
                velocityRay.origin = transform.position;
                velocityRay.direction = playerBody.velocity;
                Gizmos.DrawLine(velocityRay.origin,velocityRay.origin+velocityRay.direction*(currentSpeed*2.5f));
            }
            Gizmos.color = Color.blue;
            Ray wantedRay = new Ray();
            wantedRay.origin = transform.position;
            wantedRay.direction = tweenDirection;
            Gizmos.DrawLine(wantedRay.origin, wantedRay.origin + wantedRay.direction * 2.5f);
        }
    }
}
