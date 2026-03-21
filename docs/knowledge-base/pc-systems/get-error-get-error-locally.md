# GET_ERROR: Get error locally

**Category:** PC systems  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 287  

---

GET_ERROR: Get error locally (S7-1200, S7-1500)
GET_ERROR: Get error locally
Description
The "Get error locally" instruction is used to query the occurrence of errors within a pro-
gram block. This normally involves programming or access errors. If the system reports an
error during the execution of the program block, detailed information is output at the <Oper-
and> on the first error to occur during the execution of the block since the last execution of
the instruction.
The error information can only be saved in operands of the "ErrorStruct" system data type.
The "ErrorStruct" system data type specifies the exact structure in which the information
about the error is stored. Using additional instructions, you can evaluate this structure and
program an appropriate response. If several errors occur in the program block, the error
information about the next error to occur is output by the instruction only after the first error
that occurred has been remedied.
Note
<Operand>
The <Operand> is only changed if error information is present. To set the operand back
to "0" after handling the error, you have the following options:
• Declare the operand in the "Temp" section of the block interface.
• Reset the operand to "0" before calling the instruction.
Note
Activation of the local error handling
As soon as you insert the instruction in the program code of a program block, the local
error handling is activated and default system reactions are ignored when errors occur.
Options for error handling
You can find an overview of the options for error handling here: Overview of mechanisms
for error handling
For a detailed example of local error handling that contains several error handling options,
refer to: Example of how to handle program execution errors
Parameters
The following table shows the parameters of the instruction:
Parameters Data type Memory area Description
<Operand> ErrorStruct D, L Information about errors that have occurred
Data type "ErrorStruct"
The "ErrorStruct" data type can be inserted in a global data block or in the block interface.
You can insert the data type more then once if you assign a different name for the data
structure each time. The data structure and the name of individual structure elements can-
not be changed. If you save the error information in a global data block, it can also be read
out by other program blocks.
The following table shows the structure of the "ErrorStruct" data type:
GET_ERROR: Get error locally (S7-1200, S7-1500)
Structure component Data type Description
ERROR_ID WORD Error ID
Shows whether the error occurred
during a program block call.
16#01: Error during a program block
FLAGS BYTE
call
16#00: No error during a program
block call
Default reaction:
0: Ignore (write error)
REACTION BYTE
1: Continue with substitute value "0"
(read error)
2: Skip instruction (system error)
Information about the address and
CODE_ADDRESS CREF
type of the program block
Program block type in which the error
occurred:
1: Organization block (OB)
BLOCK_TYPE BYTE
2: Function (FC)
3: Function block (FB)
CB_NUMBER UINT Number of the code block
OFFSET UDINT Reference to the internal memory
Information about the address of an
MODE BYTE
operand
Operand number of the machine
OPERAND_NUMBER UINT
command
POINTER_NUMBER_LO-
UINT (A) Internal pointer
CATION
SLOT_NUMBER_SCOPE UINT (B) Storage area in internal memory
Information about the address of an
DATA_ADDRESS NREF
operand
(C) Memory area:
L: 16#40...16#7F, 16#86, 16#87,
16#8E, 16#8F, 16#C0...16#FF
I: 16#81
Q: 16#82
AREA BYTE
M: 16#83
DB: 16#40, 16#84, 16#85, 16#8A,
16#8B
PI: 16#01
PQ: 16#02
GET_ERROR: Get error locally (S7-1200, S7-1500)
Technological objects: 16#04
DB_NUMBER UINT (D) Number of the data block
OFFSET UDINT (E) Relative address of the operand
Structure component "ERROR_ID"
The following table shows the values that can be output at the structure component "ER-
ROR_ID":
ID* ID* Description
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
GET_ERROR: Get error locally (S7-1200, S7-1500)
Example
The following example shows how the instruction works:
SCL
LABEL: #TagOut := #Field[#index] * REAL#40.5;
GET_ERROR(#Error);
IF #Error.REACTION = 1 THEN
GOTO LABEL;
;
END_IF;
An error occurred accessing the #Field[#index] tag. The #TagOut operand returns the sig-
nal state "1" despite the read/access error and the multiplication is performed with the val-
ue "0.0". If this error scenario occurs, we recommend that you program the "Get error local-
ly" instruction after the multiplication to get the error. The error information supplied by the
"Get error locally" instruction is evaluated with a comparison. If the #Error.REACTION
structure component has the value "1", there is a read/access error and the program exe-
cution begins again at the jump label LABEL.
See also
Switching display formats in the program status
Overview of the valid data types
Basics of the EN/ENO mechanism
Use of the instructions GET_ERROR and GET_ERR_ID
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)