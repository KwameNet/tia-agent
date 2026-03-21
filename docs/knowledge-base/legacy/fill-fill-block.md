# FILL: Fill block

**Category:** Legacy  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 218  

---

FILL: Fill block (S7-1500)
FILL: Fill block
Description
You can use the "Fill block" instruction to fill a memory area (destination area) with the con-
tent of another memory area (source area). The "Fill block" instruction moves the content
of the source area to the destination area until the destination area is completely written.
The move operation takes place in the direction of ascending addresses.
You use VARIANT to define the source and destination areas.
Note
You can also define the source and destination areas using the ANY data type
If you use the ANY data type, you must observe the following in connection with the
STRING data type:
• In the case of an assignment of STRING (source area) using ANY after STRING (des-
tination area), the content of the STRING is copied to the destination area repeatedly
until it is full.
Source area: 'STEP7-SCL-TIA-Portal'
Destination area: 'STEP7-SCL-TIA-PortalSTEP7-SCL-TIA-PortalSTEP7-SCL'
• In the case of an assignment of WSTRING (source area) using ANY after WSTRING
(destination area), the entire WSTRING including type information is copied only once
to the destination area.
Source area: WSTRING#'STEP7-SCL-TIA-Portal'
Destination area: WSTRING#'STEP7-SCL-TIA-Portal'
Note
The tags of the instruction can only be used within memory areas in which the "Opti-
mized block access" attribute is not activated. This applies to data blocks (DBs), organi-
zation blocks (OBs), functions (FCs), bit memory (M), inputs (I), and outputs (Q).
If a tag of the instruction has been declared with the retentivity setting "Set in IDB", how-
ever, you can also use this tag in memory areas "with optimized block access".
For blocks with the "Optimized block access" attribute, you can use the "FILL_BLK in-
struction: Fill block" instruction.
The following figure shows the principle of the move operation:
FILL: Fill block (S7-1500)
Example: The contents of the range MW100 to MW118 are to be preassigned with the con-
tents of the memory words MW14 to MW20.
Consistency of source and destination data
Note that while the instruction "Fill block" is being executed, the source data remains un-
changed; otherwise the consistency of the destination data cannot be guaranteed.
Memory areas
You can use the "Fill block" instruction to move the following memory areas:
• Areas of a data block
• Bit memory
• Process image input
• Process image output
General rules for moving
The source and destination areas must not overlap. If the destination area to be preset is
not an integer multiple of the length of the BVAL input parameter, the destination area is
nevertheless written up to the last byte.
If the destination area to be preset is smaller than the source area, the function only copies
as much data as can be written to the destination area.
If the destination or source area actually present is smaller than the assigned memory area
for the source or destination area (BVAL, BLK parameters), no data is transferred.
If the ANY pointer (source or destination) is of the data type BOOL, it must be addressed
absolutely and the length specified must be divisible by 8; otherwise the instruction is not
executed.
If the destination area is STRING data type, the instruction writes the entire string including
the administration information.
FILL: Fill block (S7-1500)
Rules for moving structures
When you transfer a structure as an input parameter you must bear in mind that the length
of a structure is always oriented to an even number of bytes. The structure will need one
byte of additional memory space if you declare a structure with an odd number of bytes.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
Specification of the mem-
ory area (source area),
the content of which is
BVAL Input VARIANT I, Q, M, D, L, P
used to fill the destination
area at the BLK parame-
ter.
Specification of the mem-
ory area that will be filled
BLK Output 1) VARIANT I, Q, M, D, L, P
with the content of the
source area.
Function value
INT I, Q, M, D, L, P Error information
(RET_VAL)
1) The BLK parameter is declared as Output, since the data flow into the tag. However,
the tag itself must be declared as InOut in the block interface.
For additional information on valid data types, refer to "See also".
BVAL parameter
When you transfer a structure as an input parameter, remember that the length of a struc-
ture is always based on an even number of bytes. The structure will need one byte of addi-
tional memory space if you declare a structure with an odd number of bytes.
RET_VAL parameter
The following table shows the meaning of the values of the RET_VAL parameter:
Error code Explanation
(W#16#...)
0000 No error
8092 The source or target area is only available in the load memory.
The WSTRING, WCHAR, BOOL, ARRAY of STRING, ARRAY of WSTRING
8152
and ARRAY of WCHAR data types are not supported at the BVAL parameter.
The WSTRING, WCHAR, BOOL, ARRAY of STRING, ARRAY of WSTRING
8352
and ARRAY of WCHAR data types are not supported at the BLK parameter.
General
error in- See also: "GET_ERR_ID: Get error ID locally"
formation
*The error codes can be displayed as integer or hexadecimal value in the program editor.
For information on toggling display formats, refer to "See also".
Example
FILL: Fill block (S7-1500)
The following example shows how the instruction works:
SCL
"Tag_RetVal" := FILL(BVAL := P#M14.0 WORD 4,
BLK => P#M100.0 WORD 10);
The instruction copies the source area from MW14 to MW20 and fills the destination area
from MW100 to MW118 with the content of the 4 words contained in the memory area in
the BVAL parameter.
See also
Overview of the valid data types
Switching display formats in the program status
GET_ERR_ID: Get error ID locally (S7-1200, S7-1500)
Memory areas (S7-1500)
Basics of SCL