# TypeOfDB: Query data type of a DB

**Category:** Comparator operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 76  

---

TypeOfDB: Query data type of a DB (S7-1500)
TypeOfDB: Query data type of a DB
Description
The "Query data type of a DB" instruction is used to query which data type the data block
has that the tag of the DB_ANY data type addresses. You can compare the data type of
the DB addressed by the tag <Operand> either with the data type of another tag or directly
with a data type for "Equal" or "Not equal".
The tag must have the DB_ANY data type. The comparison operand, for example, can be
a PLC data type, a system data type, an axis or an FB.
You can only use the "Query data type of a DB" instruction within an IF or CASE instruc-
tion.
Parameter
The following table shows the parameters of the instruction:
Parameter Declaration Data type Memory area Description
L (The declara-
tion is possible
in the "Input",
Operand to
<Operand> Input DB_ANY "InOut" and
query
"Temp" sections
of the block in-
terface.)
You can find additional information on valid data types under "See also".
Example
The following example shows how the instruction works:
SCL
IF TypeOfDB(#InputDBAny) = TO_SpeedAxis THEN
"TagOut" := 1;
END_IF;
The "TagOut" output is set if the data type of the #InputDBAny operand addressed DB is
equal to the TO_SpeedAxis data type.
The "TagOut" output is not set when the following conditions are fulfilled:
• The number of the data block is "0".
• The data block does not exist.
• The data block is an ARRAY DB.
• The data block contains a tag of the data type UDT (PLC data type).
See also
Overview of the valid data types
Switching display formats in the program status
TypeOfDB: Query data type of a DB (S7-1500)
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)
Using the DB_ANY data type (S7-1200, S7-1500)