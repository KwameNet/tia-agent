# S_ODTS: Assign retentive on-delay timer parameters and start

**Category:** Legacy  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 41  

---

S_ODTS: Assign retentive on-delay timer parameters and start (S7-1500)
S_ODTS: Assign retentive on-delay timer parame-
ters and start
Description
The "Assign retentive on-delay timer parameters and start" instruction starts a programmed
timer when a positive signal edge is detected at the S parameter. The timer runs for the
programmed time (TV) even if the signal state at the S parameter changes to "0".
When the timer expires, the "Q" parameter returns signal state "1" regardless of the signal
state of the "S" parameter. If the signal state at the S parameter changes from "0" to "1"
while the timer is running, the timer is restarted with the programmed time TV.
Internally, the time is made up of a time value and a time base and is programmed in the
TV parameter. When the instruction starts, the programmed time value counts down to
zero. The time base specifies the time increment by which the time value changes. The
current time value is provided at the parameter BI.
Signal state "1" at parameter R resets the current time value and time base to "0", inde-
pendent of the signal state at parameter S. In this case, the signal state at parameter Q is
"0".
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
S5TIME,
TV Input I, Q, M, D, L Preset time value
WORD
R Input BOOL I, Q, M, D, L, P Reset input
Q Output BOOL I, Q, M, D, L, P Status of the timer
Current dual-coded timer
BI Output WORD I, Q, M, D, L, P
value
Function value S5TIME I, Q, M, D, L Current timer value
For additional information on valid data types, refer to "See also".
S_ODTS: Assign retentive on-delay timer parameters and start (S7-1500)
Pulse timing diagram
The following figure shows the pulse timing diagram of the "Assign retentive on-delay timer
parameters and start" instruction:
Example
The following example shows how the instruction works:
SCL
"Tag_Result" := S_ODTS(T_NO := "Timer_1",
S := "Tag_1",
TV := "Tag_Number",
R := "Tag_Reset",
Q => "Tag_Status",
BI => "Tag_Value");
"Timer_1" starts when the signal state of the "Tag_1" operand changes from "0" to "1". The
timer runs for the duration "Tag_Number".
If the timer expires, operand "Tag_Status" returns signal state "1" independent of the signal
state of operand "Tag_1". If the signal state of the "Tag_1" operand changes from "0" to "1"
while the timer is running, the timer is restarted with the time "Tag_Number".
The current time value is stored both dual-coded at the "Tag_Value" operand and returned
as a function value.
See also
Overview of the valid data types
L: Load timer value (S7-1500)
Memory areas (S7-1500)
Basics of SCL
S_ODTS: Assign retentive on-delay timer parameters and start (S7-1500)