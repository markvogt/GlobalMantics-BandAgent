using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//ADD COMPILER DIRECTIVE for using Azure Devices.Client library...
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

namespace Globalmantics.BandAgent
{
    class Program
    {
        //DECLARE & INITIALIZE a DeviceConnectionString setting for access Azure IoT Hub...
        private const String strDeviceConnectionString = "HostName=ps-course-IoT-Hub.azure-devices.net;DeviceId=device-01;SharedAccessKey=UzzCD7ugx7hNjaOg4c5ETEoY3eJGyPipM8LlPRrPAZQ=";

        //BEGIN the main() method that is a "console app"...
        //REPLACE "void" output of method with "async Task" for working asychronously with Azure IoT more smooooothly...
        static async Task Main(string[] args)
        {
            //Console.WriteLine("Hello World from Mark and BandAgent!");
            //LOG initialize entry to console... 
            Console.WriteLine("INITIALIZING Band Agent, and connection to Azure IoT Hub...");

            //INSTANTIATE a new "device client" object... 
            var oDeviceClient = DeviceClient.CreateFromConnectionString(strDeviceConnectionString);

            //ESTABLISH a connection between the device on Azure IoT Hub...
            oDeviceClient.OpenAsync();

            //LOG successful connection to console... 
            Console.WriteLine("Device successfully connected ! ...");

            //INITIALIZE a counter for the loop...
            var count = 1; 

            //BEGIN looping (endlessly to start)...
            while(true){

                //INSTANTIATE a new Telemetry object for working with messages in a loop... 
                var oTelemetry = new Telemetry {
                    Message = "Sending complex object to AzureIoTHub...",
                    StatusCode = count++                 
                };

                //SERIALIZE this oTelemtry object into a JSON object which CAN be streamed up into AzureIoTHub...
                var oTelemetryJSON = JsonConvert.SerializeObject(oTelemetry);

                //INSTANTIATE a new message using the Message class... 
                //var oMessage = new Message( Encoding.ASCII.GetBytes("This is a message the console app for device-01...") );
                var oMessage = new Message(Encoding.ASCII.GetBytes(oTelemetryJSON));

                //SEND the message asynchronously and wait for re response... 
                await oDeviceClient.SendEventAsync(oMessage);

                //LOG current status to the console...
                Console.WriteLine("oMessage sent to AzureIoTHub in the cloud...");

                //SLEEP for some time... 
                Thread.Sleep(2000);

            }
            
            //INSTRUCT Console App User to press a key to exit...
            Console.WriteLine("Press any key to exit... ");

            //START listening to the console for any key input... 
            Console.ReadKey();

        }
    }
}
