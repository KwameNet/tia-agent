# ABS: Form absolute value

**Category:** Math functions  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 79  

---

ABS: Form absolute value (S7-1200, S7-1500)
ABS: Form absolute value
Description
Use the "Form absolute value" instruction to calculate the absolute value of an input value
and to save the result in the specified operands.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
S7-1200 S7-1500
SINT, INT,
SINT, INT,
DINT,
DINT, float- I, Q, M, D, L,
<expression> Input LINT, float- Input value
ing-point P
ing-point
numbers
numbers
SINT, INT,
SINT, INT,
DINT, Absolute val-
DINT, float- I, Q, M, D, L,
Function value LINT, float- ue of the in-
ing-point P
ing-point put value
numbers
numbers
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
SCL
"Tag_Result1" := ABS("Tag_Value");
"Tag_Result2" := ABS("Tag_Value1"*"Tag_Value2");
The absolute value of the input value is returned in the format of the input value as a func-
tion value.
The following table shows how the instruction works using specific operand values:
Operand Value
Tag_Value -2
Tag_Result1 2
Tag_Value1 4
Tag_Value2 -1
Tag_Result2 4
See also
Overview of the valid data types
Memory areas (S7-1500)
ABS: Form absolute value (S7-1200, S7-1500)
Basics of SCL
Memory areas (S7-1200)