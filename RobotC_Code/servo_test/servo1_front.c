#pragma config(Hubs,  S1, HTMotor,  HTServo,  none,     none)
#pragma config(Servo,  srvo_S1_C2_1,    servo1,                     tServoNormal)
#pragma config(Servo,  srvo_S1_C2_2,    servo2,                     tServoNormal)
//*!!Code automatically generated by 'ROBOTC' configuration wizard               !!*//

/*--------------------------------------------------------------------------------------------------------*\
|*                                                                                                        *|
|*                                    - Servo Control by NXT Motor -                                      *|
|*                                        ROBOTC on NXT + TETRIX                                          *|
|*                                                                                                        *|
|*  This program simply sweeps the servo back and forth.  Servo Change Rate can be changed by changing    *|
|*  'delta'.  (0 = max servo speed, # > 0 = how many servo positions to move per update (every 20ms).     *|
|*                                                                                                        *|
|*                                        ROBOT CONFIGURATION                                             *|
|*    NOTES:                                                                                              *|
|*    1)  Be sure to attach a servo somewhere on the robot.                                               *|
|*                                                                                                        *|
|*    MOTORS & SENSORS:                                                                                   *|
|*    [I/O Port]              [Name]              [Type]              [Description]                       *|
|*    Port 1                  none                TETRIX Controller   TETRIX                              *|
|*    Port 1 - Chan. 1        servo1              std. Servo          The servo that you wish to test.    *|
\*---------------------------------------------------------------------------------------------------4246-*/
task main()
{
  int delta = 2;                        // Create int 'delta' to the be Servo Change Rate.

    servoChangeRate[servo1] = delta;// Slow the Servo Change Rate down to only 'delta' positions per update.
		servoChangeRate[servo2] = delta;

    servo[servo1] += 10;                              // Move servo1 to position to 255.
    wait1Msec(500);                          // Wait 1 second.
}