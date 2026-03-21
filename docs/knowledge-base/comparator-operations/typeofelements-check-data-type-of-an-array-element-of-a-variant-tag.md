# TypeOfElements: Check data type of an ARRAY element of a VARIANT tag

**Category:** Comparator operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 72  

---

TypeOfElements: Check data type of an ARRAY element of a VARIANT tag (S7-1200, S7-1500)
TypeOfElements: Check data type of an ARRAY ele-
ment of a VARIANT tag
Description
You can use the "Check data type of an ARRAY element of a VARIANT tag" instruction to
query the data type of a tag to which a VARIANT tag is pointing. You compare the data
type of a tag to the data type of the tag that you have declared in the block interface to
determine whether they are "Equal" or "Not equal".
The operand must have the VARIANT data type. The comparison operand can be an ele-
mentary data type or a PLC data type.
If the data type of the VARIANT tag is an ARRAY, the data type of the ARRAY elements is
compared.
You can only use the "Check data type of an ARRAY element of a VARIANT tag" instruc-
tion within an IF or CASE instruction.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
L (The declara-
tion is possible
in the "Input",
<Operand> Input VARIANT "InOut" and Operand to query
"Temp" sections
of the block in-
terface.)
You can find additional information on valid data types under "See also".
Example
The following example shows how the instruction works:
SCL
IF TypeOfElements("Tag_Variant") = TypeOF("GlobalDB".Product[1]) THEN
"Tag_Variant" := "GlobalDB".Product[1] * 3;
END_IF;
The following table shows how the instruction works using specific operand values:
Operand Value
"GlobalDB".Product[1] 1.5
Tag_Variant 4.5
If the tag to which the VARIANT points and the "GlobalDB".Product[1] operand have the
REAL data type, the "GlobalDB".Product[1] operand is then multiplied by 3 and the result is
written to the "Tag_Variant" operand.
See also
TypeOfElements: Check data type of an ARRAY element of a VARIANT tag (S7-1200, S7-1500)
Overview of the valid data types
Switching display formats in the program status
Basic information on VARIANT (S7-1200, S7-1500)
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)
Example of programming a queue (FIFO) (S7-1200, S7-1500)