using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Tcp.CommonUtil
{
    public enum Commands
    {
        None,
        Message,    //Send a text message to all the chat clients
        BeginMessage,    //Send a text message to all the chat clients
        EndMessage,    //Send a text message to all the chat clients
        ReadMessage    //Send a text message to all the chat clients
    }

    public class ProgressBarEventArgs
    {
        public ProgressBarEventArgs(int value, int max) { Value = value; Max = max; }
        public int Value { get; private set; } // readonly
        public int Max { get; private set; } // readonly
    }

    public class MessageEventArgs
    {
        public byte[] inData { get; private set; } // readonly
        public MessageEventArgs(byte[] array)
        {
            inData = new byte[array.Length];
            Array.Copy(array, inData, inData.Length);
        }        
    }

    public struct ClientInfo
    {
        public Socket socket;   //Socket of the client
        public string Text;  //Name by which the user logged into the chat room
    }

    public static class ConvertData
    {

        public static byte[] ToByte(string message)
        {
            byte[] Result = new byte[1024];
            List<byte> result = new List<byte>();

            result.AddRange(BitConverter.GetBytes(message.Length));
            result.AddRange(Encoding.GetEncoding(1251).GetBytes(message));
            result.ToArray().CopyTo(Result, 0);

            return Result;
        }

        public static string ToData(Commands command, byte[] data, int length)
        {
            string message = string.Empty;
            try
            {

                switch (command)
                {
                    case Commands.BeginMessage:
                        if (data.Length > 4)
                            message = Encoding.GetEncoding(1251).GetString(data, 4, length - 4).Replace('\0', ' ').Trim();
                        break;
                    case Commands.Message:
                    case Commands.ReadMessage:
                        message = Encoding.GetEncoding(1251).GetString(data, 0, length).Replace('\0', ' ').Trim();
                        break;
                }
            }
            catch { }

            return message;
        }
    }

}
