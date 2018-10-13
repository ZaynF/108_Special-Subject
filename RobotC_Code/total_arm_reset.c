#pragma config(Hubs,  S1, HTMotor,  HTServo,  none,     none)
#pragma config(Sensor, S1,     ,               sensorI2CMuxController)
#pragma config(Motor,  motorA,           ,             tmotorNXT, openLoop)
#pragma config(Motor,  motorB,           ,             tmotorNXT, openLoop)
#pragma config(Motor,  motorC,           ,             tmotorNXT, openLoop)
#pragma config(Motor,  mtr_S1_C1_1,     motorD,        tmotorTetrix, openLoop, reversed)
#pragma config(Motor,  mtr_S1_C1_2,     motorE,        tmotorTetrix, openLoop)
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
      	if(ServoValue[servo2] == 45 && ServoValue[servo3] == 60){
      		break;
      	}else{
      		servo[servo2] = 45;
      		servo[servo3] = 60;
      	}
    }
}
