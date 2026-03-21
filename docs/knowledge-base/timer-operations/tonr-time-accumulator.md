# TONR: Time accumulator

**Category:** Timer operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 22  

---

TONR: Time accumulator (S7-1200, S7-1500)
TONR: Time accumulator
Description
The instruction "Time accumulator" is used to accumulate time values within a period set
by the parameter PT. When the signal state at the IN parameter changes to "1", the time
measurement is executed and the time PT is started. While the time duration PT is run-
ning, the time values that are recorded when the IN parameter has signal state "1" are ac-
cumulated. If the signal state at the "IN" input changes to "0", the time measurement is in-
terrupted. If the signal state at the "IN" input changes back to "1", the time measurement is
restarted. The accumulated time is output in the ET parameter and can be queried there.
When the time duration PT is reached, the Q parameter has signal state "1". The Q param-
eter remains set to "1", even when the signal state at the IN parameter changes to "0".
The R parameter resets the ET and Q parameters regardless of the signal state at the IN
parameter.
Each call of the "Time accumulator" instruction must be assigned an IEC timer in which the
instance data is stored.
For information on calling IEC timers within structures (multi-instance), please refer to:
Calling IEC timers
For CPUs of the S7-1200 series
An IEC Timer is a structure of the data type IEC_TIMER or TONR_TIME that you can de-
clare as follows:
• Declaration of an instance data block of system data type IEC_TIMER (for example,
"MyIEC_TIMER_DB")
• Declaration as local tag of the data type TONR_TIME in the "Static" section of a program
block (for example, #MyIEC_TIMER_Instance)
For CPUs of the S7-1500 series
An IEC Timer is a structure of the data type IEC_TIMER, IEC_LTIMER, TONR_TIME or
TONR_LTIME that you can declare as follows:
• Declaration of an instance data block of system data type IEC_TIMER or IEC_LTIMER
(for example, "MyIEC_TIMER_DB")
• Declaration as local tag of the data type TONR_TIME or TONR_LTIME in the "Static"
section of a program block (for example, #MyIEC_TIMER_Instance)
IEC timer as instance data block of system data type IEC_<Timer> (Shared
DB)
You can declare the IEC timer as a data block as follows:
<IEC_Timer_DB>.TONR();
IEC timer as local tag from the block interface (multi-instance)
You can declare the IEC timer as a local tag as follows:
#myLocal_Timer();
Updating the actual values in the instance data
The instance data from "Time accumulator" is updated according to the following rules:
TONR: Time accumulator (S7-1200, S7-1500)
• IN input
The "Time accumulator" instruction compares the current RLO with the RLO from the
previous query, which is saved in the IN parameter in the instance data. If the instruction
detects a change in the RLO from "0" to "1", there is a positive signal edge and the time
measurement is continued. If the instruction in the RLO detects a change from "1" to "0",
there is a negative signal edge and the time measurement is interrupted. After the "Time
accumulator" instruction has been processed, the value of the IN parameter is updated
in the instance data and is used as edge memory bit for the next query.
Note that the edge evaluation is disrupted when the actual values of the IN parameter
are written or initialized by other functions.
• PT input
The value at the PT input is written to the PT parameter in the instance data when the
edge changes at the IN input.
• R input
The signal "1" at input R resets the time measurement and blocks it. Edges at the IN in-
put are ignored. The signal "0" at input R enables time measurement again.
Note
Initialize parameter R
In order to initialize parameter R, you have to define a tag which you interconnect and
then initialize in the "Time accumulator" instruction.
Example:
#IEC_Timer_0_Instance(IN:=#Start,
R:=#Reset,
PT:=_time_in_,
Q=>_bool_out_,
ET=>_time_out);
#Reset := TRUE;
• Q and ET outputs
The actual values of the Q and ET outputs are updated in the following cases:
o When the instruction is called, if the ET or Q outputs are interconnected.
Or
o At an access to Q or ET.
If the outputs are not interconnected and also not queried, the current time value at the
Q and ET outputs is not updated. The outputs are not updated, even if the instruction is
skipped in the program.
The internal parameters of the "Time accumulator" instruction are used to calculate the
time values for Q and ET. Note that the time measurement is disrupted when the actual
values of the instruction are written or initialized by other functions.
DANGER
Danger when reinitializing the actual values
Reinitializing the actual values of an IEC timer while the time measurement is running dis-
rupts the function of the IEC timer. Changing the actual values can result in inconsisten-
TONR: Time accumulator (S7-1200, S7-1500)
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
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
S7-1200 S7-1500
I, Q, M, D, L,
IN Input BOOL BOOL Start input
P
Reset of the
I, Q, M, D, L,
R Input BOOL BOOL ET and Q pa-
P
rameters
Maximum du-
ration of time
recording.
TIME, I, Q, M, D, L,
PT Input TIME
LTIME P The value of
the PT pa-
rameter must
be positive.
Operand that
remains set
I, Q, M, D, L,
Q Output BOOL BOOL when the tim-
P
er PT has ex-
pired.
TIME, I, Q, M, D, L, Accumulated
ET Output TIME
LTIME P time
Pulse timing diagram
The following figure shows the pulse timing diagram of the "Time accumulator" instruction:
TONR: Time accumulator (S7-1200, S7-1500)
Example
The following example shows how the instruction works:
SCL
"TONR_DB".TONR(IN := "Tag_Start",
R := "Tag_Reset",
PT := "Tag_PresetTime",
Q => "Tag_Status",
ET => "Tag_Time");
When the signal state of the "Tag_Start" operand changes from "0" to "1", the time pro-
grammed for the PT parameter is started. While the time is running, the time values are
accumulated that are recorded when the "Tag_Start" operand has signal state "1". The ac-
cumulated time is stored in the "Tag_Time" operand. When the time value specified for the
PT parameter is reached, the "Tag_Status" operand is set to the signal state "1". The cur-
rent time value is stored in the "Tag_Time" operand.
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)