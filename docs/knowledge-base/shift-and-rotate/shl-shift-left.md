# SHL: Shift left

**Category:** Shift and Rotate  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 316  

---

SHL: Shift left (S7-1200, S7-1500)
SHL: Shift left
Description
The "Shift left" instruction shifts the contents of the IN parameter bit-by-bit to the left and
returns it as a function value. The parameter N is used to specify the number of bit posi-
tions by which the specified value should be shifted.
If the value of the N parameter is "0", the value of the IN parameter is given as a result.
If the value of the N parameter is greater than the number of bit places, the value of the IN
parameter is shifted to the left by the available number of bit places.
The bit positions freed by the shift are filled with zeros in the result value.
The following figure shows how the content of an operand of the WORD data type is shif-
ted six bit positions to the left:
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
S7-1200 S7-1500
Bit strings, Bit strings, Value to be
IN Input I, Q, M, D, L
integers integers shifted
USINT, Number of bits
USINT,
UINT, by which the
N Input UINT, I, Q, M, D, L
UDINT, value (IN) is
UDINT
ULINT shifted
Bit strings, Bit strings, Result of the
Function value I, Q, M, D, L
integers integers instruction
SHL: Shift left (S7-1200, S7-1500)
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
SCL
"Tag_Result" := SHL(IN := "Tag_Value",
N := "Tag_Number");
The following table shows how the instruction works using specific operand values:
Parameters Operand Value
IN Tag_Value 0011 1111 1010 1111
N Tag_Number 4
Function value Tag_Result 1111 1010 1111 0000
The value of the "Tag_Value" operand is shifted by four bit places to the left. The result of
the instruction is returned in the operand "Tag_Result" as a function value.
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)