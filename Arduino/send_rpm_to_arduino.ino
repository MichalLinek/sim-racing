char c;
String appendSerialData;
byte incomingByte;
int led = 13;
int tachPin = 9;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  pinMode(tachPin, OUTPUT);
}

void loop() {
  if (Serial.available() > 0) {
    // read the incoming byte:
    c = Serial.read();
    appendSerialData += c;
  }
  if (c == '#') {
    int val = appendSerialData.substring(0, appendSerialData.length() -1).toInt();
    tone(tachPin, val / 30);
    appendSerialData = "";
  }
}
