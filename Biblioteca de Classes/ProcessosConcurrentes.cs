﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
namespace BibliotecaDeClasses
{
    public class ProcessosConcurrentes
    {
        Socket socket;
        
       public ProcessosConcurrentes(Socket socket)
        {
            this.socket = socket;
        }
        public void enviarMensagens()
        {
            string mensagem = "";
            do
            {
                mensagem = enviarMensagem();
            } while (mensagem != "exit");

        }

        public String enviarMensagem()
        {
            string mensagem = "";
            mensagem = Console.ReadLine();
            byte[] data = new byte[1024];
            data = Encoding.ASCII.GetBytes(mensagem);
            socket.Send(data);
            return mensagem;
        }

        public void receberMensagens()
        {
            do
            {
                receberMensagem();
            } while (socket.Connected);
        }

        public void receberMensagem()
        {
            byte[] data = new byte[1024];
            socket.Receive(data);
            string mensagemRecebida = Encoding.ASCII.GetString(data);
            Console.Write(mensagemRecebida);
        }
    }
}
