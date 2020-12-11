#include <dht.h>
dht DHT;

#define DHT11_PIN 7

int light;

int sensor_pin = A1;

int output_value ;

void setup() {

   Serial.begin(9600);

   Serial.println("Reading From the Sensor ...");

   delay(2000);

   }

void loop() {

   output_value= analogRead(sensor_pin);

   output_value = map(output_value,1023,0,0,100);

   Serial.print("Mositure : ");

   Serial.print(output_value);

   Serial.println("%");

    light = 1023-analogRead(A0);
    Serial.print("Lichtinval = ");
//    lichtinval in procenten berekening
      light = (light/1023)*100;
      Serial.print(light);
      Serial.println("%");
    
//    if (light < 10) {
//      Serial.println("Donker");
//    } else if (light < 200) {
//      Serial.println("Schemerig");
//    } else if (light < 500) {
//      Serial.println("Licht");
//    } else if (light < 800) {
//      Serial.println("Fel");
//    } else {
//      Serial.println("Heel fel");
//    }
    int chk = DHT.read11(DHT11_PIN);
    Serial.print("Temperatuur = ");
    Serial.print(DHT.temperature);
    Serial.println(" C");
    Serial.print("Luchtvochtigheid = ");
    Serial.print(DHT.humidity);
    Serial.println("%");
    Serial.println("");
    delay(1500);

   }
