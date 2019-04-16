using System;
using System.Reflection;
using System.Threading;
using Grpc.Core;
using NLog;
using SSD.ATLASti.Common.HelloWorld.Server;

namespace HelloWorldServer
{
    internal static class Program
    {
        private const           string ServerHost = "127.0.0.1";
        private const           int    ServerPort = 50052;
        private static readonly Logger Log        = LogManager.GetCurrentClassLogger();

        private static void LogVersion(this Assembly assembly)
        {
            var assemblyName = assembly.GetName();
            Log.Info($"{assemblyName.Name}: {assemblyName.Version}");
        }
        
        public static void Main(string[] args)
        {
            var exitEvent = new ManualResetEvent(false);

            Console.CancelKeyPress += (sender, eventArgs) =>
                                      {
                                          eventArgs.Cancel = true;
                                          exitEvent.Set();
                                      };

            var port = 0;
            if (args.Length == 1)
            {
                int.TryParse(args[0], out port);
            }

            if (port == 0)
            {
                port = ServerPort;
            }

            var protobufServer = ProtobufServer.StartInBackground(ServerHost, port);

            exitEvent.WaitOne();

            protobufServer.Shutdown();
        }
    }
}
