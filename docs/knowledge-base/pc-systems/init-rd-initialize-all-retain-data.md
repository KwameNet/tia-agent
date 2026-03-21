# INIT_RD: Initialize all retain data

**Category:** PC systems  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 294  

---

INIT_RD: Initialize all retain data (S7-1500)
INIT_RD: Initialize all retain data
Description
The "Initialize all retain data" instruction is used to reset the retentive data of all data
blocks, bit memories and SIMATIC timers and counters at the same time. The instruction
can only be executed within a startup OB because the execution exceeds the program cy-
cle duration.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
If the input "REQ" has the
<Operand> Input BOOL I, Q, M, D, L signal state "1", all reten-
tive data are reset.
Error information:
If an error occurs during
Function value
(RET_VAL) INT I, Q, M, D, L the execution of the in-
struction, an error code is
output at the RET_VAL
parameter.
For additional information on valid data types, refer to "See also".
RET_VAL parameter
The following table shows the meaning of the values of the RET_VAL parameter:
Error code* Explanation
(W#16#...)
0000 No error
The instruction cannot be executed because it was not programmed within a
80B5
startup OB.
General
error in- See also: "GET_ERR_ID: Get error ID locally"
formation
*The error codes can be displayed as integer or hexadecimal value in the program editor.
For information on toggling display formats, refer to "See also".
Example
The following example shows how the instruction works:
SCL
"Tag_Result" := INIT_RD("Tag_REQ");
If the operand "Tag_REQ" has the signal state "1", the instruction is executed. All retentive
data of all data blocks, bit memories and SIMATIC timers and counters are reset.
INIT_RD: Initialize all retain data (S7-1500)
See also
Overview of the valid data types
Switching display formats in the program status
GET_ERR_ID: Get error ID locally (S7-1200, S7-1500)
Evaluating errors with output parameter RET_VAL
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)