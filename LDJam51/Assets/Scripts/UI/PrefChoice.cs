using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PrefChoice : MonoBehaviour
{
    [SerializeField] private CreatePreference _preferenceCreator;
    [SerializeField] private int _enumIndex;
    [SerializeField] List<TextMeshProUGUI> _choices;
    private int _choiceIndex;

    public void PickChoice(int index){
        for (int i = 0; i < _choices.Count; i++)
        {
            if (i == index){
                _choices[i].fontStyle = FontStyles.Normal;
            } else {
                _choices[i].fontStyle = FontStyles.Strikethrough;
            }
        }
        _preferenceCreator.SetEnums(_enumIndex, index);
        _choiceIndex = index;
    }
}
