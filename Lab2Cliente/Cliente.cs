using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using BibliotecaDeClasses;

namespace Lab2Cliente
{
    class Cliente
    {
        const int PORTA = 6000;
        static void Main(string[] args)
        {

            Socket socket = conectar("127.0.0.1");
            ProcessosComunicacao oPC = new ProcessosComunicacao(socket);
            Thread Receber=new Thread(new ThreadStart(oPC.enviarMensagens));
            Thread Enviar = new Thread(new ThreadStart(oPC.receberMensagens));

            //Receber.Start();
            //Enviar.Start();

            oPC.receberMensagem();
            oPC.enviarMensagens();

            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            //Console.ReadLine();

        }

        static Socket conectar(String ipStr)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipad = IPAddress.Parse(ipStr);
            IPEndPoint ip = new IPEndPoint(ipad, PORTA);

            socket.Connect(ip);

            return socket;
        }
    }
}
