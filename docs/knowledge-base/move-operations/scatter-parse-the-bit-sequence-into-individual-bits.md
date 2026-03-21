# SCATTER: Parse the bit sequence into individual bits

**Category:** Move operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 125  

---

SCATTER: Parse the bit sequence into individual bits (S7-1200, S7-1500)
SCATTER: Parse the bit sequence into individual
bits
Description
The "Parse the bit sequence into individual bits" instruction parses a tag of the BYTE,
WORD, DWORD or LWORD data type into individual bits and saves them in an ARRAY of
BOOL, an anonymous STRUCT or a PLC data type exclusively with Boolean elements.
Note
Multi-dimensional ARRAY of BOOL
With the "Parse the bit sequence into individual bits" instruction, the use of a multidimen-
sional ARRAY of BOOL is not permitted.
Note
Length of the ARRAY, STRUCT or PLC data type
The ARRAY, the anonymous STRUCT or the PLC data type must have exactly the num-
ber of elements that is specified by the bit sequence.
This means for the data type BYTE, for example, the ARRAY, STRUCT or the PLC data
type must have exactly 8 elements (WORD = 16, DWORD = 32 and LWORD = 64).
Note
Availability of the instruction
The instruction can be used with a CPU of the S7-1200 series as of firmware version
>4.2, and for a CPU of the S7-1500 series as of firmware version 2.1.
This way you can, for example, parse a status word, and read and change the status of the
individual bits using the index. Using GATHER, you can merge the bits once again into a
bit sequence.
The enable output ENO returns signal state "0" if one of the following conditions applies:
• Enable input EN has signal state "0".
• The ARRAY, STRUCT or PLC data type does not provide enough BOOL elements.
Parameter
The following table shows the parameters of the instruction:
Parameter Declaration Data type Memory area Description
S7-1200 S7-1500
Bit sequence that is
parsed.
BYTE,
BYTE,
WORD, I, Q, M, D,
IN Input WORD, The values must not be lo-
DWORD, L
DWORD cated in the I/O area or in
LWORD
the DB of a technology ob-
ject.
ARRAY[*] of ARRAY[*] ARRAY, STRUCT or PLC
I, Q, M, D,
OUT Output BOOL, of BOOL, data type in which the indi-
L
STRUCT or STRUCT vidual bits are stored
SCATTER: Parse the bit sequence into individual bits (S7-1200, S7-1500)
PLC data or PLC da-
type ta type
*: 8, 16, 32 *: 8, 16, 32
or 64 ele- or 64 ele-
ments ments
You can find additional information on valid data types under "See also".
Example with an ARRAY
Create the following tags in the block interface:
Tag Section Data type
SourceWord Input WORD
DestinationArray Output ARRAY[0..15] of BOOL
The following example shows how the instruction works:
SCL
SCATTER(IN := #SourceWord,
OUT => #DestinationArray);
The following table shows how the instruction works using specific operand values:
Parameter Operand Data type
IN SourceWord WORD (16 bits)
The operand "DestinationAr-
ray" is of the data type AR-
RAY[0..15] of BOOL. It con-
OUT DestinationArray
sists of 16 elements and is
therefore just as large as the
WORD that is to be parsed.
The operand #SourceWord of the data type WORD is parsed into its individual bits (16)
and assigned to the individual elements of the #DestinationArray operand.
Example with a PLC data type (UDT)
Create the following PLC data type "myBits":
SCATTER: Parse the bit sequence into individual bits (S7-1200, S7-1500)
Create the following tags in the block interface:
Tag Section Data type
SourceWord Input WORD
DestinationUDT Output "myBits"
The following example shows how the instruction works:
SCL
SCATTER(IN := #SourceWord,
OUT => #DestinationUDT);
The following table shows how the instruction works using specific operand values:
Parameter Operand Data type
IN SourceWord WORD (16 bits)
The "DestinationUDT" oper-
and has the PLC data type
(UDT). It consists of 16 ele-
OUT DestinationUDT
ments and is therefore just
as large as the WORD that
is to be parsed.
The #SourceWord operand of the data type WORD is parsed into its individual bits (16)
and assigned to the individual elements of the #DestinationArray operand.
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)