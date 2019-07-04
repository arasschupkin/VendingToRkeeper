using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;
using Tcp.CommonUtil;

namespace Tcp.Client
{
    public class AsyncTcpClient
    {
        private int BufferSize = 1024;
        //private byte[] SendingBuffer = null;
        public delegate void EventProgressBar(object sender, ProgressBarEventArgs e);
        public delegate void EventMessage(object sender, MessageEventArgs e);
        public event EventMessage OnMessage;
        public event EventProgressBar OnProgressBar;
        private IPAddress[] addresses;
        public int Port;
        public string Host;
        //private string message;
        private List<byte> byteMessage = new List<byte>();
        private WaitHandle addressesSet;        
        private TcpClient tcpClient;
        private int failedConnectionCount;
        public bool Conected;
        public Encoding encoding;
        private SynchronizationContext context;
        private int _index = 0;
        private int _max = 0;
        //private NetworkStream NetStream = null;
        string SaveFileName = string.Empty;
        private uint MessageLength = 0;
        private int GetLength = 0;
        //private long TotalLength = 0;
        public FileStream Fs = null;
        public CommonUtil.Commands commands;
        public AsyncTcpClient()
        {
            tcpClient = new TcpClient();
            encoding = Encoding.Default;
            Conected = false;
            context = SynchronizationContext.Current;
            commands = CommonUtil.Commands.None;
        }

        private void MessageCallBack(object state)
        {

            if (OnMessage != null)
            {
                MessageEventArgs e = new MessageEventArgs((byte[])state);
                OnMessage(this, e);
            }

        }  

        private void ProgressBarCallBack(object state)
        {
            if (OnProgressBar != null)
            {
                ProgressBarEventArgs e = new ProgressBarEventArgs(_index, _max);
                OnProgressBar(this, e);
            }
        }

        public void SendPoolMessage()
        {

            NetworkStream netstream  = null; tcpClient.GetStream();
            //Data Message = new Data();
            byte[] _sendData = null;


            for (int i = 0; i < 2048; i++)
            {

                //Message.Message = i.ToString();
                //Message.Command = Commands.Message;
                netstream = tcpClient.GetStream();
                _sendData = Encoding.GetEncoding(1251).GetBytes(i.ToString());
                netstream.BeginWrite(_sendData, 0, _sendData.Length, new AsyncCallback(CallbackBeginWrite), netstream);
            }

        }

        public void SendMessage(byte[] _byteData)
        {

            NetworkStream netstream = tcpClient.GetStream();
            int CurrentPacketLength = 0;
            int TotalLength = 0;
            byte[] length = new byte[4];                        
            byte[] SendingBuffer = null;
            List<byte> result = new List<byte>();
            int arrayIndex = 0;

            try
            {


                

                length = BitConverter.GetBytes(_byteData.Length);

                int NoOfPackets = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(_byteData.Length) / Convert.ToDouble(BufferSize)));
                TotalLength = _byteData.Length;

                //Не нужно
                //netstream.BeginWrite(length, 0, length.Length, new AsyncCallback(CallbackBeginWrite), netstream);

                for (int i = 0; i < NoOfPackets; i++)
                {

                    if (TotalLength > BufferSize)
                    {
                        CurrentPacketLength = BufferSize;
                        TotalLength = TotalLength - CurrentPacketLength;
                    }
                    else
                        CurrentPacketLength = TotalLength;

                    SendingBuffer = new byte[CurrentPacketLength];
                    Array.Copy(_byteData, arrayIndex, SendingBuffer, 0, CurrentPacketLength);
                    arrayIndex += CurrentPacketLength;

                    netstream.BeginWrite(SendingBuffer, 0, SendingBuffer.Length, new AsyncCallback(CallbackBeginWrite), netstream);

                }

                
            }
            catch
            {
                //MessageBox.Show("Unable to send message to the server.", "SGSclientTCP: " + strName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void SendMessage(string message)
        {

            NetworkStream netstream = tcpClient.GetStream();            
            int CurrentPacketLength = 0;
            int TotalLength = 0;
            byte[] length = new byte[4];
            byte[] _byteData = null;            
            byte[] SendingBuffer = null;
            List<byte> result = new List<byte>();
            int arrayIndex = 0;

            try
            {
                                


                _byteData = Encoding.GetEncoding(1251).GetBytes(message);

                //length = BitConverter.GetBytes(_byteData.Length);

                int NoOfPackets = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(_byteData.Length) / Convert.ToDouble(BufferSize)));
                TotalLength = _byteData.Length;

                //netstream.BeginWrite(length, 0, length.Length, new AsyncCallback(CallbackBeginWrite), netstream);

                for (int i = 0; i < NoOfPackets; i++)
                {

                    if (TotalLength > BufferSize)
                    {
                        CurrentPacketLength = BufferSize;
                        TotalLength = TotalLength - CurrentPacketLength;
                    }
                    else
                        CurrentPacketLength = TotalLength;

                    SendingBuffer = new byte[CurrentPacketLength];
                    Array.Copy(_byteData, arrayIndex, SendingBuffer, 0, CurrentPacketLength);
                    arrayIndex += CurrentPacketLength;

                    netstream.BeginWrite(SendingBuffer, 0, SendingBuffer.Length, new AsyncCallback(CallbackBeginWrite), netstream);
                }

                
            }
            catch 
            {
                //MessageBox.Show("Unable to send message to the server.", "SGSclientTCP: " + strName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        public void CallbackBeginWrite(IAsyncResult ar)
        {

            NetworkStream networkStream = (NetworkStream)ar.AsyncState;
            networkStream.EndWrite(ar);
            
        }

        public void Connect()
        {

            addressesSet = new AutoResetEvent(false);
            Dns.BeginGetHostAddresses(Host, new AsyncCallback(GetHostAddressesCallback), null);

            if (Port < 0)
                throw new ArgumentException();

            if (addressesSet != null)
                //Wait for the addresses value to be set
                addressesSet.WaitOne();
            //Set the failed connection count to 0
            Interlocked.Exchange(ref failedConnectionCount, 0);
            //Start the async connect operation
            tcpClient.BeginConnect(addresses, Port, new AsyncCallback(ConnectCallback), tcpClient);
        }

        public void Disconnect()
        {            
            tcpClient.Close();
        }
 
        public void Write(string data)
        {
            byte[] bytes = encoding.GetBytes(data);
            Write(bytes);
        }
 
        public void Write(byte[] bytes)
        {
            NetworkStream networkStream = tcpClient.GetStream();
            //Start async write operation
            networkStream.BeginWrite(bytes, 0, bytes.Length, WriteCallback, null);
        }
 
        private void WriteCallback(IAsyncResult result)
        {
            NetworkStream networkStream = tcpClient.GetStream();
            networkStream.EndWrite(result);
        }
 
        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                TcpClient t = (TcpClient)ar.AsyncState;
                t.EndConnect(ar);
                Conected = true;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                //Increment the failed connection count in a thread safe way
                Interlocked.Increment(ref failedConnectionCount);
                if (failedConnectionCount >= addresses.Length)
                {
                    //We have failed to connect to all the IP Addresses
                    //connection has failed overall.
                    return;
                }
            }
 
            //We are connected successfully.
            NetworkStream networkStream = tcpClient.GetStream();
            byte[] buffer = new byte[1024]; //new byte[tcpClient.ReceiveBufferSize];
            //Now we are connected start asyn read operation.
            networkStream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(ReadCallback), buffer);
        }
 
        private void ReadCallback(IAsyncResult result)
        {
            int read;            
            string message = string.Empty;

            NetworkStream networkStream;
            try
            {
                networkStream = tcpClient.GetStream();
                read = networkStream.EndRead(result);
            }
            catch
            {
                //An error has occured when reading
                return;
            }
 
            if (read == 0)
            {
                //The connection has been closed.
                return;
            }
 
            byte[] buffer = result.AsyncState as byte[];
            try
            {
                if (commands == CommonUtil.Commands.None) {

                    byteMessage.Clear();                    
                    byte[] length = new byte[4];                    
                    Array.Copy(buffer, length, 4);
                    uint i = length[0];
                    i = (i << 8) + length[1];
                    i = (i << 8) + length[2];
                    i = (i << 8) + length[3];


                    MessageLength = i + 4;

                    commands = CommonUtil.Commands.ReadMessage;                    
                }

                switch (commands)
                {
                    case CommonUtil.Commands.ReadMessage:

                        byteMessage.AddRange(buffer);
                        GetLength += buffer.Length;


                        if (GetLength >= MessageLength)
                        {
                            byte[] inData = new byte[MessageLength];
                            Array.Copy(byteMessage.ToArray(), inData, inData.Length);
                            context.Post(new SendOrPostCallback(MessageCallBack), (object)inData);
                            commands = CommonUtil.Commands.None;
                        }
                        break;
                }                             
            }
            catch
            {

            }

            buffer = new byte[1024];
            networkStream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(ReadCallback), buffer);

        }
 
        private void GetHostAddressesCallback(IAsyncResult result)
        {
            addresses = Dns.EndGetHostAddresses(result);
            //Signal the addresses are now set
            ((AutoResetEvent)addressesSet).Set();         
        }
    }
}
