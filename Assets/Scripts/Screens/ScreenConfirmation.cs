using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenConfirmation : MonoBehaviour {


    public Text text;


    public void ChangeText(string new_text) {
        text.text = new_text;
        gameObject.SetActive(true);
    }


    public void Close() {
        gameObject.SetActive(false);
    }
}
