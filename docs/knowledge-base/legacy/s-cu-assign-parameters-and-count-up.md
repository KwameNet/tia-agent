# S_CU: Assign parameters and count up

**Category:** Legacy  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 63  

---

S_CU: Assign parameters and count up (S7-1500)
S_CU: Assign parameters and count up
Description
You can use the "Assign parameters and count up" instruction to increment the value of a
counter. When the signal state of the CU parameter changes from "0" to "1" (positive signal
edge), the current counter value is incremented by one. The current counter value is provi-
ded at the parameter CV. The counter value is incremented until the limit of "999" is
reached. When the limit value is reached, the counter value is no longer incremented on a
positive signal edge.
When the signal state of the S parameter changes from "0" to "1", the counter value is set
to the value of the PV parameter. If the counter is set and if the result of logic operation
(RLO) at the CU input is "1", the counter counts once in the next cycle, even when no sig-
nal edge change is detected.
The counter value is set to zero when the signal state of the R parameter changes to "1".
As long as the R parameter has the signal state "1", a change in the signal state of the
parameters CU and S has no effect on the counter value.
The signal state at parameter Q is "1" if the counter value is greater than zero. When the
counter value equals zero, parameter Q returns signal state "0".
Note
Only use a counter at a single point in the program to avoid the risk of counting errors.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
Counter operations
C_NO Input COUNTER, INT C
The number of counters
depends on the CPU.
CU Input BOOL I, Q, M, D, L Count up input
Input for presetting the
S Input BOOL I, Q, M, D, L
counter
Preset counter value
PV Input WORD I, Q, M, D, L (C#0 to C#999) in BCD
format
R Input BOOL I, Q, M, D, L Reset input
Q Output BOOL I, Q, M, D, L Status of the counter
CV Output WORD I, Q, M, D, L Current counter value
Current counter value in
Function value WORD I, Q, M, D, L
BCD format
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
S_CU: Assign parameters and count up (S7-1500)
SCL
"Tag_Result" := S_CU(C_NO := "Counter_1",
CU := "Tag_Start",
S := "Tag_1",
PV := "Tag_PresetValue",
R := "Tag_Reset",
Q => "Tag_Status",
CV => "Tag_Value");
If the signal state of the "Tag_Start" parameter changes from "0" to "1" (positive signal
edge) and the current counter value is less than "999", the counter value is incremented by
one. If the signal state of the "Tag_1" input changes from "0" to "1", the counter value in
BCD format is set to the value of the "Tag_PresetValue" operand. The counter value is re-
set to "0" when the "Tag_Reset" operand has signal state "1".
The current counter value is stored in hexadecimal form in the operand "Tag_Value".
The "Tag_Status" output has the signal state "1" as long as the current counter value is not
equal to "0". The current counter value is returned in the "Tag_Value" operand and as a
function value.
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL