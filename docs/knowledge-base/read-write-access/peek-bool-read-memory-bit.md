# PEEK_BOOL: Read memory bit

**Category:** Read/write access  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 161  

---

PEEK_BOOL: Read memory bit (S7-1200, S7-1500)
PEEK_BOOL: Read memory bit
Description
The "Read memory bit" instruction is used to read a memory bit from a standard memory
area without specifying a data type.
Use of memory area 16#84: DB
If you use a data block as memory area and this is not yet known during the creation of the
program code you have the option of using the EN/ENO mechanism to recognize possible
access errors:
SCL
#Peeker_BOOL := PEEK_BOOL(AREA := 16#84,
DBNUMBER := 1,
BYTEOFFSET := 2,
BITOFFSET := 1,
ENO => ENO);
IF NOT ENO THEN;
#Peeker_BOOL := 0;
END_IF;
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
The following areas can be selec-
ted:
• 16#81: Input
• 16#82: Output
AREA Input BYTE I, Q, M, D
• 16#83: Bit memory
• 16#84: DB
• 16#1: Peripheral input
(S7-1500 only)
DBNUM- DINT, Number of the data block if
Input D
BER DB_ANY AREA = DB, otherwise "0"
Address to read from
BYTEOFF-
Input DINT I, Q, M, D
SET Only the 16 least significant bits
are used.
BITOFF-
Input INT I, Q, M, D Bit to be read from
SET
Function value BOOL I, Q, M, D Result of the instruction
Note
PEEK_BOOL: Read memory bit (S7-1200, S7-1500)
If you read the memory bit from the input, output or bit memory areas, you must assign
the DBNUMBER parameter the value "0", as the instruction is invalid otherwise.
Example
The following example shows how the instruction works:
SCL
"Tag_Result" := PEEK_BOOL(AREA := "Tag_Area",
DBNUMBER := "Tag_DBNumber",
BYTEOFFSET := "Tag_Byte",
BITOFFSET := "Tag_Bit");
The following table shows how the instruction works using specific operand values:
Parameters Operand Value
AREA Tag_Area 16#84
DBNUMBER Tag_DBNumber 5
BYTEOFFSET Tag_Byte 20
BITOFFSET Tag_Bit 3
Function value Tag_Result 3
The instruction reads the value of memory bit "3" from the "Tag_Bit" operand at byte "20" of
data block "5" and returns the result at the "Tag_Result" operand as function value.
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)