# TON: Generate on-delay

**Category:** Timer operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 14  

---

TON: Generate on-delay (S7-1200, S7-1500)
TON: Generate on-delay
Description
You can use the "Generate on-delay" instruction to delay the setting of the Q parameter for
the programmed duration PT. The instruction is started when the result of logic operation
(RLO) of the IN parameter changes from "0" to "1" (positive signal edge). The programmed
time PT begins when the instruction starts. When the PT duration has expired, the Q pa-
rameter returns signal state "1". The Q parameter remains set as long as the start input is
still "1". If the signal state of the IN parameter changes from "1" to "0", the parameter Q will
be reset. The timer function is restarted when a new positive signal edge is detected at the
IN parameter.
The current time value can be queried in the ET parameter. The time value starts at T#0s
and ends when the value of the time duration PT is reached. The ET parameter is reset as
soon as the signal state of the IN parameter changes to "0".
Note
If the instruction is not called in the program because it is skipped, for example, the ET
output returns a constant value as soon as the time PT has expired.
Each call of the "Generate on-delay" instruction must be assigned to an IEC timer in which
the instance data is stored.
For information on calling IEC timers within structures (multi-instance), please refer to:
Calling IEC timers
For CPUs of the S7-1200 series
An IEC timer is a structure of the data type IEC_TIMER or TON_TIME that you can declare
as follows:
• Declaration of an instance data block of system data type IEC_TIMER (for example,
"MyIEC_TIMER_DB")
• Declaration as local tag of the data type TON_TIME in the "Static" section of a program
block (for example, #MyIEC_TIMER_Instance)
For CPUs of the S7-1500 series
An IEC Timer is a structure of the data type IEC_TIMER, IEC_LTIMER, TON_TIME or
TON_LTIME that you can declare as follows:
• Declaration of an instance data block of system data type IEC_TIMER or IEC_LTIMER
(for example, "MyIEC_TIMER_DB")
• Declaration as local tag of the data type TON_TIME or TON_LTIME in the "Static" sec-
tion of a program block (for example, #MyIEC_TIMER_Instance)
IEC timer as instance data block of system data type IEC_<Timer> (Shared
DB)
You can declare the IEC timer as a data block as follows:
<IEC_Timer_DB>.TON();
IEC timer as local tag from the block interface (multi-instance)
You can declare the IEC timer as a local tag as follows:
TON: Generate on-delay (S7-1200, S7-1500)
#myLocal_Timer();
Updating the actual values in the instance data
The instance data from "Generate on-delay" is updated according to the following rules:
• IN input
The "Generate on-delay" instruction compares the current RLO with the RLO from the
previous query, which is saved in the IN parameter in the instance data. If the instruction
detects a change in the RLO from "0" to "1", there is a positive signal edge and the time
measurement is started. After the "Generate on-delay" instruction has been processed,
the value of the IN parameter is updated in the instance data and is used as edge mem-
ory bit for the next query.
Note that the edge evaluation is disrupted when the actual values of the IN parameter
are written or initialized by other functions.
• PT input
The value at the PT input is written to the PT parameter in the instance data when the
edge changes at the IN input.
• Q and ET outputs
The actual values of the Q and ET outputs are updated in the following cases:
o When the instruction is called, if the ET or Q outputs are interconnected.
Or
o At an access to Q or ET.
If the outputs are not interconnected and also not queried, the current time value at the
Q and ET outputs is not updated. The outputs are not updated, even if the instruction is
skipped in the program.
The internal parameters of the "Generate on-delay" instruction are used to calculate the
time values for Q and ET. Note that the time measurement is disrupted when the actual
values of the instruction are written or initialized by other functions.
DANGER
Danger when reinitializing the actual values
Reinitializing the actual values of an IEC timer while the time measurement is running dis-
rupts the function of the IEC timer. Changing the actual values can result in inconsisten-
cies between the program and the actual process. This can cause serious damage to
property and personal injury.
The following functions can cause the actual values to be reinitialized:
• Loading the block with reinitialization
• Loading snapshots as actual values
• Controlling or forcing the actual values
• The "WRIT_DBL" instruction
Before you execute these functions, take the following precautions:
• Make sure that the plant is in a safe state before you overwrite the actual values.
• Make sure that the IEC timer has expired before initializing its actual values.
• If you overwrite the actual values with a snapshot, make sure that the snapshot was
taken at a time when the system was in a safe state.
TON: Generate on-delay (S7-1200, S7-1500)
• Make sure that the program does not read or write the affected data during transmis-
sion.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
S7-1200 S7-1500
I, Q, M, D, L,
IN Input BOOL BOOL Start input
P
Duration of
the on delay.
TIME, I, Q, M, D, L,
PT Input TIME The value of
LTIME P
the PT pa-
rameter must
be positive.
Operand that
I, Q, M, D, L, is set when
Q Output BOOL BOOL
P the timer PT
expires.
TIME, I, Q, M, D, L, Current timer
ET Output TIME
LTIME P value
Pulse timing diagram
The following figure shows the pulse timing diagram of the "Generate on-delay" instruction:
Example
The following example shows how the instruction works:
SCL
TON: Generate on-delay (S7-1200, S7-1500)
"TON_DB".TON(IN := "Tag_Start",
PT := "Tag_PresetTime",
Q => "Tag_Status",
ET => "Tag_ElapsedTime");
When the signal state of the "Tag_Start" operand changes from "0" to "1", the time pro-
grammed for the PT parameter is started. When the time duration expires, the "Tag_Sta-
tus" operand is set to the signal state "1". The Tag_Status operand remains set to "1" as
long as the Tag_Start operand has signal state "1". The current time value is stored in the
"Tag_ElapsedTime" operand. When the signal state at the operand "Tag_Start" changes
from "1" to "0", the "Tag_Status" operand is reset.
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)