using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using MiniGrpc.Common;

namespace MiniGrpc.Server
{
    class MiniMathImpl : MiniGrpc.Common.MiniService.MiniServiceBase
    {
        public override Task<AddResponse> Add(AddRequest request, ServerCallContext context)
        {
            return Task.FromResult(new AddResponse() { C = request.A + request.B });
        }

        public async override Task<Note> ReportPosition(IAsyncStreamReader<Position> requestStream, ServerCallContext context)
        {
            
            while (await requestStream.MoveNext(CancellationToken.None))
            {
                var position = requestStream.Current;
                Console.WriteLine($"Client position at {position.Lattitude} - {position.Longitude} ");
            }
            return new Note() { Message = "Bon Voyage!" };
        }
    }

    class Program
    {
        const int Port = 50505;

        public static void Main(string[] args)
        {
            var server = new Grpc.Core.Server
            {
                Services = { MiniService.BindService(new MiniMathImpl()) },
                Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine("Listening on port " + Port);
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }
}
