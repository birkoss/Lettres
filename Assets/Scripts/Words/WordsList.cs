
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

[XmlRoot("WordsList")]
public class WordsList {
   [XmlArray("Words"), XmlArrayItem("Word")]
   public Word[] words;


   public void Load(string path) {
       TextAsset textAsset = (TextAsset) Resources.Load(path);

       var serializer = new XmlSerializer(typeof(WordsList));
       words = (serializer.Deserialize(new StringReader(textAsset.text)) as WordsList).words;
   }


   public Word Get(string name = "Main") {
     for(int i=0; i<words.Length; i++) {
       if( words[i].name == name ) {
         return words[i];
       }
     }
     return null;
   }
}
