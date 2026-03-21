# READ_BIG: Read data in big endian format

**Category:** Read/write access  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 174  

---

READ_BIG: Read data in big endian format (S7-1200, S7-1500)
READ_BIG: Read data in big endian format
Description
The instruction "Read data in little endian format" is used to read data from a memory area
and to write this to a single tag in the big endian byte sequence. With the big endian for-
mat, the byte with the most significant bits is saved first, which means at the lowest memo-
ry address.
The parameters SRC_ARRAY and DEST_VARIABLE are of data type VARIANT. However,
there are a few restrictions regarding the data type with which the parameters can be inter-
connected. The VARIANT at parameter DEST_VARIABLE has to be an elementary data
type. The VARIANT at the SRC_ARRAY parameter points to the memory area to be read
from, and this must be an ARRAY of BYTE.
You can also use an actual parameter with variable ARRAY index for the parameters
SRC_ARRAY and DEST_VARIABLE.
The operand at the POS parameter determines the position in the memory area at which
reading starts.
Note
Reading tag of data type VARIANT or BOOL
If you want to read a tag to which a VARIANT points, use the "Serialize" or "Deserialize"
instructions.
If you want to read a tag of the data type BOOL, use a "Slice access".
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
S7-1200 V4.0 S7-1500
and higher
SRC_AR- ARRAY of ARRAY of I, Q, M, D, Memory area to be
Input
RAY BYTE BYTE L read from
Bit strings,
Bit strings, integers,
integers, floating-
floating- point num-
DEST_VAR point num- bers, LDT, I, Q, M, D,
Output Read value
IABLE bers, TOD, TOD, L
DATE, LTOD,
CHAR, DATE,
WCHAR CHAR,
WCHAR
Determines the posi-
tion at which the read-
I, Q, M, D,
POS InOut DINT DINT ing starts. The POS
L
parameter is calcula-
ted zero-based.
READ_BIG: Read data in big endian format (S7-1200, S7-1500)
Function value I, Q, M, D,
INT INT Error information
(RET_VAL) L
RET_VAL parameter
The following table shows the meaning of the values of the RET_VAL parameter:
Error code* Explanation
(W#16#...)
0000 No error
80B4 The data type at the SRC_ARRAY parameter is not ARRAY of BYTE.
8382 The value at the POS parameter is outside the limits of the ARRAY.
The value at the POS parameter is within the limits of the ARRAY but the size
8383
of the memory area exceeds the high limit of the ARRAY.
Example
The following example shows how the instruction works:
SCL
#TagResult := READ_BIG(SRC_ARRAY := #SourceField,
DEST_VARIABLE => #DINTVariable,
POS := #TagPos);
The following table shows how the instruction works using specific operand values:
Parameters Operand Value
ARRAY[0..10] of BYTE
SRC_ARRAY #SourceField
:= 16#1A, 16#2B, 16#3C,
16#4D
439041101
DEST_VARIABLE #DINTVariable
16#1A2B3C4D
POS #TagPos 0 => 4
The instruction reads the integer 439_041_101 from the #SourceField memory area and
writes it to the #DINTVariable operand in big endian format. The data type at the
DEST_VARIABLE parameter specifies how many bytes are read. The quantity 4 is stored
in the #TagPos operand.
See also
Overview of the valid data types
Deserialize: Deserialize (S7-1200, S7-1500)
Serialize: Serialize (S7-1200, S7-1500)
Addressing areas of a tag with slice access (S7-1200, S7-1500)
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)