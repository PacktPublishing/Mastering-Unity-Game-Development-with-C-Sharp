using UnityEngine;

namespace Chapter6
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "Data/Player Stats")]
    public class PlayerStats : ScriptableObject
    {
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float jumpForce = 5f;
        [SerializeField] float dashForce = 10f;
        [SerializeField] float dashCooldown = 2f;

        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public float JumpForce { get => jumpForce; set => jumpForce = value; }
        public float DashForce { get => dashForce; set => dashForce = value; }
        public float DashCooldown { get => dashCooldown; set => dashCooldown = value; }
    }

}