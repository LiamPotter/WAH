using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboRogue.Input
{
    public static class InputAxis
    {
        private static Vector2 movementVector;
        public static Vector2 MovementInput()
        {
            if (movementVector == null)
                movementVector = new Vector2();
            movementVector.x = UnityEngine.Input.GetAxisRaw("Horizontal");
            movementVector.y = UnityEngine.Input.GetAxisRaw("Vertical");
            return movementVector;
        }

        private static Vector2 lookVector;
        
        public static Vector2 LookInput(bool UseMouse)
        {
            if (lookVector == null)
                lookVector = new Vector2();

            lookVector = UseMouse ? MouseLookInput() : ControllerLookInput();

            return lookVector;
        }

        private static Vector2 mouseLookVector;
        public static Vector2 MouseLookInput()
        {
            if (mouseLookVector == null)
                mouseLookVector = new Vector2();

            //mouseLookVector.x = UnityEngine.Input.mousePosition.x - Screen.width / 2;
            //mouseLookVector.y = UnityEngine.Input.mousePosition.y - Screen.height / 2;
            mouseLookVector = UnityEngine.Input.mousePosition;


            return mouseLookVector;
        }

        private static Vector2 controllerLookVector;
        public static Vector2 ControllerLookInput()
        {
            if (controllerLookVector == null)
                controllerLookVector = new Vector2();

         
            controllerLookVector.x = UnityEngine.Input.GetAxis("Right Horizontal");
            controllerLookVector.y = UnityEngine.Input.GetAxis("Right Vertical");


            return controllerLookVector;
        }

        /// <summary>
        /// Is there a controller currently plugged into the computer?
        /// </summary>
        public static bool IsControllerPluggedIn
        {
            get
            {
                if (UnityEngine.Input.GetJoystickNames().Length > 0)
                {
                    if (UnityEngine.Input.GetJoystickNames()[0] == string.Empty)
                        return false;
                    else return true;
                }
                else return false;
            }
        }

        public static bool IsPlayerLooking(float deadZone)
        {
            if (controllerLookVector == null)
                return false;
            if (controllerLookVector.x > deadZone || controllerLookVector.x < -deadZone)
                return true;
            else if (controllerLookVector.y > deadZone || controllerLookVector.y < -deadZone)
                return true;

            return false;
        }
    }
}
