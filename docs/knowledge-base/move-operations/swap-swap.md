# SWAP: Swap

**Category:** Move operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 144  

---

SWAP: Swap (S7-1200, S7-1500)
SWAP: Swap
Description
You can use the "Swap" instruction to change the arrangement of the bytes of an input val-
ue and save the result in the specified operand.
The following figure shows how the bytes of an operand of the DWORD data type are
swapped using the "Swap" instruction:
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
S7-1200 S7-1500
WORD,
WORD, I, Q, M, D, L,
<Expression> Input DWORD, Input value
DWORD P
LWORD
WORD,
WORD, I, Q, M, D, L, Result of the
Function value DWORD,
DWORD P instruction
LWORD
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
SCL
"Tag_Result" := SWAP("Tag_Value");
The result of the instruction is returned as a function value.
The following table shows how the instruction works using specific operand values:
SWAP: Swap (S7-1200, S7-1500)
Operand Value
Tag_Value 0000 1111 0101 0101
Tag_Result 0101 0101 0000 1111
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)