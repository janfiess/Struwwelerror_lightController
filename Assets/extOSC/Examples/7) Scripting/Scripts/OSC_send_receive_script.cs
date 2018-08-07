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

        public Color color_toilette;
        // private OSCReceiver _receiver;
        // LEDs Toilette -> OSC in OSC_sender.cs
        public GameObject softwareLight_toilette_gui;
        // float masterfader_toilette;
        public Slider masterfader_toilette_ui;
        Color prevLed_toilette_Color;

        #endregion

        #region Unity Methods

        protected virtual void Start()
        {
            string IP_toilette = ip_textfield_toilette.text;
            if (ip_textfield_toilette.text == "") IP_toilette = ip_textfield_toilette.placeholder.GetComponent<Text>().text;

            string IP_lichterkette = ip_textfield_lichterkette.text;
            if (ip_textfield_lichterkette.text == "") IP_lichterkette = ip_textfield_lichterkette.placeholder.GetComponent<Text>().text;


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

        public void Toilette_toggle()
        {
            if (masterfader_toilette_ui.value <= 0.5f)
            {
                // StartCoroutine(SmoothValue(masterfader_toilette_ui, 1f));
                masterfader_toilette_ui.value = 1f;
            }
            else if (masterfader_toilette_ui.value > 0.5f)
            {
                // StartCoroutine(SmoothValue(masterfader_toilette_ui, 0f));
                masterfader_toilette_ui.value = 0f;
            }
        }
        // IEnumerator SmoothValue(Slider slider, float target)
        // {
        //     float journey = 0f;
        //     float origin = slider.value;
        //     float duration = 1.0f;
        //     while (journey <= duration)
        //     {
        //         journey = journey + Time.deltaTime;
        //         float percent = Mathf.Clamp01(journey / duration);
        //         // description: float interpolated result between startValue and endValue = Vector3.Lerp(float startValue, float endValue, float interpolation value between the two floats)
        //         slider.value = Mathf.Lerp(origin, target, percent);
        //         yield return null;
        //     }
        // }

        protected virtual void Update()
        {
            if (color_toilette != prevLed_toilette_Color)
            {
                // print (color_toilette);
                // DMXData[port_led_toilette] = (byte)(color_toilette.r * masterfader_toilette_ui.value * 255);


                var message = new OSCMessage("/1/driveLight");
                message.AddValue(OSCValue.Int((int)(color_toilette.r * masterfader_toilette_ui.value * 255)));

                // ));

                // Send message
                _transmitter_toilette.Send(message);




                // see color also on an UI element in scene
                softwareLight_toilette_gui.GetComponent<Image>().color = new Color(
                    color_toilette.r,
                    color_toilette.r,
                    color_toilette.r,
                    masterfader_toilette_ui.value);

                prevLed_toilette_Color = color_toilette;
            }
        }

        #endregion

        #region Protected Methods

        protected void MessageReceived(OSCMessage message)
        {
            Debug.Log(message);
        }

        #endregion

        public void SendOscMsg(string oscAddress, float value)
        {
            if (_transmitter_toilette == null) return;

            // Create message
            var message = new OSCMessage(oscAddress);
            message.AddValue(OSCValue.Float(value));

            // Send message
            _transmitter_toilette.Send(message);
        }

        // public void SendOscArrayMsg()
        // {
        //     if (_transmitter_lichterkette == null) return;

        //     var message = new OSCMessage("/1/fader1");

        //     for (int i = 0; i < 120; i++)
        //     {
        //         message.AddValue(OSCValue.Int(i + 1));
        //     }

        //     // Send message
        //     _transmitter_lichterkette.Send(message);
        // }
    }
}