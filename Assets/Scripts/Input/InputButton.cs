using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboRogue.Input
{
    public static class InputButton
    {
        public static bool IsAttacking()
        {
            return UnityEngine.Input.GetButton("Attack");
        }
    }
}
