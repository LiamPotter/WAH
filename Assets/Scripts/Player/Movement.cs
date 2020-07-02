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

        [Tooltip("How quickly the Player turns.")]
        public float TurnSpeed=2f;

        public enum MoveMode { Tracks, Legs, Hover}

        public MoveMode MovementMode;

        public bool DrawGizmos=false;
        #endregion

        protected float currentSpeed = 0f;

        protected Vector3 lastMovementDirection;

        protected Vector3 tweenDirection;

        protected Quaternion lookDirection;

        protected float directionAngle;

        protected bool instantTurn = false;

        private MasterController master;

        private Transform transform;

        private CharacterController playerCharController;

        private ModelHolder model;

        protected Vector3 debugVelocity = Vector3.zero;
        protected float debugVelocityMag = 0;
        public override bool Initialize<T>(T mController)
        {
            master = mController as MasterController;
            transform = master.PTransform;
            model = master.PModel;
            playerCharController = master.PCharController;
            lookDirection = Quaternion.identity;
            lastMovementDirection = model.Movement.forward;
            tweenDirection = Vector3.zero;

            if (!master||!playerCharController||!transform||!model)
            {
                Debug.LogError("The player Movement cannot initialize!");
                Debug.Log("Movement Variables: Master-" + master +" Rigidbody-"+ playerCharController);
                Debug.Log("Transform-" + transform + " Model-" + model);
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
            ApplyRotation();
        }

        /// <summary>
        /// Returns the current Input Direction if input is detected.
        /// Otherwise returns the current direction the player is facing.
        /// </summary>
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
                lastMovementDirection = model.Movement.forward;
                    
            }
            else
            {
                currentSpeed -= Time.deltaTime * DecelerationRamp;             
            } 
            
            currentSpeed=Mathf.Clamp(currentSpeed, 0, SpeedCap);
            directionAngle = Vector3.Angle(tweenDirection, InputOrFacingDirection());
            if (directionAngle >= 170)
                instantTurn = true;
        }

        protected void ApplySpeed()
        {
            //playerBody.velocity = model.Movement.forward * currentSpeed;
            playerCharController.SimpleMove(model.Movement.forward * currentSpeed);
            debugVelocity = playerCharController.velocity;
            debugVelocityMag = debugVelocity.magnitude;
        }

        protected void ApplyRotation()
        {
            float time = Time.fixedDeltaTime * (TurnSpeed);
            tweenDirection = Vector3.Lerp(tweenDirection, InputOrFacingDirection(), time);
            if (instantTurn)
            {
                tweenDirection = InputOrFacingDirection();
                instantTurn = false;
            }
            if (tweenDirection != Vector3.zero)
                lookDirection = Quaternion.LookRotation(tweenDirection, Vector3.up);
            model.Movement.rotation = lookDirection;
          

        }

        private float CurrentVsWantedVelocity()
        {
            float _magnitude=0;

            Vector3 _cur = tweenDirection;

            Vector3 _wanted = InputOrFacingDirection();

            return _magnitude;
        }

        public void DoDrawGizmos()
        {
            if (!transform)
                return;
            Gizmos.color = Color.red;
            Ray inputRay = new Ray();
            inputRay.origin = transform.position;
            inputRay.direction = InputOrFacingDirection() * 2;
            Gizmos.DrawRay(inputRay);
            if (playerCharController)
            {
                Gizmos.color = Color.green;
                Ray velocityRay = new Ray();
                velocityRay.origin = transform.position;
                velocityRay.direction = playerCharController.velocity;
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
