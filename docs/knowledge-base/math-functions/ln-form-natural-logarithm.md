# LN: Form natural logarithm

**Category:** Math functions  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 89  

---

LN: Form natural logarithm (S7-1200, S7-1500)
LN: Form natural logarithm
Description
You can use the "Form natural logarithm" instruction to calculate the natural logarithm to
base e (e=2.718282) from the input value. The instruction has a positive result if the input
value is greater than zero. If input values are less than zero, the instruction returns an inva-
lid floating-point number.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
<Expres- Floating-point
Input I, Q, M, D, L, P Input value
sion> numbers
Floating-point Natural logarithm of the
Function value I, Q, M, D, L, P
numbers input value
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
SCL
"Tag_Result1" := LN("Tag_Value");
"Tag_Result2" := LN("Tag_Value1"+"Tag_Value2");
The result of the instruction is returned in the operand "Tag_Resultxy" as a function value.
The following table shows how the instruction works using specific operand values:
Operand Value
Tag_Value 2.5
Tag_Result1 0.916
Tag_Value1 1.5
Tag_Value2 3.2
Tag_Result2 1.548
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)