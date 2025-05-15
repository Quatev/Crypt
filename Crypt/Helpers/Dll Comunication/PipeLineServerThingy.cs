using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crypt.Helpers.Dll_Comunication
{
    internal class PipeLineServerThingy
    {
        public static List<string> Logs { get; private set; } = new List<string>();

        public static NamedPipeServerStream PipeServer;
        public static bool IsConnected { get; private set; } = false;

        public static async void SendMessage(string message)
        {
            if (IsConnected && PipeServer?.IsConnected == true)
            {
                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                await PipeServer.WriteAsync(messageBytes, 0, messageBytes.Length);
                Log("[You] " + message);
            }
            else
            {
                Log("Not connected.");
            }
        }

        public static async void StartPipeServer()
        {
            try
            {
                PipeServer = new NamedPipeServerStream("CryptInternalPipe", PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
                IsConnected = false;

                Log("Waiting for DLL connection...");
                await PipeServer.WaitForConnectionAsync();
                IsConnected = true;
                Log("DLL connected!");

                _ = Task.Run(async () =>
                {
                    byte[] buffer = new byte[byte.MaxValue];

                    try
                    {
                        while (PipeServer.IsConnected)
                        {
                            int bytesRead = await PipeServer.ReadAsync(buffer, 0, buffer.Length);
                            if (bytesRead > 0)
                            {
                                CommandHandler(Encoding.UTF8.GetString(buffer, 0, bytesRead));
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        Log($"Error reading from pipe: {ex.Message}");
                    }

                    IsConnected = false;
                    Log("DLL disconnected.");
                    RestartPipeServer();
                });
            }
            catch (Exception ex)
            {
                Log($"Pipe server error: {ex.Message}");
            }
        }

        public static void RestartPipeServer()
        {
            PipeServer?.Dispose();
            StartPipeServer();
        }

        public static void Log(string message)
        {
            //MessageBox.Show(message);
            Logs.Add($"{DateTime.Now:ss} - {message}");
        }

        public static void CommandHandler(string message)
        {
            switch (message)
            {
                default:
                    MessageBox.Show(message, "UNKNOWN MESSAGE RECEIVED");
                    break;
            }
        }
    }
}
