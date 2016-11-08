using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Letter : MonoBehaviour {

    public Sprite spriteDisabled;
    public Sprite spriteError;
    public Sprite spriteNormal;


    public void ChangeState(bool error = false) {
        if (GetComponent<DragHandler>().isDragable) {
            GetComponent<Image>().sprite = (error ? spriteError : spriteNormal);
        }
    }


    public void Disable() {
        GetComponent<Image>().sprite = spriteDisabled;
        GetComponent<DragHandler>().isDragable = false;
    }


}
