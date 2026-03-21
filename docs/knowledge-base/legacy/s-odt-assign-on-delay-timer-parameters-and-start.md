# S_ODT: Assign on-delay timer parameters and start

**Category:** Legacy  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 38  

---

S_ODT: Assign on-delay timer parameters and start (S7-1500)
S_ODT: Assign on-delay timer parameters and start
Description
The "Assign on-delay timer parameters and start" instruction starts a programmed timer as
on-delay when a positive signal edge is detected at the S parameter. The timer runs for the
programmed time (TV) as long as the signal state of the S parameter is "1".
If the timer expires correctly and parameter S still has signal state "1" then parameter Q
returns signal state "1". If the signal state at the S parameter changes from "1" to "0" while
the timer is running, the timer is stopped. In this case, output Q is reset to signal state "0".
Internally, the time is made up of a time value and a time base and is programmed in the
TV parameter. When the instruction starts, the programmed time value counts down to
zero. The time base specifies the time increment by which the time value changes. The
current time value is provided at the parameter BI.
If the time is running and the signal state at input R changes from "0" to "1" then the current
time value and the time base are also set to zero. In this case, the signal state at parame-
ter Q is "0". The timer is reset if the signal state at the R parameter is "1", even if the timer
is not running and the result of logic operation (RLO) at the S parameter is "1".
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
S_ODT: Assign on-delay timer parameters and start (S7-1500)
Pulse timing diagram
The following figure shows the pulse timing diagram of the "Assign on-delay timer parame-
ters and start" instruction:
Example
The following example shows how the instruction works:
SCL
"Tag_Result" := S_ODT(T_NO := "Timer_1",
S := "Tag_1",
TV := "Tag_Number",
R := "Tag_Reset",
Q => "Tag_Status",
BI => "Tag_Value");
"Timer_1" starts when the signal state of the "Tag_1" operand changes from "0" to "1". The
timer runs for the duration "Tag_Number" as long as the signal state of operand "Tag_1" is
"1".
If the timer expires correctly and operand "Tag_Status" has signal state "1" then operand
"Tag_Status" is reset to "1". If the signal state of the "Tag_1" operand changes from "1" to
"0" while the timer is running, the timer is stopped. In this case, operand "Tag_Status" re-
turns signal state "0".
S_ODT: Assign on-delay timer parameters and start (S7-1500)
The current time value is stored both dual-coded at the "Tag_Value" operand and returned
as a function value.
See also
Overview of the valid data types
L: Load timer value (S7-1500)
Memory areas (S7-1500)
Basics of SCL