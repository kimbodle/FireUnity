using System.Collections.Generic;
using UnityEngine;

public abstract class DayController : MonoBehaviour
{
    protected Dictionary<string, bool> gameState = new Dictionary<string, bool>();
    public abstract void Initialize(int currentTask);
    public abstract void CompleteTask(int task);
    public abstract bool IsDayComplete(int currentTask);

    public Dictionary<string, bool> GetGameState()
    {
        return new Dictionary<string, bool>(gameState);
    }

    public void SetGameState(Dictionary<string, bool> state)
    {
        gameState = new Dictionary<string, bool>(state);
    }
}
