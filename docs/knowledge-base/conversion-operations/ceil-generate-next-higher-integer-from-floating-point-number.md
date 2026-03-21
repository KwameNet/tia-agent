# CEIL: Generate next higher integer from floating-point number

**Category:** Conversion operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 226  

---

CEIL: Generate next higher integer from floating-point number (S7-1200, S7-1500)
CEIL: Generate next higher integer from floating-
point number
Description
Use the "Generate next higher integer from floating-point number" instruction to round the
value to the nearest integer. The instruction interprets the input value as floating-point num-
ber and converts it to the next higher integer. The function value can be greater than or
equal to the input value.
Use the following syntax to change the data type of the instruction:
CEIL_<data type>();
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
<Expres- Floating-point
Input I, Q, M, D, L, P Input value
sion> numbers
Data type of the function val-
ue:
1. You can specify the data
type of the instruction ex-
plicitly using "_".
2. If you do not specify the
Integers,
data type explicitly, it will
floating-point
_<Data type> - be determined by the uti-
numbers De-
lized tags or type-coded
fault: DINT
constants.
3. If you neither specify the
data type explicitly nor
specify defined tags or
type-coded constants,
the default data type will
be used.
Integers,
Function value floating-point I, Q, M, D, L Input value rounded up
numbers
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
SCL
"Tag_Result1" := CEIL("Tag_Value");
"Tag_Result2" := CEIL_REAL("Tag_Value");
The following table shows how the instruction works using specific operand values:
CEIL: Generate next higher integer from floating-point number (S7-1200, S7-1500)
Operand Value
Tag_Value 0.5 -0.5
Tag_Result1 1 0
Tag_Result2 1.0 0.0
The result of the instruction is returned in the operand "Tag_Resultxy" as a function value.
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)