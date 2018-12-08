using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButton : MonoBehaviour {

    public GameObject button;
    public int maxP;
    public SceneLoader sceneloader;
    public bool onStart = true;

    private void Start()
    {
        if (onStart)
        {
            StartCoroutine(SpawnWait());
        }
    }

    public void OnClick()
    {
        StartCoroutine(SpawnWait());
    }

    public void onJoin()
    {
        sceneloader.LoadScene("Playtest_2_waitingroom");
    }

    public void onCreate()
    {
        if (this.transform.childCount > 1)
        {
            sceneloader.LoadScene("Game");
        }
        else
        {
            StartCoroutine(SpawnNOWait());
        }
    }

    IEnumerator SpawnWait()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(button, this.transform);
        button.GetComponentInChildren<UnityEngine.UI.Text>().text = "You";
        yield return new WaitForSeconds(3);
        Instantiate(button, this.transform);
        button.GetComponentInChildren<UnityEngine.UI.Text>().text = "Player 2";
        if (maxP == 3)
        {
            yield return new WaitForSeconds(0.2f);
            Instantiate(button, this.transform);
            button.GetComponentInChildren<UnityEngine.UI.Text>().text = "Player 3";
        }
        if (maxP == 4)
        {
            yield return new WaitForSeconds(0.3f);
            Instantiate(button, this.transform);
            button.GetComponentInChildren<UnityEngine.UI.Text>().text = "Player 4";
        }
        yield return new WaitForSeconds(2f);
        sceneloader.LoadScene("Game");
    }

    IEnumerator SpawnNOWait()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(button, this.transform);
        button.GetComponentInChildren<UnityEngine.UI.Text>().text = "You";
        yield return new WaitForSeconds(3);
        Instantiate(button, this.transform);
        button.GetComponentInChildren<UnityEngine.UI.Text>().text = "Player 2";
        if (maxP == 3)
        {
            yield return new WaitForSeconds(0.2f);
            Instantiate(button, this.transform);
            button.GetComponentInChildren<UnityEngine.UI.Text>().text = "Player 3";
        }
        if (maxP == 4)
        {
            yield return new WaitForSeconds(0.3f);
            Instantiate(button, this.transform);
            button.GetComponentInChildren<UnityEngine.UI.Text>().text = "Player 4";
        }
        yield return new WaitForSeconds(2f);
        //sceneloader.LoadScene("Game");
    }
}
