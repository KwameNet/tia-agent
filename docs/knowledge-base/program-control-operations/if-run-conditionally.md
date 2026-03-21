# IF: Run conditionally

**Category:** Program control operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 254  

---

IF: Run conditionally (S7-1200, S7-1500)
IF: Run conditionally
Description
The instruction "Run conditionally" branches the program flow depending on a condition.
The condition is an expression with Boolean value (TRUE or FALSE). Logical expression
or comparative expressions can be stated as conditions.
When the instruction is executed, the stated expressions are evaluated. If the value of an
expression is TRUE, the condition is fulfilled; if the value is FALSE, it is not fulfilled.
Parameters
Depending on the type of branch, you can program the following forms of the instruction:
• Branch through IF:
IF <condition> THEN <instructions>
END_IF;
If the condition is satisfied, the instructions programmed after the THEN are executed. If
the condition is not satisfied, the execution of the program continues with the next instruc-
tion after the END_IF.
• Branch through IF and ELSE:
IF <condition> THEN <instructions1>
ELSE <Instructions0>
END_IF;
If the condition is satisfied, the instructions programmed after the THEN are executed. If
the condition is not satisfied, the instructions programmed after the ELSE are executed.
Then the execution of the program continues with the next instruction after the END_IF.
• Branch through IF, ELSIF and ELSE:
IF <condition1> THEN <instructions1>
ELSIF <condition2> THEN <instruction2>
ELSE <Instructions0>
END_IF;
If the first condition (<Condition1>) is satisfied, the instructions (<Instructions1>) after the
THEN are executed. After execution of the instructions, the execution of the program con-
tinues after the END_IF.
If the first condition is not satisfied, the second condition (<Condition2>) is checked. If the
second condition (<Condition2>) is fulfilled, the instructions (<Instructions2>) after the
THEN are executed. After execution of the instructions, the execution of the program con-
tinues after the END_IF.
If none of the conditions are fulfilled, the instructions (<Instructions0>) after ELSE are exe-
cuted followed by the execution of the program after END_IF.
You can nest as many combinations of ELSIF and THEN as you like within the IF instruc-
tion. The programming of an ELSE branch is optional.
The syntax of the IF instruction consists of the following parts:
IF: Run conditionally (S7-1200, S7-1500)
Parameters Data type Memory area Description
<Condi-
BOOL I, Q, M, D, L Expression to be evaluated
tion>
Instructions to be executed with satisfied
condition. An exception are instructions pro-
<Instruc-
- - grammed after the ELSE. These are execu-
tions>
ted if no condition within the program loop
is satisfied.
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
SCL
IF "Tag_1" = 1
THEN "Tag_Value" := 10;
ELSIF "Tag_2" = 1
THEN "Tag_Value" := 20;
ELSIF "Tag_3" = 1
THEN "Tag_Value" := 30;
ELSE "Tag_Value" := 0;
END_IF;
The following table shows how the instruction works using specific operand values:
Operand Value
Tag_1 1 0 0 0
Tag_2 0 1 0 0
Tag_3 0 0 1 0
Tag_Value 10 20 30 0
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)