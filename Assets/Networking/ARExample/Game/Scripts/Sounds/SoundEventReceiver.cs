using UnityEngine;
using System.Collections;

public class SoundEventReceiver : MonoBehaviour {

    public AudioSource catnipSpawn;
    public AudioSource catnipPurr;
    public AudioSource catnipStolen;

    private void OnEnable()
    {
        EventManager.StartListening("SPAWN_CATNIP", OnSpawned);
        EventManager.StartListening("CATNIP_GOT", OnGotCatnip);
    }

    private void OnDisable()
    {
        EventManager.StopListening("SPAWN_CATNIP", OnSpawned);
        EventManager.StopListening("CATNIP_GOT", OnGotCatnip);
    }

    public void OnSpawned()
    {
        transform.position = GameObject.FindWithTag("Catnip").transform.position;
        catnipSpawn.Play();
        StartCoroutine(StartPurring());
    }

    public void OnGotCatnip()
    {
        catnipStolen.Play();
    }

    IEnumerator StartPurring()
    {
        catnipPurr.Play();
        yield return new WaitForSeconds(5);
        catnipPurr.Stop();
    }
}
