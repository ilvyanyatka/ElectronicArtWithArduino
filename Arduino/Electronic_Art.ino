#include <Sharer.h>

#define CLEAR_PIN 3           // the number of the clear pushbutton pin
#define SAVE_PIN 5           // the number of the save pushbutton pin
#define DRAW_RATE_PIN A0
#define LINE_TYPE_PIN A3
#define STOP_PIN A2
#define LINE_WIDTH_PIN A4
#define ANGLE_PIN A5
//#define DEBUG


// variables will change:
int ClearButtonState = 0;         
int LineTypeButtonState = 0;         
int ExecButtonState = 0;         
int LineWidthValue = 0;         
int AngleValue = 0;         
int DrawRateValue = 0;
int SaveButtonState = 0;

void setup() {
  Sharer.init(115200); // Init Serial communication with 115200 bauds
  
  #ifdef DEBUG
  Serial.begin(9600); // opens serial port, sets data rate to 9600 bps
  #endif
  
  // Share variables for read/write from desktop application
  Sharer_ShareVariable(int, ClearButtonState);
  Sharer_ShareVariable(int, LineTypeButtonState);
  Sharer_ShareVariable(int, ExecButtonState);
  Sharer_ShareVariable(int, LineWidthValue);
  Sharer_ShareVariable(int, AngleValue);
  Sharer_ShareVariable(int, DrawRateValue);
  Sharer_ShareVariable(int, SaveButtonState);
  
  analogReference(DEFAULT);
  pinMode(CLEAR_PIN, INPUT_PULLUP);
  pinMode(SAVE_PIN, INPUT_PULLUP);
  pinMode(LINE_TYPE_PIN, INPUT_PULLUP);
  pinMode(STOP_PIN, INPUT_PULLUP);
  pinMode(LINE_WIDTH_PIN, INPUT);
  pinMode(ANGLE_PIN, INPUT);
  pinMode(DRAW_RATE_PIN, INPUT);
}

void loop() {
  Sharer.run();
  
  // read the state of the pushbutton value:
  ClearButtonState = digitalRead(CLEAR_PIN);
  SaveButtonState = digitalRead(SAVE_PIN);
  LineTypeButtonState = digitalRead(LINE_TYPE_PIN);
  ExecButtonState = digitalRead(STOP_PIN);
  LineWidthValue = analogRead(LINE_WIDTH_PIN);
  LineWidthValue = map(LineWidthValue, 0, 1023, 3, 20);
  AngleValue = analogRead(ANGLE_PIN);
  AngleValue = map(AngleValue, 1, 1023, 30, 178);
  DrawRateValue = analogRead(DRAW_RATE_PIN);
  DrawRateValue = map(DrawRateValue, 0, 1023, 100, 2000);
  
   
  #ifdef DEBUG
  Serial.print("Clear ");
  Serial.println(ClearButtonState);
  Serial.print("Save ");
  Serial.println(SaveButtonState);
  Serial.print("Line Type ");
  Serial.println(LineTypeButtonState);
  Serial.print("Exec ");
  Serial.println(ExecButtonState);
  Serial.print("Line Width ");
  Serial.println(LineWidthValue);
  Serial.print("Angle ");
  Serial.println(AngleValue);
  Serial.print("Draw Rate ");
  Serial.println(DrawRateValue);
  delay(2500);
  #endif
}
