using GorillaLocomotion;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Crypt.Injection
{
    internal class PipeLineServerThingy : MonoBehaviour
    {
        public static NamedPipeClientStream PipeServer;

        public static void CommandHandler(string message)
        {
            string[] parts = message.Split(' ');

            switch (parts[0])
            {
                case "color_change":
                    //SendMessage(message);
                    GorillaTagger.Instance.UpdateColor(float.Parse(parts[1]), float.Parse(parts[2]), float.Parse(parts[3]));
                    break;
                case "name_change":
                    PhotonNetwork.LocalPlayer.NickName = parts[1];
                    NetworkSystem.Instance.LocalPlayer.SanitizedNickName = parts[1];
                    GorillaTagger.Instance.offlineVRRig.playerNameVisible = parts[1];
                    GorillaTagger.Instance.offlineVRRig.SetNameTagText(parts[1]);
                    GorillaTagger.Instance.offlineVRRig.UpdateName(true);
                    break;

                default:
                    Debug.Log("[EXE] UNKNOWN MESSAGE RECEIVED - " + message);
                    break;
            }
        }
        public static async void SendMessage(string message)
        {
            if (PipeServer?.IsConnected == true)
            {
                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                await PipeServer.WriteAsync(messageBytes, 0, messageBytes.Length);
                Debug.Log("[You] " + message);
            }
            else
                Debug.Log("Not connected.");
        }

        public static void RestartPipeServer()
        {
            PipeServer?.Dispose();
        }
        public static async Task ConnectAndListen()
        {
            try
            {
                PipeServer = new NamedPipeClientStream(".", "CryptInternalPipe", PipeDirection.InOut, PipeOptions.Asynchronous);
                await PipeServer.ConnectAsync(5000);

                Debug.Log("Connected to EXE!");

                _ = Task.Run(async () =>
                {
                    var buffer = new byte[1024];
                    var stream = PipeServer;

                    try
                    {
                        while (true)
                        {
                            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                            if (bytesRead == 0)
                            {
                                Debug.Log("Pipe closed by the server.");
                                break;
                            }

                            string command = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim('\0', '\r', '\n');
                            if (!string.IsNullOrWhiteSpace(command))
                            {
                                CommandHandler(command);
                                Debug.Log("[EXE] " + command);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError("Pipe read error: " + ex.Message);
                    }
                    finally
                    {
                        stream.Dispose();
                    }
                });
            }
            catch (Exception ex)
            {
                Debug.LogError("Pipe connection error: " + ex.Message);
            }
        }

    }
}
