# SIN: Form sine value

**Category:** Math functions  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 91  

---

SIN: Form sine value (S7-1200, S7-1500)
SIN: Form sine value
Description
Use the "Form sine value" instruction to calculate the sine of the input value. The input val-
ue must be given in radians.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
<Expres- Floating-point Input value (size of an
Input I, Q, M, D, L, P
sion> numbers angle in radians)
Floating-point
Function value I, Q, M, D, L, P Result of the instruction
numbers
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
SCL
"Tag_Result" := SIN("Tag_Value");
The result of the instruction is returned in the operand "Tag_Result" as a function value.
The following table shows how the instruction works using specific operand values:
Operand Value
Tag_Value +1.570796 (π/2)
Tag_Result 1.0
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)