# MCAT: Motor control-timer alarm

**Category:** Legacy  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 333  

---

MCAT: Motor control-timer alarm (S7-1500)
MCAT: Motor control-timer alarm
Description
The "Motor control-timer alarm" instruction is used to accumulate the time from the point at
which one of the command inputs (opening or closing) is switched on. The time is accumu-
lated until the preset time is exceeded or the relevant feedback input indicates that the de-
vice has executed the requested operation within the specified time. If the preset time is
exceeded before the feedback is received, the corresponding alarm is triggered.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory space Description
O_CMD Input BOOL I, Q, M, D, L "Open" command input
C_CMD Input BOOL I, Q, M, D, L "Close" command input
S_CMD Input BOOL I, Q, M, D, L "Stop" command input
Feedback input when
O_FB Input BOOL I, Q, M, D, L
opening
Feedback input when
C_FB Input BOOL I, Q, M, D, L
closing
OO Output BOOL I, Q, M, D, L "Open" output
CO Output BOOL I, Q, M, D, L "Close" output
Alarm output when open-
OA Output BOOL I, Q, M, D, L
ing
Alarm output when clos-
CA Output BOOL I, Q, M, D, L
ing
A signal state of "0" indi-
Q Output BOOL I, Q, M, D, L
cates an error condition.
Currently elapsed time,
ET Static DINT D, L
where one count = 1 ms
Preset timer value, where
PT Static DINT D, L
one clock pulse = 1 ms
PREV_TIM
Static DWORD D, L Previous system time
E
O_HIS Static BOOL D, L "Open" history bit
C_HIS Static BOOL D, L "Close" history bit
The static parameters are not visible when calling the instruction in the program. These are
saved in the instance of the instruction.
Execution of the "Motor control-timer alarm" instruction
The following table shows the reactions of the "Motor control-timer alarm" instruction to the
various input conditions:
Input parameters Output parameters
MCAT: Motor control-timer alarm (S7-1500)
O_ C_ O_ C_ S_ O_ C_
O_ C_
ET HI HI CM CM CM OO CO OA CA ET HI HI Q Status
FB FB
S S D D D S S
X 1 1 X X X X X 0 0 1 1 PT 0 0 0 Alarm
X X X X X X 1 1 0 0 1 1 PT 0 0 0 Alarm
X X X X X 1 X X 0 0 0 0 X 0 0 1 Stop
X X X 1 1 X X X 0 0 0 0 X 0 0 1 Stop
Start
X 0 X 1 0 0 X X 1 0 0 0 0 1 0 1
opening
<P IN
1 0 X 0 0 0 X 1 0 0 0 1 0 1 Open
T C
X 1 0 X 0 0 1 0 0 0 0 0 PT 1 0 1 Opened
>= Opening
1 0 X 0 0 0 X 0 0 1 0 PT 1 0 0
PT alarm
Start
X X 0 0 1 0 X X 0 1 0 0 0 0 1 1
closing
<P IN
0 1 0 X 0 X 0 0 1 0 0 0 1 1 Close
T C
X 0 1 0 X 0 0 1 0 0 0 0 PT 0 1 1 Closed
>= Closing
0 1 0 X 0 X 0 0 0 0 1 PT 0 1 0
PT alarm
X 0 0 0 0 0 X X 0 0 0 0 X 0 0 1 Stopped
Legend:
INC Add the time difference (ms) since the last processing of the FB to ET
PT PT is set to the same value as ET
X Cannot be used
<PT ET < PT
>=PT ET >= PT
If the input parameters O_HIS and C_HIS both have the signal state "1", they are immedi-
ately set to signal state "0". In this case, the last row in the table (X) mentioned above is
valid. Because it is therefore not possible to check whether the input parameters O_HIS
and C_HIS have the signal state "1", the output parameters are set as follows in this
case:
OO = FALSE
CO = FALSE
OA = FALSE
CA = FALSE
ET = PT
Q = TRUE
Example
The following example shows how the instruction works:
MCAT: Motor control-timer alarm (S7-1500)
Note
You can initialize static parameters in the data block.
SCL
"MCAT_DB"(O_CMD := "Tag_Iput_O_CMD",
C_CMD := "Tag_Input_C_CMD",
S_CMD := "Tag_Input_S_CMD",
O_FB := "Tag_Input_O_FB",
C_FB := "Tag_Input_C_FB",
OO => "Tag_OutputOpen",
CO => "Tag_OutputClosed",
OA => "Tag_Output_OA",
CA => "Tag_Output_CA",
Q => "Tag_Output_Q");
The following tables show how the instruction works using specific values.
Before processing
In this example, the following values are used for the input and output parameters:
Parameters Operand Value
O_CMD Tag_Input_O_CMD TRUE
C_CMD Tag_Input_C_CMD FALSE
S_CMD Tag_Input_S_CMD FALSE
O_FB Tag_Input_O_FB FALSE
C_FB Tag_Input_C_FB FALSE
OO Tag_OutputOpen FALSE
CO Tag_OutputClosed FALSE
OA Tag_Output_OA FALSE
CA Tag_Output_CA FALSE
Q Tag_Output_Q FALSE
The following values are saved in the instance data block "MCAT_DB" of the instruction:
Parameters Address Value
ET DBD4 L#2
PT DBD8 L#22
O_HIS DBX16.0 TRUE
C_HIS DBX16.1 FALSE
After processing
MCAT: Motor control-timer alarm (S7-1500)
The following values are written to the output parameters after the instruction has been
executed:
Parameters Operand Value
OO Tag_OutputOpen TRUE
CO Tag_OutputClosed FALSE
OA Tag_Output_OA FALSE
CA Tag_Output_CA FALSE
Q Tag_Output_Q TRUE
The following values are saved in the instance data block "MCAT_DB" of the instruction:
Parameters Address Value
ET DBD4 L#0
O_HIS DBX16.0 TRUE
CMD_HIS DBX16.1 FALSE
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL