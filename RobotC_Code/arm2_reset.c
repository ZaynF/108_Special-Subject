#pragma config(Hubs,  S1, HTMotor,  HTServo,  none,     none)
#pragma config(Servo,  srvo_S1_C2_1,    servo1,                     tServoNormal)
#pragma config(Servo,  srvo_S1_C2_2,    servo2,                     tServoNormal)
#pragma config(Servo,  srvo_S1_C2_3,    servo3,                     tServoNormal)

task main()
{
  int delta = 2;                        // Create int 'delta' to the be Servo Change Rate.

    servoChangeRate[servo1] = delta;// Slow the Servo Change Rate down to only 'delta' positions per update.
		servoChangeRate[servo2] = delta;
		servoChangeRate[servo3] = delta;

    while(1)               // While the ServoValue of servo1 is less than 255:
      {
      	if(ServoValue[servo3] == 50){
      		break;
      	}else{
      		servo[servo3] = 50;                      // Move servo1 to position to 255.
      	}
        servo[servo1] = 255;
      }
}
