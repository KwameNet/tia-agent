# SMC: Compare scan matrix

**Category:** Legacy  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 339  

---

SMC: Compare scan matrix (S7-1500)
SMC: Compare scan matrix
Description
You can use the "Compare scan matrix" instruction to compare the signal state of up to 16
programmed input bits (IN_BIT0 to IN_BIT15) with the corresponding bit of the comparison
masks for each step. Processing starts at step 1 and is continued until the last program-
med step (LAST) or until a match is found. The input bit of the IN_BIT0 parameter is com-
pared with the value of the mask CMP_VAL[x,0], with "x" indicating the step number. All
programmed values are compared in the same manner. If a match is found the signal state
of the OUT parameter is set to "1" and the step number with the matching mask is written
in the OUT_STEP parameter. Unprogrammed input bits or unprogrammed bits of the mask
have a default signal state FALSE. If more than one step has a matching mask, only the
first one found is indicated in the OUT_STEP parameter. If no match is found, the signal
state of the OUT parameter is set to "0". In this case the value at the OUT_STEP parame-
ter is greater by "1" than the value at the LAST parameter.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
Input bit 0 is compared with bit 0
IN_BIT0 Input BOOL I, Q, M, D, L
of the mask.
Input bit 1 is compared with bit 1
IN_BIT1 Input BOOL I, Q, M, D, L
of the mask.
Input bit 2 is compared with bit 2
IN_BIT2 Input BOOL I, Q, M, D, L
of the mask.
Input bit 3 is compared with bit 3
IN_BIT3 Input BOOL I, Q, M, D, L
of the mask.
Input bit 4 is compared with bit 4
IN_BIT4 Input BOOL I, Q, M, D, L
of the mask.
Input bit 5 is compared with bit 5
IN_BIT5 Input BOOL I, Q, M, D, L
of the mask.
Input bit 6 is compared with bit 6
IN_BIT6 Input BOOL I, Q, M, D, L
of the mask.
Input bit 7 is compared with bit 7
IN_BIT7 Input BOOL I, Q, M, D, L
of the mask.
Input bit 8 is compared with bit 8
IN_BIT8 Input BOOL I, Q, M, D, L
of the mask.
Input bit 9 is compared with bit 9
IN_BIT9 Input BOOL I, Q, M, D, L
of the mask.
Input bit 10 is compared with bit
IN_BIT10 Input BOOL I, Q, M, D, L
10 of the mask.
Input bit 11 is compared with bit
IN_BIT11 Input BOOL I, Q, M, D, L
11 of the mask.
Input bit 12 is compared with bit
IN_BIT12 Input BOOL I, Q, M, D, L
12 of the mask.
SMC: Compare scan matrix (S7-1500)
Input bit 13 is compared with bit
IN_BIT13 Input BOOL I, Q, M, D, L
13 of the mask.
Input bit 14 is compared with bit
IN_BIT14 Input BOOL I, Q, M, D, L
14 of the mask.
Input bit 15 is compared with bit
IN_BIT15 Input BOOL I, Q, M, D, L
15 of the mask.
The signal state "1" indicates
that a match was found.
OUT Output BOOL I, Q, M, D, L
A signal state of "0" indicates
that no match was found.
Contains the step number with
the matching mask or the step
OUT_STE number which is greater by "1"
Output BYTE I, Q, M, D, L, P
P than the value of the LAST pa-
rameter, provided no match is
found.
ERR_COD
Output WORD I, Q, M, D, L, P Error information
E
Specifies the step number of the
LAST Static BYTE I, Q, M, D, L, P last step to be scanned for a
matching mask.
Comparison masks [0 to 15, 0 to
15]: The first number of the in-
ARRAY OF
CMP_VAL Static I, Q, M, D, L dex is the step number and the
WORD
second number is the bit num-
ber of the mask.
The static parameters are not visible when calling the instruction in the program. These are
saved in the instance of the instruction.
ERR_CODE parameter
The following table shows the meaning of the values of the ERR_CODE parameter:
Error code* Explanation
(W#16#...)
0000 No error
000E The value at the LAST parameter is greater than 15.
*The error codes can be displayed as integer or hexadecimal value in the program editor.
For information on toggling display formats, refer to "See also".
See also
Overview of the valid data types
Switching display formats in the program status
Memory areas (S7-1500)
Basics of SCL