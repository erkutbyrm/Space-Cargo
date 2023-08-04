using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceStationBehaviour : MonoBehaviour
{
    private LevelController _levelController;
    private void Start()
    {
        _levelController = GameObject.FindAnyObjectByType<LevelController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Constants.TAG_SPACESHIP))
        {
            _levelController.TryWin();
        }
    }
}
