// script attached to the Manager GameObject
/* Copyright (c) 2018 ExT (V.Sigalkin) */

using UnityEngine;
using extOSC.Examples;

namespace extOSC.Examples
{
    public class OSC_send_receive_script : MonoBehaviour
    {
        public int localPort = 8000;
        public int remotePort = 9000;
        public string RemoteIp = "192.168.0.61";  
        // public string _oscAddress = "/1/fader1";  

        #region Private Vars

        private OSCTransmitter _transmitter;

        private OSCReceiver _receiver;

        #endregion

        #region Unity Methods

        protected virtual void Start()
        {
            // Creating a transmitter.
            _transmitter = gameObject.AddComponent<OSCTransmitter>();

            // Set remote host address.
            _transmitter.RemoteHost = RemoteIp;    

            // Set remote port;
            _transmitter.RemotePort = remotePort;                             


            // // Creating a receiver.
            // _receiver = gameObject.AddComponent<OSCReceiver>(); 

            // // Set local port.
            // _receiver.LocalPort = localPort;              

            // // Bind "MessageReceived" method to special address.
            // _receiver.Bind(_oscAddress, MessageReceived);               
        }

        protected virtual void Update()
        {
        }

        #endregion

        #region Protected Methods

        protected void MessageReceived(OSCMessage message)
        {
            Debug.Log(message);
        }

        #endregion

        public void SendOscMsg(string oscAddress, float value){
            if (_transmitter == null) return;

            // Create message
            var message = new OSCMessage(oscAddress);
            // message.AddValue(OSCValue.String("/1/fader1"));
            message.AddValue(OSCValue.Float(value));

            // Send message
            _transmitter.Send(message);
        }
    }
}