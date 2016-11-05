using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour {

    public GameObject confirmation;

    private delegate void ConfirmationCallback();
    private ConfirmationCallback callback;


    public void PlayGame() {
        SceneManager.LoadScene("game");
    }


    public void GoHome() {
        confirmation.GetComponent<Confirmation>().ChangeText("Es-tu sur de vouloir retourner au menu?");
        callback = LoadMainMenu;
    }


    public void RestartGame() {
        confirmation.GetComponent<Confirmation>().ChangeText("Es-tu sur de vouloir recommencer ce mot?");
        callback = RestartCurrentLevel;
    }


    public void RestartCurrentLevel() {
        ExecuteEvents.ExecuteHierarchy<IResetWord>(gameObject, null, (x,y) => x.ResetWord());
    }

    public void Ok() {
        confirmation.GetComponent<Confirmation>().Close();
        callback();
    }


    public void Cancel() {
        confirmation.GetComponent<Confirmation>().Close();
    }


    public void LoadMainMenu() {
        SceneManager.LoadScene("main");
    }


    public void LoadNextLevel() {
        ExecuteEvents.ExecuteHierarchy<IChangeWord>(gameObject, null, (x,y) => x.ChangeWord());
    }


}
