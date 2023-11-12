using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneJump : MonoBehaviour
{
    public string scenename;
    public void SwitchScene()
    {
        SceneManager.LoadScene(scenename);
    }
}
