using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyAR : MonoBehaviour {

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
