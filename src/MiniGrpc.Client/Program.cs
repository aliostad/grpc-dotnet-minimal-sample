using System;
using Grpc.Core;
using MiniGrpc.Common;
using System.Threading.Tasks;

namespace MiniGrpc.Client
{
    class Program
    {
        public static void Main(string[] args)
        {
            var channel = new Channel("127.0.0.1:50505", ChannelCredentials.Insecure);

            var client = new MiniGrpc.Common.MiniService.MiniServiceClient(channel);

            if(args.Length == 0)
            {
                Console.WriteLine("Usage: MiniGrpc.Client.exe <feature> \nFeatures: add, travel");
                return;
            }

            switch (args[0]){
                case "add":
                    var a = 3;
                    var b = 39;

                    var reply = client.Add(new AddRequest() { A = a, B = b });
                    Console.WriteLine($"Adding {a} and {b}: {reply.C}");
                    break;

                case "travel":
                    AcidTrip(client).Wait();
                    break;
            }

            channel.ShutdownAsync().Wait();
        }

        private async static Task AcidTrip(MiniGrpc.Common.MiniService.MiniServiceClient client)
        {
            var r = new Random();
            var stream = client.ReportPosition();
            var longitude = 45.123d;
            var lattitude = 123.301d;

            for (int i = 0; i < 10; i++)
            {
                lattitude += r.NextDouble() / 1000;
                longitude += r.NextDouble() / 1000;

                await stream.RequestStream.WriteAsync(new Position()
                {
                    Lattitude = lattitude,
                    Longitude = longitude
                });

                await Task.Delay(2000);
                Console.WriteLine("Moving...");
            }

            Console.WriteLine("Ahhhh... falling off the cliff... blame dotnetcore!");
            await stream.RequestStream.CompleteAsync();
            var note = await stream.ResponseAsync;
            Console.WriteLine($"Here is the note: {note.Message}");
        }
    }
}
