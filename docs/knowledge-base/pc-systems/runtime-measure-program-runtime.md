# RUNTIME: Measure program runtime

**Category:** PC systems  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 299  

---

RUNTIME: Measure program runtime (S7-1200, S7-1500)
RUNTIME: Measure program runtime
Description
The "Measure program runtime" instruction is used to measure the runtime of the entire
program, individual blocks or command sequences.
If you want to measure the runtime of your entire program, call the instruction "Measure
program runtime" in OB1. Measurement of the runtime is started with the first call and the
output RET_VAL returns the runtime of the program after the second call. The measured
runtime includes all CPU processes that can occur during the program execution, for ex-
ample, interruptions caused by higher-level events or communication. The instruction
"Measure program runtime" reads an internal counter of the CPU and writes the value to
the in/out parameter. The instruction calculates the current program runtime according to
the internal counter frequency and writes it to output RET_VAL.
If you want to measure the runtime of individual blocks or individual command sequences,
you need three separate networks. Call the instruction "Measure program runtime" in an in-
dividual network within your program. You set the starting point of the runtime measure-
ment with this first call of the instruction. Then you call the required program block or the
command sequence in the next network. In another network, call the "Measure program
runtime" instruction a second time and assign the same memory to the in/out parameter as
you did during the first call of the instruction. The "Measure program runtime" instruction in
the third network reads an internal CPU counter and calculates the current runtime of the
program block or the command sequence according to the internal counter frequency and
writes it to the output RET_VAL.
The "Measure program runtime" instruction uses an internal high-frequency counter to cal-
culate the time. If the counter overruns, the instruction returns values <= 0.0. This can oc-
cur up to once per minute for S7-1200 CPUs with firmware version <V4.2. Ignore these
runtime values.
Note
The runtime of a command sequence cannot be determined exactly, because the se-
quence of instructions within a command sequence is changed during optimized compi-
lation of the program.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
The content is for internal
<Operand> InOut LREAL I, Q, M, D, L
purposes only.
Function value Returns the measured
LREAL I, Q, M, D, L
runtime in seconds
Example
The following example shows the how the instruction works based on the runtime calcula-
tion of a program block:
SCL
RUNTIME: Measure program runtime (S7-1200, S7-1500)
"Tag_Result" := RUNTIME("Tag_Memory");
"Best_before_date_DB" ();
"Tag_Result" := RUNTIME("Tag_Memory");
The starting point for the runtime measurement is set with the first call of the instruction
and buffered as reference for the second call of the instruction in the "TagMemory" oper-
and.
The "Best_before_date" program block FB1 is called.
When the program block FB1 has been processed, the instruction is executed a second
time. The second call of the instruction calculates the runtime of the program block and
writes the result to the output "Tag_Result".
You can find a more detailed example of how you can measure the total cycle time of a
program in the Siemens Industry Online Support under the following entry ID: 87668055
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)