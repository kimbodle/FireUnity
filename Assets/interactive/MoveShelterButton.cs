using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShelterButton : MonoBehaviour
{
    StateManager Scenebutton;
    // Start is called before the first frame update
    void Start()
    {
        Scenebutton = StateManager.Instance;
    }

    public void OnMoveShelterButton()
    {
        Scenebutton.LoadSubScene("ShleterScene");
    }
}
