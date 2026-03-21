# ReadFromArrayDBL: Read from array data block in load memory

**Category:** ARRAY DB  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 151  

---

ReadFromArrayDBL: Read from array data block in load memory (S7-1500)
ReadFromArrayDBL: Read from array data block in
load memory
Description
The instruction "Read from ARRAY data block in load memory" is used to read the element
ti which the index references from a data block of the ARRAY DB block type in the load
memory and write it to the target range.
An ARRAY data block is a data block that consists of exactly one ARRAY of <Data type>.
The elements of the ARRAY can be of PLC data type or any other elementary data type.
Counting of the ARRAY always begins with the low limit "0".
If the ARRAY data block has been designated with the block attribute "Only store in load
memory", it will only be stored in the load memory.
The instruction is executed when a positive signal edge is detected at the REQ parameter.
The BUSY parameter then has the signal state "1". The instruction is terminated if a nega-
tive signal edge is detected at the BUSY parameter. The DONE parameter has the signal
state "1" for one program cycle and the read value is output at the VALUE parameter within
this cycle. With all other program cycles, the value at the VALUE parameter is not changed.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
REQ = "1": Begin with the
REQ Input BOOL I, Q, M, D, L
reading of the ARRAY DB
ARRAY data block that is
DB 1) Input DB_ANY I, Q, M, D, L
read.
Element in the DB that is
read. The specification
INDEX Input DINT I, Q, M, D, L, P can be a constant, a
global tag or an indexed
value.
D (element of a
global data
Pointer to the DB in the
block)
work memory that is to
L (The declara- be read and the value of
VALUE 1) InOut VARIANT tion is possible which is to be written.
in the "Input",
No local constants or
"InOut" and
tags from the TEMP sec-
"Temp" sections
tion must be used.
of the block in-
terface.)
BUSY = "1": The array
BUSY Output BOOL I, Q, M, D, L
DB is still being read
DONE = "1": The instruc-
DONE Output BOOL I, Q, M, D, L tion was executed suc-
cessfully
ERROR Output INT I, Q, M, D, L Error information:
ReadFromArrayDBL: Read from array data block in load memory (S7-1500)
If an error occurs during
execution of the instruc-
tion, an error code is out-
put at the ERROR pa-
rameter.
1) The data blocks must be created with the "Optimized" block property.
ERROR parameter
The following table shows the meaning of the values of the ERROR parameter:
Error code* Explanation
(W#16#...)
0000 No error
The element data type stored in the ARRAY data block does not match the
80B4
element data type transferred in the VARIANT.
8230 The data block number is incorrect.
8231 The data block does not exist.
8232 The data block is too short, or is not located in load memory.
8235 The data block is not an ARRAY DB.
8254 The data block has the incorrect data type.
8382 The value at the INDEX parameter is outside the limits of the ARRAY.
8750 The data type VARIANT at parameter VALUE provides the value "0".
8751 Code generation error
8752 Code generation error
The size of the VALUE parameter does not match the element length in the
8753
ARRAY data block.
*The error codes can be displayed as integer or hexadecimal value in the program editor.
For information on toggling display formats, refer to "See also".
You can find descriptions of the error codes triggered by the instructions "READ_DBL" and
"WRIT_DBL" in the corresponding descriptions of the instructions.
Example
The following example shows how the instruction works:
SCL
"ReadFromArrayDBL_DB"(REQ := "TagReg",
DB := "ArrayDB",
INDEX := 2,
VALUE := "TargetField",
BUSY => "TagBusy",
DONE => "TagDone",
ERROR => "TagError");
The following table shows how the instruction works using specific operand values:
ReadFromArrayDBL: Read from array data block in load memory (S7-1500)
Parameters Operand Value
REQ TagReq BOOL
The "ArrayDB" operand is
DB ArrayDB an ARRAY DB of the data
type ARRAY [0 to 10] of INT.
2nd element of the "Ar-
INDEX 2
rayDB"
The "TargetField" operand is
VALUE TargetField a global tag of the data type
INT.
BUSY TagBusy BOOL
DONE TagDone BOOL
The instruction is executed when a positive signal edge is detected at the "TagReq" oper-
and. The 2nd element is read from "ArrayDB" and output at the "VALUE" parameter. As
soon as a negative signal edge is detected at the "TagBusy" operand, the instruction is ter-
minated and the value at the VALUE parameter is no longer changed. After the instruction
has been processed, the "TagDone" operand has the signal state TRUE.
See also
Overview of the valid data types
READ_DBL: Read from data block in the load memory (S7-1200, S7-1500)
WRIT_DBL: Write to data block in the load memory (S7-1200, S7-1500)
Difference between synchronous and asynchronous instructions (S7-1200, S7-1500)
Memory areas (S7-1500)
Basics of SCL
Example of the use of ARRAY data blocks