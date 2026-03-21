# BITSUM: Count number of set bits

**Category:** Legacy  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 347  

---

BITSUM: Count number of set bits (S7-1500)
BITSUM: Count number of set bits
Description
The "Count number of set bits" instruction is used to count the number of bits of an oper-
and that are set to the signal state "1".
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
Operand whose
<Operand> Input DWORD I, Q, M, D, L, P set bits are coun-
ted
Result of the in-
Function value INT I, Q, M, D, L, P
struction
Example
The following example shows how the instruction works:
SCL
"Tag_Result" := BITSUM("Tag_Input");
The following table shows how the instruction functions using specific values:
Operand Value*
Tag_Input DW#16#12345678
Tag_Result W#16#000D (13 bits)
*The error codes can be displayed as integer or hexadecimal value in the program editor.
For information on toggling display formats, refer to "See also".
See also
Overview of the valid data types
Switching display formats in the program status
Memory areas (S7-1500)
Basics of SCL