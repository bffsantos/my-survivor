using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Void Event", menuName = "Scriptable Objects/Events/Void Event")]
public class GameEvent : ScriptableObject
{
    public event UnityAction gameEvent;

    public void Broadcast()
    {
        gameEvent?.Invoke();
    }

}