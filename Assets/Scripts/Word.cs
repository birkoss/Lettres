using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Word : MonoBehaviour, IHasChanged {
    public Transform containerDestination;
    public Transform containerOrigin;

    public GameObject letter;
    public GameObject slot;

    private string word;

	void Start() {
        ChangeWord("AVION");
        HasChanged();
	}


    public void HasChanged() {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        foreach (Transform slotTransform in containerDestination) {
            GameObject item = slotTransform.GetComponent<Slot>().item;
            if (item) {
                builder.Append(item.transform.GetChild(0).gameObject.GetComponent<Text>().text);
            } else {
                builder.Append("#");
            }
        }
        Debug.Log("Current word:" + builder.ToString());
    }


    private void ChangeWord(string new_word) {
        word = new_word;

        // Delete all existing slots
        foreach (Transform child in containerOrigin) {
            Destroy(child.gameObject);
        }

        for (int i=0; i<word.Length; i++) {
            // Add Origin slots and letters
            GameObject new_slot = Instantiate(slot);
            new_slot.transform.SetParent(containerOrigin);
            new_slot.transform.localScale = new Vector3(1f, 1f, 1f);

            GameObject new_letter = Instantiate(letter);
            new_letter.transform.SetParent(new_slot.transform);
            new_letter.transform.localScale = new Vector3(1f, 1f, 1f);
            new_letter.transform.GetChild(0).gameObject.GetComponent<Text>().text = word[i].ToString();

            // Add new destination slots
            new_slot = Instantiate(slot);
            new_slot.transform.SetParent(containerDestination);
            new_slot.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }


}


namespace UnityEngine.EventSystems {
    public interface IHasChanged : IEventSystemHandler {
        void HasChanged();
    }
}
