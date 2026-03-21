# EXIT: Exit loop immediately

**Category:** Program control operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 267  

---

EXIT: Exit loop immediately (S7-1200, S7-1500)
EXIT: Exit loop immediately
Description
The instruction "Exit loop immediately" cancels the execution of a FOR, WHILE or REPEAT
loop at any point regardless of conditions. The execution of the program is continued after
the end of the loop (END_FOR, END_WHILE, END_REPEAT).
The instruction affects the program loop which directly contains the instruction.
Example
The following example shows how the instruction works:
SCL
FOR i := 15 TO 1 BY -2 DO
IF (i < 5)
THEN EXIT;
END_IF;
"DB10".Test[i] := 1;
END_FOR;
For additional information on valid data types, refer to "See also".
If the condition i < 5 is satisfied, then the execution of the loop will be canceled. Program
execution resumes after the END_FOR.
If the condition i < 5 is not satisfied, the subsequent value allocation ("DB10".Test[i] :=1) will
be executed and a new loop started. The run tag (i) is decreased by the increment of "-2"
and it is checked whether its current value lies in the programmed value range. If the (i) run
variable lies within the value range, the IF condition is evaluated again.
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)