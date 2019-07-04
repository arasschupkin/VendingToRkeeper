using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Tcp.CommonUtil;

namespace Tcp.Server
{

    public class TcpServer : TcpListener
    {
        public TcpServer(IPAddress localaddr, int port) : base(localaddr, port)
        {

        }

        public bool State()
        {
            return this.Active;
        }

    }

    public class AsyncTcpServer
    {
        public delegate void EventProgressBar(object sender, ProgressBarEventArgs e);
        public delegate void EventMessage(object sender, MessageEventArgs e);
        public delegate void EventRun(object sender, MessageEventArgs e);
        public event EventRun OnRun;
        public event EventMessage OnMessage;
        public event EventProgressBar OnProgressBar;
        internal class Client
        {
            public Client(TcpClient tcpClient, byte[] buffer)
            {
                if (tcpClient == null) throw new ArgumentNullException("tcpClient");
                if (buffer == null) throw new ArgumentNullException("buffer");
                this.TcpClient = tcpClient;
                this.Buffer = buffer;
                commands = CommonUtil.Commands.None;
            }
            public TcpClient TcpClient { get; private set; }
            public byte[] Buffer;
            public string Host = string.Empty;
            public List<byte> byteMessage = new List<byte>();
            public string Message = string.Empty;
            public uint MessageLength;
            public Int32 GetLength;
            public CommonUtil.Commands commands;
            public NetworkStream NetworkStream { get { return TcpClient.GetStream(); } }
        }
        public byte[] SendingBuffer = null;
        public FileStream Fs = null;
        string SaveFileName = string.Empty;
        public IEnumerable<TcpClient> TcpClients
        {
            get
            {
                foreach (Client client in this.Clients)
                {
                    yield return client.TcpClient;
                }
            }
        }
        public int Port { get; set; }
        public Encoding Encoding { get; set; }
        public TcpServer tcpServer;
        private List<Client> Clients;
        private SynchronizationContext context;
        public int _index = 0;
        public int _max = 0;
        public int BufferSize = 1024;
        public AsyncTcpServer()
        {            
            Encoding = Encoding.Default;
            Clients = new List<Client>();
            context = SynchronizationContext.Current;            
        }

		public int GetCouuntClient()
		{

			return Clients.Count;
		}

        public TcpClient GetTcpClient(Int32 index)
        {
            return Clients[index].TcpClient;
        }

        private void MessageCallBack(object state)
        {

            if (OnMessage != null)
            {

                MessageEventArgs e = new MessageEventArgs((byte[])state);
                OnMessage(this, e);
            }

        }

        private void RunCallBack(object state)
        {            
            if (OnRun != null)
            {
                MessageEventArgs e = new MessageEventArgs((byte[])state);

                OnRun(this, e);
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


        public void Start()
        {
            tcpServer = new TcpServer(IPAddress.Any, Port);
            tcpServer.Start();
            tcpServer.BeginAcceptTcpClient(new System.AsyncCallback(OnAccept), tcpServer);
        }

        public void Stop()
        {
            this.tcpServer.Stop();
            lock (this.Clients)
            {
                foreach (Client client in this.Clients)
                {
                    client.TcpClient.Client.Disconnect(false);
                }
                this.Clients.Clear();
            }
        }

        public void Write(TcpClient tcpClient, string data)
        {
            byte[] bytes = this.Encoding.GetBytes(data);
            Write(tcpClient, bytes);
        }

        public void Write(string data)
        {
            foreach (Client client in this.Clients)
            {
                Write(client.TcpClient, data);
            }
        }

        public void Write(byte[] bytes)
        {
            foreach (Client client in this.Clients)
            {
                Write(client.TcpClient, bytes);
            }
        }

        public void Write(TcpClient tcpClient, byte[] bytes)
        {
            NetworkStream networkStream = tcpClient.GetStream();
            networkStream.BeginWrite(bytes, 0, bytes.Length, WriteCallback, tcpClient);
        }




        public void SendMessage(TcpClient tcpClient, byte[] byteData)
        {

            NetworkStream netstream = tcpClient.GetStream(); // clients[0].TcpClient.GetStream();            
            int CurrentPacketLength = 0;
            int TotalLength = 0;            
            byte[] length = new byte[4];
            byte[] SendingBuffer = null;
            List<byte> result = new List<byte>();
            int arrayIndex = 0;            


            try
            {

                //length = BitConverter.GetBytes(byteData.Length);
                //netstream.BeginWrite(length, 0, length.Length, new AsyncCallback(CallbackBeginWrite), netstream);

                int NoOfPackets = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(byteData.Length) / Convert.ToDouble(BufferSize)));
                TotalLength = byteData.Length;

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
                    Array.Copy(byteData, arrayIndex, SendingBuffer, 0, CurrentPacketLength);
                    arrayIndex += CurrentPacketLength;

                    netstream.BeginWrite(SendingBuffer, 0, SendingBuffer.Length, new AsyncCallback(CallbackBeginWrite), netstream);

                }

                

            }
            catch (Exception)
            {
                //MessageBox.Show("Unable to send message to the server.", "SGSclientTCP: " + strName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        public void CallbackBeginWrite(IAsyncResult ar)
        {

            NetworkStream networkStream = (NetworkStream)ar.AsyncState;
            networkStream.EndWrite(ar);

        }


        private void WriteCallback(IAsyncResult result)
        {
            TcpClient tcpClient = result.AsyncState as TcpClient;
            NetworkStream networkStream = tcpClient.GetStream();
            networkStream.EndWrite(result);
        }

        private IPAddress GetIPv4(IPAddress[] addressList)
        {
            IPAddress ip = null;

            foreach (IPAddress curIP in addressList)
            {
                if (curIP.AddressFamily == AddressFamily.InterNetwork)
                {
                    ip = curIP;
                    break;
                }
            }

            return ip;

        }

        private void OnAccept(IAsyncResult result)
        {
            TcpClient tcpClient = tcpServer.EndAcceptTcpClient(result);
            byte[] buffer = new byte[1024]; //tcpClient.ReceiveBufferSize
            Client client = new Client(tcpClient, buffer);
            string message = "Connected to " + GetIPv4(Dns.GetHostEntry(Dns.GetHostName()).AddressList).ToString();
            try
            {
                lock (this.Clients)
                {
                    this.Clients.Add(client);
                    //this.SendMessage(tcpClient, message);
                }
            }
            catch { }

            tcpServer.BeginAcceptTcpClient(new AsyncCallback(OnAccept), tcpServer);
            client.NetworkStream.BeginRead(client.Buffer, 0, client.Buffer.Length, new AsyncCallback(OnRead), client);

        }

        /// <summary>
        /// Callback for the read opertaion.
        /// </summary>
        /// <param name="result">The async result object</param>
        private void OnRead(IAsyncResult ar)
        {
            string message = string.Empty;
            Client client = ar.AsyncState as Client;
            byte[] length = new byte[4];
            try
            {

                int numberOfBytesRead = 0;
                try
                {
                    numberOfBytesRead = client.NetworkStream.EndRead(ar);
                }
                catch { }

                if (numberOfBytesRead == 0)
                {
                    lock (this.Clients)
                    {
                        this.Clients.Remove(client);
                        return;
                    }
                }

                if (numberOfBytesRead > 0)
                {



                    if (client.commands == CommonUtil.Commands.None)
                    {
                        client.GetLength = 0;
                        client.byteMessage.Clear();
                        client.commands = CommonUtil.Commands.ReadMessage;
                        Array.Copy(client.Buffer, length, 4);

                        uint i = length[0];
                        i = (i << 8) + length[1];
                        i = (i << 8) + length[2];
                        i = (i << 8) + length[3];


                        client.MessageLength = i + 4;
                    }

                    switch (client.commands)
                    {
                        case CommonUtil.Commands.ReadMessage:

                            client.byteMessage.AddRange(client.Buffer);
                            client.GetLength += client.Buffer.Length;


                            if (client.GetLength >= client.MessageLength)
                            {
                                byte[] inData = new byte[client.MessageLength];
                                Array.Copy(client.byteMessage.ToArray(), inData, inData.Length);
                                

                                context.Post(new SendOrPostCallback(MessageCallBack), (object)inData);
                                client.commands = CommonUtil.Commands.None;
                            }
                            break;
                    }
                }

            }
            catch (Exception e)
            {
                string s = e.Message;
                context.Post(new SendOrPostCallback(MessageCallBack), (object)s);
            }

            client.Buffer = new byte[1024];
            client.NetworkStream.BeginRead(client.Buffer, 0, client.Buffer.Length, new AsyncCallback(OnRead), client);
        }
    }
}
