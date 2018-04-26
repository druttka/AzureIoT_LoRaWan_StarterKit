﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace LoRaWan.NetworkServer
{
    public class UdpServer
    {
        const int port = 1680;

        public async Task RunServer()
        {
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
                using (var udpClient = new UdpClient(endPoint))
                {
                    Console.WriteLine($"UDP Listener started on port {port}");

                    while (true)
                    {
                        UdpReceiveResult receivedResults = await udpClient.ReceiveAsync();
                        Console.WriteLine($"UDP message received ({receivedResults.Buffer.Length} bytes).");
                        MessageProcessor messageProcessor = new MessageProcessor();
                        await messageProcessor.processMessage(receivedResults.Buffer);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Failed to start UDP Listener on port {port}: {ex.Message}");
            }
        }
    }
}
