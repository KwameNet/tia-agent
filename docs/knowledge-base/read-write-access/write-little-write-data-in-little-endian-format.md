# WRITE_LITTLE: Write data in little endian format

**Category:** Read/write access  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 172  

---

WRITE_LITTLE: Write data in little endian format (S7-1200, S7-1500)
WRITE_LITTLE: Write data in little endian format
Description
You use the "Write data in little endian format" instruction to write the data of a single tag in
the little endian byte sequence to a memory area. With the little endian format, the byte
with the least significant bits is saved first, which means at the lowest memory address.
The parameters SRC_VARIABLE and DEST_ARRAY are of data type VARIANT. However,
there are a few restrictions regarding the data type with which the parameters can be inter-
connected. The VARIANT at the SRC_VARIABLE parameter must point to an elementary
data type. The VARIANT at the DEST_ARRAY parameter points to the memory area to
which the data is written, and this must be an ARRAY of BYTE.
You can also use an actual parameter with variable ARRAY index for the parameters
SRC_ARRAY and DEST_VARIABLE.
The operand at the POS parameter determines the position in the memory area at which
writing starts.
Note
Writing tag of data type VARIANT or BOOL
If you want to write a tag to which a VARIANT points, use the "Serialize" or "Deserialize"
instructions.
If you want to write a tag of the data type BOOL, use a "Slice access".
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
S7-1200 V4.0 S7-1500
and higher
Bit strings,
Bit strings,
integers,
integers,
floating-point
floating-
numbers,
SRC_VARI- point num- I, Q, M, D, Tag whose data are
Input LDT, TOD,
ABLE bers, TOD, L written
LTOD,
DATE,
DATE,
CHAR,
CHAR,
WCHAR
WCHAR
Memory area to
DEST_AR- ARRAY of ARRAY of I, Q, M, D,
InOut which the data are
RAY BYTE BYTE L
written
Determines the posi-
tion at which the writ-
I, Q, M, D,
POS InOut DINT DINT ing starts. The POS
L
parameter is calcula-
ted zero-based.
Function value I, Q, M, D,
INT INT Error information
(RET_VAL) L
WRITE_LITTLE: Write data in little endian format (S7-1200, S7-1500)
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
#TagResult := WRITE_LITTLE(SRC_VARIABLE := #DINTVariable,
DEST_ARRAY := #TargetField,
POS := #TagPos);
The following table shows how the instruction works using specific operand values:
Parameters Operand Value
1295788826
SRC_VARIABLE #DINTVariable
16#4D3C2B1A
ARRAY[0..10] of BYTE
DEST_ARRAY #TargetField
= 16#1A, 16#2B, 16#3C,
16#4D
POS #TagPos 0 => 4
The instruction writes the integer 1_295_788_826 from the #DINTVariable operand to the
#TargetField memory area in little endian format. The data type at the SRC_VARIABLE pa-
rameter specifies how many bytes are written. The quantity 4 is stored in the #TagPos op-
erand.
See also
Overview of the valid data types
Deserialize: Deserialize (S7-1200, S7-1500)
Serialize: Serialize (S7-1200, S7-1500)
Addressing areas of a tag with slice access (S7-1200, S7-1500)
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)