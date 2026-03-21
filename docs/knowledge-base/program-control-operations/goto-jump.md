# GOTO: Jump

**Category:** Program control operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 268  

---

GOTO: Jump (S7-1200, S7-1500)
GOTO: Jump
Description
Use the instruction "Jump" to resume the execution of a program at a given point marked
with a jump label.
The jump labels and the instruction "Jump" must be in the same block. The name of a jump
label can only be assigned once within a block. Each jump label can be the target of sever-
al jump instructions.
A jump from the "outside" into a program loop is not permitted, but a jump from a loop to
the "outside" is possible.
Adhere to the following grammatical rules for jump labels:
• Letters (a to z, A to Z)
• A combination of letters and numbers. Check that the order is correct, i.e. first the letters,
then the numbers (a - z, A - Z, 0 - 9).
• Special characters or a combination of letters and numbers must not be used in reverse
order, i.e. first the numbers and then the letters (0-9, a - z, A - Z).
You can declare the instruction as follows:
GOTO <jump label>;
...
<Jump label>: <Instructions>
Parameters
The following table shows the parameters of the instruction:
Parameters Data type Description
<jump label> - Jump label to be jumped to
<instructions> - Instructions which are executed after the jump.
Example
The following example shows how the instruction works:
SCL
CASE "Tag_Value" OF
1 : GOTO MyLABEL1;
2 : GOTO MyLABEL2;
3 : GOTO MyLABEL3;
ELSE GOTO MyLABEL4;
END_CASE;
MyLABEL1: "Tag_1" := 1;
MyLABEL2: "Tag_2" := 1;
MyLABEL3: "Tag_3" := 1;
GOTO: Jump (S7-1200, S7-1500)
MyLABEL4: "Tag_4" := 1;
Depending on the value of the "Tag_Value" operand, the execution of the program will re-
sume at the point identified by the corresponding jump label. If the operand "Tag_Value"
has the value 2, for example, program execution will resume at the jump label "MyLA-
BEL2". The program line identified by the jump label "MyLABEL1" will be skipped in this
case.
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)