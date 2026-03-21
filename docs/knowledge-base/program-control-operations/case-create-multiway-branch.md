# CASE: Create multiway branch

**Category:** Program control operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 256  

---

CASE: Create multiway branch (S7-1200, S7-1500)
CASE: Create multiway branch
Description
The instruction "Create multiway branch" executes one of several instruction sequences
depending on the value of an expression.
The value of the expression must be an integer or a bit string. When the CASE instruction
is executed, the value of the expression (tag) is compared with the values of several con-
stants. If the value of the expression (tag) agrees with the value of a constant the condition
is fulfilled and the instructions that are programmed directly after this constant are execu-
ted. The constants can assume various values.
You can declare the instruction as follows:
CASE <Tag> OF
<Constant1>: <Instructions1>;
<Constant2>: <Instructions2>;
<ConstantX>: <InstructionsX>; // X >= 3
ELSE <Instructions0>;
END_CASE;
Parameters
The following table shows the parameters of the instruction:
Parameters Data type Memory area Description
Bit strings, Value which is compared to the program-
<Tag> I, Q, M, D, L
integers med constant values.
The constants can assume the following
values in case of a bit string:
• A binary number (for example, 2#10)
• An octal number (for example, 8#77)
• A hexadecimal number (for example
16#AD)
<Constant> • An untypified constant (for example,
1000)
Local and
global con- Bit strings - In the case of a typified byte tag, byte con-
stants can stants (for example, BYTE#2) must be pro-
be pro- grammed.
grammed.
In the case of a typified Word tag, byte or
word constants (for example, BYTE#2,
WORD# 2) can be programmed.
In the case of a typified DWord tag, byte,
Word, or DWord constants (for example,
BYTE#2, WORD#2, DWORD#2) can be
programmed.
CASE: Create multiway branch (S7-1200, S7-1500)
In case of a typified LWord tag, byte, Word,
DWord or LWord constants (for example,
BYTE#2, WORD#2, DWORD#2,
LWORD#2) can be programmed.
The constants can assume the following
values in case of an integer:
• An integer (for example, 5)
Integers
• A range of integers (for example 15...20)
• An enumeration consisting of integers
and ranges (for example, 10, 11, 15...20)
Any instruction that is executed when the
value of the expression agrees with the val-
<Instruc- ue of a constant. An exception are instruc-
- -
tion> tions programmed after the ELSE. These
instructions are executed if the values do
not agree.
For additional information on valid data types, refer to "See also".
If the value of the expression agrees with the value of the first constant (<Constant1>), the
instructions (<Instructions1>) which are programmed directly after the first constant are
executed. Program execution subsequently resumes after the END_CASE.
If the value of the expression does not agree with the value of the first constant (<Con-
stant1>), this value is compared to the value of the constant which is programmed next. In
this way, the CASE instruction is executed until the values agree. If the value of the expres-
sion does not correspond to any of the programmed constant values, the instructions (<In-
structions0>) which are programmed after the ELSE are executed. ELSE is an optional
part of the syntax and can be omitted.
The CASE instruction can also be nested by replacing an instruction block with CASE.
END_CASE represents the end of the CASE instruction.
Example
The following example shows how the instruction works:
SCL
CASE "Tag_Value" OF
0 :
"Tag_1" := 1;
1,3,5 :
"Tag_2" := 1;
6...10 :
"Tag_3" := 1;
16,17,20...25 :
"Tag_4" := 1;
ELSE
"Tag_5" := 1;
CASE: Create multiway branch (S7-1200, S7-1500)
END_CASE;
The following table shows how the instruction works using specific operand values:
Operand Values
16, 17, 20,
Tag_Value 0 1, 3 , 5 6, 7, 8, 9, 10 21, 22, 23, 2
24, 25
Tag_1 1 - - - -
Tag_2 - 1 - - -
Tag_3 - - 1 - -
Tag_4 - - - 1 -
Tag_5 - - - - 1
1: The operand is set to the signal state "1".
-: The signal state of the operand remains unaltered.
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)