# TP: Generate pulse

**Category:** Timer operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 10  

---

TP: Generate pulse (S7-1200, S7-1500)
TP: Generate pulse
Description
You can use the "Generate pulse" instruction to set the parameter Q for a programmed du-
ration. The instruction is started when the result of logic operation (RLO) of the IN parame-
ter changes from "0" to "1" (positive signal edge). The programmed time PT begins when
the instruction starts. The Q parameter is set for the duration PT, independent of the further
course of the input signal. While the duration PT is running, the detection of a new positive
signal edge at the IN input has no influence on the signal state at the Q output.
You can query the current time value set in the ET parameter. The timer value starts at
T#0s and ends when the value of the time duration PT is reached. When the time duration
PT is reached and the signal state at the IN parameter is "0", the ET parameter is reset.
Note
If the instruction is not called in the program because it is skipped, for example, the ET
output returns a constant value as soon as the time has expired.
Each call of the "Generate pulse" instruction must be assigned to an IEC timer in which the
instance data is stored.
For information on calling IEC timers within structures (multi-instance), please refer to:
Calling IEC timers
For CPUs of the S7-1200 series
An IEC timer is a structure of the data type IEC_TIMER or TP_TIME that you can declare
as follows:
• Declaration of an instance data block of system data type IEC_TIMER (for example,
"MyIEC_TIMER_DB")
• Declaration as local tag of the data type TP_TIME in the "Static" section of a program
block (for example, #MyIEC_TIMER_Instance)
For CPUs of the S7-1500 series
An IEC Timer is a structure of the data type IEC_TIMER, IEC_LTIMER, TP_TIME or
TP_LTIME that you can declare as follows:
• Declaration of an instance data block of system data type IEC_TIMER or IEC_LTIMER
(for example, "MyIEC_LTIMER_DB")
• Declaration as local tag of the data type TP_TIME or TP_LTIME in the "Static" section of
a program block (for example, #MyIEC_LTIMER_Instance)
IEC timer as instance data block of system data type IEC_<Timer> (Shared
DB)
You can declare the IEC timer as a data block as follows:
<IEC_Timer_DB>.TP();
IEC timer as local tag from the block interface (multi-instance)
You can declare the IEC timer as a local tag as follows:
#myLocal_Timer();
TP: Generate pulse (S7-1200, S7-1500)
Updating the actual values in the instance data
The instance data from "Generate pulse" is updated according to the following rules:
• IN input
The "Generate pulse" instruction compares the current RLO with the RLO from the previ-
ous query, which is saved in the IN parameter in the instance data. If the instruction de-
tects a change in the RLO from "0" to "1", there is a positive signal edge and the time
measurement is started. After the "Generate pulse" instruction has been processed, the
value of the IN parameter is updated in the instance data and is used as edge memory
bit for the next query.
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
The internal parameters of the "Generate pulse" instruction are used to calculate the
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
• Make sure that the program does not read or write the affected data during transmis-
sion.
TP: Generate pulse (S7-1200, S7-1500)
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
S7-1200 S7-1500
I, Q, M, D, L,
IN Input BOOL BOOL Start input
P
Duration of
the pulse.
TIME, I, Q, M, D, L,
PT Input TIME The value of
LTIME P
the PT pa-
rameter must
be positive.
Operand that
I, Q, M, D, L,
Q Output BOOL BOOL is set for the
P
PT duration.
TIME, I, Q, M, D, L, Current timer
ET Output TIME
LTIME P value
Pulse timing diagram
The following figure shows the pulse timing diagram of the "Generate pulse" instruction:
Example
The following example shows how the instruction works:
SCL
"TP_DB".TP(IN := "Tag_Start",
PT := "Tag_PresetTime",
TP: Generate pulse (S7-1200, S7-1500)
Q => "Tag_Status",
ET => "Tag_ElapsedTime");
When the signal state of the "Tag_Start" operand changes from "0" to "1", the time pro-
grammed for the PT parameter is started and the "Tag_Status" operand is set to "1". The
current time value is stored in the "Tag_ElapsedTime" operand.
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)