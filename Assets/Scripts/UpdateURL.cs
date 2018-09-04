using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateURL : MonoBehaviour {

    public InputField Field;

    void Start()
    {
        if (PlayerPrefs.HasKey("host")) {
            Field.text = PlayerPrefs.GetString("host");
        }
    }

    public void ValueChangeCheck()
    {
        PlayerPrefs.SetString("host", Field.text);
    }
}
