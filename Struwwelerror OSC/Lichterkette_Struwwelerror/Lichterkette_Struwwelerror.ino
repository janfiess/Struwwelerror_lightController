/**
 * Receive OSC messages at NodeMCU from any OSC speaking sender.
 * Case: Switch an LED (connected to NodeMCU) on or off via Smartphone
 * Created by Jan Fiess in August 2018
 * Inspired by Oscuino Library Examples, Make Magazine 12/2015, https://trippylighting.com/teensy-arduino-ect/touchosc-and-arduino-oscuino/
 */



#include <ESP8266WiFi.h>
#include <WiFiUdp.h>
#include <OSCBundle.h>                 // for receiving OSC messages

// Neopixels

#include <Adafruit_NeoPixel.h>
#ifdef __AVR__
  #include <avr/power.h>
#endif

// Which pin on the Arduino is connected to the NeoPixels?
// On a Trinket or Gemma we suggest changing this to 1
#define PIN            2

// How many NeoPixels are attached to the Arduino?
#define NUMPIXELS      40

// When we setup the NeoPixel library, we tell it how many pixels, and which pin to use to send signals.
// Note that for older NeoPixel strips you might need to change the third parameter--see the strandtest
// example for more information on possible values.
Adafruit_NeoPixel strip = Adafruit_NeoPixel(NUMPIXELS, PIN, NEO_GRB + NEO_KHZ800);

int delayval = 500; // delay for half a second




// WLAN

char ssid[] = "dreammakers";                 // your network SSID (name)
char pass[] = "dreammakers";              // your network password

// Button Input + LED Output
const int ledPin = 2;                 // D5 pin at NodeMCU
const int boardLed = LED_BUILTIN;      // Builtin LED


WiFiUDP Udp;                           // A UDP instance to let us send and receive packets over UDP
const unsigned int localPort = 9000;   // local port to listen for UDP packets at the NodeMCU (another device must send OSC messages to this port)
const unsigned int destPort = 8000;    // remote port of the target device where the NodeMCU sends OSC to

//unsigned int ledState = 1;             // LOW means led is *on*
float intensity = 0; 
void setup() {
    Serial.begin(115200);

     // Specify a static IP address for NodeMCU
     // If you erase this line, your ESP8266 will get a dynamic IP address
    WiFi.config(IPAddress(192,168,0,125),IPAddress(192,168,0,1), IPAddress(255,255,255,0)); 

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
 
    analogWrite(ledPin, PWMRANGE); // off
    // analogWrite(boardLed, PWMRANGE); // off


    
     // Neopixels

    // This is for Trinket 5V 16MHz, you can remove these three lines if you are not using a Trinket
    #if defined (__AVR_ATtiny85__)
      if (F_CPU == 16000000) clock_prescale_set(clock_div_1);
    #endif
    // End of trinket special code
  
    strip.begin(); // This initializes the NeoPixel library.
  
    uint16_t i;
    // switch al LEDs on means switch them off on ESP8266
    for(i=0; i<strip.numPixels(); i++) {
      strip.setPixelColor(i,strip.Color(0,0,0));
    }
        
    strip.show(); // Initialize all pixels to 'off'
}

void loop() {
  OSCMessage msgIN;
  int size;
  if((size = Udp.parsePacket())>0){
   // Serial.println("size; ");
    //Serial.println(size);
    while(size--)
      msgIN.fill(Udp.read());
    if(!msgIN.hasError()){

      Serial.println("msg received"); 
      // Serial.println(msgIN);
          
      msgIN.route("/1/fader1",lightStatic);
      //msgIN.route("/1/intensity",changeLightIntensity);
    }
  }
  delay(50);
}


void lightStatic(OSCMessage &msg, int addrOffset){
  int i = 0;
  
  //Serial.println(msg.getInt(0));
  Serial.println("go");
  
  for(i = 0; i < strip.numPixels(); i ++){
    // int myReceivedOscMsg = msg.getInt(i);
    strip.setPixelColor(i,strip.Color(msg.getInt(3*i) ,msg.getInt(3*i + 1), msg.getInt(3*i + 2)));
    Serial.println();
    Serial.println(i);
    Serial.println(msg.getInt(i));
  }

  
  
  //strip.setPixelColor(0,strip.Color(255,255,255));
  //strip.setPixelColor(1,strip.Color(255,255,0));
  strip.show();
  
  
  
  

  //Serial.println("LED on");
  //analogWrite(ledPin, PWMRANGE - intensity * PWMRANGE);
  //analogWrite(boardLed, PWMRANGE - intensity * PWMRANGE);  
}




/*
void changeLightIntensity(OSCMessage &msg, int addrOffset){
  float myReceivedOscMsg = msg.getFloat(0);
  Serial.println(myReceivedOscMsg);
  intensity = myReceivedOscMsg;
  analogWrite(ledPin, PWMRANGE - intensity * PWMRANGE);
  analogWrite(boardLed, PWMRANGE - intensity * PWMRANGE);
}*/
