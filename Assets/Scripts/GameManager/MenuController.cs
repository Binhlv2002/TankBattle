using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void OnPlayButtonPressed()
    {
        SceneLoader.LoadScene("SampleScene");
    }
}
