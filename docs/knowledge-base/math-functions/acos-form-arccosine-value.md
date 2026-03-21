# ACOS: Form arccosine value

**Category:** Math functions  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 95  

---

ACOS: Form arccosine value (S7-1200, S7-1500)
ACOS: Form arccosine value
Description
You can use the "Form arccosine value" instruction to calculate the size of the angle from a
cosine value, which corresponds to this value. Only valid floating-point numbers within the
range -1 to +1 can be specified as input values. The calculated angle size is given in radi-
ans and can range in value from 0 to +π.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
<Expres- Floating-point
Input I, Q, M, D, L, P Cosine value
sion> numbers
Floating-point
Function value I, Q, M, D, L, P Size of angle in radians
numbers
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
SCL
"Tag_Result" := ACOS("Tag_Value");
The result of the instruction is returned in the operand "Tag_Result" as a function value.
The following table shows how the instruction works using specific operand values:
Operand Value
Tag_Value 0
Tag_Result +1.570796 (π/2)
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)