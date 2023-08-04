using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;
    public virtual void Die()
    {
        //ResetSavedData();
        //LevelController levelController = GameObject.FindObjectOfType<LevelController>();
        //levelController.DisplayEndGame();
        GameObject explosion = GameObject.Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Animator explosionAnimator = explosion.GetComponent<Animator>();
        float delay = explosionAnimator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(explosion, delay);
        Destroy(gameObject);
    }



    //private void ResetSavedData()
    //{
    //    PlayerPrefs.DeleteKey(Constants.PREFS_KEY_CURRENT_QUEST);
    //}
}
