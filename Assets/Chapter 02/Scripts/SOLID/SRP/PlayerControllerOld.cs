using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter2.SRP
{
    public class PlayerControllerOld : MonoBehaviour
    {
        private Animator playerAnimator;
        private Rigidbody rigidBody;
        private float speed;
        private float jumpForce;
        private void Start()
        {
            playerAnimator = GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            // Logic for handling animations
            playerAnimator.SetBool("IsRunning", rigidBody.velocity.magnitude >= speed);

            // Logic for handling player jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

            // Logic for handling player movement
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            Vector3 movePos = new Vector3(x, y);
            rigidBody.MovePosition(movePos);


        }

    }
}