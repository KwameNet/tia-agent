# SCATTER_BLK: Parse elements of an ARRAY of bit sequence into individual bits

**Category:** Move operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 128  

---

SCATTER_BLK: Parse elements of an ARRAY of bit sequence into individual bits (S7-1200, S7-1500)
SCATTER_BLK: Parse elements of an ARRAY of bit
sequence into individual bits
Description
The "Parse elements of an ARRAY of bit sequence into individual bits" instruction parses
one or more elements of an ARRAY of BYTE, WORD, DWORD or LWORD into individual
bits and saves them in an ARRAY of BOOL, an anonymous STRUCT or a PLC data type
exclusively with Boolean elements. At the COUNT_IN parameter you specify how many el-
ements of the source ARRAY are going to be parsed. The source ARRAY at the IN param-
eter may have more elements than specified at the COUNT_IN parameter. The ARRAY of
BOOL, the anonymous STRUCT or the PLC data type must have sufficient elements to
save the bits of the parsed bit sequences. However, the destination memory area may also
be larger.
Note
Multi-dimensional ARRAY of BOOL
If the ARRAY is a multi-dimensional ARRAY of BOOL, the padding bits of the dimensions
contained are counted as well even if they were not explicitly declared and are not ac-
cessible.
Example 1: An ARRAY[1..10,0..4,1..2] of BOOL is handled like an ARRAY[1..10,0..4,1..8]
of BOOL or like an ARRAY[0..399] of BOOL.
Example 2: At the IN parameter, an ARRAY[0..5] of WORD (sourceArrayWord[2]) is in-
terconnected. The COUNT_IN parameter has the value "3". At the OUT parameter, an
ARRAY[0..1,0..5,0..7] of BOOL (destinationArrayBool[0,0,0]) is interconnected. Both the
array at the IN parameter and at the OUT parameter has a size of 96 bits. The ARRAY of
WORD is parsed into 48 individual bits.
Note
If the ARRAY low limit of the target ARRAY is not "0", note the following:
For performance reasons the index must always start at a BYTE, WORD, DWORD or
LWORD limit. This means the index must be calculated starting at the low limit of the AR-
RAY. The following formula is used as basis for this calculation:
Valid indices = ARRAY low limit + n(number of bit sequences) × number of bits of the
desired bit sequence
For an ARRAY[-2..45] of BOOL and the bit sequence WORD the calculation looks as fol-
lows:
• Valid index (-2) = -2 + 0 × 16
• Valid index (14) = -2 + 1 × 16
• Valid index (30) = -2 + 2 × 16
You can find an example described below.
Note
Availability of the instruction
The instruction can be used with a CPU of the S7-1200 series as of firmware version
>4.2, and for a CPU of the S7-1500 series as of firmware version 2.1.
SCATTER_BLK: Parse elements of an ARRAY of bit sequence into individual bits (S7-1200, S7-1500)
This way you can, for example, parse status words, and read and change the status of the
individual bits using the index. Using GATHER, you can merge the bits once again into a
bit sequence.
The enable output ENO returns signal state "0" if one of the following conditions applies:
• Enable input EN has signal state "0".
• The source ARRAY has fewer elements than specified at the COUNT_IN parameter.
• The index of the destination ARRAY does not start at a BYTE, WORD, DWORD or
LWORD limit. In this case, no result is written to the ARRAY of BOOL.
• The ARRAY[*] of BOOL, STRUCT or PLC data type does not provide the required num-
ber of elements.
o S7-1500-CPU: In this case as many bit sequences as possible are parsed and writ-
ten to the ARRAY of BOOL, the anonymous STRUCT or the PLC data type. The re-
maining bit sequences are no longer taken into account.
o S7-1200-CPU: There is no copying procedure.
Note
S7-1200-CPU: Enable output ENO = 0
When the enable output ENO has signal state "0", no data are written to the output pa-
rameter OUT.
Parameter
The following table shows the parameters of the instruction:
Parameter Declaration Data type Memory area Description
S7-1200 S7-1500
Element of
an AR-
Element of an ARRAY of <bit se-
RAY[*] of
ARRAY[*] of quence> that is parsed.
IN Input - BYTE - BYTE I, Q, M, The values must not be
D, L
- WORD located in the I/O area or
- WORD
in the DB of a technology
- DWORD - DWORD object.
- LWORD
Counter for the number
of elements of the source
ARRAY that are going to
USINT,
be parsed.
COUNT_I USINT, UINT, UINT, I, Q, M,
Input
N UDINT UDINT, D, L
The value must not be in
ULINT
the I/O area or in the da-
tabase of a technology
object.
Element of
Element of an
an AR-
ARRAY[*] of ARRAY, STRUCT or PLC
RAY[*] of I, Q, M,
OUT Output data type in which the in-
- BOOL D, L
- BOOL dividual bits are stored
- STRUCT
- STRUCT
SCATTER_BLK: Parse elements of an ARRAY of bit sequence into individual bits (S7-1200, S7-1500)
- PLC data - PLC data
type type
You can find additional information on valid data types under "See also".
Example of a destination ARRAY with the low limit "0"
Create the following tags in the block interface:
Tag Section Data type
SourceArrayWord ARRAY[0..5] of WORD
Input
CounterInput UDINT
DestinationArrayBool Output ARRAY[0..95] of BOOL
The following example shows how the instruction works:
SCL
SCATTER_BLK(IN := #SourceArrayWord[2],
COUNT_IN := #CounterInput,
OUT => #DestinationArrayBool[0]);
The following table shows how the instruction works using specific operand values:
Parameter Operand Data type
ARRAY[0..5] of WORD (96
IN SourceArrayWord[2]
bits can be parsed.)
UDINT3 (3 WORDs or 48
bits are to be parsed. This
COUNT_IN CounterInput = 3 means at least 48 bits must
be available in the destina-
tion ARRAY.)
The operand "DestinationAr-
rayBool" is of the data type
OUT DestinationArrayBool[0] ARRAY[0..95] of BOOL. This
means it provides 96 BOOL
elements.
The 3rd, 4th and 5th WORD of the #SourceArrayWord operand is parsed into its individual
bits (48) and as of the 1st element assigned to the individual elements of the #Destinatio-
nArrayBool operand.
Example of a destination ARRAY with the low limit "-2"
Create the following tags in the block interface:
Tag Section Data type
SourceArrayWord ARRAY[0..5] of WORD
Input
CounterInput UDINT
DestinationArrayBool Output ARRAY[-2..93] of BOOL
The following example shows how the instruction works:
SCATTER_BLK: Parse elements of an ARRAY of bit sequence into individual bits (S7-1200, S7-1500)
SCL
SCATTER_BLK(IN := #SourceArrayWord[2],
COUNT_IN := #CounterInput,
OUT => #DestinationArrayBool[14]);
The following table shows how the instruction works using specific operand values:
Parameter Operand Data type
ARRAY[0..5] of WORD (96
IN SourceArrayWord[2]
bits can be parsed.)
UDINT3 (3 WORDs or 48
bits are to be parsed. This
COUNT_IN CounterInput = 3 means at least 48 bits must
be available in the destina-
tion ARRAY.)
The operand "DestinationAr-
rayBool" is of the data type
OUT DestinationArrayBool[14] ARRAY[-2..93] of BOOL.
This means it provides 96
BOOL elements.
The 3rd, 4th and 5th WORD of the #SourceArrayWord operand is parsed into its individual
bits (48) and as of the 16th Element assigned to the individual elements of the #Destinatio-
nArrayBool operand.
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)