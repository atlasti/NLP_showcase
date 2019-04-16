using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using NLog;
using SSD.ATLASti.Common.HelloWorld.Model;
using ProgressCallback = System.Func<float, string, System.Threading.Tasks.Task>;

namespace SSD.ATLASti.Common.HelloWorld.Server
{
    public static class GrpcServerExtensions
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public static void Shutdown(this Grpc.Core.Server server)
        {
            Log.Info("Server is shutting down...");

            server.ShutdownAsync().Wait();
        }
    }

    public class ProtobufServer : HelloWorldService.HelloWorldServiceBase
    {
        private static readonly Logger Log                                = LogManager.GetCurrentClassLogger();

        public static Grpc.Core.Server StartInBackground(string host, int port)
        {
            var server = new Grpc.Core.Server(new List<ChannelOption>
                                              {
                                                  new
                                                      ChannelOption(ChannelOptions.MaxReceiveMessageLength,
                                                                    int.MaxValue),
                                                  new
                                                      ChannelOption(ChannelOptions.MaxSendMessageLength,
                                                                    int.MaxValue)
                                              })
                         {
                             Services = {HelloWorldService.BindService(new ProtobufServer())},
                             Ports    = {new ServerPort(host, port, ServerCredentials.Insecure)}
                         };

            var thread = new Thread(server.Start) {IsBackground = true};
            thread.Start();

            Log.Info($"Starting server on {host}:{port}...");

            return server;
        }

        private static string CallPythonScript(string scriptName, string parameters)
        {
            const string fileName  = "python";
            var          arguments = $"{scriptName} {parameters}";
            var process = new Process
                          {
                              StartInfo =
                              {
                                  UseShellExecute = false, RedirectStandardOutput = true,
                                  FileName        = fileName, Arguments           = arguments
                              }
                          };
            process.Start();
            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return output;
        }

        public override async Task SayHelloAgain(SayHelloAgainActionRequest request, IServerStreamWriter<SayHelloAgainActionReply> responseStream, ServerCallContext context)
        {
            Log.Info($"SayHelloAgainActionRequest with name {request.Name} received.");
            
            await responseStream.WriteAsync(new SayHelloAgainActionReply
                {PercentageComplete = 0.0f, StatusMessage = "Started..."});

            await responseStream.WriteAsync(new SayHelloAgainActionReply
            {
                PercentageComplete = 0.1f,
                StatusMessage      = "Thinking..."
            });

            var message = CallPythonScript(GetScriptPath("HelloWorld.py"), request.Name);
            
            var reply = new SayHelloAgainActionReply
            {
                PercentageComplete = 100.0f, StatusMessage = $"Remembered {request.Name}",
                HelloWorld = new Model.HelloWorld { Message = message}
            };
            await responseStream.WriteAsync(reply);

            Log.Info("Replying with hello again message.");
        }

        private static string GetScriptPath(string fileName, [CallerFilePath] string sourceFilePath = "") => Path.Combine(Path.GetDirectoryName(sourceFilePath), fileName);
        
        public override Task<SayHelloActionReply> SayHello(SayHelloActionRequest request, ServerCallContext context)
        {
            Log.Info($"SayHelloAgainActionRequest with name {request.Name} received.");

            var message = CallPythonScript(GetScriptPath("HelloWorld.py"), request.Name);
            
            var reply = new SayHelloActionReply
            {
                HelloWorld = new Model.HelloWorld { Message = message }
            };
            
            Log.Info("Replying with hello world message.");
            
            return Task.FromResult(reply);
        }
    }
}
