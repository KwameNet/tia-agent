# UBLKMOV: Move block uninterruptible

**Category:** Legacy  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 215  

---

UBLKMOV: Move block uninterruptible (S7-1500)
UBLKMOV: Move block uninterruptible
Description
You can use the "Move block uninterruptible" instruction to move the content of a memory
area (source area) to another memory area (destination area). The move operation takes
place in the direction of ascending addresses. You use VARIANT to define the source and
destination areas.
The move operation cannot be interrupted by other operating system activities. As a result,
the interrupt reaction time of the CPU can increase during the execution of the "Move block
uninterruptible" instruction.
Note
The tags of the instruction can only be used within memory areas in which the "Opti-
mized block access" attribute is not activated. This applies to data blocks (DBs), organi-
zation blocks (OBs), functions (FCs), bit memory (M), inputs (I), and outputs (Q).
If a tag of the instruction has been declared with the retentivity setting "Set in IDB", how-
ever, you can also use this tag in memory areas "with optimized block access".
Memory areas
You can use the "Move block uninterruptible" instruction to move the following memory
areas:
• Areas of a data block
• Bit memory
• Process image input
• Process image output
General rules for moving
The source and destination area must not overlap during the execution of the "Move block
uninterruptible" instruction. If the source area is smaller than the destination area, the en-
tire source area will be written to the destination area. The remaining bytes of the destina-
tion area remain unchanged.
If the destination area is smaller than the source area, the entire destination area will be
written. The remaining bytes of the source area are ignored.
If a source or destination area defined as a formal parameter is smaller than a destination
or source area specified in the SRCBLK or DSTBLK parameter, no data is transferred.
If a block of data type BOOL is moved, the tag must be addressed absolutely and the
specified length of the area must be divisible by 8, otherwise the instruction cannot be exe-
cuted.
You can use the "Move block uninterruptible" instruction to move a maximum of 16 KB.
Note the CPU-specific restrictions for this.
Rules for moving character strings
You can use the "Move block uninterruptible" instruction to also move source and destina-
tion areas of the STRING data type. If only the source area is STRING data type, the char-
acters will be moved that are actually contained in the character string. Information on the
UBLKMOV: Move block uninterruptible (S7-1500)
actual and maximum length are not written in the destination area. If the source and desti-
nation area are both STRING data type, the current length of the character string in the
destination area is set to the number of characters actually moved. If areas of the STRING
data type are moved, specify "1" as the area length.
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
Error code Explanation
(W#16#...)
0000 No error
8091 The source or target area is only available in the load memory.
The WSTRING, WCHAR, BOOL, ARRAY of STRING, ARRAY of WSTRING
8152 and ARRAY of WCHAR data types are not supported at the SRCBLK pa-
rameter.
The WSTRING, WCHAR, BOOL, ARRAY of STRING, ARRAY of WSTRING
8352 and ARRAY of WCHAR data types are not supported at the DSTBLK pa-
rameter.
General er-
ror informa- See also: "GET_ERR_ID: Get error ID locally"
tion
*The error codes can be displayed as integer or hexadecimal value in the program editor.
For information on toggling display formats, refer to "See also".
Example
The following example shows how the instruction works:
SCL
"Tag_RetVal" := UBLKMOV(SRCBLK := P#M100.0 BYTE 10,
DSTBLK => P#DB1.DBX0.0 BYTE 10);
UBLKMOV: Move block uninterruptible (S7-1500)
The instruction copies 10 bytes starting from MB100 and writes them to DB1. If an error
occurs during the move operation, its error code is output in the "Tag_RetVal" tag.
See also
Overview of the valid data types
Switching display formats in the program status
GET_ERR_ID: Get error ID locally (S7-1200, S7-1500)
Memory areas (S7-1500)
Basics of SCL