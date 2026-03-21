# GET_ERR_ID: Get error ID locally

**Category:** PC systems  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 291  

---

GET_ERR_ID: Get error ID locally (S7-1200, S7-1500)
GET_ERR_ID: Get error ID locally
Description
The "Get error ID locally" instruction is used to query the occurrence of errors within a
block. This is usually to access error. If the system reports during the block processing er-
rors within the block execution since the last execution of the instruction, the instruction
outputs the error ID of the first error that occurred.
The error ID can only be saved in operands of the WORD data type. If several errors occur
in the block, the error ID of the next error to occur in the instruction is output only after the
first error that occurred has been remedied.
Note
The <Operand> is only changed if error information is present. To set the operand back
to "0" after handling the error, you have the following options:
• Declare the operand in the "Temp" section of the block interface.
• Reset the operand to "0" before calling the instruction.
The output of the "Get error ID locally" instruction is only set if error information is present.
If one of these conditions is not fulfilled, the remaining program execution is not affected by
the "Get error ID locally" instruction.
You can find an example of how you can implement the instruction in combination with oth-
er troubleshooting options under "See also".
Note
The "Get error ID locally" instruction enables local error handling within a block. If the
"Get error ID locally" instruction is inserted in the program code of a block, any prede-
fined system reactions are ignored if errors occur.
Options for error handling
You can find an overview of the options for error handling here: Overview of mechanisms
for error handling
For a detailed example of local error handling that contains several error handling options,
refer to: Example of how to handle program execution errors
Parameters
The following table shows the parameters of the instruction:
Parameters Data type Memory area Description
Function
WORD D, L Error ID
value
Error ID
The following table shows the values that can be output:
ID* ID* Description
GET_ERR_ID: Get error ID locally (S7-1200, S7-1500)
(hexadecimal) (decimal)
0 0 No error
2503 9475 Invalid pointer
2520 9504 Invalid STRING
2522 9506 Read error: Operand outside the valid range
2523 9507 Write error: Operand outside the valid range
2524 9508 Read error: Invalid operand
2525 9509 Write error: Invalid operand
2528 9512 Read error: Data alignment
2529 9513 Write error: Data alignment
252C 9516 Invalid pointer
2530 9520 Write error: Data block
2533 9523 Invalid reference used
2538 9528 Access error: DB does not exist
2539 9529 Access error: Wrong DB used
253A 9530 Global data block does not exist
253C 9532 Faulty information or the function does not exist
253D 9533 System function does not exist
253E 9534 Faulty information or the function block does not exist
253F 9535 System block does not exist
2550 9552 Access error: DB does not exist
2551 9553 Access error: Wrong DB used
2575 9589 Error in the program nesting depth
2576 9590 Error in the local data distribution
The block property "Parameter passing via registers" is not
2577 9591
selected.
25A0 9632 Internal error in TP
25A1 9633 Tag is write-protected
25A2 9634 Invalid numerical value of tag
2942 10562 Read error: Input
2943 10563 Write error: Output
*The error codes can be displayed as integer or hexadecimal value in the program editor.
For information on toggling display formats, refer to "See also".
Example
The following example shows how the instruction works:
SCL
#TagOut := #Field[#index] * REAL#40.5;
#TagID := GET_ERR_ID();
GET_ERR_ID: Get error ID locally (S7-1200, S7-1500)
IF #TagID = 16#2522 THEN
MOVE_BLK(IN := #TagArrayIn[0],
COUNT := 1,
OUT => #TagArrayOut[1]);
END_IF;
An error occurred accessing the #Field[#index] tag. The #TagOut operand returns the sig-
nal state "1" despite the read/access error and the multiplication is performed with the val-
ue "0.0". If this error scenario occurs, we recommend that you program the "Get error ID
locally" instruction after the multiplication to get the error. The error ID supplied by the "Get
error ID locally" instruction is evaluated with a comparison. If the #TagID operand returns
ID 16#2522, there is a read/access error and the value "100.0" of the #TagArrayIn[0] oper-
and is written to the #TagArrayOut[1] operand.
See also
Switching display formats in the program status
Overview of the valid data types
Basics of the EN/ENO mechanism
Use of the instructions GET_ERROR and GET_ERR_ID
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)