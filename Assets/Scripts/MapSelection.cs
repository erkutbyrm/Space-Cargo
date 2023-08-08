using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Newtonsoft.Json;


public class MapSelection : MonoBehaviour
{
    [SerializeField] private List<LevelScriptableObject> _levelList;// = new List<LevelScriptableObject>();
    //[SerializeField] private GameObject _buttonMap;
    [SerializeField] private Transform _buttonMapContainer;
    [SerializeField] private Image _background;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _levelList.Count; i++)
        {
            Debug.Log(_levelList[i].name);
        }
        //foreach (LevelScriptableObject level in _levelList)
        //{
        //    Debug.Log(level.LevelName);
        //    GameObject newButton = GameObject.Instantiate(_buttonMap, _buttonMapContainer);
        //    newButton.transform.GetComponentInChildren<TextMeshProUGUI>().text = level.LevelName;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel(string name)
    {
        switch (name)
        {
            case "Earth":
                SceneTypeChange(name);
                break;
            case "Saturn":
                SceneTypeChange(name);
                break;
            default:
                SceneTypeChange("Earth");
                break;
        }
        SceneManager.LoadScene(Constants.SCENE_GAME);
        
        //Debug.Log("click");
        //Debug.Log(_levelList[0].BackgroundImage.transform.GetComponent<SpriteRenderer>().sprite.name);
        //TODO: load level with desired specifications in scObject
        //_background.sprite = _levelList[0].BackgroundSprite;
        //_background.sprite = _levelList[0].BackgroundSprite;

        //SceneManager.LoadScene(Constants.SCENE_GAME);
    }

    public void OnHover(string name)
    {
        Debug.Log("hover");
        //_levelList[1].BackgroundImage.transform.GetComponent<SpriteRenderer>().sprite.name;
        switch (name)
        {
            case "Earth":
                _background.sprite = _levelList[0].BackgroundSprite;
                break;
            case "Saturn": 
                _background.sprite = _levelList[1].BackgroundSprite;
                break;
            default: Debug.Log("default");
                break;
        }
        //TODO: these are null 
    }

    private void SceneTypeChange(string levelName)
    {
        Debug.Log(levelName);
        string incomingJsonString = PlayerPrefs.GetString(Constants.PREFS_PLAYER_DATA, string.Empty);
        Debug.Log(incomingJsonString);

        PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(incomingJsonString, settings: new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
        });
        Debug.Log(playerData.LevelName +" "+playerData.SpaceShipName);
        playerData.LevelName = levelName;
        Debug.Log(playerData.LevelName + " " + playerData.SpaceShipName);

        string outgoingJsonString = JsonConvert.SerializeObject(playerData, settings: new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
        });
        PlayerPrefs.SetString(Constants.PREFS_PLAYER_DATA, outgoingJsonString);
    }
}
