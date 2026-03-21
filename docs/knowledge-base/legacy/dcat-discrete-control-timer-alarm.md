# DCAT: Discrete control-timer alarm

**Category:** Legacy  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 330  

---

DCAT: Discrete control-timer alarm (S7-1500)
DCAT: Discrete control-timer alarm
Description
You can use the "Discrete control-timer alarm" to count the time from the point at which the
CMD parameter issues the command to open or close. The time is accumulated until the
preset time (PT) is exceeded or the information is received that the device was opened or
closed (O_FB or C_FB) within the specified time. If the preset time is exceeded before the
information on the opening or closing of the device is received, the corresponding alarm is
activated. If the signal state of the command input changes before the preset time, the time
is restarted.
The "Discrete control-timer alarm" instruction has the following reactions to the input condi-
tions:
• When the signal state of the CMD parameter changes from "0" to "1", the signal states of
the parameters Q, CMD_HIS, ET (only if ET is < PT) OA and CA are influenced as fol-
lows:
o The Q and CMD_HIS parameters are set to "1".
o The ET, OA, and CA parameters are reset to "0".
• When the signal state on the parameter CMD changes from "1" to "0", the parameters Q,
ET (only if ET < PT), OA, CA and CMD_HIS are reset to "0".
• When the signal state of the parameters CMD and CMD_HIS is "1" and the parameter
O_FB is set to "0", the time difference (ms) since the last execution of the instruction is
added to the value at the parameter ET. If the value of the ET parameter exceeds the
value of the PT parameter, the signal state at the OA parameter is set to "1". If the value
of the ET parameter does not exceed the value of the PT parameter, the signal state of
the OA parameter is reset to "0". The value at the parameter CMD_HIS is reset to the
value of the parameter CMD.
• When the signal state of the parameters CMD, CMD_HIS and O_FB are set to "1" and
the parameter C_FB has the value "0", the signal state of the parameter OA is set to "0".
The value of the ET parameter is set to the value of the PT parameter. If the signal state
of the parameter O_FB changes to "0", the alarm is set the next time the instruction is
executed. The value of the CMD_HIS parameter is set to the value of the CMD parame-
ter.
• If the parameters CMD, CMD_HIS and C_FB have the value "0", the time difference (ms)
since the last execution of the instruction is added to the value of the parameter ET. If
the value of the ET parameter exceeds the value of the PT parameter, the signal state of
the parameter CA is reset to "1". If the value at the parameter PT is not exceeded, the
parameter CA has the signal state "0". The value of the CMD_HIS parameter is set to
the value of the CMD parameter.
• If the parameters CMD, CMD_HIS and O_FB have the signal state "0" and the parame-
ter C_FB is set to "1", the parameter CA is set to "0". The value of the ET parameter is
set to the value of the PT parameter. If the signal state of the C_FB parameter changes
to "0", the alarm is set the next time the instruction is executed. The value of the
CMD_HIS parameter is set to the value of the CMD parameter.
• If the parameters O_FB and C_FB simultaneously have the signal state "1", the signal
states of both alarm outputs are set to "1".
The "Discrete control-timer alarm" instruction has no error information.
Parameters
The following table shows the parameters of the instruction:
DCAT: Discrete control-timer alarm (S7-1500)
Parameters Declaration Data type Memory area Description
A signal state of "0" indicates
a "close" command.
CMD Input BOOL I, Q, M, D, L
A signal state of "1" indicates
an "open" command.
Feedback input when open-
O_FB Input BOOL I, Q, M, D, L
ing
C_FB Input BOOL I, Q, M, D, L Feedback input when closing
Shows the status of the pa-
Q Output BOOL I, Q, M, D, L
rameter CMD
OA Output BOOL I, Q, M, D, L Alarm output when opening
CA Output BOOL I, Q, M, D, L Alarm output when closing
Currently elapsed time,
ET Static DINT D, L
where one count = 1 ms.
Preset timer value, where
PT Static DINT D, L
one clock pulse = 1 ms.
PREV_TIME Static DWORD D, L Previous system time
CMD_HIS Static BOOL D, L CMD history bit
The static parameters are not visible when calling the instruction in the program. These are
saved in the instance of the instruction.
Example
In the following example the parameter CMD changes from "0" to "1". After the execution of
the instruction the parameter Q is set to "1" and the two alarm outputs OA and CA have the
signal state "0". The CMD_HIS parameter of the instance data block is set to the signal
state "1" and the ET parameter is reset to "0".
Note
You can initialize static parameters in the data block.
SCL
"DCAT_DB"(CMD := "Tag_Input_CMD",
O_FB := "Tag_Input_O_FB",
C_FB := "Tag_Input_C_FB",
Q => "Tag_Output_Q",
OA => "Tag_Output_OA",
CA => "Tag_Output_CA");
The following tables show how the instruction works using specific values.
Before processing
In this example, the following values are used for the input and output parameters:
Parameters Operand Value
DCAT: Discrete control-timer alarm (S7-1500)
CMD Tag_Input_CMD TRUE
O_FB Tag_Input_O_FB FALSE
C_FB Tag_Input_C_FB FALSE
Q Tag_Output_Q FALSE
OA Tag_Output_OA FALSE
CA Tag_Output_CA FALSE
The following values are saved in the instance data block "DCAT_DB" of the instruction:
Parameters Address Value
ET DBD4 L#12
PT DBD8 L#222
CMD_HIS DBX16.0 FALSE
After processing
The following values are written to the output parameters after the instruction has been
executed:
Parameters Operand Value
Q Tag_Output_Q TRUE
OA Tag_Output_OA FALSE
CA Tag_Output_CA FALSE
The following values are saved in the instance data block "DCAT_DB" of the instruction:
Parameters Address Value
ET DBD4 L#0
CMD_HIS DBX16.0 TRUE
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL