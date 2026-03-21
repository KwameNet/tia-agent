# ENDIS_PW: Limit and enable password legitimation

**Category:** Runtime control  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 276  

---

ENDIS_PW: Limit and enable password legitimation (S7-1200, S7-1500)
ENDIS_PW: Limit and enable password legitimation
Description
You use the "Limit and enable password legitimation" instruction to specify whether config-
ured passwords may be legitimated or not for the CPU. In this way, you can prevent legiti-
mized connections, even if the correct password is known.
When you call the instruction and the REQ parameter has the signal state "0", the currently
set state is displayed at the output parameters. If changes are made to the input parame-
ters, these changes are not transferred to the output parameters.
If the instruction is called and the REQ parameter has the signal state "1", the signal state
is taken from the input parameters (F_PWD, FULL_PWD, R_PWD, HMI_PWD).
• If signal state "0" is present, authorization via password is not allowed.
• If signal state "1" is present, the password can be used.
Disable or enabling of the passwords can be allowed or prohibited individually. For exam-
ple, all passwords can be prohibited. You can thus limit access options to a small user
group. The output parameters (F_PWD_ON, FULL_PWD_ON, R_PWD_ON,
HMI_PWD_ON) always show the current status of the password use, regardless of the
REQ parameter.
Passwords that are not configured must have the signal state TRUE at the input and return
the signal state TRUE at the output. The fail-safe password can only be configured for an
F-CPU and must therefore always be interconnected with the signal state TRUE in a stand-
ard CPU. If the instruction returns an error, the call has no effect and the previous lock is
still in effect.
Disabled passwords can be enabled again under the following conditions:
• The CPU is reset to its factory settings.
• The front panel of the S7-1500 CPU supports a dialog that allows you to navigate to the
appropriate menu in which the passwords can be enabled again.
• When you call the "Limit and enable password legitimation" instruction, the input param-
eter of the desired password has the signal state "1".
• Set the mode selector to STOP. The restriction to password legitimation is re-established
as soon as the switch is set back to RUN.
• Plugging an empty memory card (transfer module or program card) into an S7-1200
CPU.
• The transition from POWER OFF to POWER ON disables the protection in the S7-1200
CPU. The "Limit and enable password legitimation" instruction must be called again in
the program (e.g. in the startup OB).
Note
The "Limit and enable password legitimation" instruction blocks access from HMI sys-
tems if the HMI password has not been enabled.
Note
Already legitimized connections
ENDIS_PW: Limit and enable password legitimation (S7-1200, S7-1500)
Existing connections that were already legitimized before an execution of the "Limit and
enable password legitimation" instruction may be terminated by the execution of the in-
struction. This depends on the existing password protection level.
Examples:
• A connection that is only authorized with "read access" is interrupted by the execution
of the ENDIS_PW instruction (REQ = 1; R_PWD = 0).
• Other connections that have a lower password protection level are also interrupted.
• Connections that have "Read/write access" are retained.
Preventing unintentional lockout on an S7-1500 CPU
The settings can be made on the front panel of the CPU and the CPU saves the most re-
cent setting.
To prevent unintentional lockout, the protection can be disabled by setting the mode selec-
tor to STOP with an S7-1500 CPU. The protection is automatically enabled again by setting
mode selector to RUN without having to call the "Limit and enable password legitimation"
instruction again or taking additional actions on the front panel.
Preventing unintentional lockout of an S7-1200 CPU
Because the S7-1200 CPU does not have a mode selector, protection is disabled with
POWER OFF - POWER ON. This means that it is possible and advisable to prevent unin-
tentional lockout with the help of specific program sequences within your program.
To do so, program a time control either by means of a cyclic interrupt OB or a timer in the
Main OB (OB 1). This gives you the option of calling the "Limit and enable password legiti-
mation" instruction in the respective OB (e.g., OB 1 or OB 35) relatively quickly after a tran-
sition from POWER OFF to POWER ON and the associated disabling of the protection.
Call the instruction in the startup OB (OB 100) in order to keep as short as possible the
time window in which the application is not active and thus ensure that no restrictions exist
in the password legitimation. This procedure offers you the greatest possible protection
against unauthorized access.
If there has been an accidental lockout, you can skip the call in the startup OB (by querying
an input parameter, for example) and you have the set time (for example, 10 seconds up to
1 minute) to establish a connection to the CPU before the lock becomes active once again.
If there is no timer in your program code and a lockout has occurred, insert an empty trans-
fer card or an empty program card into the CPU. The empty transfer card or program card
deletes the internal load memory of the CPU. You must then download the user program
once again from STEP 7 to the CPU.
Procedure for lost passwords with an S7-1200 CPU
If you have lost the password for a password-protected S7-1200 CPU, delete the pass-
word-protected program with an empty transfer card or program card. The empty transfer
card or program card deletes the internal load memory of the CPU. You can then load a
new user program from STEP 7 Basic in the CPU.
WARNING
Inserting an empty transfer card
When you insert a transfer card in a CPU during runtime, the CPU goes to STOP mode. If
operating states are unstable, controllers may fail and thus cause the uncontrolled opera-
tion of the controlled devices. This leads to an unforeseen operation of the automation
system which can cause deadly or serious injuries and/or damage to property.
ENDIS_PW: Limit and enable password legitimation (S7-1200, S7-1500)
Once you have pulled the transfer card, the content of the transfer card is available in the
internal load memory. Make sure that the card does not include a program at this point.
WARNING
Inserting an empty program card
When you insert a program card in a CPU during runtime, the CPU goes to STOP mode.
If operating states are unstable, controllers may fail and thus cause the uncontrolled op-
eration of the controlled devices. This leads to an unforeseen operation of the automation
system which can cause deadly or serious injuries and/or damage to property.
Make sure that the program card is empty. The internal load memory is copied to the
empty program card. Once you have pulled the previously empty program card, the inter-
nal load memory is empty.
You must remove the transfer card or program card before you put the CPU into RUN
mode.
Effects of password use on the operating modes
The following table shows what effects the password use by means of the "Limit and ena-
ble password legitimation" instruction has on the operating modes and the corresponding
user actions.
Action Password protection by means of the instruction
Basic state after
• Operating mode switch to STOP
Not activated
• Reset memory manually (PG, switch, change
(no restrictions)
of MC (Motion Control))
• Reset to factory setting
• S7-1200 CPU:
The lock is disabled and the instruc-
tion must be called once again in the
program (e.g., in the startup OB).
Basic state after POWER ON • S7-1500 CPU:
Enabled (if a lock was activated be-
fore POWER OFF). The option of not
allowing passwords is retentive.
Operating mode transition RUN/STARTUP/
Activated
HOLD -> STOP (by terminating the instruction,
an error or communication) or STOP ->
Passwords still cannot be used.
STARTUP/RUN/HOLD
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
When the REQ parame-
ter has the signal state
I, Q, M, D, L or
REQ Input BOOL "0", the currently set sig-
constant
nal state of the pass-
words is queried.
ENDIS_PW: Limit and enable password legitimation (S7-1200, S7-1500)
Read/write access includ-
ing fail-safe
I, Q, M, D, L or • F_PWD = "0": Do not
F_PWD Input BOOL
constant allow password
• F_PWD = "1": Allow
password
Read/write access
• FULL_PWD = "0": Do
I, Q, M, D, L or
FULL_PWD Input BOOL not allow password
constant
• FULL_PWD = "1": Al-
low password
Read access
• R_PWD = "0": Do not
I, Q, M, D, L or
R_PWD Input BOOL allow password
constant
• R_PWD = "1": Allow
password
HMI access
• HMI_PWD = "0": Do
I, Q, M, D, L or
HMI_PWD Input BOOL not allow password
constant
• HMI_PWD = "1": Allow
password
Read/write access includ-
ing fail-safe
F_PWD_O • F_PWD_ON = "0":
Output BOOL I, Q, M, D, L
N Password not allowed
• F_PWD_ON = "1":
Password allowed
Read/write access status
• FULL_PWD_ON = "0":
FULL_PWD
Output BOOL I, Q, M, D, L Password not allowed
_ON
• FULL_PWD_ON = "1":
Password allowed
Read-access status
• R_PWD_ON = "0":
R_PWD_O
Output BOOL I, Q, M, D, L Password not allowed
N
• R_PWD_ON = "1":
Password allowed
HMI-access status
• HMI_PWD_ON = "0":
HMI_PWD_
Output BOOL I, Q, M, D, L Password not allowed
ON
• HMI_PWD_ON = "1":
Password allowed
Function value
WORD I, Q, M, D, L Error information
(RET_VAL)
You can find additional information on valid data types under "See also".
Parameter RET_VAL
ENDIS_PW: Limit and enable password legitimation (S7-1200, S7-1500)
The following table shows the meaning of the values of the RET_VAL parameter:
Error code* Explanation
(W#16#...)
0000 No error
8090 The "Limit and enable password legitimation" instruction is not supported
The password for fail-safe is not configured. The signal state must be TRUE
80D0
for standard CPUs.
80D1 The read/write access is not configured
80D2 The read access is not configured
80D3 The HMI access is not configured
*The error codes can be displayed as integer or hexadecimal value in the program editor.
You can find information on switching the display formats under "See also".
Example
The following example shows how the instruction works:
SCL
"Tag_Result" := ENDIS_PW(REQ := 0,
F_PWD := 0,
FULL_PWD := 0,
R_PWD := 1,
HMI_PWD := 0,
F_PWD_ON => "Status_F_PWD",
FULL_PWD_ON => "Status_FULL_PWD",
R_PWD_ON => "Status_R_PWD",
HMI_PWD_ON => "Status_HMI_PWD");
The following table shows how the instruction works using specific operand values:
Operand Data type Value
REQ BOOL 0
F_PWD BOOL 0
FULL_PWD BOOL 0
R_PWD BOOL 1
HMI_PWD BOOL 0
Status_F_PWD BOOL 0
Status_FULL_PWD BOOL 0
Status_R_PWD BOOL 1
Status_HMI_PWD BOOL 0
The instruction is executed because the REQ operand has the signal state "1". The
R_PWD operand has the signal state "1", which means that read access is allowed when
ENDIS_PW: Limit and enable password legitimation (S7-1200, S7-1500)
the assigned password is entered. The R_PWD_ON status operand also has the signal
state "1", thereby signaling that the R_PWD operand is activated.
See also
Overview of the valid data types
Switching display formats in the program status
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)