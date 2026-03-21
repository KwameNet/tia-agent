# CTU: Count up

**Category:** Counter operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 53  

---

CTU: Count up (S7-1200, S7-1500)
CTU: Count up
Description
You can use the "Count up" instruction to increment the value at the CV parameter. When
the signal state of the parameter CU changes from "0" to "1" (positive signal edge), the in-
struction is executed and the current counter value of the parameter CV is incremented by
one. The counter value is incremented each time a positive signal edge is detected, until it
reaches the high limit of the data type specified at the CV parameter. When the high limit is
reached, the signal state of the CU parameter no longer has an effect on the instruction.
You can query the count status of the Q parameter. The signal state of the Q parameter is
determined by the PV parameter. When the current counter value is greater than or equal
to the value of the PV parameter, the Q parameter is set to signal state "1". In all other ca-
ses, the signal state of the Q parameter is "0". You can also specify a constant for the PV
parameter.
The value of the CV parameter is reset to zero when the signal state at the R parameter
changes to "1". As long as the signal state of the R parameter is "1", the signal state of the
CU parameter has no effect on the instruction.
Note
Only use a counter at a single point in the program to avoid the risk of counting errors.
Each call of the "Count up" instruction must be assigned an IEC counter in which the in-
struction data is stored. An IEC counter is a structure with one of the following data types:
For CPUs of the S7-1200 series
Data block of system data type IEC_<Counter> (Shared DB) Local tag
• IEC_SCOUNTER / IEC_USCOUNTER • CTU_SINT / CTU_USINT
• IEC_COUNTER / IEC_UCOUNTER • CTU_INT / CTU_UINT
• IEC_DCOUNTER / IEC_UDCOUNTER • CTU_DINT / CTU_UDINT
For CPUs of the S7-1500 series
Data block of system data type IEC_<Counter> (Shared DB) Local tag
• IEC_SCOUNTER / IEC_USCOUNTER • CTU_SINT / CTU_USINT
• IEC_COUNTER / IEC_UCOUNTER • CTU_INT / CTU_UINT
• IEC_DCOUNTER / IEC_UDCOUNTER • CTU_DINT / CTU_UDINT
• IEC_LCOUNTER / IEC_ULCOUNTER • CTU_LINT / CTU_ULINT
You can declare an IEC counter as follows:
• Declaration of an instance data block of system data type IEC_<Counter> (for example,
"MyIEC_COUNTER_DB")
• Declaration as local tag of the data type CTUD_<DatenType> in the "Static" section of a
program block (for example, #MyIEC_COUNTER_Instance)
When you set up the IEC counter in a separate data block (single instance), the instance
data block is created by default with "optimized block access" and the individual tags are
defined as retentive.
CTU: Count up (S7-1200, S7-1500)
When you set up the IEC counter as local tag (multi-instance) in a function block with "opti-
mized block access", it is defined as retentive in the block interface.
IEC counter as data block of system data type IEC_<Counter> (Shared DB)
You can declare the IEC counter as a data block as follows:
<IEC_Counter_DB>.CTU();
IEC counter as local tag from the block interface (multi-instance)
You can declare the IEC counter as a local tag as follows:
#myLocal_Counter();
For information on calling IEC counters within structures (multi-instance), please refer to:
Calling IEC counters
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
CU Input BOOL I, Q, M, D, L Count input
R Input BOOL I, Q, M, D, L, P Reset input
Value at which the Q out-
PV Input Integers I, Q, M, D, L, P
put is set
Q Output BOOL I, Q, M, D, L Counter status
Integers, CHAR,
CV Output I, Q, M, D, L, P Current counter value
WCHAR, DATE
Example
The following example shows how the instruction works:
SCL
"IEC_COUNTER_DB".CTU(CU := "Tag_Start",
R := "Tag_Reset",
PV := "Tag_PresetValue",
Q => "Tag_Status",
CV => "Tag_CounterValue");
When the signal state of the "Tag_Start" operand changes from "0" to "1", the "Count up"
instruction is executed and the current counter value of the "Tag_CounterValue" operand is
incremented by one. With each additional positive signal edge, the counter value is incre-
mented until the high limit of the specified data type (INT = 32767) is reached.
The "Tag_Status" output has signal state "1" as long as the current counter value is greater
than or equal to the value of the "Tag_PresetValue" operand. In all other cases, the
"Tag_Status" output has signal state "0". The current counter value is stored in the
"Tag_CounterValue" operand.
See also
CTU: Count up (S7-1200, S7-1500)
Overview of the valid data types
Setting retentivity in an instance data block
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)