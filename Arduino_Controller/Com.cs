using CommandMessenger;
using CommandMessenger.Transport.Serial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Arduino_Controller
{
    // This is the list of recognized commands. These can be commands that can either be sent or received. 
    // In order to receive, attach a callback function to these events
    enum Command
    {
        SetLed,
        Status,
    };

    class Com
    {
        public bool RunLoop { get; set; }
        private SerialTransport _serialTransport;
        private CmdMessenger _cmdMessenger;
        private bool _ledState;
        private int _count;

        public Com()
        {
            SetupCommunication("COM3", 115200);
        }

        private void SetupCommunication(string COMPort, int BaudRate)
        {
            _ledState = false;

            // Create Serial Port transport object
            // Note that for some boards (e.g. Sparkfun Pro Micro) DtrEnable may need to be true.
            _serialTransport = new SerialTransport
            {
                CurrentSerialSettings = { PortName = COMPort, BaudRate = BaudRate, DtrEnable = false } // object initializer
            };

            // Initialize the command messenger with the Serial Port transport layer
            // Set if it is communicating with a 16- or 32-bit Arduino board
            _cmdMessenger = new CmdMessenger(_serialTransport, BoardType.Bit32);

            // Attach the callbacks to the Command Messenger
            AttachCommandCallBacks();

            // Start listening
            var status = _cmdMessenger.Connect();
            if (!status)
            {
                Console.WriteLine("No connection could be made");
                return;
            }
        }

        // Loop function
        public void Loop()
        {
            _count++;

            // Create command
            var command = new SendCommand((int)Command.SetLed, _ledState);

            // Send command
            _cmdMessenger.SendCommand(command);

            // Wait for 1 second and repeat
            Thread.Sleep(1000);
            _ledState = !_ledState;                                        // Toggle led state  

            if (_count > 100) RunLoop = false;                             // Stop loop after 100 rounds
        }

        // Exit function
        public void Exit()
        {

            if (_cmdMessenger != null)
            {
                // Stop listening
                _cmdMessenger.Disconnect();
                // Dispose Command Messenger
                _cmdMessenger.Dispose();
            }


            // Dispose Serial Port object
            if (_serialTransport != null) _serialTransport.Dispose();
        }

        /// Attach command call backs. 
        private void AttachCommandCallBacks()
        {
            _cmdMessenger.Attach(OnUnknownCommand);
            _cmdMessenger.Attach((int)Command.Status, OnStatus);
        }

        /// Executes when an unknown command has been received.
        void OnUnknownCommand(ReceivedCommand arguments)
        {
            Console.WriteLine("Command without attached callback received");
        }

        // Callback function that prints the Arduino status to the console
        void OnStatus(ReceivedCommand arguments)
        {
            Console.Write("Arduino status: ");
            Console.WriteLine(arguments.ReadStringArg());
        }
    }
}
