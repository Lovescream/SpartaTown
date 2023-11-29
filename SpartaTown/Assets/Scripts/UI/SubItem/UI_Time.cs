using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Time : MonoBehaviour {

    private TextMeshProUGUI txtTime;

    void Awake() {
        txtTime = this.GetComponent<TextMeshProUGUI>();
    }
    void Update() {
        txtTime.text = DateTime.Now.ToString("HH:mm");
    }

}