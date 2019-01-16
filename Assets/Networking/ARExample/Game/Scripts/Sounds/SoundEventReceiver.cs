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
        EventManager.StartListening("CATNIP_STOLEN", OnStolen);
    }

    private void OnDisable()
    {
        EventManager.StopListening("SPAWN_CATNIP", OnSpawned);
        EventManager.StopListening("CATNIP_GOT", OnGotCatnip);
        EventManager.StopListening("CATNIP_STOLEN", OnStolen);
    }

    public void OnSpawned()
    {
        StartCoroutine(StartPurring());
    }

    public void OnGotCatnip()
    {
        catnipSpawn.Play();
    }

    public void OnStolen()
    {
        catnipStolen.Play();
    }

    IEnumerator StartPurring()
    {
        yield return new WaitForSeconds(0.1f);
        transform.position = GameObject.FindWithTag("Catnip").transform.position;
        catnipPurr.Play();
        yield return new WaitForSeconds(5);
        catnipPurr.Stop();
    }
}
