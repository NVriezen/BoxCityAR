using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersionText : MonoBehaviour {

    private void Awake()
    {
        this.GetComponent<UnityEngine.UI.Text>().text = "Version " + Application.version;
    }
}
