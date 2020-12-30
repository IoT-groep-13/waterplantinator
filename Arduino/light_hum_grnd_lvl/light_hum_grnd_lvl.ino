#include <dht.h>
dht DHT;

#define DHT11_PIN 7

int light;

int sensor_pin = A1;

int output_value;


#define sensorPower 8
#define sensorPin A3

// Value for storing water level
int val = 0;
const int led = 13;

void setup() {

   Serial.begin(9600);

   Serial.println("Reading From the Sensor ...");

   
    pinMode(sensorPower, OUTPUT);
    digitalWrite(sensorPower, LOW);
    pinMode(led, OUTPUT);

   delay(2000);

   }

void loop() {

    int analogValue = analogRead(A0);

    Serial.print("Analog reading = ");
    Serial.print(analogValue); 
  
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
    
    int level = readSensor();
    
    Serial.print("Water level: ");
    Serial.print(level);
    Serial.println("%");
  
    if(level <= 20){
      digitalWrite(led, HIGH);
    }
    else {
      digitalWrite(led, LOW);
    }
    Serial.println("");
    delay(1500);

   }

   int readSensor() {
    digitalWrite(sensorPower, HIGH);  // Turn the sensor ON
    delay(10);              // wait 10 milliseconds
    val = analogRead(sensorPin);    // Read the analog value form sensor
    digitalWrite(sensorPower, LOW);   // Turn the sensor OFF
    return val;             // send current reading
  }
