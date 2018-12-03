using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesChanger : MonoBehaviour {

    private int maxP;

    public void SetMaxPlayers(int maxPlayers)
    {
        maxP = maxPlayers;
    }

    public void ChangeScene(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
