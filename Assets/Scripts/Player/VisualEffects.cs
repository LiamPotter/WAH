using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WAH.Types;

namespace WAH.Player
{
    [CreateAssetMenu(fileName = "Visual Effects", menuName = "WAH Scriptables/Visual Effects Object", order = 1)]
    public class VisualEffects:BehaviourObject
    {
        [Tooltip("The positive/negative visual height dips gained while hovering.")]
        public float HoverDipHeight=0.2f;

        [Tooltip("The speed at which the player dips while hovering")]
        public float HoverDipSpeed = 1f;

        private MasterController master;

        private Transform transform;

        public override bool Initialize<T>(T input)
        {
            master = input as MasterController;
            transform = master.transform;

            if(!master||!transform)
            {
                Debug.LogError("The Visual Effects cannot Initialize!");
                return false;
            }
            return true;

        }
    }
}
