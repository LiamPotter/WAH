using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WAH.Camera
{
    [CreateAssetMenu(fileName = "Default Camera Values", menuName = "WAH Scriptables/Camera Values", order = 0)]
    public class CameraValues : ScriptableObject
    {
        // A struct is used here so the values can be easily altered in other scripts
        // without needing to create more ScriptableObject instances.
        [Serializable]
        public struct Values
        {
            /// <summary>
            /// Constructor that will copy all values from another instance.
            /// </summary>
            /// <param name="copyFrom"></param>
            public Values(Values copyFrom)
            {
                FollowDeadZone = copyFrom.FollowDeadZone;
                FollowMaxDistance = copyFrom.FollowMaxDistance;
                FollowMaxSpeed = copyFrom.FollowMaxSpeed;
                FollowMinSpeed = copyFrom.FollowMinSpeed;            
            }
            /// <summary>
            /// How much the target can move before the camera follows.
            /// </summary>
            public float FollowDeadZone;

            /// <summary>
            /// The maximum distance the camera can be from it's target.
            /// </summary>
            public float FollowMaxDistance;

            /// <summary>
            /// The maximum speed the camera can reach while following it's target.
            /// </summary>
            public float FollowMaxSpeed;

            /// <summary>
            /// The minimum speed the camera cna reach while following it's target.
            /// </summary>
            public float FollowMinSpeed;

        }

        /// <summary>
        /// The actual values. This will be used for default camera settings,
        /// as well as custom settings for cinematics/tutorials.
        /// </summary>
        [SerializeField]
        public Values values = new Values();
    }
}
