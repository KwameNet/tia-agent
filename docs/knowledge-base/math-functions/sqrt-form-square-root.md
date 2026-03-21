# SQRT: Form square root

**Category:** Math functions  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 88  

---

SQRT: Form square root (S7-1200, S7-1500)
SQRT: Form square root
Description
Use the "Form square root" instruction to calculate the square root of the input value and
save the result in the specified operand. The instruction has a positive result if the input
value is greater than zero. If input values are less than zero, the instruction returns an inva-
lid floating-point number. If the input value is "0", the result is also "0".
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
<Expres- Floating-point
Input I, Q, M, D, L, P Input value
sion> numbers
Floating-point Square root of the input
Function value I, Q, M, D, L, P
numbers value
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
SCL
"Tag_Result1" := SQRT("Tag_Value");
"Tag_Result2" := SQRT((SQR("Tag_Value1"))+"Tag_Value2");
The square root of the input value is returned in the operand "Tag_Resultxy" as a function
value.
The following table shows how the instruction works using specific operand values:
Operand Value
Tag_Value 4.0
Tag_Result1 2.0
Tag_Value1 3.0
Tag_Value2 16.0
Tag_Result2 5.0
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)