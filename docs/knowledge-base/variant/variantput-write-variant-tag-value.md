# VariantPut: Write VARIANT tag value

**Category:** VARIANT  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 181  

---

VariantPut: Write VARIANT tag value (S7-1200, S7-1500)
VariantPut: Write VARIANT tag value
Description
You can use the "Write VARIANT tag value" instruction to write the value of the tag at the
SRC parameter to the memory at the DST parameter to which the VARIANT points.
The DST parameter has the VARIANT data type. Any data type except for VARIANT can
be specified at the SRC parameter.
The data type of the tag at the SRC parameter must match the data type to which the VAR-
IANT points.
Note
To copy structures and ARRAYs, you can use the "MOVE_BLK_VARIANT instruction:
Move block" instruction. For additional information, refer to "See also".
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
S7-1200 S7-1500
Bit strings, inte-
gers, floating-
point numbers,
timers, date and I, Q, M, D,
SRC Input I, Q, M, D, L Tag to be read
time, character L, P
strings, ARRAY
elements, PLC
data types
L (The declaration is pos-
sible in the "Input", "InOut" Result of the in-
DST Input VARIANT
and "Temp" sections of struction
the block interface.)
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
SCL
VariantPut(SRC := "TagIn_Source",
DST := #TagIn_Dest);
The value of the "TagIn_Source" operand is written to the tag to which the VARIANT at the
#TagIn_Dest operand points.
See also
Overview of the valid data types
MOVE_BLK_VARIANT: Move block (S7-1200, S7-1500)
VariantPut: Write VARIANT tag value (S7-1200, S7-1500)
Memory areas (S7-1500)
(Not available)
Basics of SCL
Memory areas (S7-1200)