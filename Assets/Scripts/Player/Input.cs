using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WAH.Player
{
    /// <summary>
    /// The static class responsible for collecting all player input
    /// </summary>
    public static class Input
    {        

        public static bool IsMoving()
        {
            return (
                Mathf.Abs(UnityEngine.Input.GetAxisRaw("Horizontal")) >= 0.05f ||
                Mathf.Abs(UnityEngine.Input.GetAxisRaw("Vertical")) >= 0.05f
                );
        }

        public static Vector3 MoveDirection()
        {
            Vector3 _dir = new Vector3(UnityEngine.Input.GetAxisRaw("Horizontal"),0,
                UnityEngine.Input.GetAxisRaw("Vertical"));
            _dir = Vector3.ClampMagnitude(_dir, 1);
            return _dir;
        }

    }
}