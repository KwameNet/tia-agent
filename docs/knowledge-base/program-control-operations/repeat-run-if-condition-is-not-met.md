# REPEAT: Run if condition is not met

**Category:** Program control operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 264  

---

REPEAT: Run if condition is not met (S7-1200, S7-1500)
REPEAT: Run if condition is not met
Description
The instruction "Run if condition is not met" causes a program loop to be repeatedly execu-
ted until a termination condition is met. The condition is an expression with Boolean value
(TRUE or FALSE). Logical expression or comparative expressions can be stated as condi-
tions.
When the instruction is executed, the stated expressions are evaluated. If the value of an
expression is TRUE, the condition is fulfilled; if the value is FALSE, it is not fulfilled.
The instructions are executed once, even if the termination condition is fulfilled.
Program loops can also be nested. Within a program loop, you can program additional pro-
gram loops with other loop variables.
The current continuous run of a program loop can be ended by the instruction "Recheck
loop condition" (CONTINUE). The instruction "Exit loop immediately" (EXIT) ends the entire
loop execution. For additional information on this topic, refer to "See also".
You can declare the instruction as follows:
REPEAT <instructions>;
UNTIL <condition> END_REPEAT;
Parameters
The following table shows the parameters of the instruction:
Parameters Data type Memory area Description
Instructions that are executed as long as
the programmed condition has the value
<Instruc-
- FALSE. The instructions are executed
tions>
once, even if the termination condition is
fulfilled.
Expression which is evaluated after each
loop. If the expression has the value
<Condi- FALSE, the program loop is executed once
BOOL I, Q, M, D, L
tion> again. If the expression has the value
TRUE, the program loop continues after
END_REPEAT.
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
SCL
REPEAT "Tag_Result"
:= "Tag_Value";
UNTIL "Tag_Error"
REPEAT: Run if condition is not met (S7-1200, S7-1500)
END_REPEAT;
As long as the value of the operand "Tag_Error" has the signal state "0", the value of the
operand "Tag_Value" is allocated to the operand "Tag_Result".
See also
CONTINUE: Recheck loop condition (S7-1200, S7-1500)
EXIT: Exit loop immediately (S7-1200, S7-1500)
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)