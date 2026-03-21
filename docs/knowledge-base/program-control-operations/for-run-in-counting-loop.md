# FOR: Run in counting loop

**Category:** Program control operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 259  

---

FOR: Run in counting loop (S7-1200, S7-1500)
FOR: Run in counting loop
Description
The instruction "Run in counting loop" causes repeated execution of a program loop until a
loop variable lies within a specified value range.
Program loops can also be nested. Within a program loop, you can program additional pro-
gram loops with other loop variables.
The current continuous run of a program loop can be ended by the instruction "Recheck
loop condition" (CONTINUE). The instruction "Exit loop immediately" (EXIT) ends the entire
loop execution. For additional information on this topic, refer to "See also".
Note
Information on the number of runs and the run tag
The number of runs cannot be changed while the program is running.
For performance reasons, the run tag should be declared in the block interface in the
"Temp" section. It is not possible to change the run tag from within the loop.
The following example program would therefore generate a syntax error and cannot be
compiled:
FOR #i := 1 TO 10 DO
#i := #i + 1;
END_FOR;
Limits for FOR statements
To program "safe" FOR statements that do not run endlessly, observe the following rules
and limits:
FOR <Run_tag> := <Start_value> TO <End_value> BY <Increment> DO <Instructions>;
END_FOR;
If ... ... then Note
End value < (PMAX incre- Run tag runs in positive di-
Start value < End value
ment) rection
Start value > End value AND End value > (NMAX incre- Run tag runs in negative di-
increment < O ment) rection
Note
Using the data types and direction of movement
A combination of signed and unsigned integers in a FOR statements is not possible.
When using unsigned integers it is not possible to program a loop with a negative direc-
tion.
Limits
Different limits apply to the possible data types:
FOR: Run in counting loop (S7-1200, S7-1500)
Data type PMAX NMAX
Run tag of type SINT 127 -128
Run tag of type INT 32767 -32768
Run tag of type DINT 2147483647 -2147483648
Run tag of type LINT 9223372036854775807 -9223372036854775808
Run tag of type USINT 255 -
Run tag of type UINT 65535 -
Run tag of type UDINT 4294967295 -
Run tag of type ULINT 18446744073709551615 -
Parameters
The following table shows the parameters of the instruction:
Parameters Data type Memory area Description
S7-1200 S7-1500
SINT, INT, SINT, INT, Operand whose value is evalu-
DINT, DINT, LINT, ated with the loop execution. The
<Run tag> USINT, USINT, UINT, I, Q, M, D, L data type of the loop variable de-
UINT, UDINT, termines the data type of the oth-
UDINT ULINT er parameters.
SINT, INT, SINT, INT,
DINT, DINT, LINT, Expression whose value is allo-
<Start val-
USINT, USINT, UINT, I, Q, M, D, L cated at the start of the loop exe-
ue>
UINT, UDINT, cution of the loop variables.
UDINT ULINT
Expression whose value defines
the last run of the program loop.
The value of the loop variable is
checked before each loop:
• End value not reached:
The instructions according to
SINT, INT, SINT, INT,
DO are executed
DINT, DINT, LINT,
<End val- USINT, USINT, UINT, I, Q, M, D, L • End value is reached:
ue>
UINT, UDINT, The FOR loop is executed one
UDINT ULINT last time
• End value exceeded:
The FOR loop is completed
An alteration to the end value is
not permitted during execution of
the instruction.
Expression by whose value the
SINT, INT, SINT, INT, loop variable is increased (posi-
DINT, DINT, LINT, tive increment) or decreased
<Incre-
USINT, USINT, UINT, I, Q, M, D, L (negative increment) after each
ment>
UINT, UDINT, loop. Specification of the incre-
UDINT ULINT ment is optional. If no increment
is given, the value of the loop
FOR: Run in counting loop (S7-1200, S7-1500)
variable is increased by 1 after
each loop.
An alteration of the increment is
not permitted during execution of
the instruction.
Instructions which are carried out
with each loop, as long as the
<Instruc- value of the loop variable lies
- -
tions> within the value range. The value
range is defined by the start and
end values.
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
SCL
FOR i := 2 TO 8 BY 2
DO "a_array[i] := "Tag_Value"*"b_array[i]";
END_FOR;
The operand "Tag_Value" is multiplied with the elements (2, 4, 6, 8) of the ARRAY tag
"b_array". The result is read in to the elements (2, 4, 6, 8) of the ARRAY tag "a_array".
See also
CONTINUE: Recheck loop condition (S7-1200, S7-1500)
EXIT: Exit loop immediately (S7-1200, S7-1500)
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)