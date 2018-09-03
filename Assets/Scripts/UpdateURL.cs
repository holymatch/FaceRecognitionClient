using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateURL : MonoBehaviour {

    public InputField Field;
    public void ValueChangeCheck()
    {
        WebRequestClient.baseurl = Field.text;
    }
}
