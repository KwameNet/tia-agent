# CTD: Count down

**Category:** Counter operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 56  

---

CTD: Count down (S7-1200, S7-1500)
CTD: Count down
Description
The "Count down" instruction is used to decrement the value at the parameter CV. When
the signal state of the CD parameter changes from "0" to "1" (positive signal edge), the in-
struction is executed and the current counter value of the CV parameter is decremented by
one. Each time a positive signal edge is detected, the counter value is decremented until it
reaches the low limit of the specified data type. When the low limit is reached, the signal
state of the CD parameter no longer has an effect on the instruction.
You can query the count status of the Q parameter. If the current counter value is less than
or equal to zero, the Q parameter is set to signal state "1". In all other cases, the signal
state of the Q parameter is "0". You can also specify a constant for the PV parameter.
The value of the CV parameter is set to the value of the PV parameter when the signal
state of the LD parameter changes to "1". As long as the signal state of the LD parameter
is "1", the signal state of the CD parameter has no effect on the instruction.
Note
Only use a counter at a single point in the program to avoid the risk of counting errors.
Each call of the "Count down" instruction must be assigned an IEC counter in which the
instruction data is stored. An IEC counter is a structure with one of the following data types:
For CPUs of the S7-1200 series
Data block of system data type IEC_<Counter> (Shared DB) Local tag
• IEC_SCOUNTER / IEC_USCOUNTER • CTD_SINT / CTD_USINT
• IEC_COUNTER / IEC_UCOUNTER • CTD_INT / CTD_UINT
• IEC_DCOUNTER / IEC_UDCOUNTER • CTD_DINT / CTD_UDINT
For CPUs of the S7-1500 series
Data block of system data type IEC_<Counter> (Shared DB) Local tag
• IEC_SCOUNTER / IEC_USCOUNTER • CTD_SINT / CTD_USINT
• IEC_COUNTER / IEC_UCOUNTER • CTD_INT / CTD_UINT
• IEC_DCOUNTER / IEC_UDCOUNTER • CTD_DINT / CTD_UDINT
• IEC_LCOUNTER / IEC_ULCOUNTER • CTD_LINT / CTD_ULINT
You can declare an IEC counter as follows:
• Declaration of an instance data block of system data type IEC_<Counter> (for example,
"MyIEC_COUNTER_DB")
• Declaration as local tag of the data type CTD_<DatenType> in the "Static" section of a
program block (for example, #MyIEC_COUNTER_Instance)
When you set up the IEC counter in a separate data block (single instance), the instance
data block is created by default with "optimized block access" and the individual tags are
defined as retentive.
When you set up the IEC counter as local tag (multi-instance) in a function block with "opti-
mized block access", it is defined as retentive in the block interface.
CTD: Count down (S7-1200, S7-1500)
IEC counter as data block of system data type IEC_<Counter> (Shared DB)
You can declare the IEC counter as a data block as follows:
<IEC_Counter_DB>.CTD();
IEC counter as local tag from the block interface (multi-instance)
You can declare the IEC counter as a local tag as follows:
#myLocal_Counter();
For information on calling IEC counters within structures (multi-instance), please refer to:
Calling IEC counters
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
CD Input BOOL I, Q, M, D, L Count input
LD Input BOOL I, Q, M, D, L, P Load input
Value to which the CV
PV Input Integers I, Q, M, D, L, P
output is set with LD = 1.
Q Output BOOL I, Q, M, D, L Counter status
Integers, CHAR,
CV Output I, Q, M, D, L, P Current counter value
WCHAR, DATE
Example
The following example shows how the instruction works:
SCL
"IEC_SCOUNTER_DB".CTD(CD := "Tag_Start",
LD := "Tag_Load",
PV := "Tag_PresetValue",
Q => "Tag_Status",
CV => "Tag_CounterValue");
When the signal state of the "Tag_Start" operand changes from "0" to "1", the instruction is
executed and the value of the "Tag_CounterValue" operand is decremented by one. With
each additional positive signal edge, the counter value is decremented until it reaches the
low limit of the specified data type (-128).
The operand "Tag_Status" has signal state "1" as long as the current counter value is less
than or equal to zero. In all other cases, the "Tag_Status" output has signal state "0". The
current counter value is stored in the "Tag_CounterValue" operand.
See also
Overview of the valid data types
Setting retentivity in an instance data block
Memory areas (S7-1500)
CTD: Count down (S7-1200, S7-1500)
Basics of SCL
Memory areas (S7-1200)