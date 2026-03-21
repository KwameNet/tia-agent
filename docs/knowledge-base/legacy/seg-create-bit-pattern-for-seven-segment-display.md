# SEG: Create bit pattern for seven-segment display

**Category:** Legacy  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 344  

---

SEG: Create bit pattern for seven-segment display (S7-1500)
SEG: Create bit pattern for seven-segment display
Description
The "Create bit pattern for seven-segment display" instruction is used to convert each of
the four hexadecimal digits of the specified source word (IN) into an equivalent bit pattern
for a 7-segment display. The result of the instruction is output in the double word on the
OUT parameter.
The following relation exists between the hexadecimal digits and the assignment of the 7
segments (a, b, c, d, e, f, g):
Assignment of Display
Input digit
the segments
Seven-segment display
(Hexadeci-
(Binary)
‑ g f e d c b a mal)
0000 00111111 0
0001 00000110 1
0010 01011011 2
0011 01001111 3
0100 01100110 4
0101 01101101 5
0110 01111101 6
0111 00000111 7
1000 01111111 8
1001 01100111 9
1010 01110111 A
1011 01111100 B
1100 00111001 C
1101 01011110 D
1110 01111001 E
1111 01110001 F
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
Source word with
IN Input WORD I, Q, M, D, L, P four hexadecimal
digits
Bit pattern for the
OUT Output DWORD I, Q, M, D, L, P seven-segment
display
Empty function
Function value VOID
value
SEG: Create bit pattern for seven-segment display (S7-1500)
Example
The following example shows how the instruction works:
SCL
SEG(IN := "Tag_Input",
OUT => "Tag_Output");
The following table shows how the instruction works using specific operand values:
Parameters Operand Value
Hexadecimal Binary
IN Tag_Input W#16#1234 0001 0010 0011 0100
00000110 01011011
01001111 01100110
OUT Tag_Output DW16#065B4F66
Display: 1234
See also
Overview of the valid data types
Switching display formats in the program status
Memory areas (S7-1500)
Basics of SCL