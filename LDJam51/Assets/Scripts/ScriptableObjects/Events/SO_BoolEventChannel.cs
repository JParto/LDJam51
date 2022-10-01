using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Events/Bool Event Channel")]
public class SO_BoolEventChannel : SO_DescriptionBase
{
    public UnityAction<bool> onEventRaised;

    public void RaiseEvent(bool val){
        if(onEventRaised != null){
            onEventRaised.Invoke(val);
        }
    }
}
