# ATAN: Form arctangent value

**Category:** Math functions  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 96  

---

ATAN: Form arctangent value (S7-1200, S7-1500)
ATAN: Form arctangent value
Description
You can use the "Form arctangent value" instruction to calculate the size of the angle from
a tangent value, which corresponds to this value. It is only permitted to specify valid float-
ing-point numbers (or -NaN/+NaN) as input value. The calculated angle size is given in ra-
dians and can range in value from -π/2 to +π/2.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
<Expres- Floating-point
Input I, Q, M, D, L, P Tangent value
sion> numbers
Floating-point
Function value I, Q, M, D, L, P Size of angle in radians
numbers
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
SCL
"Tag_Result" := ATAN("Tag_Value");
The result of the instruction is returned in the operand "Tag_Result" as a function value.
The following table shows how the instruction works using specific operand values:
Operand Value
Tag_Value 1.0
Tag_Result +0.785398 (π/4)
See also
Overview of the valid data types
Invalid floating-point numbers
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)