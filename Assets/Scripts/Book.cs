using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Book  {

    private Dictionary<string, string> words;


    public Book() {
        words = new Dictionary<string, string>();
    }


    public void Add(string image, string word) {
        words.Add(image, word);
    }


    public string Get(string image) {
        return words[image];
    }


    public string GetRandom() {
        string[] keys = words.Keys.ToArray();

        return keys[ Random.Range(0, keys.Length) ];
    }

}
