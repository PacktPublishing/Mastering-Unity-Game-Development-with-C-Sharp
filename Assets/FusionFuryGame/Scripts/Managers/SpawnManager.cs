using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnSettings
    {
        public string enemyKey;           // Key to retrieve the enemy from the pool
        public int enemiesPerWave;        // Number of enemies per wave
        public int numberOfWaves;         // Total number of waves
        public float timeBetweenWaves;    // Time between each wave
        public float cooldownTime;        // Cooldown before allowing spawning again
    }

    public SpawnSettings[] spawnAreasSettings; // Settings for each SpawnArea

    private Dictionary<SpawnArea, bool> isSpawning;
    private Dictionary<SpawnArea, bool> isCooldown;

    private void Start()
    {
        isSpawning = new Dictionary<SpawnArea, bool>();
        isCooldown = new Dictionary<SpawnArea, bool>();

        // Find all SpawnArea components in the scene and assign settings
        SpawnArea[] spawnAreas = FindObjectsOfType<SpawnArea>();
        for (int i = 0; i < spawnAreas.Length; i++)
        {
            if (i < spawnAreasSettings.Length)
            {
                SpawnArea spawnArea = spawnAreas[i];
                SpawnSettings settings = spawnAreasSettings[i];

                // Initialize spawning and cooldown states
                isSpawning[spawnArea] = false;
                isCooldown[spawnArea] = false;

                // Subscribe to events
                spawnArea.onPlayerEnter.AddListener(() => StartSpawnCoroutine(spawnArea, settings));
                spawnArea.onPlayerExit.AddListener(() => StartCooldownCoroutine(spawnArea, settings.cooldownTime));
            }
            else
            {
                Debug.LogWarning("Not enough SpawnSettings defined for all SpawnAreas.");
            }
        }
    }

    // Start the spawn coroutine for the specific SpawnArea
    private void StartSpawnCoroutine(SpawnArea spawnArea, SpawnSettings settings)
    {
        if (!isSpawning[spawnArea] && !isCooldown[spawnArea])
        {
            StartCoroutine(SpawnEnemies(spawnArea, settings));
        }
    }

    // Start the cooldown coroutine for the specific SpawnArea
    private void StartCooldownCoroutine(SpawnArea spawnArea, float cooldownTime)
    {
        if (!isCooldown[spawnArea])
        {
            StartCoroutine(Cooldown(spawnArea, cooldownTime));
        }
    }

    // Coroutine to handle spawning enemies in waves for the specific SpawnArea
    private IEnumerator SpawnEnemies(SpawnArea spawnArea, SpawnSettings settings)
    {
        isSpawning[spawnArea] = true;

        for (int wave = 0; wave < settings.numberOfWaves; wave++)
        {
            for (int i = 0; i < settings.enemiesPerWave; i++)
            {
                SpawnEnemy(settings.enemyKey, spawnArea);
                yield return new WaitForSeconds(0.5f); // Optional: Time between enemy spawns within a wave
            }

            yield return new WaitForSeconds(settings.timeBetweenWaves);
        }

        isSpawning[spawnArea] = false;
    }

    // Coroutine to handle cooldown after spawning for the specific SpawnArea
    private IEnumerator Cooldown(SpawnArea spawnArea, float cooldownTime)
    {
        isCooldown[spawnArea] = true;
        yield return new WaitForSeconds(cooldownTime);
        isCooldown[spawnArea] = false;
    }

    // Method to spawn a single enemy at a random position within the SpawnArea
    private void SpawnEnemy(string enemyKey, SpawnArea spawnArea)
    {
        GameObject enemy = PoolManager.Instance.GetPooledObject(enemyKey);
        if (enemy != null)
        {
            Vector3 spawnPosition = GetRandomPositionWithinBox(spawnArea.GetComponent<BoxCollider>());
            enemy.transform.position = spawnPosition;
            enemy.transform.rotation = Quaternion.identity;

            // Get original scale
            Vector3 originalScale = enemy.transform.localScale;

            // Set scale to zero or a small value
            enemy.transform.localScale = Vector3.zero;

            enemy.SetActive(true);

            // Animate the scale from zero to the original scale
            enemy.transform.DOScale(originalScale, 0.5f).SetEase(Ease.OutBounce); // You can tweak duration and easing as needed

            // Optionally, initialize the enemy here (e.g., reset health, activate behaviors)
        }
        else
        {
            Debug.LogWarning($"No available enemy with key '{enemyKey}' in the pool.");
        }
    }

    // Generate a random position within the BoxCollider on the XZ plane
    private Vector3 GetRandomPositionWithinBox(BoxCollider box)
    {
        Vector3 center = box.bounds.center;
        Vector3 size = box.bounds.size;
        Vector3 randomPos = new Vector3(
            UnityEngine.Random.Range(center.x - size.x / 2, center.x + size.x / 2),
            center.y, // Assuming Y is up; adjust if necessary
            UnityEngine.Random.Range(center.z - size.z / 2, center.z + size.z / 2)
        );

        return randomPos;
    }
}
