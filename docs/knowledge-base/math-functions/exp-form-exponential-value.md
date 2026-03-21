# EXP: Form exponential value

**Category:** Math functions  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 90  

---

EXP: Form exponential value (S7-1200, S7-1500)
EXP: Form exponential value
Description
The "Form exponential value" instruction calculates the exponent from the base e (e =
2.718282) and the input value and saves the result in the specified operand.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
<Expres- Floating-point
Input I, Q, M, D, L, P Input value
sion> numbers
Floating-point Exponential value of the
Function value I, Q, M, D, L, P
numbers input value
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
SCL
"Tag_Result1" := EXP("Tag_Value");
"Tag_Result2" := EXP("Tag_Value1"/"Tag_Value2");
The result of the instruction is returned in the operand "Tag_Resultxy" as a function value.
The following table shows how the instruction works using specific operand values:
Operand Value
Tag_Value 20.5
Tag_Result1 799 902 200
Tag_Value1 15.5
Tag_Value2 30.2
Tag_Result2 1.671
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)