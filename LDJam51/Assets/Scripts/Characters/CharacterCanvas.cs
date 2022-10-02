using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCanvas : MonoBehaviour
{
    [SerializeField] private CharacterCard _characterCard;
    [SerializeField] private GameObject _textParent;
    [SerializeField] private CharacterTextBubble _textPrefab;

    #region TextBubbles
        public void SpawnTextBubble(int index, string t){ // text should go away
            CharacterTextBubble bubble = Instantiate(_textPrefab, _textParent.transform);
            // Debug.LogWarning("TODO: dictionary voor textbubbles voor uitkiezen van strings");
            string text = t;
            bubble.Spawn(index, text);
        }

        public bool EvaluatePrefIndex(int index){
            return _characterCard.EvaluatePrefIndex(index);
        }

    #endregion
}
