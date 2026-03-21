# NORM_X: Normalize

**Category:** Conversion operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 235  

---

NORM_X: Normalize (S7-1200, S7-1500)
NORM_X: Normalize
Description
You can use the instruction "Normalize" to normalize the value of the tag at the VALUE in-
put by mapping it to a linear scale. You can use the MIN and MAX parameters to define the
limits of a value range that is applied to the scale. The result at the OUT output is calcula-
ted and stored as a floating-point number depending on the location of the value to be nor-
malized within this value range. If the value to be normalized equals the value at input MIN,
the instruction returns the result "0.0". If the value to be normalized equals the value at in-
put MAX, the instruction returns the result "1.0".
Use the following syntax to change the data type of the instruction:
NORM_X_<data type>();
The following figure shows an example of how values can be normalized:
The "Normalize" instruction works with the following equation:
OUT = (VALUE – MIN) / (MAX – MIN)
Note
For more information on the conversion of analog values, refer to the respective manual.
Enable output ENO returns the signal state "0" if one of the following conditions applies:
• Enable input EN has the signal state "0".
• The value at input MIN is greater than or equal to the value at input MAX.
• The value of a specified floating-point number is outside the range of the normalized
numbers according to IEEE-754.
• The value at input VALUE is NaN (result of an invalid arithmetic operation).
Note
NORM_X: Normalize (S7-1200, S7-1500)
Use enable input (EN) and enable output (ENO)
The parameters EN and ENO are not generated automatically when creating the instruc-
tion ("NORM_X") in the SCL programming language.
For additional information on EN/ENO, refer to "See also".
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
I, Q, M, D, L or
EN Input BOOL Enable input
constant
ENO Output BOOL I, Q, M, D, L Enable output
Integers, float-
Low limit of the value
MIN 1) Input ing-point num- I, Q, M, D, L
range
bers
Integers, float-
VALUE 1) Input ing-point num- I, Q, M, D, L Value to be normalized.
bers
Integers, float-
High limit of the value
MAX 1) Input ing-point num- I, Q, M, D, L
range
bers
Data type of the function
value:
1. You can specify the
data type of the in-
struction explicitly
using "_".
2. If you do not specify
the data type explic-
Floating-point itly, it will be deter-
_<Data type> numbers - mined by the utiliz-
Default: REAL ed tags or type-co-
ded constants.
3. If you neither speci-
fy the data type ex-
plicitly nor specify
defined tags or
type-coded con-
stants, the default
data type will be
used.
Floating-point Result of the normaliza-
Function value I, Q, M, D, L
numbers tion
1) If you use constants in these three parameters, you only need to declare one of them.
For additional information on valid data types, refer to "See also".
For additional information on declaring constants, refer to "See also".
Example
NORM_X: Normalize (S7-1200, S7-1500)
The following example shows how the instruction works:
SCL
"Tag_Result1" := NORM_X(MIN := "Tag_Value1",
VALUE := "Tag_InputValue",
MAX := "Tag_Value2");
"Tag_Result2" := NORM_X_LREAL(MIN := "Tag_Value1",
VALUE := "Tag_InputValue",
MAX := "Tag_Value2");
The result of the instruction is returned in the operand "Tag_Result" as a function value.
The following table shows how the instruction works using specific operand values:
Operand Value
Tag_InputValue 20
Tag_Value1 10
Tag_Value2 30
Tag_Result1 0.5
Tag_Result2 0.5
See also
Overview of the valid data types
Declaring global constants
Overview of the EN/ENO mechanism in SCL
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)