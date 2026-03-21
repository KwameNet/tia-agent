# POKE: Write memory address

**Category:** Read/write access  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 163  

---

POKE: Write memory address (S7-1200, S7-1500)
POKE: Write memory address
Description
The "Write memory address" instruction is used to write a memory address to a standard
memory area without specifying a data type.
Use of memory area 16#84: DB
If you use a data block as memory area and this is not yet known during the creation of the
program code you have the option of using the EN/ENO mechanism to recognize possible
access errors:
SCL
POKE(AREA := 16#84,
DBNUMBER := 1,
BYTEOFFSET := 2,
VALUE := BYTE#7,
ENO => ENO);
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
• 16#2: Peripheral output
(S7-1500 only)
DBNUM- DINT, Number of the data block if AREA
Input D
BER DB_ANY = DB, otherwise "0"
Address to be written
BYTE-
Input DINT I, Q, M, D
OFFSET Only the 16 least significant bits
are used.
Bit sequen-
VALUE Input I, Q, M, D Value to be written
ces
Note
If you write the memory address to the input, output or bit memory areas, you must as-
sign the DBNUMBER parameter the value "0", as the instruction is invalid otherwise.
Example
POKE: Write memory address (S7-1200, S7-1500)
The following example shows how the instruction works:
SCL
POKE(AREA := "Tag_Area",
DBNUMBER := "Tag_DBNumber",
BYTEOFFSET := "Tag_Byte",
VALUE := "Tag_Value");
The following table shows how the instruction works using specific operand values:
Parameters Operand Value
AREA Tag_Area 16#84
DBNUMBER Tag_DBNumber 5
BYTEOFFSET Tag_Byte 20
VALUE Tag_Value 16#11
The instruction overwrites the memory address "20" in the data block "5" with value
"16#11".
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)