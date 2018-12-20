using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour {

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

    IEnumerator SpawnWait()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject newBut = Instantiate(button, this.transform);
        newBut.GetComponentInChildren<UnityEngine.UI.Text>().text = "Your Room";
        newBut.AddComponent<BlueButton>();
        
        //yield return new WaitForSeconds(3);
        //Instantiate(button, this.transform);
        //button.GetComponentInChildren<UnityEngine.UI.Text>().text = "Room 2";
        //if (maxP == 3)
        //{
        //    yield return new WaitForSeconds(0.2f);
        //    Instantiate(button, this.transform);
        //    button.GetComponentInChildren<UnityEngine.UI.Text>().text = "Player 3";
        //}
        //if (maxP == 4)
        //{
        //    yield return new WaitForSeconds(0.3f);
        //    Instantiate(button, this.transform);
        //    button.GetComponentInChildren<UnityEngine.UI.Text>().text = "Player 4";
        //}
        //yield return new WaitForSeconds(2f);
        //sceneloader.LoadScene("Game");
    }
}
