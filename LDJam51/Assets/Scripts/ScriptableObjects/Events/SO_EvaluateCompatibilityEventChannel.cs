using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Events/Evaluation Event Channel")]
public class SO_EvaluateCompatibilityEventChannel : SO_DescriptionBase
{
    public UnityAction<Preferences> onEventRaised;

    public void RaiseEvent(Preferences prefs){
        if(onEventRaised != null){
            onEventRaised.Invoke(prefs);
        }
    }
}
