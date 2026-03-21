# WAIT: Configure time delay

**Category:** PC systems  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 296  

---

WAIT: Configure time delay (S7-1500)
WAIT: Configure time delay
Description
The "Configure time delay" instruction pauses the program execution for a specific period
of time. You indicate the period of time in microseconds on the WT parameter of the in-
struction.
You can configure time delays from -32768 up to 32767 microseconds (μs). The smallest
possible delay time depends on the CPU and corresponds to the execution time of the in-
struction.
The execution of the instruction can be interrupted by higher priority events and does not
return any error information.
Note
Negative delay time
If you specify a negative delay time at parameter WT, the enable output ENO, or the
RLO and the BR bit, will return the signal state FALSE. A negative delay time does not
affect the CPU. The following instructions linked to enable output ENO are not executed
in LAD or FBD.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
I, Q, M, D, L, P Time delay in microsec-
WT Input INT
or constant onds (μs)
Example of the influence of the planned delay time
In the following example, you will see the influence of the delay time of the instruction
"WAIT" in various scenarios.
The following figure is a schematic representation of the scenarios:
WAIT: Configure time delay (S7-1500)
Remaining time = The period between the end of the planned delay time (via "WAIT") until
the end of the interrupt OB
Excess time = The period between the end of the interrupt OB and the end of the planned
delay time (via "WAIT").
Case 1:
The "WAIT" instruction is called in an OB1. The "WAIT" instruction can be interrupted by
higher-priority OBs or higher-priority processes (e.g. System Threads). However, the delay
time of the "WAIT" instruction is neither changed nor deferred.
Cases 2 and 3:
Processing of the program in OB1 is resumed after a time delay of 20 ms. This time delay
is calculated by calling the "WAIT" instruction in OB1 (see OB1 with WAIT). Within these
20 ms, an interrupt OB can run its own program code. The send clock of the CPU is not
changed.
Case 4:
Processing of the program in OB1 is resumed after the higher-priority process has ended.
The 20 ms delay time in OB1 has elapsed, but the higher-priority process still needs to be
ended. The send clock of the CPU is increased.
Note
Order of execution of system or communication processes (System Threads)
WAIT: Configure time delay (S7-1500)
The System Threads usually use the priority "15". There are also System Threads with a
higher priority than "26", but these processes cause a lower CPU load. System Threads
are not displayed in the figure.
Runtime measurement of the OB1 using the "RT_INFO" instruction:
Case 2: 20 ms - 8 ms - System Threads = <12 ms. Send clock: ~20 ms.
Case 3: 20 ms - 11 ms - System Threads - <9 ms. Send clock: ~20 ms.
Case 4: 20 ms - 15 ms - System Threads - <7 ms. Send clock: ~22 ms.
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)