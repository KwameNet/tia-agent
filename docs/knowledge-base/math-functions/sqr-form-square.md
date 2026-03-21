# SQR: Form square

**Category:** Math functions  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 87  

---

SQR: Form square (S7-1200, S7-1500)
SQR: Form square
Description
Use the "Form square" instruction to square the input value and save the result in the
specified operand.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
<Expres- Floating-point
Input I, Q, M, D, L, P Input value
sion> numbers
Floating-point
Function value I, Q, M, D, L, P Square of the input value
numbers
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
SCL
"Tag_Result1" := SQR("Tag_Value");
"Tag_Result2" := SQR((SQR("Tag_Value1"))*"Tag_Value2");
The square of the input value is returned in the operand "Tag_Resultxy" as a function val-
ue.
The following table shows how the instruction works using specific operand values:
Operand Value
Tag_Value 2.5
Tag_Result1 6.25
Tag_Value1 6.0
Tag_Value2 2.0
Tag_Result2 5184.0
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)