using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace UniWebServer
{
    [RequireComponent(typeof(EmbeddedWebServerComponent))]
    public class ChangeFaceWebIP : MonoBehaviour, IWebResource
    {

        public string path = "/changeip";
        EmbeddedWebServerComponent server;

        public void HandleRequest(Request request, Response response)
        {
            Debug.Log("Request body: " + request.body);
            var obj = JsonUtility.FromJson<ChangeIPObj>(request.body);
            Debug.Log("Object: " + obj.host);
            PlayerPrefs.SetString("host", obj.host);
            response.statusCode = 200;
            response.headers.AddHeaderLine("Content-Type:application/json");
            response.message = "OK.";
            response.Write("{\"Status\":\"OK\", \"host\":\"" + obj.host + "\"}");
        }

        // Use this for initialization
        void Start()
        {
            server = GetComponent<EmbeddedWebServerComponent>();
            server.AddResource(path, this);
        }

        [System.Serializable]
        class ChangeIPObj
        {
            public string host;
        }
    }
}