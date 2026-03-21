# BCDCPL: Create tens complement

**Category:** Legacy  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 346  

---

BCDCPL: Create tens complement (S7-1500)
BCDCPL: Create tens complement
Description
The "Create tens complement" instruction is used to create the tens complement of a sev-
en-digit BCD number specified by the operand. This instruction uses the following mathe-
matical formula to calculate:
10000000 (as BCD)
– 7-digit BCD value
----------------------------------------
Tens complement (as BCD)
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
7-digit BCD num-
<Operand> Input Bit strings I, Q, M, D, L, P
ber
Result of the in-
Function value DWORD I, Q, M, D, L, P
struction
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
SCL
"Tag_Result" := BCDCPL("Tag_Input");
The following table shows how the instruction functions using specific values:
Operand Value*
Tag_Input DW#16#01234567
Tag_Result DW#16#08765433
*The error codes can be displayed as integer or hexadecimal value in the program editor.
For information on toggling display formats, refer to "See also".
See also
Overview of the valid data types
Switching display formats in the program status
Memory areas (S7-1500)
Basics of SCL