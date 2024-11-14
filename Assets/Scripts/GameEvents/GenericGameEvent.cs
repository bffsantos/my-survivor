using UnityEngine;
using UnityEngine.Events;

public class GameEvent<T> : ScriptableObject
{
    public event UnityAction<T> gameEvent;

    public void Broadcast(T parameter)
    {
        gameEvent?.Invoke(parameter);
    }
}
