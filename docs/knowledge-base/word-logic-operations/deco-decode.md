# DECO: Decode

**Category:** Word logic operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 302  

---

DECO: Decode (S7-1200, S7-1500)
DECO: Decode
Description
The instruction "Decode" sets a bit specified by the input value in the output value.
The instruction "Decode" reads the value of the parameter IN and sets the bit in the output
value, whose bit position corresponds to the read value. The other bits in the output value
are filled with zeroes. If the value of the IN parameter is greater than 31, a modulo 32 in-
struction is executed.
Use the following syntax to change the data type of the instruction:
DECO_<data type>();
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
Position of the bit in the out-
IN Input UINT I, Q, M, D, L, P
put value which is set.
Data type of the function
value:
1. You can specify the da-
ta type of the instruc-
tion explicitly using "_".
2. If you do not specify
Bit strings the data type explicitly,
_<Data type> default: - it will be determined by
DWORD the utilized tags or
type-coded constants.
3. If you neither specify
the data type explicitly
nor specify defined
tags or type-coded
constants, the default
data type will be used.
Function value Bit strings I, Q, M, D, L, P Current output value
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
SCL
"Tag_Result" := DECO(IN := "Tag_Value");
"Tag_Result2" := DECO_BYTE(IN := "Tag_Value2");
The following figure shows how the instruction works using specific operand values:
DECO: Decode (S7-1200, S7-1500)
The instruction reads the number "3" from the value of the operand "Tag_Value" and sets
the third bit to the value of the operand "Tag_Result".
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)