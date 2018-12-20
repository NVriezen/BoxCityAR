using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueButton : MonoBehaviour {


    public void OnCLick()
    {
        this.GetComponent<UnityEngine.UI.Image>().color = new Color(0,0,1);
    }

    public void OnEnable()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnCLick);
    }
}
