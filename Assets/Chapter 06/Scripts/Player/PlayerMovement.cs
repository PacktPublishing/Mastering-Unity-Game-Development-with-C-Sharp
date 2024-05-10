using UnityEngine;

namespace Chapter6
{
    public class PlayerMovement : MonoBehaviour
    {
        public PlayerStats playerStats;
        public Transform groundChecker;
        public LayerMask groundLayer;
        public float groundDistance;
        public Rigidbody playerRigidbody;

        private bool isGrounded = true;
        private bool canDash = true;
        private Vector3 movementVector;

        private void MovePlayer()
        {
            Vector3 movement = new Vector3(movementVector.x, 0f, movementVector.y) * playerStats.MoveSpeed * Time.deltaTime;
            transform.Translate(movement);
        }
        //rest of code 
    }

}