using Grpc.Core;
using Boerse;
using System;
using System.Text;
using System.Threading.Tasks;
using BoersenGeschaeft;

namespace Boersengeschaeft
{
    class Program
    {
        public class BoersenGeschaeftClient
        {
            readonly Boerse.BoersenGeschaeft.BoersenGeschaeftClient client;

            public BoersenGeschaeftClient(Boerse.BoersenGeschaeft.BoersenGeschaeftClient client)
            {
                this.client = client;
            } 
            public void GetBoerse(string tradenummer)
            {
                try
                {
                    Log("*** GetBoerse: Tradenummer = {0}", tradenummer);

                    BoerseResponse boerse = client.GetBoerse(new BoerseRequest { TradeNummer = tradenummer});
     
                if (boerse.Exists())
                    {
                        Log("Boerse gefunden mit Tradenummer {0}, Bezeichnung {1}, Wert {2} ",
                            boerse.TradeNummer, boerse.Bezeichnung, boerse.Wert);
                    }
                    else
                    {
                        Log("Keine Boerse gefunden mit Tradenummer {0}",
                            tradenummer);
                    }
                }
                catch (RpcException e)
                {
                    Log("RPC failed " + e);
                    throw;
                }
            }

            public async Task ListBoersen(int minuten)
            {

              Log("*** ListBoersen die in {0} minuten getaetigt werden koennen", minuten);

              if (minuten < 0)
                throw new ArgumentOutOfRangeException("Minuten muessen >= 1 sein");

              try
                    {
                
                BoerseRequest2 request = new BoerseRequest2
                  {
                    Minuten = minuten
                  };

                using (var call = client.ListBoersen(request))
                {
                  var responseStream = call.ResponseStream;
                  StringBuilder responseLog = new StringBuilder("Result: ");

                  while (await responseStream.MoveNext())
                  {
                    BoerseResponse boerse = responseStream.Current;
                    responseLog.Append(boerse.ToString());
                  }
                  Log(responseLog.ToString());
          }
              }

              catch (RpcException e)
              {
                Log("RPC failed " + e);
                throw;
              }
                  }

            private void Log(string s, params object[] args)
            {
                Console.WriteLine(string.Format(s, args));
                
            }

            private void Log(string s)
            {
                Console.WriteLine(s + "\n");     
              }   
        }

        static void Main(string[] args)
        {
            var channel = new Channel("127.0.0.1:30052", ChannelCredentials.Insecure);
            var client = new BoersenGeschaeftClient(new Boerse.BoersenGeschaeft.BoersenGeschaeftClient(channel));

            client.GetBoerse("222");
            Console.WriteLine();
            client.GetBoerse("345");
            Console.WriteLine();
            client.GetBoerse("1");

            Console.WriteLine();
            client.ListBoersen(1200).Wait();
            Console.WriteLine();
            client.ListBoersen(20).Wait();

            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
