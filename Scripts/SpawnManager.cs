using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Vector3 enemyHive;

    public float waveCooldownMin = 5f;
    public float waveCooldownMax = 10f;
    public int waveCount = 1;
    
    private float waveCooldown;
    private float timeSinceWave = 0f;
    private int enemyCount;
    
    private GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        waveCooldown = Random.Range(waveCooldownMin, waveCooldownMax);
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        // We only want to coutn down wave count once enemies are 0.
        if (enemyCount == 0)
        {
            timeSinceWave += Time.deltaTime;
        }

        if (timeSinceWave > waveCooldown && enemyCount == 0)
        {
            SpawnWave();
            waveCount++;
            // Change our wave cooldown
            waveCooldown = Random.Range(waveCooldownMin, waveCooldownMax);
            timeSinceWave = 0f;
        }
        
    }

    void SpawnWave()
    {
        for (int i = 0; i < waveCount; i++)
        {
            Vector3 spawnPos = new Vector3(enemyHive.x, 0, enemyHive.z - i);
            Instantiate(enemyPrefab, spawnPos, enemyPrefab.transform.rotation);
        }

        // Update our UI.
        manager.AddWave();
    }
}
