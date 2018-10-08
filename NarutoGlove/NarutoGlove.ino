#include <SoftwareSerial.h>
#include <SerialCommand.h>

SerialCommand sCmd;
static int input1 = A0;
static int input2 = A1;
static int input3 = A2;
static int input4 = A3;
static int input5 = A4;
static int input6 = A5;
static int input7 = 2;
int threshold = 30;//950; // min reading for it to be considered active
int waitOut = 200; //Duration to hold the hand signs

char rx_byte = 0;
String message = "";

void setup() {
  pinMode(input1, INPUT_PULLUP);
  pinMode(input2, INPUT_PULLUP);
  pinMode(input3, INPUT_PULLUP);
  pinMode(input4, INPUT_PULLUP);
  pinMode(input5, INPUT_PULLUP);
  pinMode(input6, INPUT_PULLUP);
  //pinMode(input7, INPUT);
  pinMode(13, OUTPUT);
  pinMode(10, OUTPUT);
  Serial.begin(9600);
  
  while(!Serial);
  sCmd.addCommand("PING", pingHandler);
  sCmd.addCommand("ECHO", echoHandler);
  sCmd.addCommand("PALM", palmHandler);
  sCmd.addCommand("TIGER", tigerHandler);
  sCmd.addCommand("SNAKE", snakeHandler);
  sCmd.addCommand("BOAR", boarHandler);
  sCmd.addCommand("HORSE", horseHandler);
  
}

void loop() {
  if(Serial.available()>0){
    sCmd.readSerial();
  }
//  testRead();
}

void horseHandler(const char *command){
  checkHorse();
}

void boarHandler(const char *command){
  checkBoar();
}

void snakeHandler(const char *command){
  checkSnake();
}

void palmHandler(const char *command){
  checkPalm();
}

void tigerHandler(const char *command){
  checkTiger();
}

void pingHandler(const char *command){
  Serial.println("PONG");
}

void echoHandler(){
  char *arg;
  arg = sCmd.next();
  if(arg != NULL)
    Serial.println(arg);
  else
    Serial.println("Nothing to echo");
}

void testUnity(){
    if(Serial.available()>0){
    sCmd.readSerial();
    digitalWrite(13, HIGH);
    }
  delay(50);
  digitalWrite(13, LOW);
  delay(50);
}

void testRead(){
  int record1 = analogRead(input1);
  int record2 = analogRead(input2);
  int record3 = analogRead(input3);
  int record4 = analogRead(input4);
  int record5 = analogRead(input5);
  int record6 = analogRead(input6);
  int record7 = analogRead(input7);
  Serial.print("N1 :");
  Serial.print(record1);
  Serial.print(" N2 :");
  Serial.print(record2);
  Serial.print(" N3 :");
  Serial.print(record3);
  Serial.print(" N4 :");
  Serial.print(record4);
  Serial.print(" N5 :");
  Serial.print(record5);
  Serial.print(" N6 :");
  Serial.print(record6);
  Serial.println("");
  delay(500);
}

void checkPalm(){
  int record3 = analogRead(input3);
  int record4 = analogRead(input4);
  int record5 = analogRead(input5);
  if(record3 < threshold and record4 < threshold and record5 < threshold){
    delay(waitOut);
  record3 = analogRead(input3);
  record4 = analogRead(input4);
  record5 = analogRead(input5);
    if(record3 < threshold and record4 < threshold and record5 < threshold){
      Serial.println("Palm check");
      digitalWrite(13, HIGH);
    }
    else{
      //Serial.println("Palm failed");
      digitalWrite(13, LOW);
    }
  }
  else{
    //Serial.println("Palm failed");
    digitalWrite(13, LOW);
    delay(waitOut);
  }
}

void checkTiger(){
  int record2 = analogRead(input2);
  int record3 = analogRead(input3);
  int record5 = analogRead(input5);
  if(record2 < threshold and record3 < threshold and record5 < threshold){
    delay(waitOut);
    record2 = analogRead(input2);
    record3 = analogRead(input3);
    record5 = analogRead(input5);
    if(record2 < threshold and record3 < threshold and record5 < threshold){
      Serial.println("Tiger check");
      digitalWrite(10, HIGH);
    }
    else{
      digitalWrite(10, LOW);
    }
  }
  else{
    digitalWrite(10, LOW);
    delay(waitOut);
  }
}

void checkMonkey(){
  int record1 = analogRead(input1);
  int record4 = analogRead(input4);
  if(record1 < threshold and record4 < threshold){
    delay(waitOut);
    record1 = analogRead(input1);
    record4 = analogRead(input4);
    if(record1 < threshold and record4 < threshold){
      Serial.println("Monkey Check");
    }
    else{
      
    }
  }
  else{
    
  }
}

void checkRat(){
  int record3 = analogRead(input3);
  int record5 = analogRead(input5);
  if(record3 < threshold and record5 < threshold){
    delay(waitOut);
    record3 = analogRead(input3);
    record5 = analogRead(input5);
    if(record3 < threshold and record5 < threshold){
      Serial.println("Rat check");
    }
    else{
      //fail one
    }
  }
  else{
    //fail two
  }
}

void checkSnake(){
  int record5 = analogRead(input5);
  if(record5 < threshold){
    delay(waitOut);
    record5 = analogRead(input5);
    if(record5 < threshold){
      Serial.println("Snake check");
      digitalWrite(10, HIGH);
    }
    else {
      digitalWrite(10, LOW);
    }
  }
  else{
    digitalWrite(10, LOW);
    delay(waitOut);
  }
}

void checkBoar(){
  int record6 = analogRead(input6);
  if(record6 < threshold){
    delay(waitOut);
    record6 = analogRead(input6);
    if(record6 < threshold){
      Serial.println("Boar check");
      digitalWrite(10, HIGH);
    }
    else {
      digitalWrite(10, LOW);
    }
  }
  else{
    digitalWrite(10, LOW);
    delay(waitOut);
  }
}

void checkHorse(){
  int record2 = analogRead(input2);
  if(record2 < threshold){
    delay(waitOut);
    record2 = analogRead(input2);
    if(record2 < threshold){
      Serial.println("Horse check");
      digitalWrite(10, HIGH);
    }
    else {
      digitalWrite(10, LOW);
    }
  }
  else{
    digitalWrite(10, LOW);
    delay(waitOut);
  }
}


