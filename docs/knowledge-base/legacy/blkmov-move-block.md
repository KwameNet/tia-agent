# BLKMOV: Move block

**Category:** Legacy  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 212  

---

BLKMOV: Move block (S7-1500)
BLKMOV: Move block
Description
You can use the "Move block" instruction to move the content of a memory area (source
area) to another memory area (destination area). The move operation takes place in the
direction of ascending addresses. You use VARIANT to define the source and destination
areas.
Note
The tags of the instruction can only be used within memory areas in which the "Opti-
mized block access" attribute is not activated. This applies to data blocks (DBs), organi-
zation blocks (OBs), functions (FCs), bit memory (M), inputs (I), and outputs (Q).
If a tag of the instruction has been declared with the retentivity setting "Set in IDB", how-
ever, you can also use this tag in memory areas "with optimized block access".
The following figure shows the principle of the move operation:
Consistency of source and destination data
Note that while the instruction "Move block" is being executed, the source data remains un-
changed; otherwise the consistency of the destination data cannot be guaranteed.
Interruptibility
There is no limit to the nesting depth.
Memory areas
You can use the "Move block" instruction to move the following memory areas:
• Areas of a data block
BLKMOV: Move block (S7-1500)
• Bit memory
• Process image input
• Process image output
General rules for moving
The source and destination areas must not overlap. If the source and destination areas
have different lengths, only the length of the smaller area will be moved.
If the source area is smaller than the destination area, the entire source area will be written
to the destination area. The remaining bytes of the destination area remain unchanged.
If the destination area is smaller than the source area, the entire destination area will be
written. The remaining bytes of the source area are ignored.
If a block of data type BOOL is moved, the tag must be addressed absolutely and the
specified length of the area must be divisible by 8, otherwise the instruction cannot be exe-
cuted.
Rules for moving character strings
You can use the "Move block" instruction to also move source and destination areas of the
STRING data type. If only the source area is STRING data type, the characters will be
moved that are actually contained in the character string. Information on the actual and
maximum length are not written in the destination area. If the source and destination area
are both STRING data type, the current length of the character string in the destination
area is set to the number of characters actually moved.
If you want to move the information on the maximum and actual length of a character
string, specify the areas in bytes in the SRCBLK and DSTBLK parameters. Alternatively,
you can use the "Serialize" / "Deserialize" instructions.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
Specifies the memory
SRCBLK Input VARIANT I, Q, M, D, L, P area to be moved (source
area).
Specifies the memory
area to which the block is
DSTBLK Output 1) VARIANT I, Q, M, D, L, P
to be moved (destination
area).
Function value
INT I, Q, M, D, L, P Error information
(RET_VAL)
1) The DSTBLK parameter is declared as Output, since the data flow into the tag. Howev-
er, the tag itself must be declared as InOut in the block interface.
For additional information on valid data types, refer to "See also".
RET_VAL parameter
The following table shows the meaning of the values of the RET_VAL parameter:
Error code* Explanation
BLKMOV: Move block (S7-1500)
(W#16#...)
0000 No error
8092 The source or target area is only available in the load memory.
The WSTRING, WCHAR, BOOL, ARRAY of STRING, ARRAY of
8152 WSTRING and ARRAY of WCHAR data types are not supported at the
SRCBLK parameter.
The WSTRING, WCHAR, BOOL, ARRAY of STRING, ARRAY of
8352 WSTRING and ARRAY of WCHAR data types are not supported at the
DSTBLK parameter.
General er-
ror informa- See also: "GET_ERR_ID: Get error ID locally"
tion
*The error codes can be displayed as integer or hexadecimal value in the program editor.
For information on toggling display formats, refer to "See also".
Example
The following example shows how the instruction works:
SCL
"Tag_RetVal" := BLKMOV(SRCBLK := P#M100.0 BYTE 10,
DSTBLK => P#DB1.DBX0.0 BYTE 10);
The instruction copies 10 bytes starting from MB100 and writes them to DB1. If an error
occurs during the move operation, its error code is output in the "Tag_RetVal" tag.
See also
Overview of the valid data types
Switching display formats in the program status
GET_ERR_ID: Get error ID locally (S7-1200, S7-1500)
Memory areas (S7-1500)
Basics of SCL