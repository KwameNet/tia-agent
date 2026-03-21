# FLOOR: Generate next lower integer from floating-point number

**Category:** Conversion operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 228  

---

FLOOR: Generate next lower integer from floating-point number (S7-1200, S7-1500)
FLOOR: Generate next lower integer from floating-
point number
Description
Use the "Generate next lower integer from floating-point number" instruction to round the
value of a floating point number to the next lower integer. The instruction interprets the in-
put value as floating-point number and converts it to the next lower integer. The function
value can be equal or less than the input value.
Use the following syntax to change the data type of the instruction:
FLOOR_<data type>();
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
<Expres- Floating-point
Input I, Q, M, D, L, P Input value
sion> numbers
Data type of the function
value:
1. You can specify the da-
ta type of the instruc-
tion explicitly using "_".
2. If you do not specify
Integers,
the data type explicitly,
floating-point
_<Data type> - it will be determined by
numbers De-
the utilized tags or
fault: DINT
type-coded constants.
3. If you neither specify
the data type explicitly
nor specify defined
tags or type-coded
constants, the default
data type will be used.
Integers,
Function value floating-point I, Q, M, D, L Input value rounded
numbers
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
SCL
"Tag_Result1" := FLOOR("Tag_Value");
"Tag_Result2" := FLOOR_REAL("Tag_Value");
The following table shows how the instruction works using specific operand values:
FLOOR: Generate next lower integer from floating-point number (S7-1200, S7-1500)
Operand Value
Tag_Value 0.5 -0.5
Tag_Result1 0 -1
Tag_Result2 0.0 -1.0
The result of the instruction is returned in the operand "Tag_Resultxy" as a function value.
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)