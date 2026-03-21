# WriteToArrayDB: Write to array data block

**Category:** ARRAY DB  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 149  

---

WriteToArrayDB: Write to array data block (S7-1500)
WriteToArrayDB: Write to array data block
Description
The instruction "Write to ARRAY data block" is used to write the element to which the index
references to a data block of the ARRAY DB block type.
An ARRAY data block is a data block that consists of exactly one ARRAY of <Data type>.
The elements of the ARRAY can be of PLC data type or any other elementary data type.
Counting of the ARRAY always begins with the low limit "0".
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
Data block to which data
DB Input DB_ANY I, Q, M, D, L
is written
Element in the DB to
which data is written. The
INDEX Input DINT I, Q, M, D, L, P specification can be a
constant, a global tag or
an indexed value.
L (The declara-
tion is possible
in the "Input",
VALUE Input VARIANT "InOut" and Value to be written
"Temp" sections
of the block in-
terface.)
Function value
INT I, Q, M, D, L Result of the instruction
(RET_VAL)
For additional information on valid data types, refer to "See also".
RET_VAL parameter
The following table shows the meaning of the values of the RET_VAL parameter:
Error code* Explanation
(W#16#...)
0000 No error
The element data type stored in the ARRAY data block does not match the
80B4
element data type transferred in the VARIANT.
80B5 The copying was interrupted.
8132 The data block does not exist, is too short, or is located in load memory.
8134 The data block is write protected.
8135 The data block is not an ARRAY data block.
8154 The data block has the incorrect data type.
WriteToArrayDB: Write to array data block (S7-1500)
8282 The value at the INDEX parameter is outside the limits of the ARRAY.
8350 The data type VARIANT at parameter VALUE provides the value "0".
8352 Code generation error
There are two possible causes of the error:
• The size of the VALUE parameter does not match the element length in the
ARRAY data block.
8353
• The two tags are not in a memory area with optimized access. For addition-
al information on the memory area access types, refer to: Basics of block
access
*The error codes can be displayed as integer or hexadecimal value in the program editor.
For information on toggling display formats, refer to "See also".
Example
The following example shows how the instruction works:
SCL
"TagResult" := WriteToArrayDB(DB := "ArrayDB",
INDEX := 2,
VALUE := "SourceField");
The following table shows how the instruction works using specific operand values:
Parameters Operand Value
The "ArrayDB" operand is
DB ArrayDB an ARRAY DB of the data
type Array [0 to 10] of INT.
2nd element of the "Ar-
INDEX 2
rayDB"
The "SourceField" operand
VALUE SourceField is a global tag of the data
type INT.
The value of the "SourceField" operand is written to the 2nd element of the ARRAY DB.
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Example of the use of ARRAY data blocks