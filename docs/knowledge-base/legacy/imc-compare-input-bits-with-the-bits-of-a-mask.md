# IMC: Compare input bits with the bits of a mask

**Category:** Legacy  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 337  

---

IMC: Compare input bits with the bits of a mask (S7-1500)
IMC: Compare input bits with the bits of a mask
Description
You can use the "Compare input bits with the bits of a mask" instruction to compare the
signal state of up to 16 programmed input bits (IN_BIT0 to IN_BIT15) with the correspond-
ing bits of a mask. Up to 16 steps with masks can be programmed. The value of the
IN_BIT0 parameter is compared with the value of the mask CMP_VAL[x,0], with "x" indicat-
ing the step number. In the CMP_STEP parameter, you specify the step number of the
mask that is used for the comparison. All programmed values are compared in the same
manner. Unprogrammed input bits or unprogrammed bits of the mask have a default signal
state FALSE.
If a match is found in the comparison, the signal state of the OUT parameter is set to "1".
Otherwise the OUT parameter is set to "0".
If the value of the CMP_STEP parameter is greater than 15, the instruction is not executed.
An error message is output at the ERR_CODE parameter.
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
IMC: Compare input bits with the bits of a mask (S7-1500)
Input bit 13 is compared with bit
IN_BIT13 Input BOOL I, Q, M, D, L
13 of the mask.
Input bit 14 is compared with bit
IN_BIT14 Input BOOL I, Q, M, D, L
14 of the mask.
Input bit 15 is compared with bit
IN_BIT15 Input BOOL I, Q, M, D, L
15 of the mask.
CMP_STE The step number of the mask
Input BYTE I, Q, M, D, L, P
P used for the comparison.
The signal state "1" indicates
that a match was found.
OUT Output BOOL I, Q, M, D, L
A signal state of "0" indicates
that no match was found.
ERR_COD
Output WORD I, Q, M, D, L, P Error information
E
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
000A The value at the CMP_STEP parameter is greater than 15.
*The error codes can be displayed as integer or hexadecimal value in the program editor.
For information on toggling display formats, refer to "See also".
See also
Overview of the valid data types
Switching display formats in the program status
Memory areas (S7-1500)
Basics of SCL