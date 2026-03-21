# DEMUX: Demultiplex

**Category:** Word logic operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 310  

---

DEMUX: Demultiplex (S7-1200, S7-1500)
DEMUX: Demultiplex
Description
The "Demultiplex" instruction transfers the value of the input parameter IN to a selected
output parameter. The selection of the input parameter takes place independent of the pa-
rameter value K. The K parameter specifies the output parameter number to which the val-
ue of the input parameter IN is transferred. The other output parameters are not changed.
Numbering starts at OUT0 and continues consecutively with each new output. You can de-
clare a maximum of 32 output parameters.
If the value of the K parameter is greater than the number of output parameters, the con-
tent of the IN input parameter is copied to the OUTELSE output parameter and the signal
state "0" is assigned to the enable output ENO.
The function value is invalid if any of the following requirements are met:
• The value of the K parameter is greater than the number of available outputs.
• Errors occurred during execution of the instruction.
Note
K parameter < 0 or K > available outputs
If you specify at the K parameter a value that is outside the available outputs (K < 0 or K
> available outputs), the value of the IN input parameter is output at the OUTELSE out-
put parameter.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
S7-1200 S7-1500
Specifies the out-
put to which the
input value (IN)
will be copied.
I, Q, M, D, L,
K Input Integers Integers • If K = 0 => Pa-
P
rameter OUT0
• If K = 1 => Pa-
rameter OUT1,
etc.
Binary num-
Binary num-
bers, inte-
bers, inte-
gers, float-
gers, float-
ing-point
ing-point
numbers,
numbers, I, Q, M, D, L,
IN Input timers, Input value
strings, P
STRING,
timers, TOD,
CHAR,
LTOD,
WCHAR,
DATE, DT,
TOD, DATE,
LDT
DT
DEMUX: Demultiplex (S7-1200, S7-1500)
Binary num-
Binary num-
bers, inte-
bers, inte-
gers, float-
gers, float-
ing-point
ing-point
numbers,
numbers, I, Q, M, D, L,
OUT0 Output timers, First output
strings, P
STRING,
timers, TOD,
CHAR,
LTOD,
WCHAR,
DATE, DT,
TOD, DATE,
LDT
DT
Binary num-
Binary num-
bers, inte-
bers, inte-
gers, float-
gers, float-
ing-point
ing-point
numbers,
numbers, I, Q, M, D, L,
OUT1 Output timers, Second output
strings, P
STRING,
timers, TOD,
CHAR,
LTOD,
WCHAR,
DATE, DT,
TOD, DATE,
LDT
DT
Binary num-
Binary num-
bers, inte-
bers, inte-
gers, float-
gers, float-
ing-point
ing-point
numbers,
numbers, I, Q, M, D, L,
OUTn Output timers, Optional outputs
strings, P
STRING,
timers, TOD,
CHAR,
LTOD,
WCHAR,
DATE, DT,
TOD, DATE,
LDT
DT
Binary num-
Binary num-
bers, inte-
bers, inte-
gers, float-
gers, float-
ing-point
ing-point Output to which
numbers,
numbers, I, Q, M, D, L, the value at input
OUTELSE Output timers,
strings, P IN is copied if K >
STRING,
timers, TOD, n.
CHAR,
LTOD,
WCHAR,
DATE, DT,
TOD, DATE,
LDT
DT
For additional information on available data types, refer to "See also".
Note
Data types of parameters
Make sure that the input parameter "IN" and all output parameters ("OUT0", "OUT1", etc.
and "OUTELSE") have the same data type. Otherwise the data types are implicitly con-
verted and the parameter values may be changed. This also applies to the output pa-
rameters that are currently not specified at the "K" parameter.
DEMUX: Demultiplex (S7-1200, S7-1500)
Example
The following example shows how the instruction works:
SCL
DEMUX(K := "Tag_Number",
IN := "Tag_Value",
OUT0 := "Tag_1",
OUT1 := "Tag_2",
OUTELSE := "Tag_3");
The following tables show how the instruction works using specific operand values:
Input values of the "Demultiplex" instruction before the network execution
Parameters Operand Values
K Tag_Number 2 4
IN Tag_Value DW#16#FFFFFFFF DW#16#003E4A7D
Output values of the "Demultiplex" instruction after the network execution
Parameters Operand Values
OUT0 Tag_1 Unchanged Unchanged
OUT1 Tag_2 DW#16#FFFFFFFF Unchanged
OUTELSE Tag_3 Unchanged DW#16#003E4A7D
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)