using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using WAH.Player;

namespace WAH.Camera
{
    /// <summary>
    /// The main controller for all things involving the camera.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        /// <summary>
        /// The transform of the actual camera. This should be a 
        /// child of the object with this script attached.
        /// </summary>
        public Transform ActualCameraTransform;

        //Should the camera follow the player on game start? Usually true,
        //can be set false for intro cinematics/tutorials.
        public bool FollowPlayerOnStart = true;

        /// <summary>
        /// The scriptable object that holds default values for the camera.
        /// </summary>
        public CameraValues DefaultCameraValues;

        // Should the camera be currently following the player?
        private bool followPlayer=false;

        //If following the player, set them as the current target,
        //reset camera values to default and sets initial offset values.
        public bool FollowPlayer { 
            get { 
                return followPlayer; 
                } 
            set {
                followPlayer = value;
                if (value)
                {
                    currentTarget = pTransform;
                    currentValues = DefaultCameraValues.values;
                  
                }
                else currentTarget = null;
            } }

        //The master controller of the player. This should never change,
        //thus it is a private variable set on game start.
        private MasterController pController = null;

        //The player's transform stored for easier use. Also set on start.
        private Transform pTransform = null;

        //The current target the camera should be focusing on. Usually this
        //will be the player, but it can also change contextually.
        private Transform currentTarget =null;

        //The current values in use by the camera.
        private CameraValues.Values currentValues;

        //The distance between this object and the current target.
        private float distanceToTarget {
            get {

                    return Vector3.Distance(transform.position, currentTarget.position);

                } }

        //Normalizing using (value-min)/(max-min) and clamping to prevent negative values.
        private float distanceToTargetNormalized{
            get {
               
                    return Mathf.Clamp01((distanceToTarget - currentValues.FollowDeadZone) /
                                         (currentValues.FollowMaxDistance - currentValues.FollowDeadZone));
 } }

        //The normalized direction to the current target
        private Vector3 directionToTarget
        {
            get { 
                    return Vector3.Normalize(currentTarget.position - transform.position);} }

        //Lerps between the min and max follow speeds based on 
        //the normalized distance between the camera and it's target.
        private float currentSpeed
        {
            get { return Mathf.Lerp(currentValues.FollowMinSpeed, 
                currentValues.FollowMaxSpeed, 
                distanceToTargetNormalized); } }


        void Start()
        {
            if(!ActualCameraTransform||!DefaultCameraValues)
            {
                Debug.LogError("You need to assign a public varialble on " + gameObject.name);
                return;
            }
            pController = FindObjectOfType<MasterController>();
            pTransform = pController.transform;
            FollowPlayer = FollowPlayerOnStart;
            currentValues = DefaultCameraValues.values;
           
          
        }

        void LateUpdate()
        {
            if (!currentTarget)
                return;
            FollowTarget();
        }

        //Follows the player by lerping between the current position and the current position plus the
        //direction to the target times the normalized distance to the target based on the current calculated speed.
        private void FollowTarget()
        {
            transform.position = Vector3.Lerp(
                transform.position,
                transform.position + directionToTarget * distanceToTargetNormalized,
                Time.deltaTime * currentSpeed);
            //if(cameraOffsetDifference>=0.05f)
            //{
            //    ActualCameraTransform.localPosition = currentValues.FollowOffset;
            //    Debug.Log("moving to offset "+ cameraOffsetDifference);
            //}
        }
       
    }
}
