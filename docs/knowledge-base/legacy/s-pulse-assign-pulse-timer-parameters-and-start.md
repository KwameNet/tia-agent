# S_PULSE: Assign pulse timer parameters and start

**Category:** Legacy  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 32  

---

S_PULSE: Assign pulse timer parameters and start (S7-1500)
S_PULSE: Assign pulse timer parameters and start
Description
The "Assign pulse timer parameters and start" instruction starts the time programmed in
the T_NO parameter when a change from "0" to "1" (positive signal edge) is detected in the
result of logic operation (RLO) of the S parameter. The timer runs for the programmed time
(TV) as long as the signal state of the S parameter is "1".
When the signal state of the S parameter changes to "0" before the programmed time has
expired, the timer is stopped and the Q parameter is reset to "0".
Internally, the time is made up of a time value and a time base and is programmed in the
TV parameter. When the instruction starts, the programmed time value counts down to
zero. The time base specifies the time increment by which the time value changes. The
current time value is provided at the parameter BI.
If the timer is running and the signal state at input R changes to "1" then the current time
value and the time base are also set to zero. If the timer is not running, the signal state "1"
at the R input has no effect.
Parameter Q returns signal state "1" as long as the timer is running and the signal state at
parameter S is "1". When the signal state of the S parameter changes to "0" before the pro-
grammed time has expired, the Q parameter returns signal state "0". If the timer is reset by
parameter R or if the timer has expired then parameter Q also returns signal state "0".
The instruction data is updated with each access. It is therefore possible that the query of
the data at the start of the cycle returns different values from those at the end of the cycle.
Note
In the time cell, the operating system reduces the time value in an interval specified by
the time base by one unit until the value equals "0". The decrementation is performed
asynchronously to the user program. The resulting timer is therefore at maximum up to
one time interval shorter than the desired time base.
You can find an example of how a time cell can be formed under: See also "L: Load timer
value".
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
The timer that is started.
T_NO Input TIMER, INT T
The number of timers de-
pends on the CPU.
S Input BOOL I, Q, M, D, L Start input
TV Input S5TIME, WORD I, Q, M, D, L Preset time value
R Input BOOL I, Q, M, D, L, P Reset input
Q Output BOOL I, Q, M, D, L, P Status of the timer
Current dual-coded timer
BI Output WORD I, Q, M, D, L, P
value
S_PULSE: Assign pulse timer parameters and start (S7-1500)
Function value S5TIME I, Q, M, D, L Current timer value
For additional information on valid data types, refer to "See also".
Pulse timing diagram
The following figure shows the pulse timing diagram of the "Assign pulse timer parameters
and start" instruction:
Example
The following example shows how the instruction works:
SCL
"Tag_Result" := S_PULSE(T_NO := "Timer_1",
S := "Tag_1",
TV := "Tag_Number",
R := "Tag_Reset",
Q => "Tag_Status",
BI => "Tag_Value");
"Timer_1" starts when the signal state of the "Tag_1" operand changes from "0" to "1". The
timer counts down with the time value of the operand "Tag_Number" until operand "Tag_1"
returns signal state "1".
S_PULSE: Assign pulse timer parameters and start (S7-1500)
If the signal state at the S parameter changes to "0" before the programmed time has
elapsed, the "Tag_Status" operand is reset to "0". If the timer is reset by the R parameter or
if the timer has expired, the "Tag_Status" operand also returns signal state "0".
The current time value is stored both dual-coded at the "Tag_Value" operand and returned
as a function value.
See also
Overview of the valid data types
L: Load timer value (S7-1500)
Memory areas (S7-1500)
Basics of SCL