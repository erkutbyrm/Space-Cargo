using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private Color _gemColor;

    [SerializeField] private GameObject[] _asteroidPrefabs;
    [SerializeField] private GameObject _gemPrefab;
    [SerializeField] private GameObject _cargoPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _speedBoostPrefab;
    [SerializeField] private LevelController _levelController;

    private int _asteroidSpawnCount = 0;
    private int _gemSpawnCount = 0;
    private int _cargoSpawnCount = 0;
    private int _enemySpawnCount = 0;
    private int _speedBoostSpawnCount = 0;
    [SerializeField] private float _spawnSpaceInterval;

    private readonly Vector2 NOT_FOUND_VECTOR2 = new Vector2(-99f,-99f);
    
    public void Initialize(LevelScriptableObject currentLevel, out int totalSpawnedCargo)
    {
        SpawnShip(DataController.Instance.PlayerData.CurrentShipID);
        totalSpawnedCargo = 0;
        InitializeFromLocal(currentLevel);
        while (_asteroidSpawnCount > 0 || _cargoSpawnCount > 0 || _gemSpawnCount > 0 || _enemySpawnCount > 0 || _speedBoostSpawnCount > 0)
        {
            if(_cargoSpawnCount > 0)
            {
                if (SpawnCargo())
                {
                    totalSpawnedCargo++;
                }
                _cargoSpawnCount--;
            }
            
            if(_asteroidSpawnCount > 0)
            {
                SpawnAsteroid();
                _asteroidSpawnCount--;
            }

            if(_gemSpawnCount > 0)
            {
                SpawnGem();
                _gemSpawnCount--;
            }

            if(_enemySpawnCount > 0)
            {
                SpawnEnemy();
                _enemySpawnCount--;
            }

            if(_speedBoostSpawnCount > 0)
            {
                SpawnSpeedBoost();
                _speedBoostSpawnCount--;
            }
        }
    }
    public GameObject SpawnShip(int ID)
    {
        return Instantiate(DataController.Instance.PlayerData.CurrentShipData.SpaceShipScriptableObject.Prefab,
            new Vector3(13, 6, 0), Quaternion.identity);
    }

    private void SpawnAsteroid()
    {
        Vector2 spawnPos = FindSpawnPosition();
        if (spawnPos == NOT_FOUND_VECTOR2)
        {
            return;
        }

        int index = Random.Range(0, _asteroidPrefabs.Length);
        GameObject.Instantiate(_asteroidPrefabs[index], spawnPos, Quaternion.identity, transform);
    }

    private GameObject SpawnCargo()
    {
        Vector2 spawnPos = FindSpawnPosition();
        if (spawnPos == NOT_FOUND_VECTOR2)
        {
            return null;
        }

        return GameObject.Instantiate(_cargoPrefab, spawnPos, Quaternion.identity, transform);
    }

    private void SpawnGem()
    {
        Vector2 spawnPos = FindSpawnPosition();
        if(spawnPos == NOT_FOUND_VECTOR2)
        {
            return;
        }

        GameObject newGem = GameObject.Instantiate(_gemPrefab, spawnPos, Quaternion.identity, transform);
        _gemPrefab.transform.GetComponent<SpriteRenderer>().color = _gemColor;
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPos = FindSpawnPosition();
        if (spawnPos == NOT_FOUND_VECTOR2)
        {
            return;
        }

        GameObject.Instantiate(_enemyPrefab, spawnPos, Quaternion.identity, transform);
    }

    private void SpawnSpeedBoost()
    {
        Vector2 spawnPos = FindSpawnPosition();
        if (spawnPos == NOT_FOUND_VECTOR2)
        {
            return;
        }

        GameObject.Instantiate(_speedBoostPrefab, spawnPos, Quaternion.identity, transform);
    }

    private bool isSpawnable(Vector2 expectedSpawnPosition)
    {
        Collider2D collider2D = Physics2D.OverlapCircle(expectedSpawnPosition, _spawnSpaceInterval);
        if(collider2D != null )
        {
            return false;
        }
        return true;
    }

    private Vector2 FindSpawnPosition()
    {
        Vector2 spawnPos = new Vector2();
        int tryLimit = 20;
        do
        {
            spawnPos.x = Random.Range(0, _levelController.mapLimits.x);
            spawnPos.y = Random.Range(0, _levelController.mapLimits.y);
        } while (!isSpawnable(spawnPos) && --tryLimit > 0);
        if (isSpawnable(spawnPos))
        {
            return spawnPos;
        }
        else
        {
            return NOT_FOUND_VECTOR2;
        }
    }

    private void InitializeFromLocal(LevelScriptableObject currentLevel)
    {
        _asteroidSpawnCount = currentLevel.AsteroidCount;
        _gemSpawnCount = currentLevel.GemCount;
        _cargoSpawnCount = currentLevel.CargoCount;
        _enemySpawnCount = currentLevel.EnemyCount;
        _speedBoostSpawnCount = currentLevel.SpeedBoostCount;
        _gemColor = currentLevel.GemColour;
    }
}
