using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingText : MonoBehaviour {

    // Use this for initialization

    public static bool DisplayText = true;

    int frame = 0;
    bool plus = true;

	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (DisplayText)
        {
            GetComponent<TextMesh>().text = "Detecting Face";
        } else
        {
            GetComponent<TextMesh>().text = "";
        }

        if (frame > 100)
        {
            plus = false;
        }
        else if (frame < 0)
        {
            plus = true;
        }
        if (plus)
        {
            frame++;
        }
        else
        {
            frame--;
        }

        Color color = Color.white;
        color.a = frame/100f;
        GetComponent<TextMesh>().color = color;
    }
}
