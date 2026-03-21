# MIN: Get minimum

**Category:** Math functions  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 81  

---

MIN: Get minimum (S7-1200, S7-1500)
MIN: Get minimum
Description
The "Get minimum" instruction compares the values of the available inputs and returns the
lowest value as the result.
A minimum of two and a maximum of 32 inputs can be specified at the instruction.
The result is invalid if any of the following conditions are met:
• The implicit conversion of the data types fails during execution of the instruction.
• A floating-point number has an invalid value.
Parameters
The following table shows the parameters of the "Get minimum" instruction:
Parameters Declaration Data type Memory area Description
S7-1200 S7-1500
Integers,
floating-
point num-
Integers, bers,
floating-point TIME,
I, Q, M, D, L, First input
IN1 Input numbers, LTIME,
P value
TIME, TOD, TOD,
DATE, DTL LTOD,
DATE,
LDT, DT,
DTL
Integers,
floating-
point num-
Integers, bers,
floating-point TIME,
I, Q, M, D, L, Second input
IN2 Input numbers, LTIME,
P value
TIME, TOD, TOD,
DATE, DTL LTOD,
DATE,
LDT, DT,
DTL
Integers,
floating-
point num-
Integers, bers, Additionally
floating-point TIME, inserted in-
I, Q, M, D, L,
INn Input numbers, LTIME, puts whose
P
TIME, TOD, TOD, values are to
DATE, DTL LTOD, be compared
DATE,
LDT, DT,
DTL
MIN: Get minimum (S7-1200, S7-1500)
Integers,
floating-
point num-
Integers, bers,
floating-point TIME,
I, Q, M, D, L, Result of the
Function value numbers, LTIME,
P instruction
TIME, TOD, TOD,
DATE, DTL LTOD,
DATE,
LDT, DT,
DTL
The data types TOD, LTOD, DATE, and LDT can only be used if the IEC test is not ena-
bled.
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
SCL
"Tag_Result" := MIN(IN1 := "Tag_Value1",
IN2 := "Tag_Value2",
IN3 := "Tag_Value3");
The following table shows how the instruction works using specific operand values:
Parameters Operand Value
IN1 Tag_Value1 12222
IN2 Tag_Value2 14444
IN3 Tag_Value3 13333
Function value Tag_Result 12222
The instruction compares the values of the available inputs and copies the lowest value
("Tag_Value1") to operand "Tag_Result".
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)