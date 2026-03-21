# DB_ANY_TO_VARIANT: Convert DB_ANY to VARIANT

**Category:** VARIANT  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 245  

---

DB_ANY_TO_VARIANT: Convert DB_ANY to VARIANT (S7-1200, S7-1500)
DB_ANY_TO_VARIANT: Convert DB_ANY to VAR-
IANT
Description
The "Convert DB_ANY to VARIANT" instruction is used to generate a VARIANT tag from a
data block that meets the requirements listed below. The operand at the IN parameter has
the data type DB_ANY, which means that the data block does not have to be known when
the program is created. The data block number is read during runtime.
Requirements
If the requirements are met, the instruction is executed. If the requirements are not met or
the data block does not exist, the value NULL is output at the RET_VAL parameter. All oth-
er accesses with the RET_VAL tag fail.
The input tag of data type ... homed... Conversion options
...a data block which is an
instance data block of a PLC
DB_ANY Conversion is possible
data type or a system data
type (SDT).
...a data block which is an
DB_ANY Conversion is possible
ARRAY DB.
...a data block which is an
instance data block of a
DB_ANY Conversion is not possible
function block or a global
data block.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
Data block whose num-
ber is read. You can use
IN Input DB_ANY I, Q, M, D, L
a local or global tag at
the IN parameter.
ERR Output INT I, Q, M, D, L Error information
L (The declara-
tion is possible
in the "Input",
Function value
VARIANT "InOut" and Number of the data block
(RET_VAL) 1)
"Temp" sections
of the block in-
terface.)
1) The RET_VAL parameter is declared as Output, since the data flow into the tag. How-
ever, the tag itself must be declared as InOut in the block interface.
For additional information on valid data types, refer to "See also".
ERR parameter
The following table shows the meaning of the values of the ERR parameter:
DB_ANY_TO_VARIANT: Convert DB_ANY to VARIANT (S7-1200, S7-1500)
Error code* Explanation
(W#16#...)
0000 No error
8130 The number of the data block is "0"
8131 The data block does not exist, is too short, or is located in load memory.
8132 The data block is too short and not an ARRAY data block.
8134 The data block is write protected.
8154 The data block has the incorrect data type.
8155 The data block has an unknown data type. 1)
*The error codes can be displayed as integer or hexadecimal value in the program editor.
For information on toggling display formats, refer to "See also".
1) The reason for the error code #8155 to be output is:
A PLC data type (UDT1) was declared and a data block (DB2) of the data type "UDT1" was
created. The tag table contains a tag (3) of the data type DB_ANY. Next the instruction
"DB_ANY_TO_VARIANT" was called in a program block (4) and supplied with the tag (3)
at the IN parameter. During execution the instruction "DB_ANY_TO_VARIANT" returns the
error code 16#8155.
Follow these steps to resolve the error code:
1. Create a function (FC5) and declare a tag of the data type VARIANT at the InOut in-
terface.
2. Create an additional function (FC6) and call FC5 in it.
3. Create a tag (7) of the data type "UDT1" in FC6 in the Temp interface.
4. Supply the InOut interface of the FC5 with tag (7).
5. Compile and download the two blocks (FC5 and FC6) to your CPU. You do not need
to call these blocks (FC5 and FC6) in the user program.
Result:
The error code 16#8155 is no longer output because the user program is now familiar with
the data type.
This procedure is not necessary if you call one of the two instructions "VariantGet" or "Var-
iantPut" after calling the instruction "DB_ANY_TO_VARIANT".
Example
The following example shows how the instruction works:
SCL
#tempVARIANT := DB_ANY_TO_VARIANT(IN := "InputDB",
ERR := "Tag_Error");
The following table shows how the instruction works using specific operand values:
Parameters Declaration in the Operand Value
block interface
DB_ANY_TO_VARIANT: Convert DB_ANY to VARIANT (S7-1200, S7-1500)
IN Input InputDB 11
<function value> Temp tempVARIANT -
The number of any data block that is specified at the "InputDB" operand is used to gener-
ate a tag of data type VARIANT that addresses the data block. Because the operand at the
IN parameter has the DB_ANY data type, the data block that will be used during runtime
does not have to be known when the program is created (neither the name nor the number
of the data block). Because the operand at the RET_VAL parameter has the data type
VARIANT, you do not need to know the data type of the data block when the program is
created.
See also
Overview of the valid data types
Memory areas (S7-1500)
(Not available)
Basics of SCL
Memory areas (S7-1200)