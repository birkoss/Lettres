using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public static GameObject itemBeginDragged;

    private Vector3 startPosition;
    private Transform startParent;     // The original parent of the item
    private Transform globalParent;    // The global parent to be always on top

    public void OnBeginDrag(PointerEventData eventData) {
        itemBeginDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        globalParent = transform.parent.parent.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        transform.SetParent(globalParent);
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }


    public void OnDrag(PointerEventData eventData) {
        transform.position = Input.mousePosition;
    }


    public void OnEndDrag(PointerEventData eventData) {
        itemBeginDragged = null;
        transform.localScale = new Vector3(1f, 1f, 1f);

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


}
