# PRESET_TIMER: Load time duration

**Category:** Timer operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 28  

---

PRESET_TIMER: Load time duration (S7-1200, S7-1500)
PRESET_TIMER: Load time duration
Description
You can use the "Load time duration" instruction to set the time for an IEC timer. The in-
struction is executed in every cycle when the result of logic operation (RLO) at the input of
the instruction has the signal state "1".
You assign an IEC timer declared in the program to the "Load time duration" instruction.
The instruction writes the specified time to the structure of the specified IEC timer.
The instruction does not influence the RLO.
Note
If the specified IEC timer is running while the instruction executes, the instruction over-
writes the current time of the specified IEC timer. This can change the timer status of the
IEC timer.
Updating of the actual values
The instruction data is updated only when the instruction is called and each time the as-
signed IEC timer is accessed. The query on Q or ET (for example, "MyTimer".Q or "MyTim-
er".ET) updates the IEC_TIMER structure.
DANGER
Danger when reinitializing the actual values
Reinitializing the actual values of an IEC timer while the timer is running disrupts the func-
tion of the IEC timer. Changing the actual values can result in inconsistencies between
the program and the actual process. This can cause serious damage to property and per-
sonal injury.
The following functions can cause the actual values to be reinitialized:
• Loading the block with reinitialization
• Loading snapshots as actual values
• Controlling or forcing the actual values
• The "WRIT_DBL" instruction
Before you execute these functions, take the following precautions:
• Make sure that the plant is in a safe state before you overwrite the actual values.
• Make sure that the IEC timer has expired before initializing its actual values.
• If you overwrite the actual values with a snapshot, make sure that the snapshot was
taken at a time when the system was in a safe state.
• Make sure that the program does not read or write the affected data during transmis-
sion.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
PRESET_TIMER: Load time duration (S7-1200, S7-1500)
S7-1200 S7-1500
Duration with
<Time dura- TIME,
Input TIME I, Q, M, D, L which the IEC
tion> LTIME
timer runs.
IEC_TIM-
ER,
IEC_LTI-
MER,
TP_TIME,
IEC_TIM-
TP_LTIME,
ER,
TON_TIME
TP_TIME,
, IEC timer
TON_TIME
TON_LTIM whose dura-
<IEC timer> Output , D, L
E, tion of which
TOF_TIME
TOF_TIME is set.
,
,
TONR_TIM
TOF_LTIM
E
E,
TONR_TIM
E,
TONR_LTI
ME
Example
The following example shows how the instruction works:
SCL
IF #started = false THEN
"TON_DB".TON(IN := "Tag_Start",
PT := "Tag_PresetTime",
Q => "Tag_Status",
ET => "Tag_ElapsedTime");
#started := true;
#preset = true
END_IF;
IF "TON_DB".ET < T#10s AND #preset = true THEN
PRESET_TIMER(PT := T#25s,
TIMER := "TON_DB");
#preset := false;
END_IF;
When the tag #started has the signal state "0" and the operand "Tag_Start" has a positive
signal edge, the instruction "Generate on-delay" is executed. The IEC timer stored in the
instance data block "TON_DB" is started with the time duration that is specified by the op-
erand "Tag_PresetTime". The operand "Tag_Status" is set if the time duration PT specified
by the operand "Tag_PresetTime" has expired. The parameter Q remains set as long as
the operand "Tag_Start" still has the signal state "1". When the signal state of the start in-
put changes from "1" to "0", the operand on the Q parameter is reset.
PRESET_TIMER: Load time duration (S7-1200, S7-1500)
The instruction "Load time duration" is executed when the expired time of the IEC timer
"TON_DB" is less than 10s and the tag #preset has the signal state "1". The instruction
writes the time duration that is specified at the parameter PT in the instance data block
"TON_DB", thereby overwriting the time value of the operand "Tag_PresetTime" within the
instance data block. The signal state of the timer status may therefore change at the next
query or when "TON_DB".Q or "TON_DB".ET are accessed.
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)