using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateTest : MonoBehaviour
{
    StateManager Scenebutton;
    // Start is called before the first frame update
    void Start()
    {
        Scenebutton = StateManager.Instance;
    }

    private void OnMouseDown()
    {
        Scenebutton.LoadSubScene("Day1Scene");
    }

}
