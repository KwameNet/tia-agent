# SEL: Select

**Category:** Word logic operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 305  

---

SEL: Select (S7-1200, S7-1500)
SEL: Select
Description
The instruction "Select" selects one of the parameters IN0 or IN1 depending on a switch (G
parameter) and issues its content as a result. When the parameter G has the signal status
"0", the value at parameter IN0 is moved. When the parameter G has the signal status "1",
the value at parameter IN1 is moved and returned as a function value.
The instruction is only executed if the tags of all parameters have the same data type
class.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
S7-1200 S7-1500
I, Q, M, D,
G Input BOOL BOOL Switch
L
Binary num-
Binary num- bers, integers,
bers, integers, floating-point
floating-point numbers, I, Q, M, D,
IN0 Input First input value
numbers, timers, L, P
timers, strings, strings, DATE,
TOD, DATE, DT TOD, LTOD,
DT, LDT
Binary num-
Binary num- bers, integers,
bers, integers, floating-point
floating-point numbers, I, Q, M, D, Second input
IN1 Input
numbers, timers, L, P value
timers, strings, strings, DATE,
TOD, DATE, DT TOD, LTOD,
DT, LDT
Binary num-
Binary num- bers, integers,
bers, integers, floating-point
floating-point numbers, I, Q, M, D, Result of the in-
Function value
numbers, timers, L, P struction
timers, strings, strings, DATE,
TOD, DATE, DT TOD, LTOD,
DT, LDT
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
SCL
SEL: Select (S7-1200, S7-1500)
"Tag_Result" := SEL(G := "Tag_Value",
IN0 := "Tag_0",
IN1 := "Tag_1");
The result of the instruction is returned in the operand "Tag_Result" as a function value.
The following table shows how the instruction works using specific operand values:
Operand Value
Tag_Value 0 1
Tag_0 W#16#0000 W#16#4C
Tag_1 W#16#FFFF D#16#5E
Tag_Result W#16#0000 D#16#5E
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)