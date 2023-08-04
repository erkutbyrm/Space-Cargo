using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipBehaviour : ShipBehaviour
{
    public override void Die()
    {
        base.Die();
        ResetSavedData();
        LevelController levelController = GameObject.FindObjectOfType<LevelController>();
        levelController.DisplayEndGame();
    }

    private void ResetSavedData()
    {
        PlayerPrefs.DeleteKey(Constants.PREFS_KEY_CURRENT_QUEST);
    }
}
