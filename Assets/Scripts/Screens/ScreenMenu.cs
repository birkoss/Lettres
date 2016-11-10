using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScreenMenu : MonoBehaviour {

    public Button buttonEasy;
    public Button buttonNormal;
    public Button buttonHard;
    public Button buttonChangeLanguage;

    public void Start() {
        string lang = PlayerPrefs.GetString("lang");
        if (lang == null) {
            lang = "fr";
            PlayerPrefs.SetString("lang", lang);
        }

        buttonEasy.gameObject.transform.GetChild(0).GetComponent<Text>().text = (lang == "fr" ? "Débutant" : "Easy");
        buttonNormal.gameObject.transform.GetChild(0).GetComponent<Text>().text = (lang == "fr" ? "Apprenti" : "Normal");
        buttonHard.gameObject.transform.GetChild(0).GetComponent<Text>().text = (lang == "fr" ? "Champion" : "Hard");
        buttonChangeLanguage.gameObject.transform.GetChild(0).GetComponent<Text>().text = (lang == "fr" ? "English" : "Français");
    }


    public void Show() {
        gameObject.SetActive(true);
    }

}
