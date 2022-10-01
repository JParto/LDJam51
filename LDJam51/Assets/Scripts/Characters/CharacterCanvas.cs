using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCanvas : MonoBehaviour
{
    [SerializeField] private CharacterCard _characterCard;
    [SerializeField] private GameObject _textParent;
    [SerializeField] private CharacterTextBubble _textPrefab;

    #region TextBubbles
        public bool EvaluatePrefIndex(int index){
            return _characterCard.EvaluatePrefIndex(index);
        }
    #endregion
}
