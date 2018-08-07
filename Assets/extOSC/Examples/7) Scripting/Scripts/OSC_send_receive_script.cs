// script attached to the Manager GameObject
/* Copyright (c) 2018 ExT (V.Sigalkin) */

using UnityEngine;
using UnityEngine.UI;
using extOSC.Examples;

namespace extOSC.Examples
{
    public class OSC_send_receive_script : MonoBehaviour
    {
        // public int localPort = 8000;
        public int remotePort_toilette = 9000, remotePort_lichterkette = 9000;
        public InputField ip_textfield_toilette, ip_textfield_lichterkette;

        #region Private Vars

        [HideInInspector] public OSCTransmitter _transmitter_toilette, _transmitter_lichterkette;

        // private OSCReceiver _receiver;

        #endregion

        #region Unity Methods

        protected virtual void Start()
        {
            string IP_toilette = ip_textfield_toilette.text;
            if(ip_textfield_toilette.text == "") IP_toilette = ip_textfield_toilette.placeholder.GetComponent<Text>().text;

            string IP_lichterkette = ip_textfield_lichterkette.text;
            if(ip_textfield_lichterkette.text == "") IP_lichterkette = ip_textfield_lichterkette.placeholder.GetComponent<Text>().text;
                 

            // Creating a transmitter.
            _transmitter_toilette = gameObject.AddComponent<OSCTransmitter>();
            _transmitter_lichterkette = gameObject.AddComponent<OSCTransmitter>();

            // Set remote host address.
            _transmitter_toilette.RemoteHost = IP_toilette;   
            _transmitter_lichterkette.RemoteHost = IP_lichterkette; 

            // Set remote port;
            _transmitter_toilette.RemotePort = remotePort_toilette; 
            _transmitter_lichterkette.RemotePort = remotePort_lichterkette;                             


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
            if (_transmitter_toilette == null) return;

            // Create message
            var message = new OSCMessage(oscAddress);
            // message.AddValue(OSCValue.String("/1/fader1"));
            message.AddValue(OSCValue.Float(value));

            // Send message
            _transmitter_toilette.Send(message);
        }

        public void SendOscArrayMsg(){
            if (_transmitter_lichterkette == null) return;

            var message = new OSCMessage("/1/fader1");

            for(int i = 0; i < 120; i++){
                message.AddValue(OSCValue.Int(i+1));
            }

            // Send message
            _transmitter_lichterkette.Send(message);
        }
    }
}