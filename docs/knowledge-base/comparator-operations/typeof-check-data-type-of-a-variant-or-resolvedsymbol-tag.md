# TypeOf: Check data type of a VARIANT or ResolvedSymbol tag

**Category:** Comparator operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 70  

---

TypeOf: Check data type of a VARIANT or ResolvedSymbol tag (S7-1200, S7-1500)
TypeOf: Check data type of a VARIANT or Resolved-
Symbol tag
Description
You can use the "Check data type of a VARIANT or ResolvedSymbol tag" instruction to
check the data type of a tag to which a VARIANT or a ResolvedSymbol is pointing. You can
compare the data type of the that you have declared in the block interface either to the da-
ta type of another tag or directly with a data type to determine whether they are "Equal" or
"Not equal".
The comparison operand can be an elementary data type or a PLC data type.
You can only use the "Check data type of a VARIANT or ResolvedSymbol tag" instruction
within an IF or CASE instruction.
Note
Symbolic access during runtime
You need the system data type "ResolvedSymbol"" to access tags in the PLC program
by an external application during runtime. You can find additional information under:
Symbolic access during runtime
Parameters
The following table shows the parameters of the instruction:
Parameter Declaration Data type Memory area Description
Binary numbers,
L (The declara-
integers, float-
tion is possible
ing-point num-
in the "Input",
bers, times, date
<Operand> Input "InOut" and Operand to query
and time, char-
"Temp" sections
acter strings,
of the block in-
VARIANT, Re-
terface.)
solvedSymbol
You can find additional information on valid data types under "See also".
Example
The following example shows the comparison of a VARIANT with another tag:
SCL
IFTypeOf(#TagVARIANT) = TypeOf("TagBYTE") THEN
...;
END_IF;
The following example shows the comparison of a VARIANT with a data type:
SCL
IFTypeOf(#TagVARIANT) = BYTETHEN
TypeOf: Check data type of a VARIANT or ResolvedSymbol tag (S7-1200, S7-1500)
...;
END_IF;
The following example shows the comparison of a "resolvedSymbol"
SCL
CASE TypeOf(ResolvedSymbol[100]) OF
INT:
...;
END_CASE;
See also
Overview of the valid data types
Switching display formats in the program status
Basic information on VARIANT (S7-1200, S7-1500)
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)
Example of programming a queue (FIFO) (S7-1200, S7-1500)