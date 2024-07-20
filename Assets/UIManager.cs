using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject LoginUI;
    // Start is called before the first frame update
    /*void Start()
    {
        LoginUI.SetActive(false);
    }*/

    public void openLoginUI()
    {
        LoginUI.SetActive(true);
    }

    public void closeLoginUI()
    {
        LoginUI.SetActive(false);
    }
}
