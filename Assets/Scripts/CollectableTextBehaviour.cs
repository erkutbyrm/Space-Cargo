using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectableTextBehaviour : MonoBehaviour
{
    private TextMeshProUGUI _textGemsCount;
    private TextMeshProUGUI _textCargosCount;
    private void Start()
    {
        Transform gemTextTransform = transform.Find(Constants.NAME_GEM_TEXT);
        Transform cargoTextTransform = transform.Find(Constants.NAME_CARGO_TEXT);
        _textGemsCount = gemTextTransform.GetComponent<TextMeshProUGUI>();
        _textCargosCount = cargoTextTransform.GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        CollectableDataController.OnGemCountIncrease += UpdateGemCounterText;
        CollectableDataController.OnCargoCountIncrease += UpdateCargoCounterText;
    }

    private void OnDisable()
    {
        CollectableDataController.OnGemCountIncrease -= UpdateGemCounterText;
        CollectableDataController.OnCargoCountIncrease -= UpdateCargoCounterText;
    }

    private void UpdateGemCounterText(int count)
    {
        _textGemsCount.text = "Gems: " + count.ToString();
    }

    private void UpdateCargoCounterText(int count)
    {
        _textCargosCount.text = "Cargos: " + count.ToString();
    }
}
