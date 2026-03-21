# ROL: Rotate left

**Category:** Shift and Rotate  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 320  

---

ROL: Rotate left (S7-1200, S7-1500)
ROL: Rotate left
Description
The "Rotate left" instruction rotates the contents of the IN parameter bit-by-bit to the left
and returns it as a function value. The parameter N is used to specify the number of bit
places by which the specified value should be rotated. The bit positions freed by rotating
are filled with the bit positions that are pushed out.
If the value of the N parameter is "0", the value at input IN is given as a result.
If the value at the N parameter is greater than the number of available bit positions, the
operand value at the IN input is still rotated by the specified number of bit positions.
The following figure shows how the content of an operand of the DWORD data type is rota-
ted three bit positions to the left:
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
S7-1200 S7-1500
Bit strings, Bit strings, Value to be ro-
IN Input I, Q, M, D, L
integers integers tated
Number of bit
USINT,
USINT, positions by
UINT,
N Input UINT, I, Q, M, D, L which the val-
UDINT,
UDINT ue (IN) is rota-
ULINT
ted
Bit strings, Bit strings, Result of the
Function value I, Q, M, D, L
integers integers instruction
ROL: Rotate left (S7-1200, S7-1500)
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
SCL
"Tag_Result" := ROL(IN := "Tag_Value",
N := "Tag_Number");
The following table shows how the instruction works using specific operand values:
Parameters Operand Value
IN Tag_Value 1010 1000 1111 0110
N Tag_Number 5
Function value Tag_Result 0001 1110 1101 0101
The content of the operand "Tag_Value" is rotated five bit places to the left. The result of
the instruction is returned in the operand "Tag_Result" as a function value.
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)