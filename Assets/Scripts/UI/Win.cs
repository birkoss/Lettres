using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Win : MonoBehaviour {


    public GameObject[] stars;


    public void Show(int errors) {
        Debug.Log(errors);
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
    }


    public void Close() {
        gameObject.SetActive(false);
    }


}
