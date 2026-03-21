# PEEK: Read memory address

**Category:** Read/write access  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 158  

---

PEEK: Read memory address (S7-1200, S7-1500)
PEEK: Read memory address
Description
The "Read memory address" instruction is used to read a memory address from a standard
memory area without specifying a data type.
Use the following syntax to change the data type of the instruction:
PEEK_<data type>();
Use of memory area 16#84: DB
If you use a data block as memory area and this is not yet known during the creation of the
program code you have the option of using the EN/ENO mechanism to recognize possible
access errors:
SCL
#Peeker := PEEK(AREA := 16#84,
DBNUMBER := 1,
BYTEOFFSET := 2,
ENO => ENO);
IF NOT ENO THEN;
#Peeker := 16#ffff;
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
• 16#1: Peripheral input (S7-1500
only)
DBNUM- DINT, Number of the data block if AREA
Input D
BER DB_ANY = DB, otherwise "0"
Address to read from
BYTE-
Input DINT I, Q, M, D
OFFSET Only the 16 least significant bits
are used.
Bit strings
_<data type> default: - Data type of the function value:
BYTE
PEEK: Read memory address (S7-1200, S7-1500)
1. You can specify the data type
of the instruction explicitly us-
ing "_".
2. If you do not specify the data
type explicitly, it will be deter-
mined by the utilized tags or
type-coded constants.
3. If you neither specify the data
type explicitly nor specify de-
fined tags or type-coded con-
stants, the default data type
will be used.
Function value Bit strings I, Q, M, D Result of the instruction
Note
If you read the memory address from the input, output or bit memory areas, you must
assign the DBNUMBER parameter the value "0", as the instruction is invalid otherwise.
Example
The following example shows how the instruction works:
SCL
"Tag_Result1" := PEEK(AREA := "Tag_Area",
DBNUMBER := "Tag_DBNumber",
BYTEOFFSET := "Tag_Byte");
SCL
"Tag_Result2" := PEEK_WORD(AREA := "Tag_Area",
DBNUMBER := "Tag_DBNumber",
BYTEOFFSET := "Tag_Byte");
The following table shows how the instruction works using specific operand values:
Parameters Operand Value
AREA Tag_Area 16#84
DBNUMBER Tag_DBNumber 5
BYTEOFFSET Tag_Byte 20
Function value Tag_Result1 Byte value "20"
Function value Tag_Result2 Word value "20"
The instruction reads the value of address "20" from the "Tag_Byte" operand at data block
"5" and returns the result as a function value at the "Tag_Result" operand.
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
PEEK: Read memory address (S7-1200, S7-1500)
Memory areas (S7-1200)