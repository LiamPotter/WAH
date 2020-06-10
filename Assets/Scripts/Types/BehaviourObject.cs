using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WAH.Types
{
   
    /// <summary>
    /// The base type used for player behaviours
    /// </summary>
    public class BehaviourObject : ScriptableObject
    {
        // Start is called before the first frame update
        public virtual bool Initialize<T>(T input)
        {
            return true;
        }
        
       
        public virtual void Update()
        {

        }

        public virtual void LateUpdate()
        {

        }
    }
}
