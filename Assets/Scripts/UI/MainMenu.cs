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


    public void PlayGame(int new_mode) {
        SoundEngine.instance.PlaySound(SoundEngine.instance.audioClick);

        mode = new_mode;
        SceneManager.LoadScene("game");
    }


    public void GoHome() {
        SoundEngine.instance.PlaySound(SoundEngine.instance.audioClick);

        confirmation.GetComponent<Confirmation>().ChangeText("Es-tu sur de vouloir retourner au menu?");
        callback = LoadMainMenu;
    }


    public void RestartGame() {
        SoundEngine.instance.PlaySound(SoundEngine.instance.audioClick);

        confirmation.GetComponent<Confirmation>().ChangeText("Es-tu sur de vouloir recommencer ce mot?");
        callback = RestartCurrentLevel;
    }


    public void RestartCurrentLevel() {
        ExecuteEvents.ExecuteHierarchy<IResetWord>(gameObject, null, (x,y) => x.ResetWord());
    }

    public void Ok() {
        SoundEngine.instance.PlaySound(SoundEngine.instance.audioClick);

        confirmation.GetComponent<Confirmation>().Close();
        callback();
    }


    public void Cancel() {
        SoundEngine.instance.PlaySound(SoundEngine.instance.audioClick);

        confirmation.GetComponent<Confirmation>().Close();
    }


    public void LoadMainMenu() {
        SoundEngine.instance.PlaySound(SoundEngine.instance.audioClick);
        SceneManager.LoadScene("main");
    }


    public void LoadNextLevel() {
        SoundEngine.instance.PlaySound(SoundEngine.instance.audioClick);
        ExecuteEvents.ExecuteHierarchy<IChangeWord>(gameObject, null, (x,y) => x.ChangeWord());
    }



}
