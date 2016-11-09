using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour {

    public static int mode;

    public GameObject confirmation;

    private delegate void ConfirmationCallback();
    private ConfirmationCallback callback;

    public void Start() {
        string lang = PlayerPrefs.GetString("lang");
        if (lang == null) {
            lang = "fr";
            PlayerPrefs.SetString("lang", lang);
        }
    }


    public void PlayGame(int new_mode) {
        SoundEngine.instance.PlaySound(SoundEngine.instance.audioClick);

        mode = new_mode;
        GetComponent<Game>().screenGame.SetActive(true);
        GetComponent<Game>().screenMenu.SetActive(false);
        GetComponent<Game>().Init();

        //SceneManager.LoadScene("game");

    }


    public void GoHome() {
        SoundEngine.instance.PlaySound(SoundEngine.instance.audioClick);

        confirmation.GetComponent<ScreenConfirmation>().ChangeText(PlayerPrefs.GetString("lang") == "fr" ? "Es-tu sur de vouloir retourner au menu?" : "Are you sure you want to return to the menu?");
        callback = LoadMainMenu;
    }


    public void RestartGame() {
        SoundEngine.instance.PlaySound(SoundEngine.instance.audioClick);

        confirmation.GetComponent<ScreenConfirmation>().ChangeText(PlayerPrefs.GetString("lang") == "fr" ? "Es-tu sur de vouloir recommencer ce mot?" : "Are you sure you want to re-try this word?");
        callback = RestartCurrentLevel;
    }


    public void RestartCurrentLevel() {
        ExecuteEvents.ExecuteHierarchy<IResetWord>(gameObject, null, (x,y) => x.ResetWord());
    }

    public void Ok() {
        SoundEngine.instance.PlaySound(SoundEngine.instance.audioClick);

        confirmation.GetComponent<ScreenConfirmation>().Close();
        callback();
    }


    public void ChangeLanguage() {
        SoundEngine.instance.PlaySound(SoundEngine.instance.audioClick);

        PlayerPrefs.SetString("lang", (PlayerPrefs.GetString("lang") == "fr" ? "en" : "fr"));
        SceneManager.LoadScene("game");
    }


    public void Cancel() {
        SoundEngine.instance.PlaySound(SoundEngine.instance.audioClick);

        callback = DisablePopup;
        StartCoroutine(ClosePopup(confirmation.GetComponent<Animator>()));
    }


    public void LoadMainMenu() {
        SoundEngine.instance.PlaySound(SoundEngine.instance.audioClick);

        GetComponent<Game>().screenGame.SetActive(false);
        GetComponent<Game>().screenWin.SetActive(false);
        GetComponent<Game>().screenMenu.SetActive(true);
    }


    public void LoadNextLevel() {
        SoundEngine.instance.PlaySound(SoundEngine.instance.audioClick);

        callback = PrepareNextWord;
        StartCoroutine(ClosePopup(GetComponent<Game>().screenWin.GetComponent<Animator>()));
    }

    private IEnumerator ClosePopup(Animator anim) {
        anim.Play("Sliding Out");
        yield return new WaitForSeconds(0.6f);
        callback();
    }

    private void DisablePopup() {
        confirmation.SetActive(false);
    }

    private void PrepareNextWord() {
        ExecuteEvents.ExecuteHierarchy<IChangeWord>(gameObject, null, (x,y) => x.ChangeWord());
    }
}
