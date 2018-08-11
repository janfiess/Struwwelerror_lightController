#include <ESP8266WiFi.h>

const int extLed1 = 15;
const int extLed2 = 4;
const int extLed3 = 14;
const int extLed4 = 13;
const int boardLed = LED_BUILTIN;
const int ledPin = 2; 

void setup() {
    pinMode(extLed1, OUTPUT);
    pinMode(extLed2, OUTPUT);
    pinMode(extLed3, OUTPUT);
    pinMode(extLed4, OUTPUT);
    pinMode(boardLed, OUTPUT); 

    digitalWrite(extLed1, 1);// on
    digitalWrite(extLed2, 1);// on
    digitalWrite(extLed3, 1);// on
    digitalWrite(extLed4, 1);// on
    digitalWrite(boardLed, 1);// off
    digitalWrite(ledPin, 1); // off
    
}

void loop() {
  // put your main code here, to run repeatedly:

}
