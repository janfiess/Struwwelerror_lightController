/**
 * Receive OSC messages at NodeMCU from any OSC speaking sender.
 * Case: Switch an LED (connected to NodeMCU) on or off via Smartphone
 * Created by Jan Fiess in August 2018
 * Inspired by Oscuino Library Examples, Make Magazine 12/2015, https://trippylighting.com/teensy-arduino-ect/touchosc-and-arduino-oscuino/
 */

#include <ESP8266WiFi.h>
#include <WiFiUdp.h>
#include <OSCBundle.h>                 // for receiving OSC messages

char ssid[] = "dreammakers";                 // your network SSID (name)
char pass[] = "dreammakers";              // your network password

// Button Input + LED Output
const int ledPin = 2;                 // D5 pin at NodeMCU
const int boardLed = LED_BUILTIN;      // Builtin LED
const int extLed1 = 15;
const int extLed2 = 4;
const int extLed3 = 14;
const int extLed4 = 13;

WiFiUDP Udp;                           // A UDP instance to let us send and receive packets over UDP
const unsigned int localPort = 9000;   // local port to listen for UDP packets at the NodeMCU (another device must send OSC messages to this port)
const unsigned int destPort = 8000;    // remote port of the target device where the NodeMCU sends OSC to

//unsigned int ledState = 1;             // LOW means led is *on*
float intensity = 0; 
void setup() {
    Serial.begin(115200);

     // Specify a static IP address for NodeMCU
     // If you erase this line, your ESP8266 will get a dynamic IP address
    WiFi.config(IPAddress(192,168,0,123),IPAddress(192,168,0,1), IPAddress(255,255,255,0)); 

    // Connect to WiFi network
    Serial.println();
    Serial.print("Connecting to ");
    Serial.println(ssid);
    WiFi.begin(ssid, pass);

    while (WiFi.status() != WL_CONNECTED) {
        delay(500);
        Serial.print(".");
    }
    
    Serial.println("");
    Serial.println("WiFi connected");
    Serial.println("IP address: ");
    Serial.println(WiFi.localIP());

    Serial.println("Starting UDP");
    Udp.begin(localPort);
    Serial.print("Local port: ");
    Serial.println(Udp.localPort());

    // LED Output
    pinMode(boardLed, OUTPUT); 
    pinMode(ledPin, OUTPUT);
    
    pinMode(extLed1, OUTPUT);
    pinMode(extLed2, OUTPUT);
    pinMode(extLed3, OUTPUT);
    pinMode(extLed4, OUTPUT);
    
    digitalWrite(ledPin, 1); // off
    digitalWrite(boardLed, 1); // off
    
    digitalWrite(extLed1, 0); // off
    digitalWrite(extLed2, 0); // off
    digitalWrite(extLed3, 0); // off
    digitalWrite(extLed4, 0); // off
    Serial.println("PWMRANGE");
        Serial.println(PWMRANGE);
}


void loop() {
  OSCMessage msgIN;
  int size;
  if((size = Udp.parsePacket())>0){
    while(size--)
      msgIN.fill(Udp.read());
    if(!msgIN.hasError()){

      //Serial.println("msg received");

      
      msgIN.route("/1/static",lightStatic);
      msgIN.route("/1/off",lightOff);
      msgIN.route("/1/intensity",changeLightIntensity);

      msgIN.route("/1/driveLight",driveLight);
    }
  }
}


void driveLight(OSCMessage &msg, int addrOffset){
  float myReceivedOscMsg = msg.getFloat(0);
  // Serial.println(myReceivedOscMsg);


  //Serial.println("drive light");
  //analogWrite(ledPin, PWMRANGE - (myReceivedOscMsg/255) * PWMRANGE);
  //analogWrite(boardLed, PWMRANGE - (myReceivedOscMsg/255) * PWMRANGE);
  // Serial.println(myReceivedOscMsg * PWMRANGE);

  analogWrite(extLed1, myReceivedOscMsg * PWMRANGE);
  analogWrite(extLed2, myReceivedOscMsg * PWMRANGE);
  analogWrite(extLed3, myReceivedOscMsg * PWMRANGE);
  analogWrite(extLed4, myReceivedOscMsg * PWMRANGE);
}





void lightStatic(OSCMessage &msg, int addrOffset){
  float myReceivedOscMsg = msg.getFloat(0);
  //Serial.println(myReceivedOscMsg);


  Serial.println("LED on");
  //digitalWrite(ledPin, 0);
  //digitalWrite(boardLed, 0);

  digitalWrite(extLed1, 1);
  digitalWrite(extLed2, 1);
  digitalWrite(extLed3, 1);
  digitalWrite(extLed4, 1);
}



void lightOff(OSCMessage &msg, int addrOffset){
  float myReceivedOscMsg = msg.getFloat(0);
  //Serial.println(myReceivedOscMsg);
  
  Serial.println("LED off");
  //digitalWrite(ledPin, 1); // off
  //digitalWrite(boardLed, 1); // off

  digitalWrite(extLed1, 0); // off
  digitalWrite(extLed2, 0); // off
  digitalWrite(extLed3, 0); // off
  digitalWrite(extLed4, 0); // off
}


void changeLightIntensity(OSCMessage &msg, int addrOffset){
  float myReceivedOscMsg = msg.getFloat(0);
  Serial.println(myReceivedOscMsg);
  intensity = myReceivedOscMsg;

  analogWrite(ledPin, PWMRANGE - intensity * PWMRANGE);
  analogWrite(boardLed, PWMRANGE - intensity * PWMRANGE);

  analogWrite(extLed1, PWMRANGE - intensity * PWMRANGE);
  analogWrite(extLed2, PWMRANGE - intensity * PWMRANGE);
  analogWrite(extLed3, PWMRANGE - intensity * PWMRANGE);
  analogWrite(extLed4, PWMRANGE - intensity * PWMRANGE);
}
