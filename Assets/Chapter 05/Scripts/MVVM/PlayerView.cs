using UnityEngine;

namespace Chapter5
{
    // View 
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private PlayerViewModel playerViewModel;

        private void Start()
        {
            // Subscribe to ViewModel events 
            playerViewModel.UpdatePlayerData(1, 100); // Example initialization 
        }

        private void Update()
        {
            // Example of data binding 
            Debug.Log("Player Level: " + playerViewModel.PlayerLevel);
            Debug.Log("Player Score: " + playerViewModel.PlayerScore);
        }
    }
}