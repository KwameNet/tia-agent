# LEAD_LAG: Lead and lag algorithm

**Category:** Legacy  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 341  

---

LEAD_LAG: Lead and lag algorithm (S7-1500)
LEAD_LAG: Lead and lag algorithm
Description
You can use the "Lead and lag algorithm" instruction to process signals with an analog tag.
The gain value at the GAIN parameter must be greater than zero. The result of the "Lead
and lag algorithm" instruction is calculated using the following equation:
The "Lead and lag algorithm" instruction supplies plausible results only when processing is
in fixed program cycles. The same units must be specified at the parameters LD_TIME,
LG_TIME and SAMPLE_T. At LG_TIME > 4 + SAMPLE_T, the instruction approaches the
following function:
OUT = GAIN * ((1 + LD_TIME * s) / (1 + LG_TIME * s)) * IN
When the value of the GAIN parameter is less than or equal to zero, the calculation is not
performed and an error information is output on the ERR_CODE parameter.
You can use the instruction "Lead and lag algorithm" in conjunction with loops as a com-
pensator in dynamic feed-forward control. The instruction consists of two operations. The
"Lead" operation shifts the phase of output OUT so that the output leads the input. The
"Lag" operation, on the other hand, shifts the output so that the output lags behind the in-
put. Because the "Lag" operation is equivalent to an integration, it can be used as a noise
suppressor or as a low-pass filter. The "Lead" operation is equivalent to a differentiation
and can therefore be used as a high-pass filter. The two instructions together (Lead and
Lag) result in the output phase lagging behind the input at lower frequencies and leading it
at higher frequencies. This means that the "Lead and lag algorithm" instruction can be
used as a band pass filter.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
The input value
of the current
sample time (cy-
cle time) to be
processed.
IN Input REAL I, Q, M, D, L, P
Constants can
also be specified
at the IN param-
eter.
Sample time
Constants can
SAMPLE_T Input INT I, Q, M, D, L, P also be specified
at the SAM-
PLE_T parame-
ter.
LEAD_LAG: Lead and lag algorithm (S7-1500)
Result of the in-
OUT Output REAL I, Q, M, D, L
struction
ERR_CODE Output WORD I, Q, M, D, L Error information
Lead time in the
LD_TIME Static REAL I, Q, M, D, L, P same unit as
sample time.
Lag time in the
LG_TIME Static REAL I, Q, M, D, L, P same unit as
sample time
Gain as % / %
(the ratio of the
change in output
GAIN Static REAL I, Q, M, D, L, P
to a change in in-
put as a steady
state).
PREV_IN Static REAL I, Q, M, D, L, P Previous input
PREV_OUT Static REAL I, Q, M, D, L, P Previous output
The static parameters are not visible when calling the instruction in the program. These are
saved in the instance of the instruction.
ERR_CODE parameter
The following table shows the meaning of the values of the ERR_CODE parameter:
Error code* Explanation
(W#16#...)
0000 No error
0009 The value at the GAIN parameter is less than or equal to zero.
*The error codes can be displayed as integer or hexadecimal value in the program editor.
For information on toggling display formats, refer to "See also".
Example
The following example shows how the instruction works:
Note
You can initialize static parameters in the data block.
SCL
"LEAD_LAG_DB"(IN := "Tag_Input",
SAMPLE_T := "Tag_Input_SAMPLE_T",
OUT => "Tag_Output_Result",
ERR_CODE => "Tag_ErrorCode");
The following tables show how the instruction works using specific values.
Before processing
In this example the following values are used for the input parameters:
LEAD_LAG: Lead and lag algorithm (S7-1500)
Parameters Operand Value
IN Tag_Input 2.0
SAMPLE_T Tag_Input_SAMPLE_T 10
The following values are saved in the "LEAD_LAG_DB" instance data block of the instruc-
tion:
Parameters Address Value
LD_TIME DBD12 2.0
LG_TIME DBD16 2.0
GAIN DBD20 1.0
PREV_IN DBD24 6.0
PREV_OUT DBD28 6.0
After processing
The following values are written to the output parameters after the instruction has been
executed:
Parameters Operand Value
OUT Tag_Output_Result 2.0
The following values are saved in the instance data block "LEAD_LAD_DB" of the instruc-
tion:
Parameters Operand Value
PREV_IN DBD24 2.0
PREV_OUT DBD28 2.0
See also
Overview of the valid data types
Switching display formats in the program status
Memory areas (S7-1500)
Basics of SCL