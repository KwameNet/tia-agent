# LOWER_BOUND: Read out low ARRAY limit

**Category:** ARRAY[*]  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 209  

---

LOWER_BOUND: Read out low ARRAY limit (S7-1200, S7-1500)
LOWER_BOUND: Read out low ARRAY limit
Description
You can declare tags of the data type ARRAY[*] in the block interface of a function block or
function. For these local tags, you can read out the limits of the ARRAY. You will need to
specify the required dimension at the DIM parameter.
The "Read out ARRAY low limit" instruction is used to read out the variable low limit of the
ARRAY.
Note
Availability of the instruction
The instruction is available for a CPU of the S7-1200 series as of firmware version >=
4.2, and for a CPU of the S7-1500 series as of firmware version >= 2.0.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
FB => Section
InOut ARRAY of which the vari-
ARR Input ARRAY[*] able low limit is to be
FC => Sections read.
Input and InOut
Dimension of the ARRAY
I, Q, M, D, L, P
DIM Input UDINT of which the variable low
or constant
limit is to be read.
Function value DINT I, Q, M, D, L, P Result
Example
The following example shows how the instruction works:
SCL
"Result" := LOWER_BOUND(ARR := #ARRAY_A,
DIM := 2);
The instruction reads out the variable low limit of the ARRAY #ARRAY_A from the second
dimension. If the instruction is executed without errors, the result is written to operand "Re-
sult".
See also
Overview of the valid data types
Basic information on ARRAY
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)