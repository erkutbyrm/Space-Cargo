using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private List<LevelScriptableObject> _levelTypes;
    private Color _gemColor;
    //TODO:

    [SerializeField] private GameObject[] _asteroidPrefabs;
    [SerializeField] private GameObject _gemPrefab;
    [SerializeField] private GameObject _cargoPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _speedBoostPrefab;

    private int _asteroidSpawnCount = 0;
    private int _gemSpawnCount = 0;
    private int _cargoSpawnCount = 0;
    private int _enemySpawnCount = 0;
    private int _speedBoostSpawnCount = 0;

    [SerializeField] private float _spawnSpaceInterval;
    private readonly Vector2 NOT_FOUND_VECTOR2 = new Vector2(-99f,-99f);

    [SerializeField] private LevelController _levelController;
    
    // Start is called before the first frame update
    void Start()
    {
        InitializeFromLocal();
        Debug.Log("ast:" + _asteroidSpawnCount);
        while (_asteroidSpawnCount > 0 || _cargoSpawnCount > 0 || _gemSpawnCount > 0 || _enemySpawnCount > 0 || _speedBoostSpawnCount > 0)
        {
            if(_cargoSpawnCount > 0)
            {
                SpawnCargo();
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

    private void SpawnCargo()
    {
        Vector2 spawnPos = FindSpawnPosition();
        if (spawnPos == NOT_FOUND_VECTOR2)
        {
            return;
        }

        GameObject.Instantiate(_cargoPrefab, spawnPos, Quaternion.identity, transform);
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

    private void InitializeFromLocal()
    {
        string jsonString = PlayerPrefs.GetString(Constants.PREFS_PLAYER_DATA, string.Empty);
        PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(jsonString, settings: new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
        });

        LevelScriptableObject currentLevel = _levelTypes.Find((level) => level.LevelName == playerData.LevelName);

        _asteroidSpawnCount = currentLevel.AsteroidCount;
        _gemSpawnCount = currentLevel.GemCount;
        _cargoSpawnCount = currentLevel.CargoCount;
        _enemySpawnCount = currentLevel.EnemyCount;
        _speedBoostSpawnCount = currentLevel.SpeedBoostCount;
        _gemColor = currentLevel.GemColour;

    }
}
