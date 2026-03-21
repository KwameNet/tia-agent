# UNSCALE: Unscale

**Category:** Legacy  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 251  

---

UNSCALE: Unscale (S7-1500)
UNSCALE: Unscale
Description
You use the "Unscale" instruction to unscale the floating-point number in the IN parameter
into physical units between a low limit and a high limit and convert it into an integer. You
use the LO_LIM and HI_LIM parameters to specify the low limit and high limit of the value
range to which the input value is unscaled. The result of the instruction is output at the
OUT parameter.
The "Unscale" instruction works with the following equation:
OUT = [((IN–LO_LIM)/(HI_LIM–LO_LIM)) (K2–K1)] + K1
∗
The values of the "K1" and "K2" constants are determined by the signal state at the BIPO-
LAR parameter. The following signal states are possible at the BIPOLAR parameter:
• Signal state "1": It is assumed that the value at the IN parameter is bipolar and in a value
range between -27648 and 27648. In this case, the constant "K1" has the value -27648.0
and the constant "K2" the value +27648.0.
• Signal state "0": It is assumed that the value at the IN parameter is unipolar and in a val-
ue range between 0 and 27648. In this case, the constant "K1" has the value 0.0 and the
constant "K2" the value +27648.0.
When the value at parameter IN is not within the limits defined by HI_LIM and LO_LIM, an
error is output and the result is set to the nearest limit.
When the indicated low limit value is greater than the high limit value (LO_LIM > HI_LIM),
the result is scaled in reverse proportion to the input value.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
Input value to be un-
IN Input REAL I, Q, M, D, L, P scaled to an integer val-
ue.
HI_LIM Input REAL I, Q, M, D, L, P High limit
LO_LIM Input REAL I, Q, M, D, L, P Low limit
Indicates whether the val-
ue at the IN parameter is
to be interpreted as bipo-
lar or unipolar. The pa-
BIPOLAR Input BOOL I, Q, M, D, L rameter can assume the
following values:
1: Bipolar
0: Unipolar
OUT Output INT I, Q, M, D, L, P Result of the instruction
Function value
WORD I, Q, M, D, L, P Error information
(RET_VAL)
For additional information on valid data types, refer to "See also".
UNSCALE: Unscale (S7-1500)
RET_VAL parameter
The following table shows the meaning of the values of the RET_VAL parameter:
Error code Explanation
(W#16#...)
0000 No error
The value of the IN parameter is greater than the value of the high limit
0008
(HI_LIM) or less than the value of the low limit (LO_LIM).
General
error in- See also: "GET_ERR_ID: Get error ID locally"
formation
*The error codes can be displayed as integer or hexadecimal value in the program editor.
You can find information on switching the display formats here: Switching display for-
mats in the program status
Example
The following example shows how the instruction works:
"Tag_ErrorCode" := UNSCALE(IN := "Tag_InputValue",
HI_LIM := "Tag_HighLimit",
LO_LIM := "Tag_LowLimit",
BIPOLAR := "Tag_Bipolar",
OUT => "Tag_Result");
The error information is returned in the operand "Tag_ErrorCode" as a function value.
The following table shows how the instruction works using specific operand values:
Parameters Operand Value
IN Tag_InputValue 50.03978588
HI_LIM Tag_HighLimit 100.0
LO_LIM Tag_LowLimit 0.0
BIPOLAR Tag_Bipolar 1
OUT Tag_Result 22
RET_VAL Tag_ErrorCode W#16#0000
See also
Overview of the valid data types
GET_ERR_ID: Get error ID locally (S7-1200, S7-1500)
Evaluating errors with output parameter RET_VAL
Memory areas (S7-1500)
Basics of SCL