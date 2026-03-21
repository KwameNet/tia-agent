# WHILE: Run if condition is met

**Category:** Program control operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 262  

---

WHILE: Run if condition is met (S7-1200, S7-1500)
WHILE: Run if condition is met
Description
The instruction "Run if condition is met" causes a program loop to be repeatedly executed
as long as the implementation condition is satisfied. The condition is an expression with
Boolean value (TRUE or FALSE). Logical expression or comparative expressions can be
stated as conditions.
When the instruction is executed, the stated expressions are evaluated. If the value of an
expression is TRUE, the condition is satisfied; if the value is FALSE, it is not satisfied.
Program loops can also be nested. Within a program loop, you can program additional pro-
gram loops with other loop variables.
The current continuous run of a program loop can be ended by the instruction "Recheck
loop condition" (CONTINUE). The instruction "Exit loop immediately" (EXIT) ends the entire
loop execution. For additional information on this topic, refer to "See also".
You can declare the instruction as follows:
WHILE <Condition> DO <Instructions>;
END_WHILE;
Parameters
The following table shows the parameters of the instruction:
Parameters Data type Memory area Description
<Condi- Expression which is evaluated before each
BOOL I, Q, M, D, L
tion> loop.
Instructions to be executed with satisfied
<Instruc- condition. If the condition has not been sat-
-
tions> isfied, program execution continues after
END_WHILE.
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
SCL
WHILE
"Tag_Value1" <> "Tag_Value2"
DO "Tag_Result"
:= "Tag_Input";
END_WHILE;
As long as the values of the operands "Tag_Value1" and "Tag_Value2" do not match, the
value of the operand "Tag_Input" is allocated to the operand "Tag_Result".
WHILE: Run if condition is met (S7-1200, S7-1500)
See also
EXIT: Exit loop immediately (S7-1200, S7-1500)
CONTINUE: Recheck loop condition (S7-1200, S7-1500)
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)