using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Results : MonoBehaviour {

	public void NextLevelClick() {
        ExecuteEvents.ExecuteHierarchy<IChangeWord>(gameObject, null, (x,y) => x.ChangeWord());
    }
}
