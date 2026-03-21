# TRUNC: Truncate numerical value

**Category:** Conversion operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 230  

---

TRUNC: Truncate numerical value (S7-1200, S7-1500)
TRUNC: Truncate numerical value
Description
The "Truncate numerical value" instruction is used to generate an integer from the input
value without rounding. The instruction selects only the integer part of the input value and
returns this part without decimal places as the function value.
Use the following syntax to change the data type of the instruction:
TRUNC_<data type>();
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
Floating-point
<Expression> Input I, Q, M, D, L Input value
numbers
Data type of the function
value:
1. You can specify the
data type of the in-
struction explicitly
using "_".
2. If you do not specify
the data type ex-
Integers,
plicitly, it will be de-
floating-point
_<Data type> - termined by the uti-
numbers
lized tags or type-
Default: DINT
coded constants.
3. If you neither speci-
fy the data type ex-
plicitly nor specify
defined tags or
type-coded con-
stants, the default
data type will be
used.
Integers,
Integer component of
Function value floating-point I, Q, M, D, L
the input value
numbers
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
SCL
"Tag_Result1" := TRUNC("Tag_Value1");
"Tag_Result2" := TRUNC("Tag_Value2"+"Tag_Value3");
TRUNC: Truncate numerical value (S7-1200, S7-1500)
"Tag_Result3" := TRUNC_SINT("Tag_Value4");
The result of the instruction is returned in the operand "Tag_Result" as a function value.
The following table shows how the instruction works using specific operand values:
Operand Value
Tag_Value1 -1.5
Tag_Result1 -1
Tag_Value2 2.1
Tag_Value3 3.2
Tag_Result2 5
Tag_Result3 2
Tag_Value4 2.4
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)