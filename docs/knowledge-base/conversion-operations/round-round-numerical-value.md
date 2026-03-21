# ROUND: Round numerical value

**Category:** Conversion operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 225  

---

ROUND: Round numerical value (S7-1200, S7-1500)
ROUND: Round numerical value
Description
The "Round numerical value" instruction is used to round the value at input IN to the near-
est integer. The instruction interprets the value at input IN as a floating-point number and
converts it into an integer or floating-point number. If the input value is exactly between an
even and odd number, the even number is selected.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
Floating-point Input value to be
<expression> Input I, Q, M, D, L, P
numbers rounded.
Integers,
Result of the
Function value floating-point I, Q, M, D, L, P
rounding
numbers
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
SCL
"Tag_Result" := ROUND("Tag_Value");
The result of the instruction is returned in the operand "Tag_Result" as a function value.
The following table shows how the instruction works using specific operand values:
Operand Value
Tag_Value 1.50000000 -1.50000000
Tag_Result 2 -2
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)