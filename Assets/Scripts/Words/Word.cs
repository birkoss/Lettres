using System.Xml;
using System.Xml.Serialization;

public class Word {
  [XmlAttribute("name")]
  public string name;

  public string fr;
  public string en;
}
