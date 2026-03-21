# RESET_TIMER: Reset timer

**Category:** Timer operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 26  

---

RESET_TIMER: Reset timer (S7-1200, S7-1500)
RESET_TIMER: Reset timer
Description
You can use the "Reset timer" instruction to reset an IEC timer to "0". The structure compo-
nents of the timer in the specified data block are reset to "0".
Updating of the actual values
The instruction does not influence the RLO. At the TIMER parameter, the "Reset timer" in-
struction is assigned an IEC timer declared in the program. The instruction must be pro-
grammed in an IF instruction. The instruction data is updated only when the instruction is
called and not each time the assigned IEC timer is accessed. Querying the data is only
identical from the call of the instruction to the next call of the instruction.
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
S7-1200 S7-1500
IEC_TIM-
IEC_TIM-
ER,
ER,
IEC_LTI-
TP_TIME,
MER, IEC timer that
<IEC timer> Output TON_TIME D, L
TP_TIME, is reset
,
TP_LTIME,
TOF_TIME
TON_TIME
,
,
RESET_TIMER: Reset timer (S7-1200, S7-1500)
TON_LTIM
E,
TOF_TIME
,
TONR_TIM TOF_LTIM
E E,
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
END_IF;
IF "TON_DB".ET < T#25s THEN
RESET_TIMER(TIMER := "TON_DB");
#started := false;
END_IF;
When the tag #started has the signal state "0", the instruction "Generate on-delay" is exe-
cuted when there is positive signal edge on the operand "Tag_Start". The IEC timer stored
in the instance data block "TON_DB" is started with the time duration that is specified by
the operand "Tag_PresetTime". The operand "Tag_Status" is set if the time duration speci-
fied by the operand "Tag_PresetTime" has expired. The parameter Q remains set as long
as the operand "Tag_Start" still has the signal state "1". When the signal state of the start
input changes from "1" to "0", the operand on the Q parameter is reset.
If the expired time of the IEC timer "TON_DB" is less than 25s, the "Reset timer" instruction
is executed and the timer stored in the "TON_DB" instance data block is reset.
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)