using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITimer : MonoBehaviour {
    public TextMeshProUGUI theText;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        theText.text = (Mathf.FloorToInt(Time.time) / 60).ToString("D2") + ":" + (Mathf.FloorToInt(Time.time) % 60).ToString("D2");
    }
}
