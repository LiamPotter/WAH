using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WAH.Player;
using WAH.Types;

namespace WAH.Player
{
    /// <summary>
    /// The master class that controls all lower player behaviour classes.
    /// This is also the ONLY MonoBehaviour class attached to the player.
    /// </summary>
    public class MasterController : MonoBehaviour
    {
        //The player transform
        [HideInInspector]
        public Transform PTransform;

        //The player rigidbody
        [HideInInspector]
        public Rigidbody PRigidbody;

        //The player movement behaviour
        //[HideInInspector]
        public BehaviourObject PMovement;

        //If there are any errors in the Start method, this will be set to true.
        //Prevents all code from running in the update & lateupdate loops if true.
        private bool noErrorsAllClear = false;

        void Start()
        {
            PTransform = transform;
            PRigidbody = GetComponent<Rigidbody>();
            if (!PMovement)
            {
                Debug.LogError("You need a player movement script on " + gameObject.name + "!");
                return;
            }
            noErrorsAllClear = PMovement.Initialize(this);
        }
        private void Update()
        {
            if(!noErrorsAllClear)
            {
                Debug.LogWarning("There was an initialization fault in MasterController on " + gameObject.name + " aborting.");
                return;
            }
            DoAllUpdates();
        }

        private void LateUpdate()
        {
            if (!noErrorsAllClear)
                return;
            DoAllLateUpdates();
        }

        protected void DoAllUpdates()
        {
            PMovement.Update();
        }
        protected void DoAllLateUpdates()
        {
            PMovement.LateUpdate();
        }
    }
}
