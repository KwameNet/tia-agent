# DRUM: Implement sequencer

**Category:** Legacy  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 323  

---

DRUM: Implement sequencer (S7-1500)
DRUM: Implement sequencer
Description
You can use the "Implement sequencer" instruction to assign the programmed values of
the OUT_VAL parameter of the corresponding step to the programmed output bits (OUT1
to OUT16) and the output word (OUT_WORD). The specific step must thereby satisfy the
conditions of the programmed enable mask on the S_MASK parameter while the instruc-
tion remains at this step. The instruction advances to the next step if the event for the step
is true and the programmed time for the current step elapses, or if the value at the JOG
parameter changes from "0" to "1". The instruction is reset when the signal state of the RE-
SET parameter changes to "1". The current step is hereby equated to the preset step
(DSP).
The amount of time spent on a step is determined by the product of the preset timebase
(DTBP) and the preset counter value (S_PRESET) for each step. At the start of a new
step, this calculated value is loaded into the DCC parameter, which contains the time re-
maining for the current step. If, for example the value at the DTBP parameter is "2" and the
preset value for the first step is "100" (100 ms), the DCC parameter has the value "200"
(200 ms).
A step can be programmed with a timer value, an event, or both. Steps that have an event
bit and the timer value "0" advance to the next step as soon as the signal state of the event
bit is "1". Steps that are programmed only with a timer value start the time immediately.
Steps that are programmed with an event bit and a time value greater than "0" start the
time when the signal state of the event bit is "1". The event bits are initialized with a signal
state of "1".
When the sequencer is on the last programmed step (LST_STEP) and the time for this
step has expired, the signal state on the Q parameter is set to "1"; otherwise it is set to "0".
When the parameter Q is set, the instruction remains on the step until it is reset.
In the configurable mask (S_MASK) you can select the individual bits in the output word
(OUT_WORD) and set or reset the output bits (OUT1 to OUT16) by means of the output
values (OUT_VAL). When a bit of the configurable mask has a signal state of "1", the
OUT_VAL value sets/resets the corresponding bit. If the signal state of a bit of the configu-
rable mask is "0", the corresponding bit is left unchanged. All the bits of the configurable
mask for all 16 steps are initialized with a signal state of "1".
The output bit on the OUT1 parameter corresponds to the least significant bit of the output
word (OUT_WORD). The output bit on the OUT16 parameter corresponds to the most sig-
nificant bit of the output word (OUT_WORD).
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
The signal state "1" indi-
RESET Input BOOL I, Q, M, D, L
cates a reset condition.
When the signal state
changes from "0" to "1",
JOG Input BOOL I, Q, M, D, L
the instruction advances
to the next step.
DRUM: Implement sequencer (S7-1500)
The signal state "1" al-
lows the sequencer to
DRUM_EN Input BOOL I, Q, M, D, L
advance based on the
event and time criteria.
Maximum step number
(for example: LST_STEP
LST_STEP Input BYTE I, Q, M, D, L
= 16#08; a maximum of 8
steps is possible.)
EVENT(i), Event bit (i);
Input BOOL I, Q, M, D, L
1 ≤ i ≤ 16 Initial signal state is "1".
OUT(j),
Output BOOL I, Q, M, D, L Output bit (j)
1 ≤ j ≤ 16
The signal state "1" indi-
Q Output BOOL I, Q, M, D, L cates that the time for the
last step has elapsed.
Word address to which
OUT_WOR
Output WORD I, Q, M, D, L, P the sequencer writes the
D
output values.
ERR_COD
Output WORD I, Q, M, D, L, P Error information
E
JOG parameter history
JOG_HIS Static BOOL I, Q, M, D, L
bit
The signal state "1" indi-
EOD Static BOOL I, Q, M, D, L cates that the time for the
last step has elapsed.
Preset step of the se-
DSP Static BYTE I, Q, M, D, L, P
quencer
Current step of the se-
DSC Static BYTE I, Q, M, D, L, P
quencer
Current counter value of
DCC Static DWORD I, Q, M, D, L, P
the sequencer
Preset timebase of the
DTBP Static WORD I, Q, M, D, L, P
sequencer
PrevTime Static TIME I, Q, M, D, L Previous system time
Preset counter value for
ARRAY[1..16] of
S_PRESET Static I, Q, M, D, L each step [1 to 16] where
WORD
one clock pulse = 1 ms.
ARRAY[1..16, Output values for each
OUT_VAL Static I, Q, M, D, L
0..15] of BOOL step [1 to 16, 0 to 15].
Configurable mask for
ARRAY[1..16, each step [1 to 16,
S_MASK Static I, Q, M, D, L
0..15] of BOOL 0 to 15]. Initial signal
states are "1".
For additional information on valid data types, refer to "See also".
ERR_CODE parameter
The following table shows the meaning of the values of the ERR_CODE parameter:
DRUM: Implement sequencer (S7-1500)
ERR_CODE* Explanation
W#16#00
No error
00
W#16#00
The value at the LST_STEP parameter is less than 1 or greater than 16.
0B
W#16#00 The value at the DSC parameter is less than 1 or greater than the value of
0C the LST_STEP parameter.
W#16#00 The value at the DSP parameter is less than 1 or greater than the value of the
0D LST_STEP parameter.
*The error codes can be displayed as integer or hexadecimal value in the program editor.
For information on toggling display formats, refer to "See also".
Example
In the following example the instruction advances from step 1 to step 2. The output bits
(OUT1 to OUT16) and the output word (OUT_WORD) are set according to the mask con-
figured for step 2 and the values of the OUT_VAL parameter.
Note
You can initialize static parameters in the data block.
SCL
"DRUM_DB"(RESET := "Tag_Reset"
JOG := "Tag_Input_Jog"
DRUM_EN := "Tag_Input_DrumEN"
LST_STEP := "Tag_Number_LastStep"
EVENT1 := "MyTag_Event_1"
EVENT2 := "MyTag_Event_2"
EVENT3 := "MyTag_Event_3"
EVENT4 := "MyTag_Event_4"
EVENT5 := "MyTag_Event_5"
EVENT6 := "MyTag_Event_6"
EVENT7 := "MyTag_Event_7"
EVENT8 := "MyTag_Event_8"
EVENT9 := "MyTag_Event_9"
EVENT10 := "MyTag_Event_10"
EVENT11 := "MyTag_Event_11"
EVENT12 := "MyTag_Event_12"
EVENT13 := "MyTag_Event_13"
EVENT14 := "MyTag_Event_14"
EVENT15 := "MyTag_Event_15"
EVENT16 := "MyTag_Event_16"
OUT1 => "MyTag_Output_1"
OUT2 => "MyTag_Output_2"
OUT3 => "MyTag_Output_3"
DRUM: Implement sequencer (S7-1500)
OUT4 => "MyTag_Output_4"
OUT5 => "MyTag_Output_5"
OUT6 => "MyTag_Output_6"
OUT7 => "MyTag_Output_7"
OUT8 => "MyTag_Output_8"
OUT9 => "MyTag_Output_9"
OUT10 => "MyTag_Output_10"
OUT11 => "MyTag_Output_11"
OUT12 => "MyTag_Output_12"
OUT13 => "MyTag_Output_13"
OUT14 => "MyTag_Output_14"
OUT15 => "MyTag_Output_15"
OUT16 => "MyTag_Output_16"
Q => "Tag_Output_Q"
OUT_WORD => "Tag_OutputWord"
ERR_CODE => "Tag_ErrorCode");
The following tables show how the instruction works using specific values.
Before processing
In this example, the following values are used for initializing the input parameters:
Parameters Operand Address Value
RESET Tag_Reset M0.0 FALSE
JOG Tag_Input_JOG M0.1 FALSE
DRUM_EN Tag_Input_DrumEN M0.2 TRUE
Tag_Number_Last-
LST_STEP MB1 B#16#08
Step
EVENT2 MyTag_Event_2 M20.0 FALSE
EVENT4 MyTag_Event_4 M20.1 FALSE
EVENT6 MyTag_Event_6 M20.2 FALSE
EVENT8 MyTag_Event_8 M20.3 FALSE
EVENT10 MyTag_Event_10 M20.4 FALSE
EVENT12 MyTag_Event_12 M20.5 FALSE
EVENT14 MyTag_Event_14 M20.6 FALSE
EVENT16 MyTag_Event_16 M20.7 FALSE
The following values are saved in the "DRUM_DB" instance data block of the instruction:
Parameters Address Value
JOG_HIS DBX12.0 FALSE
EOD DBX12.1 FALSE
DSP DBB13 W#16#0001
DSC DBB14 W#16#0001
DRUM: Implement sequencer (S7-1500)
DCC DBD16 DW#16#0000000A
DTBP DBW20 W#16#0001
S_PRESET[1] DBW26 W#16#0064
S_PRESET[2] DBW28 W#16#00C8
OUT_VAL[1,0] DBX58.0 TRUE
OUT_VAL[1,1] DBX58.1 TRUE
OUT_VAL[1,2] DBX58.2 TRUE
OUT_VAL[1,3] DBX58.3 TRUE
OUT_VAL[1,4] DBX58.4 TRUE
OUT_VAL[1,5] DBX58.5 TRUE
OUT_VAL[1,6] DBX58.6 TRUE
OUT_VAL[1,7] DBX58.7 TRUE
OUT_VAL[1,8] DBX59.0 TRUE
OUT_VAL[1,9] DBX59.1 TRUE
OUT_VAL[1,10] DBX59.2 TRUE
OUT_VAL[1,11] DBX59.3 TRUE
OUT_VAL[1,12] DBX59.4 TRUE
OUT_VAL[1,13] DBX59.5 TRUE
OUT_VAL[1,14] DBX59.6 TRUE
OUT_VAL[1,15] DBX59.7 TRUE
OUT_VAL[2,0] DBX60.0 FALSE
OUT_VAL[2,1] DBX60.1 FALSE
OUT_VAL[2,2] DBX60.2 FALSE
OUT_VAL[2,3] DBX60.3 FALSE
OUT_VAL[2,4] DBX60.4 FALSE
OUT_VAL[2,5] DBX60.5 FALSE
OUT_VAL[2,6] DBX60.6 FALSE
OUT_VAL[2,7] DBX60.7 FALSE
OUT_VAL[2,8] DBX61.0 FALSE
OUT_VAL[2,9] DBX61.1 FALSE
OUT_VAL[2,10] DBX61.2 FALSE
OUT_VAL[2,11] DBX61.3 FALSE
OUT_VAL[2,12] DBX61.4 FALSE
OUT_VAL[2,13] DBX61.5 FALSE
OUT_VAL[2,14] DBX61.6 FALSE
OUT_VAL[2,15] DBX61.7 FALSE
S_MASK[2,0] DBX92.0 FALSE
S_MASK[2,1] DBX92.1 TRUE
S_MASK[2,2] DBX92.2 TRUE
S_MASK[2,3] DBX92.3 TRUE
DRUM: Implement sequencer (S7-1500)
S_MASK[2,4] DBX92.4 TRUE
S_MASK[2,5] DBX92.5 FALSE
S_MASK[2,6] DBX92.6 TRUE
S_MASK[2,7] DBX92.7 TRUE
S_MASK[2,8] DBX93.0 FALSE
S_MASK[2,9] DBX93.1 FALSE
S_MASK[2,10] DBX93.2 TRUE
S_MASK[2,11] DBX93.3 TRUE
S_MASK[2,12] DBX93.4 TRUE
S_MASK[2,13] DBX93.5 TRUE
S_MASK[2,14] DBX93.6 FALSE
S_MASK[2,15] DBX93.7 TRUE
The output parameters are set to the following values before the execution of the instruc-
tion:
Parameters Operand Address Value
Q Tag_Output_Q M6.0 FALSE
OUTWORD Tag_OutputWord MW8 W#16#FFFF
OUT1 MyTag_Output_1 M4.0 TRUE
OUT2 MyTag_Output_2 M4.1 TRUE
OUT3 MyTag_Output_3 M4.2 TRUE
OUT4 MyTag_Output_4 M4.3 TRUE
OUT5 MyTag_Output_5 M4.4 TRUE
OUT6 MyTag_Output_6 M4.5 TRUE
OUT7 MyTag_Output_7 M4.6 TRUE
OUT8 MyTag_Output_8 M4.7 TRUE
OUT9 MyTag_Output_9 M5.0 TRUE
OUT10 MyTag_Output_10 M5.1 TRUE
OUT11 MyTag_Output_11 M5.2 TRUE
OUT12 MyTag_Output_12 M5.3 TRUE
OUT13 MyTag_Output_13 M5.4 TRUE
OUT14 MyTag_Output_14 M5.5 TRUE
OUT15 MyTag_Output_15 M5.6 TRUE
OUT16 MyTag_Output_16 M5.7 TRUE
After processing
The following values are written to the output parameters after the instruction has been
executed:
Parameters Operand Address Value
OUT1 MyTag_Output_1 M4.0 TRUE
DRUM: Implement sequencer (S7-1500)
OUT2 MyTag_Output_2 M4.1 FALSE
OUT3 MyTag_Output_3 M4.2 FALSE
OUT4 MyTag_Output_4 M4.3 FALSE
OUT5 MyTag_Output_5 M4.4 FALSE
OUT6 MyTag_Output_6 M4.5 TRUE
OUT7 MyTag_Output_7 M4.6 FALSE
OUT8 MyTag_Output_8 M4.7 FALSE
OUT9 MyTag_Output_9 M5.0 TRUE
OUT10 MyTag_Output_10 M5.1 TRUE
OUT11 MyTag_Output_11 M5.2 FALSE
OUT12 MyTag_Output_12 M5.3 FALSE
OUT13 MyTag_Output_13 M5.4 FALSE
OUT14 MyTag_Output_14 M5.5 FALSE
OUT15 MyTag_Output_15 M5.6 TRUE
OUT16 MyTag_Output_16 M5.7 FALSE
Q Tag_Output_Q M6.0 FALSE
OUTWORD Tag_OutputWord MW8 W#16#4321
ERR_CODE Tag_ErrorCode MW10 W#16#0000
The following values are changed in the instance data block "DRUM_DB" of the instruction
after the execution of the instruction:
Parameters Address Value
JOG_HIS DBX12.0 FALSE
EOD DBX12.1 FALSE
DSC DBB14 W#16#0002
DCC DBD16 DW#16#000000C8
See also
Overview of the valid data types
Switching display formats in the program status
Memory areas (S7-1500)
Basics of SCL