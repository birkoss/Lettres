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
            Transform starting_parent = DragHandler.itemBeginDragged.transform.gameObject.GetComponent<DragHandler>().GetParent();
            item.transform.SetParent(starting_parent);
        }

        DragHandler.itemBeginDragged.transform.SetParent(transform);
        ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x,y) => x.HasChanged());
    }


}
