#include <SPI.h>
#include <Ethernet.h>

// luchtV temp
#include <dht.h>
dht DHT;
#define DHT11_PIN 7
// Grondvochtigheid
int sensor_pin = A1;
int output_value ;

// Lichtsensor
float light;

float lvl;

// Incoming data
String data;

String readString;
// network configuration.  gateway and subnet are optional.

 // the media access control (ethernet hardware) address for the shield:
byte mac[] = { 0xA8, 0x61, 0x0A, 0xAE, 0x88, 0x47 };  

//the IP address for the shield:
IPAddress ip(192,168,178,35);

EthernetServer server(80);
void setup()
{
  
  pinMode(4,OUTPUT);
  digitalWrite(4,LOW);
  // initialize the ethernet device
  Ethernet.begin(mac, ip);

  // start listening for clients
  server.begin();
  
  Serial.begin(9600);

  Serial.print("Server address: ");
  Serial.println(Ethernet.localIP());
}

void loop()
{  
    Licht();
  
    HumidityTemp();

    Grond();

    Level();

    Serial.println();
  
  // if an incoming client connects, there will be bytes available to read:
  EthernetClient client = server.available();
  
  if (client) {
    Serial.println("new client");

    boolean currentLineIsBlank = true;
    while(client.connected())
    {
      if(client.available())
      {               
        
        char thisChar = client.read();
        //Serial.print(thisChar);
        
        data += thisChar;

        if(thisChar == '>') 
        {
          Serial.println(data);
          
          // Outgoing data
          char outBuf[20];
          
          if(data == "<HUMIDITY>"){ 
            sprintf(outBuf,"<HUMIDITY>%d",int(DHT.humidity));
            Serial.println(outBuf);
            server.write(outBuf);
            outBuf[0] = 0;
          }
          if(data == "<LIGHT>"){ 
            sprintf(outBuf,"<LIGHT>%d",int(light));
            Serial.println(outBuf);
            server.write(outBuf);
            outBuf[0] = 0;
          }
          if(data == "<TEMPERATURE>"){ 
            sprintf(outBuf,"<TEMPERATURE>%d",int(DHT.temperature));
            Serial.println(outBuf);
            server.write(outBuf);
            outBuf[0] = 0;
          }
          if(data == "<WATERTANK>"){ 
            sprintf(outBuf,"<WATERTANK>%d", int(lvl));
            Serial.println(outBuf);
            server.write(outBuf);
            outBuf[0] = 0;
          }
          if(data == "<SOILMOIST>"){ 
            sprintf(outBuf,"<SOILMOIST>%d",int(output_value));
            Serial.println(outBuf);
            server.write(outBuf);
            outBuf[0] = 0;
          }

          
          data = "";
          
        }
         
      }else{
        Serial.println("Client unavailable");
      }
    }

    delay(1);

//    client.stop();
//    Serial.println("client disconnected");
  }

  while(Serial.available() > 0)
  {
    char data = Serial.read();
    server.print(data);
  }
}




/**********************************************************
 * luchtvochtigheid
 * temperatuur
 * ********************************************************/

void HumidityTemp(){
  int chk = DHT.read11(DHT11_PIN);
  
  Serial.print("Temperature = ");
  Serial.println(DHT.temperature);
  
  Serial.print("Humidity = ");
  Serial.println(DHT.humidity);
  delay(1500);
}


/**********************************************************
 * grondvochtigheid
 * ********************************************************/

void Grond() {

   output_value= analogRead(sensor_pin);

   output_value = map(output_value,1023,0,0,100);

   Serial.print("Mositure : ");

   Serial.print(output_value);

   Serial.println("%");
  
   if(output_value < 70) {
      //digitalWrite(4,HIGH);
   }
   else {
      digitalWrite(4,LOW);
   }

}

/********************************************************
 * Lichtinval
 ********************************************************/

void Licht() {
  
  int analogValue = analogRead(A0);
  light = 1023-analogValue;
  
  Serial.print("Lichtinval = ");
  light = (light/1023)*100;
  Serial.print(light);
  Serial.println("%");    
}


/********************************************************
 * Waterlevel
 ********************************************************/

 void Level() {
  
  lvl = analogRead(A2);
  
  Serial.print("Waterlevel = ");
  
  lvl = map(lvl, 0, 900, 0, 255);
  
  if(lvl > 100) {
    lvl = 100;
  }
  
  Serial.print(lvl);
  Serial.println("%");
 }
