# VARIANT_TO_DB_ANY: Convert VARIANT to DB_ANY

**Category:** VARIANT  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 242  

---

VARIANT_TO_DB_ANY: Convert VARIANT to DB_ANY (S7-1200, S7-1500)
VARIANT_TO_DB_ANY: Convert VARIANT to
DB_ANY
Description
You use the "Convert VARIANT to DB_ANY" instruction to query the data block number
that the operand that is specified at the IN parameter addresses. This can be an instance
data block or an ARRAY data block. The operand at the IN parameter has the data type
VARIANT, which means you do not need to know the data type of the data block whose
number is to be queried when the program is created. The data block number is read dur-
ing runtime and written to the operand specified at the RET_VAL parameter.
Requirements
If the requirements are met, the instruction is executed. If the requirements are not met, "0"
is output as data block number.
The output tag... homed... Conversion options
... a data block which is an
The output tag can be con-
instance data block of a PLC
VARIANT verted to a data block num-
data type or a system data
ber.
type (SDT).
The output tag can be con-
... a data block which is an
VARIANT verted to a data block num-
ARRAY DB.
ber.
It is not possible to convert
the output tag to a database
... a tag with an elementary number because a data
VARIANT
data type. block can never comprise
only one elementary data
type.
It is not possible to convert
the output tags to a data-
... a structure within a data
VARIANT base number because it is
block.
only a part within the data
block.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
L (The declara-
Tag to be read. (The
tion is possible
function value of the
in the "Input",
"DB_ANY_TO_VAR-
IN Input VARIANT "InOut" and
IANT" instruction). You
"Temp" sections
can use a local or global
of the block in-
tag at the IN parameter.
terface.)
ERR Output INT I, Q, M, D, L Error information
Function value
DB_ANY I, Q, M, D, L Result: Number of the DB
(RET_VAL)
VARIANT_TO_DB_ANY: Convert VARIANT to DB_ANY (S7-1200, S7-1500)
For additional information on valid data types, refer to "See also".
ERR parameter
The following table shows the meaning of the values of the ERR parameter:
Error code* Explanation
(W#16#...)
0000 No error
The VARIANT data type at the IN parameter has the value "0" and the CPU
252C
changes to STOP mode.
The element data type stored in the ARRAY data block does not match the
80B4
element data type transferred in the VARIANT.
8130 The IN parameter has the data type ANY.
8131 The data block does not exist, is too short, or is located in load memory.
8132 The data block is too short and not an ARRAY data block.
The data type VARIANT at parameter IN provides the value "0". To receive
this error message, the "Handle errors within block" block property must be
8150
activated. Otherwise the CPU changes to STOP mode and sends the error
code 16#252C.
The VARIANT data type at the IN parameter does not point to the start of an
8153 ARRAY data block or the length of the VARIANT does not match that of the
data block.
8154 The data block has the incorrect data type.
*The error codes can be displayed as integer or hexadecimal value in the program editor.
For information on toggling display formats, refer to "See also".
Example
The following example shows how the instruction works:
SCL
"OutputDBNumber" := VARIANT_TO_DB_ANY(IN := #tempVARIANT,
ERR := "Tag_Error");
The following table shows how the instruction works using specific operand values:
Parameters Declaration in the block in- Operand Value
terface
IN Input tempVARIANT -
<function value> Output OutputDBNumber 11
The number of a data block that is specified at the tempVARIANT operand is read. Be-
cause the operand has the data type VARIANT, you do not need to know the data type of
the tag during program creation. The number is written to the "OutputDBNumber" tag which
has the data type DB_ANY.
See also
Overview of the valid data types
VARIANT_TO_DB_ANY: Convert VARIANT to DB_ANY (S7-1200, S7-1500)
Memory areas (S7-1500)
(Not available)
Basics of SCL
Memory areas (S7-1200)