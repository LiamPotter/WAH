using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboRogue.Input;

namespace RoboRogue.Controllers
{
    [RequireComponent(typeof(CharacterController))]
    public class RoboMovement : MonoBehaviour
    {
      

        private CharacterController characterController;

        private Vector3 movementInput;

        private Vector3 movementVector;

        public Vector3 Velocity { get { return characterController.velocity; } }

        public static Vector3 PlayerPosition = new Vector3();

        void Start()
        {

            characterController = this.GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            movementInput = Input.InputAxis.MovementInput();
            movementInput.z = movementInput.y;
            movementInput.y = 0;
            movementVector = movementInput * 1;
            movementVector = Vector3.ClampMagnitude(movementVector, 1);
            characterController.SimpleMove(movementVector);
            PlayerPosition = transform.position;
        }
    }
}
