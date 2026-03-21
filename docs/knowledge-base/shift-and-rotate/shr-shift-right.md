# SHR: Shift right

**Category:** Shift and Rotate  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 314  

---

SHR: Shift right (S7-1200, S7-1500)
SHR: Shift right
Description
The "Shift right" instruction shifts the contents of the IN parameter bit-by-bit to the right and
returns it as a function value. The parameter N is used to specify the number of bit posi-
tions by which the specified value should be shifted.
If the value of the N parameter is "0", the value of the IN parameter is given as a result.
If the value of the N parameter is greater than the number of available bit places, then the
value of the IN parameter is shifted to the right by the available number of bit places.
The freed bit positions in the left area of the operand are filled by zeroes when values with-
out signs are shifted. If the specified value has a sign, the free bit positions are filled with
the signal state of the sign bit.
The following figure shows how the content of an integer data type operand is shifted by
four bit positions to the right:
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
SHR: Shift right (S7-1200, S7-1500)
Bit strings, Bit strings, Result of the
Function value I, Q, M, D, L
integers integers instruction
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
SCL
"Tag_Result" := SHR(IN := "Tag_Value",
N := "Tag_Number");
The following table shows how the instruction works using specific operand values:
Parameters Operand Value
IN Tag_Value 0011 1111 1010 1111
N Tag_Number 3
Function value Tag_Result 0000 0111 1111 0101
The content of the "Tag_Value" operand is shifted by three bit positions to the right. The
result of the instruction is returned in the operand "Tag_Result" as a function value.
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)