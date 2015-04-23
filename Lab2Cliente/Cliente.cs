﻿using System;
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

            receberMensagem(socket);
            enviarMensagens(socket);

            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            //Console.ReadLine();

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
