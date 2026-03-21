# SCALE_X: Scale

**Category:** Conversion operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 232  

---

SCALE_X: Scale (S7-1200, S7-1500)
SCALE_X: Scale
Description
Use the "Scale" instruction to scale a floating-point number by mapping it to a specific val-
ue range. You specify the value range with the MIN and MAX parameters. The result of the
scaling is an integer.
Use the following syntax to change the data type of the instruction:
SCALE_X_<data type>();
The following figure shows an example of how values can be scaled:
The "Scale" instruction works with the following equation:
OUT = [VALUE (MAX – MIN)] + MIN
∗
Note
For more information on the conversion of analog values, refer to the respective manual.
Enable output ENO returns the signal state "0" if one of the following conditions applies:
• Enable input EN has the signal state "0".
• The value at input MIN is greater than or equal to the value at input MAX.
• The value of a specified floating-point number is outside the range of the normalized
numbers according to IEEE-754.
• An overflow occurs.
• The value at input VALUE is NaN (Not a number = result of an invalid arithmetic opera-
tion).
Note
Use enable input (EN) and enable output (ENO)
SCALE_X: Scale (S7-1200, S7-1500)
The parameters EN and ENO are not generated automatically when creating the instruc-
tion ("SCALE_X") in the SCL programming language.
For additional information on EN/ENO, refer to "See also".
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
I, Q, M, D, L
EN Input BOOL Enable input
or constant
ENO Output BOOL I, Q, M, D, L Enable output
Integers, float-
Low limit of the value
MIN Input ing-point num- I, Q, M, D, L
range
bers
Value to be scaled. If you
Floating-point
VALUE Input I, Q, M, D, L enter a constant, you
numbers
must declare it.
Integers, float-
High limit of the value
MAX Input ing-point num- I, Q, M, D, L
range
bers
Data type of the function
value:
1. You can specify the
data type of the in-
struction explicitly
using "_".
2. If you do not specify
Integers, float- the data type explicit-
ing-point num- ly, it will be deter-
_<Data type> -
bers mined by the utilized
Default: INT tags or type-coded
constants.
3. If you neither specify
the data type explicit-
ly nor specify defined
tags or type-coded
constants, the de-
fault data type will be
used.
Integers, float-
Function value ing-point num- - Result of scaling
bers
For additional information on valid data types, refer to "See also".
For additional information on declaring constants, refer to "See also".
Example
The following example shows how the instruction works:
SCALE_X: Scale (S7-1200, S7-1500)
SCL
"Tag_Result1" := SCALE_X(MIN := "Tag_Value1",
VALUE := "Tag_Real",
MAX := "Tag_Value2");
"Tag_Result2" := SCALE_X_REAL(MIN := "Tag_Value1",
VALUE := "Tag_Real",
MAX := "Tag_Value2");
The result of the instruction is returned in the operand "Tag_Result" as a function value.
The following table shows how the instruction works using specific operand values:
Operand Value
Tag_Real 0.5
Tag_Value1 10
Tag_Value2 30
Tag_Result1 20
Tag_Result2 20.0
See also
Overview of the valid data types
Declaring global constants
Overview of the EN/ENO mechanism in SCL
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)