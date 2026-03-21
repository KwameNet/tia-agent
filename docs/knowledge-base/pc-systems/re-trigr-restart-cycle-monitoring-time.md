# RE_TRIGR: Restart cycle monitoring time

**Category:** PC systems  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 285  

---

RE_TRIGR: Restart cycle monitoring time (S7-1200, S7-1500)
RE_TRIGR: Restart cycle monitoring time
Description
The "Restart cycle monitoring time" instruction is used to restart the cycle monitoring time
of the CPU. The cycle monitoring time then restarts with the time you have set in the CPU
configuration.
The instruction executes completely within a time span (10 times the maximum program
cycle), regardless of the number of calls. Once this time has expired, the program cycle
can no longer be prolonged.
Calling the instruction
The following call conditions apply:
• The following applies to a CPU of the S7-1200 series:
In firmware versions <= 2.2, you can call the instruction only in a program cycle organi-
zation block of priority 1. This corresponds to the lowest priority of all organization
blocks. If the instruction is called in an organization block with a higher priority, the in-
struction is not executed and the result (BR bit, enable output ENO) is always "0".
In firmware versions >= 2.2, you can call the instruction regardless of the priority in all
organization blocks.
• The following applies to a CPU of the S7-1500 series:
You can call the instruction regardless of the priority in all organization blocks.
Parameters
The "Restart cycle monitoring time" instruction has no parameters and supplies no error in-
formation.
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)