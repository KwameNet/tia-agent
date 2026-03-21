# POKE_BLK: Write memory area

**Category:** Read/write access  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 167  

---

POKE_BLK: Write memory area (S7-1200, S7-1500)
POKE_BLK: Write memory area
Description
The "Write memory area" instruction is used to write a memory area to a different standard
memory area without specifying a data type.
Use of memory area 16#84: DB
If you use a data block as memory area and this is not yet known during the creation of the
program code you have the option of using the EN/ENO mechanism to recognize possible
access errors:
SCL
POKE_BLK(AREA_SRC := 16#84,
DBNUMBER_SRC := 1,
BYTEOFFSET_SRC := 2,
AREA_DEST := 16#84,
DBNUMBER_DEST := 11,
BYTEOFFSET_DEST := 22,
COUNT := 3,
ENO => ENO);
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
The following areas can be
selected in the source memo-
ry area:
• 16#81: Input
AREA_SRC Input BYTE I, Q, M, D
• 16#82: Output
• 16#83: Bit memory
• 16#84: DB
DINT, Number of the data block in
DBNUM-
Input DB_ANY D the source memory area, if
BER_SRC
1) AREA = DB, otherwise "0"
Address in the source memory
area to be written
BYTEOFF-
Input DINT I, Q, M, D
SET_SRC
Only the 16 least significant
bits are used.
The following areas can be
selected in the destination
AREA_DEST Input BYTE I, Q, M, D memory area:
• 16#81: Input
POKE_BLK: Write memory area (S7-1200, S7-1500)
• 16#82: Output
• 16#83: Bit memory
• 16#84: DB
DINT, Number of the data block in
DBNUM-
Input DB_ANY D the destination memory area,
BER_DEST
1) if AREA = DB, otherwise "0"
Address in the destination
memory area to be written
BYTEOFF-
Input DINT I, Q, M, D
SET_DEST
Only the 16 least significant
bits are used.
Number of bytes which are
COUNT Input DINT I, Q, M, D
copied
1) The data types of the parameters DBNUMBER_SRC and DBNUMBER_DEST must
have the same data type. In other words, both tags must be either data type DINT or data
type DB_ANY.
Note
If you write the memory address to the input, output or bit memory areas, you must as-
sign the DBNUMBER parameter the value "0", as the instruction is invalid otherwise.
Example
The following example shows how the instruction works:
SCL
POKE_BLK(AREA_SRC := "Tag_Source_Area",
DBNUMBER_SRC := "Tag_Source_DBNumber",
BYTEOFFSET_SRC := "Tag_Source_Byte");
AREA_DEST := "Tag_Destination_Area",
DBNUMBER_DEST := "Tag_Destination_DBNumber",
BYTEOFFSET_DEST := "Tag_Destination_Byte",
COUNT := "Tag_Count");
The following table shows how the instruction works using specific operand values:
Parameters Operand Value
AREA_SRC Tag_Source_Area 16#84
DBNUMBER_SRC Tag_Source_DBNumber 5
BYTEOFFSET_SRC Tag_Source_Byte 20
AREA_DEST Tag_Destination_Area 16#83
DBNUMBER_DEST Tag_Destination_DBNumber 0
BYTEOFFSET_DEST Tag_Destination_Byte 30
COUNT Tag_Count 100
POKE_BLK: Write memory area (S7-1200, S7-1500)
The instruction writes 100 byte from data block "5" starting with address "20" in the memo-
ry area of the bit memory starting at address "30".
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)