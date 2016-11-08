using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScreenWin : MonoBehaviour {

    public GameObject[] stars;

    public Text textTitle;


    public void Show(int errors) {
        textTitle.text = (PlayerPrefs.GetString("lang") == "fr" ? "Bravo!" : "Wonderful!");

        // Remove all previous stars
        for (int i=0; i<stars.Length; i++) {
            stars[i].SetActive(false);
        }
        // Show all stars depending on the number of tries
        for (int i=0; i<stars.Length - errors; i++) {
            stars[i].SetActive(true);
        }
        // Show the Win popup
        gameObject.SetActive(true);
        SoundEngine.instance.PlaySound(SoundEngine.instance.audioWin);
    }


    public void Close() {
        gameObject.SetActive(false);
    }


}
