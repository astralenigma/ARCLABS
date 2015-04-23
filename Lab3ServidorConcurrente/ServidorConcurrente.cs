using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using BibliotecaDeClasses;

namespace Lab3ServidorConcurrente
{
    class ServidorConcurrente
    {

        const int NUMERODECONECCOES = 10;
        static void Main(string[] args)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, 6000);
            socket.Bind(ip);
            socket.Listen(NUMERODECONECCOES);
            Console.WriteLine("Waiting...");
            Socket socket2 = socket.Accept();

            EndPoint ipep = socket2.RemoteEndPoint;
            Console.WriteLine("Client " + ipep + " Connectado.");

            byte[] data = new byte[1024];
            string mensagemEnviada = "Bem vindo\n";
            data = Encoding.ASCII.GetBytes(mensagemEnviada);
            socket2.Send(data);
            receberMensagens(socket2);
            Console.ReadLine();
        }

        static void receberMensagens(Socket socket)
        {
            do
            {
                receberMensagem(socket);
            } while (socket.Connected);
        }

        static void receberMensagem(Socket socket)
        {
            byte[] data = new byte[1024];
            socket.Receive(data);
            string mensagemRecebida = Encoding.ASCII.GetString(data);
            Console.Write(mensagemRecebida);
        }

        static void enviarMensagens(Socket socket)
        {
            string mensagem = "";
            do
            {
                mensagem = enviarMensagem(socket);
            } while (mensagem != "exit");

        }

        static String enviarMensagem(Socket socket)
        {
            string mensagem = "";
            mensagem = Console.ReadLine();
            byte[] data = new byte[1024];
            data = Encoding.ASCII.GetBytes(mensagem);
            socket.Send(data);
            return mensagem;
        }
    }
}
