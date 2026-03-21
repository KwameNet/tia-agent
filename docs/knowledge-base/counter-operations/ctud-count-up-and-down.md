# CTUD: Count up and down

**Category:** Counter operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 59  

---

CTUD: Count up and down (S7-1200, S7-1500)
CTUD: Count up and down
Description
Use the "Count up and down" instruction to increment or decrement the counter value at
the CV parameter. When the signal state of the CU parameter changes from "0" to "1"
(positive signal edge), the current counter value of the CV parameter is incremented by
one. When the signal state of the CD parameter changes from "0" to "1" (positive signal
edge), the counter value of the CV parameter is decremented by one. If there is a positive
signal edge at the CU and CD inputs in a program cycle, the current counter value of the
CV parameter remains unchanged.
The counter value can be incremented until it reaches the high limit of the data type speci-
fied at the CV parameter. When the high limit is reached, the counter value is no longer
incremented on a positive signal edge. The counter value is no longer decremented once
the low limit of the specified data type has been reached.
When the signal state of the LD parameter changes to "1", the counter value of the CV pa-
rameter is set to the value of the PV parameter. As long as the LD parameter has signal
state "1", the signal state of the CU and CD parameters has no effect on the instruction.
The counter value is set to zero when the signal state of the R parameter changes to "1".
As long as the R parameter has signal state "1", a change in the signal state of the CU, CD
and LD parameters has no effect on the "Count up and down" instruction.
You can query the status of the up counter at the QU parameter. When the current counter
value is greater than or equal to the value of the PV parameter, the QU parameter is set to
signal state "1". In all other cases, the signal state of the QU parameter is "0". You can also
specify a constant for the PV parameter.
You can query the status of the down counter at the QD parameter. If the current counter
value is less than or equal to zero, the QD parameter is set to signal state "1". In all other
cases, the signal state of the QD parameter is "0".
Note
Only use a counter at a single point in the program to avoid the risk of counting errors.
Each call of the "Count up and down" instruction must be assigned an IEC counter in which
the instruction data is stored. An IEC counter is a structure with one of the following data
types:
For CPUs of the S7-1200 series
Data block of system data type IEC_<Counter> (Shared DB) Local tag
• IEC_SCOUNTER / IEC_USCOUNTER • CTUD_SINT / CTUD_USINT
• IEC_COUNTER / IEC_UCOUNTER • CTUD_INT / CTUD_UINT
• IEC_DCOUNTER / IEC_UDCOUNTER • CTUD_DINT / CTUD_UDINT
For CPUs of the S7-1500 series
Data block of system data type IEC_<Counter> (Shared DB) Local tag
• IEC_SCOUNTER / IEC_USCOUNTER • CTUD_SINT / CTUD_USINT
• IEC_COUNTER / IEC_UCOUNTER • CTUD_INT / CTUD_UINT
CTUD: Count up and down (S7-1200, S7-1500)
• IEC_DCOUNTER / IEC_UDCOUNTER • CTUD_DINT / CTUD_UDINT
• IEC_LCOUNTER / IEC_ULCOUNTER • CTUD_LINT / CTUD_ULINT
You can declare an IEC counter as follows:
• Declaration of an instance data block of system data type IEC_<Counter> (for example,
"MyIEC_COUNTER_DB")
• Declaration as local tag of the data type CTUD_<DatenType> in the "Static" section of a
program block (for example, #MyCTUD_COUNTER_Instance)
When you set up the IEC counter in a separate data block (single instance), the instance
data block is created by default with "optimized block access" and the individual tags are
defined as retentive.
When you set up the IEC counter as local tag (multi-instance) in a function block with "opti-
mized block access", it is defined as retentive in the block interface.
IEC counter as data block of system data type IEC_<Counter> (Shared DB)
You can declare the IEC counter as a data block as follows:
<IEC_Counter_DB>.CTUD();
IEC counter as local tag from the block interface (multi-instance)
You can declare the IEC counter as a local tag as follows:
#myLocal_Counter();
For information on calling IEC counters within structures (multi-instance), please refer to:
Calling IEC counters
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
CU Input BOOL I, Q, M, D, L Count up input
CD Input BOOL I, Q, M, D, L Count down input
R Input BOOL I, Q, M, D, L, P Reset input
LD Input BOOL I, Q, M, D, L, P Load input
Value at which the QU
output is set / value to
PV Input Integers I, Q, M, D, L, P
which the CV output is
set when LD = 1.
QU Output BOOL I, Q, M, D, L Status of the up counter
Status of the down coun-
QD Output BOOL I, Q, M, D, L
ter
Integers, CHAR,
CV Output I, Q, M, D, L, P Current counter value
WCHAR, DATE
Example
The following example shows how the instruction works:
CTUD: Count up and down (S7-1200, S7-1500)
SCL
"IEC_COUNTER_DB".CTUD(CU := "Tag_Start1",
CD := "Tag_Start2",
LD := "Tag_Load",
R := "Tag_Reset",
PV := "Tag_PresetValue",
QU => "Tag_CU_Status",
QD => "Tag_CD_Status",
CV => "Tag_CounterValue");
If the "Tag_Start1" operand has a positive signal edge in the signal state, the current coun-
ter value is incremented by one and stored in the "Tag_CounterValue" operand. If the
"Tag_Start2" operand has a positive signal edge in the signal state, the counter value is de-
cremented by one and is also stored in the "Tag_CounterValue" operand. The counter val-
ue is incremented on the positive signal edge of the CU parameter until it reaches the high
limit of the specified data type (INT). If the CD parameter has a positive signal edge, the
counter value is decremented until it reaches the low limit of the specified data type (INT).
The operand "Tag_CU_Status" has signal state "1" as long as the current counter value is
greater than or equal to the value of the operand "Tag_PresetValue". In all other cases, the
"Tag_CU_Status" output has signal state "0".
The operand "Tag_CD_Status" has signal state "1" as long as the current counter value is
less than or equal to zero. In all other cases, the "Tag_CD_Status" output has signal state
"0".
See also
Overview of the valid data types
Setting retentivity in an instance data block
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)