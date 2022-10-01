using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterTextBubble : MonoBehaviour
{
    CharacterCanvas _canvas;
    [SerializeField] int relatedPreferenceIndex;

    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Color correctColor;
    [SerializeField] private Color incorrectColor;


    void Awake(){
        _canvas = transform.root.GetComponent<CharacterCanvas>();
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void EvaluateTrue(){
        bool b = _canvas.EvaluatePrefIndex(relatedPreferenceIndex);
        Color c = b ? correctColor : incorrectColor;
        _text.color = c;
    }

    public void EvaluateFalse(){
        _canvas.EvaluatePrefIndex(relatedPreferenceIndex);
    }
}
