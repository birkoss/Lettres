using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Win : MonoBehaviour {


    public GameObject[] stars;


    public void Show(int errors) {
        Debug.Log(errors);
        for (int i=0; i<stars.Length; i++) {
            stars[i].SetActive(false);
        }
        for (int i=0; i<stars.Length - errors; i++) {
            stars[i].SetActive(true);
        }

        gameObject.SetActive(true);
    }


    public void Close() {
        gameObject.SetActive(false);
    }


}
