using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler {

    public static GameObject itemBeginDragged;
    public static Transform canvas;

    public bool isDragable = true;
    public Sprite disable;

    private Vector3 startPosition;
    private Transform startParent;     // The original parent of the item
    private Transform globalParent;    // The container to place the dragged object


    public void OnPointerDown(PointerEventData eventData) {
        if (!isDragable) {
            return;
        }

        Debug.Log("OnPointerDown...");

        SoundEngine.instance.PlaySound(SoundEngine.instance.audioDrag);

        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }


    public void OnBeginDrag(PointerEventData eventData) {
        if (!isDragable) {
            return;
        }

        Debug.Log("OnBeginDrag...");

        itemBeginDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        globalParent = transform.parent.parent.gameObject.GetComponent<Container>().canvas.transform;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        transform.SetParent(globalParent);
    }


    public void OnDrag(PointerEventData eventData) {
        if (!isDragable) {
            return;
        }

        Debug.Log("OnDrag...");

        transform.position = Input.mousePosition;
    }


    public void OnEndDrag(PointerEventData eventData) {
        if (!isDragable) {
            return;
        }

        // SoundEngine.instance.PlaySound(SoundEngine.instance.audioDrag);

        itemBeginDragged = null;

        // If the parent is still the global parent, reset it to the start parent
        if (transform.parent == globalParent) {
            transform.SetParent(startParent);
        }
        // If the item is not in a slot, put it back where it was
        if (transform.parent == startParent) {
            transform.position = startPosition;
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }


    public void OnPointerUp(PointerEventData eventData) {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public Transform GetParent() {
        return startParent;
    }


}
