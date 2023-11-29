using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    void Start() {
        this.transform.GetComponent<Player>().SetInfo("Blossom", "Character00");
    }

}