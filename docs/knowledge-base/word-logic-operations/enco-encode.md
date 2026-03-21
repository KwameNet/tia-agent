# ENCO: Encode

**Category:** Word logic operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 304  

---

ENCO: Encode (S7-1200, S7-1500)
ENCO: Encode
Description
The instruction "Encode" reads the bit number of the lowest-value bit set in the input value
and issues this as a result.
The "Encode" instruction selects the least significant bit of the value at the IN parameter
and writes this bit number to the operand at the OUT parameter. If the IN parameter has
the value DW#16#00000001 or DW#16#00000000, the value "0" is output at the OUT out-
put.
If, in an SCL block in the block properties, the "Set ENO automatically" option is selected
and the "Encode" instruction is used, the ENO delivers the signal state 0 if the parameter
IN delivers the value DW#16#00000001 or DW#16#00000000.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
IN Input Bit strings I, Q, M, D, L, P Input value
Bit number of the
bit in the input
Function value INT I, Q, M, D, L, P
value that is read
out.
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
SCL
"Tag_Result" := ENCO(IN := "Tag_Value");
The following figure shows how the instruction works using specific operand values:
The instruction reads the lowest-value set bit of the operand "Tag_Value" and writes the bit
position "3" in the operand "Tag_Result".
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)