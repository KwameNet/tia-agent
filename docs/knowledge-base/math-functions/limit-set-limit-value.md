# LIMIT: Set limit value

**Category:** Math functions  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 85  

---

LIMIT: Set limit value (S7-1200, S7-1500)
LIMIT: Set limit value
Description
The "Set limit value" instruction limits the value of the parameter IN to the values of the pa-
rameters MN and MX. The value of the parameter MN may not be greater than the value of
the parameter MX.
If the value of the IN parameter fulfills the condition MN <= IN <= MX, it is returned as the
result of the instruction. If the condition is not fulfilled and the IN input value is less than the
MN low limit, the value of the MN parameter is returned as the result. If the high limit MX is
exceeded, the value of the MX parameter is returned as the result.
If the value at the MN input is greater than at the MX input, the result is the value specified
at the IN parameter and the enable output ENO is "0".
The instruction is only executed if the operands of all parameters are of the same data
type.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
S7-1200 S7-1500
Integers,
floating-
point num-
Integers, bers,
floating-point TIME,
I, Q, M, D, L,
MN Input numbers, LTIME, Low limit
P
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
I, Q, M, D, L,
IN Input numbers, LTIME, Input value
P
TIME, TOD, TOD,
DATE, DTL LTOD,
DATE,
LDT, DT,
DTL
Integers,
Integers, floating-
floating-point point num-
I, Q, M, D, L,
MX Input numbers, bers, High limit
P
TIME, TOD, TIME,
DATE, DTL LTIME,
TOD,
LIMIT: Set limit value (S7-1200, S7-1500)
LTOD,
DATE,
LDT, DT,
DTL
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
"Tag_Result" := LIMIT(MN := "Tag_Minimum",
IN := "Tag_Value",
MX := "Tag_Maximum");
The following table shows how the instruction works using specific operand values:
Parameters Operand Value
MN Tag_Minimum 12 000
IN Tag_Value 8 000
MX Tag_Maximum 16 000
Function value Tag_Result 12 000
The value of operand "Tag_Value" is compared with the values of operands "Tag_Mini-
mum" and "Tag_Maximum". Because the value of the operand "Tag_Value" is less than the
low limit value, the value of operand "Tag_Minimum" will be copied to operand "Tag_Re-
sult".
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)