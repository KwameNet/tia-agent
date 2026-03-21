# GATHER_BLK: Merge individual bits into multiple elements of an ARRAY of bit sequence

**Category:** Move operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 135  

---

GATHER_BLK: Merge individual bits into multiple elements of an ARRAY of bit sequence (S7-1200, S7-1500)
GATHER_BLK: Merge individual bits into multiple
elements of an ARRAY of bit sequence
Description
The "Merge individual bits into multiple elements of an ARRAY of bit sequence" instruction
merges the bits from an ARRAY of BOOL, an anonymous STRUCT or a PLC data type ex-
clusively with Boolean elements into one or multiple elements of an ARRAY of <bit se-
quence>. At the COUNT_OUT parameter you specify how many elements of the destina-
tion ARRAY are going to be written. With this step you also implicitly specify how many ele-
ments of the ARRAY of BOOL, the anonymous STRUCT or the PLC data type are re-
quired. The destination ARRAY at the OUT parameter may have more elements than
specified at the COUNT_OUT parameter. The ARRAY of <bit sequence> must have suffi-
cient elements to save the bits that are going to be merged. However, the destination AR-
RAY may also be larger.
Note
Multi-dimensional ARRAY of BOOL
If the ARRAY is a multi-dimensional ARRAY of BOOL, the padding bits of the dimensions
contained are counted as well even if they were not explicitly declared and are not ac-
cessible.
Example 1: An ARRAY[1..10,0..4,1..2] of BOOL is handled like an ARRAY[1..10,0..4,1..8]
of BOOL or like an ARRAY[0..399] of BOOL.
Example 2: At the OUT parameter, an ARRAY[0..5] of WORD (sourceArrayWord[2]) is
interconnected. The COUNT_IN parameter has the value "3". At the IN parameter, an
ARRAY[0..1,0..5,0..7] of BOOL (destinationArrayBool[0,0,0]) is interconnected. Both the
array at the IN parameter and at the OUT parameter has a size of 96 bits. 48 individual
bits are merged from the ARRAY of BOOL.
Note
If the ARRAY low limit of the source ARRAY is not "0", note the following:
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
GATHER_BLK: Merge individual bits into multiple elements of an ARRAY of bit sequence (S7-1200, S7-1500)
The enable output ENO returns signal state "0" if one of the following conditions applies:
• Enable input EN has signal state "0".
• The index of the source ARRAY does not start at a BYTE, WORD, DWORD or LWORD
limit. In this case, no result is written to the ARRAY of <bit sequence>.
• The ARRAY[*] of <bit sequence> does not provide the required number of elements.
o S7-1500-CPU: In this case as many bit sequences as possible are merged and writ-
ten to the ARRAY of <bit sequence>. The remaining bits are no longer taken into
account.
o S7-1200-CPU: There is no copying procedure.
Note
S7-1200-CPU: Enable output ENO = 0
When the enable output ENO has signal state "0", no data are written to the output pa-
rameter OUT.
Parameter
The following table shows the parameters of the instruction:
Parameter Declaration Data type Memory area Description
S7-1200 S7-1500
ARRAY of
BOOL,
STRUCT or
PLC data
Element of type whose
Element of an
an AR- bits are
ARRAY[*] of
RAY[*] of merged
- BOOL (source AR-
- BOOL
IN Input I, Q, M, D, L RAY)
- STRUCT
- STRUCT
The values
- PLC data
- PLC data must not be
type
type located in the
I/O area or in
the DB of a
technology
object.
Counter how
many ele-
ments of the
target ARRAY
are to be de-
USINT, scribed.
USINT, UINT, UINT,
COUNT_OUT Input I, Q, M, D, L
UDINT UDINT, The value
ULINT must not be
in the I/O
area or in the
database of a
technology
object.
GATHER_BLK: Merge individual bits into multiple elements of an ARRAY of bit sequence (S7-1200, S7-1500)
Element of
Element of an an AR- ARRAY of
ARRAY[*] of RAY[*] of <bit se-
quence> to
- BYTE
- BYTE
OUT Output I, Q, M, D, L which the bits
- WORD are saved
- WORD
(destination
- DWORD
- DWORD ARRAY)
- LWORD
You can find additional information on valid data types under "See also".
Example of a source ARRAY with the low limit "0"
Create the following tags in the block interface:
Tag Section Data type
SourceArrayBool ARRAY[0..95] of BOOL
Input
CounterOutput UDINT
DestinationArrayWord Output ARRAY[0..5] of WORD
The following example shows how the instruction works:
SCL
GATHER_BLK(IN := #SourceArrayBool[0],
COUNT_OUT := #CounterOutput,
OUT => #DestinationArrayWord[2]);
The following table shows how the instruction works using specific operand values:
Parameter Operand Data type
The operand "SourceArray-
Bool" is of the data type AR-
RAY[0..95] of BOOL. This
IN SourceArrayBool[0]
means it provides 96 BOOL
elements that can merged
into words once again.
UDINT3 (3 words are to be
written. This means 48 bits
COUNT_OUT CounterOutput = 3
must be available in the
source ARRAY.)
The operand "DestinationAr-
rayWord" is of the data type
OUT DestinationArrayWord[2] ARRAY[0..5] of WORD. This
means 6 WORD elements
are available.
If the operand #Enable returns the signal state "1" at the enable input EN, the instruction is
executed. As of the 1st element of the #SourceArrayBool operand, 48 bits are merged in
the #DestinationArrayWord operand. The starting point in the destination ARRAY is the 3rd
element. This means that the first 16 bits are written into the 3rd word, the second 16 nits
into the 4th word and the third 16 bits into the 5th word of the destination ARRAY. If an er-
GATHER_BLK: Merge individual bits into multiple elements of an ARRAY of bit sequence (S7-1200, S7-1500)
ror occurs during the execution of the instruction, the operand #EnableOut returns the sig-
nal state "0" at the enable output ENO.
Example of a source ARRAY with the low limit "-2"
Create the following tags in the block interface:
Tag Section Data type
SourceArrayBool ARRAY[-2..93] of BOOL
Input
CounterOutput UDINT
DestinationArrayWord Output ARRAY[0..5] of WORD
The following example shows how the instruction works:
SCL
GATHER_BLK(IN := #SourceArrayBool[14],
COUNT_OUT := #CounterOutput,
OUT => #DestinationArrayWord[2]);
The following table shows how the instruction works using specific operand values:
Parameter Operand Data type
The operand "SourceArray-
Bool" is of the data type AR-
RAY[-2..93] of BOOL. Be-
cause the starting point is
IN SourceArrayBool[14]
the 16th element, only 80
BOOL elements that can be
merged into words again are
available.
UDINT3 (3 words are to be
written. This means 48 bits
COUNT_OUT CounterOutput = 3
must be available in the
source ARRAY.)
The operand "DestinationAr-
rayWord" is of the data type
OUT DestinationArrayWord[2] ARRAY[0..5] of WORD. This
means 6 WORD elements
are available.
If the operand #Enable returns the signal state "1" at the enable input EN, the instruction is
executed. Starting with the 16th element of the #SourceArrayBool operand, 48 bits are
merged into the #DestinationArrayWord operand. The starting point in the destination AR-
RAY is the 3rd element. This means the first 16 bits of the source ARRAY are ignored. The
second 16 Bits are written into the 3rd word, the third 16 bits into the 4th word and the
fourth 16 bits into the 5th word of the destination ARRAY. The remaining 64 bits of the
source ARRAY are not taken into account either. If an error occurs during the execution of
the instruction, the operand #EnableOut returns the signal state "0" at the enable output
ENO.
See also
Overview of the valid data types
GATHER_BLK: Merge individual bits into multiple elements of an ARRAY of bit sequence (S7-1200, S7-1500)
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)