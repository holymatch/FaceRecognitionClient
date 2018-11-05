using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_UWP
using System.IO;
using System.Threading.Tasks;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
#endif

public class TcpNetworkServerManager : MonoBehaviour
{
    static string PortNumber = "8885";

    private enum ServerStatus
    {
        Started,
        Stopped
    }

    private ServerStatus serverStatus = ServerStatus.Stopped;

    private void Start()
    {
        Debug.Log("Server start.");
#if UNITY_UWP
        Task.Run(() => {
                if (serverStatus == ServerStatus.Stopped)
                {
                    StartServer();
                } 
            });
#endif
    }

    void Update()
    {
#if UNITY_UWP
        if (serverStatus == ServerStatus.Stopped)
        {
            StartServer();
        }
#endif
    }

#if UNITY_UWP

    private async void StartServer()
        {
            try
            {
                var streamSocketListener = new Windows.Networking.Sockets.StreamSocketListener();

                // The ConnectionReceived event is raised when connections are received.
                streamSocketListener.ConnectionReceived += this.StreamSocketListener_ConnectionReceived;

                // Start listening for incoming TCP connections on the specified port. You can specify any port that's not currently in use.
                await streamSocketListener.BindServiceNameAsync(PortNumber);
                serverStatus = ServerStatus.Started;
            }
            catch (Exception ex)
            {
                Windows.Networking.Sockets.SocketErrorStatus webErrorStatus = Windows.Networking.Sockets.SocketError.GetStatus(ex.GetBaseException().HResult);
                }
        }

    private async void StreamSocketListener_ConnectionReceived(Windows.Networking.Sockets.StreamSocketListener sender, Windows.Networking.Sockets.StreamSocketListenerConnectionReceivedEventArgs args)
        {
            string request = "";
            
            using (var streamReader = new StreamReader(args.Socket.InputStream.AsStreamForRead()))
            {
                using (Stream outputStream = args.Socket.OutputStream.AsStreamForWrite())
                {
                    using (var streamWriter = new StreamWriter(outputStream))
                    {
                        await SendMessage(streamWriter, "localhost");
                        while (request.ToLower() != "exit")
                        {
                            request = await streamReader.ReadLineAsync();
                            await SendMessage(streamWriter, request);
                        }
                    }
                    
                }                    
            }

            sender.Dispose();
            serverStatus = ServerStatus.Stopped;
        }

        private async Task<Boolean> SendMessage(StreamWriter streamWriter, String request)
        {
            // Echo the request back as the response.
            
            await streamWriter.WriteLineAsync(request);
            await streamWriter.FlushAsync();
            
            return true;
        }
#endif
}