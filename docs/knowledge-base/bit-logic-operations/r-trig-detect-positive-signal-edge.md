# R_TRIG: Detect positive signal edge

**Category:** Bit logic operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 3  

---

R_TRIG: Detect positive signal edge (S7-1200, S7-1500)
R_TRIG: Detect positive signal edge
Description
With the "Detect positive signal edge" instruction, you can detect a state change from "0" to
"1" at the CLK input. The instruction compares the current value at the CLK input with the
state of the previous query (edge memory bit) that is saved in the specified instance. If the
instruction detects a state change at the CLK input from "0" to "1", a positive signal edge is
generated at the Q output, i.e., the output has the value TRUE or "1" for exactly one cycle.
In all other cases, the signal state at the output of the instruction is "0".
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
Incoming signal, the edge
CLK Input BOOL I, Q, M, D, L
of which is to be queried
Q Output BOOL I, Q, M, D, L Result of edge evaluation
Example
The following example shows how the instruction works:
SCL
"R_TRIG_DB"(CLK := "TagIn",
Q => "TagOut");
The previous state of the tag at the CLK input is stored in the "R_TRIG_DB" tag. If a
change in the signal state from "0" to "1" is detected in the "TagIn" operand, the "TagOut"
output has signal state "1" for one cycle.
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)