# MUX: Multiplex

**Category:** Word logic operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 307  

---

MUX: Multiplex (S7-1200, S7-1500)
MUX: Multiplex
Description
The "Multiplex" instruction copies the value of a selected input parameter and issues it. You
can use the parameter K to determine the number of the input parameter whose value will
be moved. Numbering starts at IN0 and is incremented continuously with each new input.
You can declare a maximum of 32 inputs.
Numerical data types and time data types are permitted at the inputs. All tags with as-
signed parameters must be of the same data type.
The function value is invalid if any of the following requirements are met:
• Errors occurred during execution of the instruction.
• If the input at the K parameter is located outside the available inputs and the INELSE
input is not used, the function value is invalid.
Note
K parameter has a negative integer
If a tag with a valid data type is specified at the input parameters and the K parameter
has a negative integer, the value of the tag is changed.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
S7-1200 S7-1500
Specifies the pa-
rameter whose
content is to be
transferred.
I, Q, M, D,
K Input Integers Integers
L, P • If K = 0 => Pa-
rameter IN0
• If K = 1 => Pa-
rameter IN1, etc.
Binary num-
bers, inte-
Binary num-
gers, float-
bers, inte-
ing-point
gers, floating-
numbers,
point num- I, Q, M, D,
IN0 Input timers, First input value
bers, strings, L, P
STRING,
TOD, LTOD,
CHAR,
DATE, timers,
WCHAR,
DT, LDT
TOD,
DATE, DT
Binary num- Binary num-
bers, inte- bers, inte- I, Q, M, D,
IN1 Input Second input value
gers, float- gers, floating- L, P
ing-point point num-
MUX: Multiplex (S7-1200, S7-1500)
numbers,
timers,
bers, strings,
STRING,
TOD, LTOD,
CHAR,
DATE, timers,
WCHAR,
DT, LDT
TOD,
DATE, DT
Binary num-
bers, inte-
Binary num-
gers, float-
bers, inte-
ing-point
gers, floating-
numbers,
point num- I, Q, M, D, Optional input val-
INn Input timers,
bers, strings, L, P ues
STRING,
TOD, LTOD,
CHAR,
DATE, timers,
WCHAR,
DT, LDT
TOD,
DATE, DT
Binary num-
bers, inte-
Binary num-
gers, float-
bers, inte-
ing-point
gers, floating-
numbers, Specifies the value
point num- I, Q, M, D,
INELSE Input timers, to be copied when
bers, strings, L, P
STRING, K <> n.
TOD, LTOD,
CHAR,
DATE, timers,
WCHAR,
DT, LDT
TOD,
DATE, DT
Binary num-
bers, inte-
Binary num-
gers, float-
bers, inte-
ing-point
gers, floating-
numbers,
Function value point num- I, Q, M, D, Result of the in-
timers,
bers, strings, L, P struction
STRING,
TOD, LTOD,
CHAR,
DATE, timers,
WCHAR,
DT, LDT
TOD,
DATE, DT
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
SCL
"Tag_Result" := MUX(K := "Tag_Number",
IN0 := "Tag_1",
IN1 := "Tag_2",
INELSE := "Tag_3");
The result of the instruction is returned in the operand "Tag_Result" as a function value.
MUX: Multiplex (S7-1200, S7-1500)
The following table shows how the instruction works using specific operand values:
Operand Value
Tag_Number 1 4
Tag_1 DW#16#00000000 DW#16#00000000
Tag_2 DW#16#003E4A7D DW#16#003E4A7D
Tag_3 DW#16#FFFF0000 DW#16#FFFF0000
Tag_Result DW#16#003E4A7D DW#16#FFFF0000
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)