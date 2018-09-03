using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequestClient {

    public static string baseurl = "";
    private readonly string contentpath = "faceweb/recognize";

    private string getURL()
    {
        if (baseurl == "")
        {
            return null;
        }
        string url = "";
        if (baseurl.ToLower().StartsWith("http://"))
        {
            url = baseurl;
        } else
        {
            url = "http://" + baseurl;
        }

        if (url.EndsWith("/"))
        {
            return url + contentpath;
        }
        else
        {
            return url + "/" + contentpath;
        }
    }

    public IEnumerator Post(Person inPerson)
    {
        string faceString = Convert.ToBase64String(inPerson.face);
        string jsonBody = "{\"FaceData\":\"" + faceString + "\"}";
        //Debug.Log("Call Post Request with url " + url + " and json " + jsonBody);
        var url = getURL();
        if (url == null)
        {
            yield return null;
        }
        var request = new UnityWebRequest(getURL(), "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.Send();

        //Debug.Log("Status Code: " + request.responseCode);
        //Debug.Log("Return body: " + request.downloadHandler.text);
        ResponseObject response = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseObject>(request.downloadHandler.text);
        
        if (response.ReturnCode == 200)
        {
            RestfulClient.Person person = response.Content;
            inPerson.username = person.Name;
            inPerson.detail = person.Detail;
            if (person.Face != null)
            {
                inPerson.score = person.Face.Score;
            }
            inPerson.id = person.Id;

            if (inPerson.score < 0.4f)
            {
                inPerson.color = Color.green;
                inPerson.recognizeState = Person.RecognizeState.RECOGNIZED;
            }
            else
            {
                inPerson.color = Color.red;
                inPerson.recognizeState = Person.RecognizeState.NOTRECOGNIZED;
            }
        } else if (response.ReturnCode == 404)
        {
            inPerson.color = Color.red;
            inPerson.recognizeState = Person.RecognizeState.NOTRECOGNIZED;
        }
    }
}
