# VariantGet: Read out VARIANT tag value

**Category:** VARIANT  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 179  

---

VariantGet: Read out VARIANT tag value (S7-1200, S7-1500)
VariantGet: Read out VARIANT tag value
Description
You can use the "Read out VARIANT tag value" instruction to read the value of the tag to
which the VARIANT at the SRC parameter points and write it in the tag at the DST parame-
ter.
The SRC parameter has the VARIANT data type. Any data type except for VARIANT can
be specified at the DST parameter.
The data type of the tag specified at the DST parameter must match the data type to which
the VARIANT points.
Note
To copy structures and ARRAYs, you can use the "MOVE_BLK_VARIANT instruction:
Move block" instruction. For additional information, refer to "See also".
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
S7-1200 S7-1500
L (The declaration is pos-
sible in the "Input", "InOut"
SRC Input VARIANT Tag to be read
and "Temp" sections of
the block interface.)
Bit strings, inte-
gers, floating-
point numbers,
timers, date and I, Q, M, D, Result of the in-
DST Output I, Q, M, D, L
time, character L, P struction
strings, ARRAY
elements, PLC
data types
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
SCL
VariantGet(SRC := #TagIn_Source,
DST => "TagOut_Dest");
The value of the tag to which the VARIANT at the "#TagIn_Source" operand points, is read
and written to the "TagOut_Dest" operand.
See also
Overview of the valid data types
VariantGet: Read out VARIANT tag value (S7-1200, S7-1500)
MOVE_BLK_VARIANT: Move block (S7-1200, S7-1500)
Memory areas (S7-1500)
(Not available)
Basics of SCL
Memory areas (S7-1200)