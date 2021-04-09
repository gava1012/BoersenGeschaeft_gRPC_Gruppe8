using BoersenGeschaeftServer;
using Grpc.Core;
using System;

namespace BoersenGeschaeft
{
    class Program
    {
        static void Main(string[] args)
        {
            const int Port = 30052;

            var boerse = BoersenGeschaeftUtil.Load();

            Server server = new Server
            {
                Services = { Boerse.BoersenGeschaeft.BindService(new BoersenGeschaeftImpl(boerse)) },
                Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine("Boersengeschaeft Server arbeitet auf port " + Port);
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }
}
