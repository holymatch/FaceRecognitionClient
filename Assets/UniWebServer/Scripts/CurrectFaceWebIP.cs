using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UniWebServer
{
    [RequireComponent(typeof(EmbeddedWebServerComponent))]
    public class CurrectFaceWebIP : MonoBehaviour, IWebResource
    {
        public string path = "/checkip";
        EmbeddedWebServerComponent server;

        public void HandleRequest(Request request, Response response)
        {
            Debug.Log("Request body: " + request.body);
            var host = PlayerPrefs.GetString("host");
            response.statusCode = 200;
            response.headers.AddHeaderLine("Content-Type:application/json");
            response.message = "OK.";
            response.Write("{\"Status\":\"OK\", \"host\":\"" + host + "\"}");
        }

        // Use this for initialization
        void Start()
        {
            server = GetComponent<EmbeddedWebServerComponent>();
            server.AddResource(path, this);
        }
    }
}