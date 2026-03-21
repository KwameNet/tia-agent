# WriteToArrayDBL: Write to array data block in load memory

**Category:** ARRAY DB  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 154  

---

WriteToArrayDBL: Write to array data block in load memory (S7-1500)
WriteToArrayDBL: Write to array data block in load
memory
Description
The instruction "Write to ARRAY data block in load memory" is used to write the element to
which the index references to a data block of the ARRAY DB block type in load memory.
An ARRAY data block is a data block that consists of exactly one ARRAY of <Data type>.
The elements of the ARRAY can be of PLC data type or any other elementary data type.
Counting of the ARRAY always begins with the low limit "0".
If the ARRAY data block has been designated with the block attribute "Only store in load
memory", it will only be stored in the load memory.
The instruction is executed when a positive signal edge is detected at the REQ parameter.
The BUSY parameter then has the signal state "1". If a negative signal edge is detected at
the BUSY parameter, the instruction is terminated and the value at the VALUE parameter is
written to the data block. The DONE parameter then has the signal state "1" for one pro-
gram cycle.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
REQ = "1": Start writing
REQ Input BOOL I, Q, M, D, L
to the array DB
ARRAY data block to
DB 1) Input DB_ANY I, Q, M, D, L
which data is written
Element in the DB to
which data is written. The
INDEX Input DINT I, Q, M, D, L, P specification can be a
constant, a global tag or
an indexed value.
D (element of a
global data
Pointer to the DB in the
block)
work memory that is to
L (The declara- be read and the value of
VALUE 1) Input VARIANT tion is possible which is to be written.
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
DB is still being written
DONE = "1": The instruc-
DONE Output BOOL I, Q, M, D, L tion was executed suc-
cessfully
Error information:
ERROR Output INT I, Q, M, D, L
If an error occurs during
execution of the instruc-
WriteToArrayDBL: Write to array data block in load memory (S7-1500)
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
8234 The data block is write protected.
8235 The data block is not an ARRAY DB.
8254 The data block has the incorrect data type.
8382 The value at the INDEX parameter is outside the limits of the ARRAY.
8450 The data type VARIANT at parameter VALUE provides the value "0".
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
"WriteToArrayDBL_DB"(REQ := "TagReg",
DB := "ArrayDB",
INDEX := 2,
VALUE := "SourceField",
BUSY => "TagBusy",
DONE => "TagDone",
ERROR => "TagError");
The following table shows how the instruction works using specific operand values:
WriteToArrayDBL: Write to array data block in load memory (S7-1500)
Parameters Operand Value
REQ TagReq BOOL
The "ArrayDB" operand is
DB ArrayDB an ARRAY DB of the data
type ARRAY [0 to 10] of INT.
2nd element of the "Ar-
INDEX 2
rayDB"
The "SourceField" operand
VALUE SourceField is a global tag of the data
type INT.
BUSY TagBusy BOOL
DONE TagDone BOOL
The instruction is executed when a positive signal edge is detected at the "TagReq" oper-
and. As soon as a negative signal edge is detected at the "TagBusy" operand, the instruc-
tion is terminated and the value at the VALUE parameter is written to the 2nd element of
"ArrayDB". After the instruction has been processed, the "TagDone" operand has the signal
state TRUE.
See also
Overview of the valid data types
READ_DBL: Read from data block in the load memory (S7-1200, S7-1500)
WRIT_DBL: Write to data block in the load memory (S7-1200, S7-1500)
Difference between synchronous and asynchronous instructions (S7-1200, S7-1500)
Memory areas (S7-1500)
Basics of SCL
Example of the use of ARRAY data blocks