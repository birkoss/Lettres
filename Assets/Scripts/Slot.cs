using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler {


    public GameObject item {
        get {
            if (transform.childCount > 0) {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }


    public void OnDrop(PointerEventData eventData) {
        // Get the starting position of the dragged item, and swap it with the existing item at this slot
        if (item) {
            // If it's a disabled slot, stop right now
            if (!item.GetComponent<DragHandler>().isDragable) {
                return;
            }
            Transform starting_parent = DragHandler.itemBeginDragged.transform.gameObject.GetComponent<DragHandler>().GetParent();
            item.GetComponent<Letter>().ChangeState();
            item.transform.SetParent(starting_parent);
        }

        DragHandler.itemBeginDragged.transform.SetParent(transform);
        DragHandler.itemBeginDragged.GetComponent<Letter>().ChangeState();
        ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x,y) => x.HasChanged(DragHandler.itemBeginDragged));
    }


}
