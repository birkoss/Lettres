﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Word : MonoBehaviour, IHasChanged, IResetWord, IChangeWord {
    public Transform containerDestination;
    public Transform containerOrigin;
    public GameObject image;

    public GameObject letter;
    public GameObject slot;

    public GameObject screenWin;

    private int nb_tries;

    private string word;
    private Book book;

	void Start() {
        Debug.Log("Mode:" + MainMenu.mode);
        book = new Book();

        book.Add("apple", "Pomme");
        book.Add("pineapple", "Ananas");
        book.Add("cherries", "Cerise");
        book.Add("banana", "Banane");
        book.Add("tomato", "Tomate");
        book.Add("strawberry", "Fraise");
        book.Add("grapes", "Raisin");
        book.Add("cucumber", "Cornichon");

        ChangeWord(book.GetRandom());
	}


    public void ResetWord() {
        Debug.Log("Reset Word...");
        ChangeWord(word);
    }


    public void ChangeWord() {
        screenWin.gameObject.GetComponent<Win>().Close();
        ChangeWord(book.GetRandom());
    }

    public void HasChanged() {
        nb_tries++;

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

        if (builder.ToString() == word) {
            int nb_errors = nb_tries - word.Length;
            if (nb_errors < 0) {
                nb_errors = 0;
            }
            screenWin.gameObject.GetComponent<Win>().Show(nb_errors);
        }
    }


    private void ChangeWord(string book_key) {
        word = book.Get(book_key);
        nb_tries = 0;

        // Delete all existing slots
        foreach (Transform child in containerOrigin) {
            Destroy(child.gameObject);
        }
        foreach (Transform child in containerDestination) {
            Destroy(child.gameObject);
        }

        // Get all letters
        List<string>letters = new List<string>();
        for (int i=0; i<word.Length; i++) {
            if( MainMenu.mode != 1 || (MainMenu.mode == 1 && i == 0)) {
                letters.Add(word[i].ToString());
            }
        }

        // Add misc letters
        if (MainMenu.mode != 2) {
            for (int i=0; i<5; i++) {
                letters.Add(((char)Random.Range(65, 90)).ToString());
            }
        }

        // Shuffle the letters
        letters = Shuffle(letters);

        GameObject new_slot, new_letter;
        // Add the available letters and slots
        for (int i=0; i<letters.Count; i++) {
            new_slot = Instantiate(slot);
            new_slot.transform.SetParent(containerOrigin);
            new_slot.transform.localScale = new Vector3(1f, 1f, 1f);

            new_letter = Instantiate(letter);
            new_letter.transform.SetParent(new_slot.transform);
            new_letter.transform.localScale = new Vector3(1f, 1f, 1f);
            new_letter.transform.GetChild(0).gameObject.GetComponent<Text>().text = letters[i];
            new_letter.name = letters[i];
        }

        // Add the answer letters and slots
        for (int i=0; i<word.Length; i++) {
            new_slot = Instantiate(slot);
            new_slot.transform.SetParent(containerDestination);
            new_slot.transform.localScale = new Vector3(1f, 1f, 1f);

            if (MainMenu.mode == 1 && i > 0) {
                new_letter = Instantiate(letter);
                new_letter.transform.SetParent(new_slot.transform);
                new_letter.transform.localScale = new Vector3(1f, 1f, 1f);
                new_letter.transform.GetChild(0).gameObject.GetComponent<Text>().text = word[i].ToString();
                new_letter.name = word[i].ToString();
                new_letter.GetComponent<DragHandler>().isDragable = false;
                new_letter.GetComponent<Image>().sprite = new_letter.GetComponent<DragHandler>().disable;

            }
        }

        image.GetComponent<Image>().sprite = (Resources.Load(book_key, typeof(Sprite)) as Sprite);


    }

    private List<string> Shuffle(List<string> list) {
            int n = list.Count;
            System.Random rnd = new System.Random();
            while (n > 1) {
                int k = (rnd.Next(0, n) % n);
                n--;
                string value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }


}


namespace UnityEngine.EventSystems {
    public interface IHasChanged : IEventSystemHandler {
        void HasChanged();
    }
    public interface IResetWord : IEventSystemHandler {
        void ResetWord();
    }
    public interface IChangeWord : IEventSystemHandler {
        void ChangeWord();
    }
}
