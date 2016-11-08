using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler {

    public static GameObject itemBeginDragged;
    public static Transform canvas;

    public bool isDragable = true;

    public Sprite spriteDisabled;
    public Sprite spriteError;

    private Vector3 startPosition;
    private Transform startParent;     // The original parent of the item
    private Transform globalParent;    // The container to place the dragged object

    private bool hasDragged;            // To know if a drag occured


    public void OnPointerDown(PointerEventData eventData) {
        if (!isDragable) {
            return;
        }
        hasDragged = false;

        SoundEngine.instance.PlaySound(SoundEngine.instance.audioDrag);

        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }


    public void OnBeginDrag(PointerEventData eventData) {
        if (!isDragable) {
            return;
        }
        hasDragged = true;

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

        if (!isDragable) {
            return;
        }

        // If the letter has not been dragged, try to auto fill it
        if (!hasDragged) {
            // Get the other container (depending on our slot parent)
            GameObject canvas = GameObject.Find("Canvas");
            GameObject other_container = (canvas.GetComponent<Game>().containerOrigin.gameObject == transform.parent.parent.gameObject ? canvas.GetComponent<Game>().containerDestination.gameObject : canvas.GetComponent<Game>().containerOrigin.gameObject);

            // First the first available spot
            for (int i=0; i<other_container.transform.childCount; i++) {
                if (other_container.transform.GetChild(i).childCount == 0) {
                    transform.SetParent(other_container.transform.GetChild(i));
                    GetComponent<Letter>().ChangeState();
                    ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x,y) => x.HasChanged(gameObject));
                    break;
                }
            }
        }
    }


    public Transform GetParent() {
        return startParent;
    }


}
