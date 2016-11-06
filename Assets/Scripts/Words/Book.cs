using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Book  {

    private WordsList words;


    public Book(WordsList new_list) {
        words = new_list;
    }


    public string Get(string image) {
        return words.Get(image).fr;
    }


    public string GetRandom() {
        return words.words[ Random.Range(0, words.words.Length) ].name;
    }

}
