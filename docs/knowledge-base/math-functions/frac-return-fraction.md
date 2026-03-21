# FRAC: Return fraction

**Category:** Math functions  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 97  

---

FRAC: Return fraction (S7-1200, S7-1500)
FRAC: Return fraction
Description
The result of the instruction "Return fraction" returns the decimal places of a value. Input
value 1.125, for example, returns the value 0.125.
Use the following syntax to change the data type of the instruction:
FRAC_<data type>();
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
Floating-point I, Q, M, D, L,
<Expression> Input Input value
numbers P
Data type of the function
value:
1. You can specify the
data type of the in-
struction explicitly
using "_".
2. If you do not specify
the data type explic-
Floating-point
itly, it will be deter-
numbers
_<Data type> -
mined by the utilized
Default: REAL tags or type-coded
constants.
3. If you neither specify
the data type explic-
itly nor specify de-
fined tags or type-
coded constants, the
default data type will
be used.
Floating-point I, Q, M, D, L, Decimal places of the in-
Function value
numbers P put value
You can find additional information on valid data types under "See also".
Example
The following example shows how the instruction works:
SCL
"Tag_Result1" := FRAC("Tag_Value");
"Tag_Result2" := FRAC_LREAL("Tag_Value");
The following table shows how the instruction works using specific operand values:
FRAC: Return fraction (S7-1200, S7-1500)
Operand Value
Tag_Value 1.125
Tag_Result1 0.125
See also
Overview of the valid data types
Invalid floating-point numbers
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)