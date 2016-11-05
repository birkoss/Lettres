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
        if (!item) {
            DragHandler.itemBeginDragged.transform.SetParent(transform);
            ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x,y) => x.HasChanged());
        }

        /*
        if (!item)
        {
            DragHandler.item.transform.SetParent(transform);
        } else
        {
            Transform aux = DragHandler.item.transform.parent;
            DragHandler.item.transform.SetParent(transform);
            item.transform.SetParent(aux);

        }

        */
    }


}
