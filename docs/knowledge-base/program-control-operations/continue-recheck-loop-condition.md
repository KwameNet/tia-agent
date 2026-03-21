# CONTINUE: Recheck loop condition

**Category:** Program control operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 266  

---

CONTINUE: Recheck loop condition (S7-1200, S7-1500)
CONTINUE: Recheck loop condition
Description
The "Recheck loop condition" instruction ends the current program run of a FOR, WHILE or
REPEAT loop.
After execution of the instruction, the conditions for the continuation of the program loop
are evaluated again. The instruction affects the program loop which directly contains the
instruction.
Example
The following example shows how the instruction works:
SCL
FOR i
:= 1 TO 15 BY 2 DO
IF (i < 5) THEN
CONTINUE;
END_IF;
"DB10".Test[i] := 1;
END_FOR;
For additional information on valid data types, refer to "See also".
If the condition i < 5 is satisfied, the subsequent value allocation ("DB10".Test[i] := 1) will
not be executed. The run variable (i) is increased by the increment of "2" and checked to
see whether its current value lies in the programmed value range. If the run variable lies in
the value range, the IF condition is evaluated again.
If the condition i < 5 is not satisfied, the subsequent value allocation ("DB10".Test[i] := 1)
will be executed and a new loop started. In this case, the run variable is also increased by
the increment "2" and checked.
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)