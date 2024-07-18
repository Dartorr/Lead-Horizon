using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using Cinemachine;
using System.Linq;

public class GameBehaviour : MonoBehaviour
{
    public static GameBehaviour gameBehaviour { get; private set; }
    public List<WeaponChars> WeaponChars { get; private set; }
    public Dictionary<WeaponChars, GameObject> WeaponsDictionary { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        gameBehaviour = this;

        WeaponChars = new List<WeaponChars>();
        WeaponsDictionary = new Dictionary<WeaponChars, GameObject>();

        using (FileStream fs = new FileStream("./Assets/Data/Weapons/Weapons.xml", FileMode.OpenOrCreate))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(WeaponChars[]));
            WeaponChars.AddRange(xmlSerializer.Deserialize(fs) as WeaponChars[]);
            foreach (var item in WeaponChars)
            {
                WeaponsDictionary.Add(item, Resources.Load<GameObject>("Weapons/"+item.Name));
            }
        };
    }
}
