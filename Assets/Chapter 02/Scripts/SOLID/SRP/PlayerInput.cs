using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter2.SRP
{
    public class PlayerInput : MonoBehaviour
    {
        public bool IsJumping()
        {
            // Logic for determining if the player is jumping
            return Input.GetKeyDown(KeyCode.Space);
        }

        public bool IsRunning()
        {
            // Logic for determining if the player is running
            return Input.GetKey(KeyCode.LeftShift);
        }

    }
}