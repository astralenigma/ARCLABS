using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
namespace BibliotecaDeClasses
{
    public class ProcessosConcurrentes
    {
        public void enviarMensagens(Socket socket)
        {
            string mensagem = "";
            do
            {
                mensagem = enviarMensagem(socket);
            } while (mensagem != "exit");

        }

        public String enviarMensagem(Socket socket)
        {
            string mensagem = "";
            mensagem = Console.ReadLine();
            byte[] data = new byte[1024];
            data = Encoding.ASCII.GetBytes(mensagem);
            socket.Send(data);
            return mensagem;
        }

        public void receberMensagens(Socket socket)
        {
            do
            {
                receberMensagem(socket);
            } while (socket.Connected);
        }

        public void receberMensagem(Socket socket)
        {
            byte[] data = new byte[1024];
            socket.Receive(data);
            string mensagemRecebida = Encoding.ASCII.GetString(data);
            Console.Write(mensagemRecebida);
        }
    }
}
