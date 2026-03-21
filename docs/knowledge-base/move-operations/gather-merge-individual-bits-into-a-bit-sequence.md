# GATHER: Merge individual bits into a bit sequence

**Category:** Move operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 132  

---

GATHER: Merge individual bits into a bit sequence (S7-1200, S7-1500)
GATHER: Merge individual bits into a bit sequence
Description
The "Merge individual bits into a bit sequence" instruction merges the bits from an ARRAY
of BOOL, an anonymous STRUCT or a PLC data type exclusively with Boolean elements
into a bit sequence. The bit sequence is saved in a tag of the data type BYTE, WORD,
DWORD or LWORD.
Note
Multi-dimensional ARRAY of BOOL
With the "Merge individual bits into a bit sequence" instruction, the use of a multidimen-
sional ARRAY of BOOL is not permitted.
Note
Length of the ARRAY, STRUCT or PLC data type
The ARRAY, STRUCT or the PLC data type must have exactly the number of elements
that is specified by the bit sequence.
This means for the data type BYTE, for example, the ARRAY, anonymous STRUCT or
the PLC data type must have exactly 8 elements (WORD = 16, DWORD = 32 and
LWORD = 64).
Note
Availability of the instruction
The instruction can be used with a CPU of the S7-1200 series as of firmware version
>4.2, and for a CPU of the S7-1500 series as of firmware version 2.1.
The enable output ENO returns signal state "0" if one of the following conditions applies:
• Enable input EN has signal state "0".
• The ARRAY, anonymous STRUCT or the PLC data type (UDT) has fewer or more BOOL
elements than specified by the bit sequence. In this case the BOOL elements are not
transferred.
• Fewer than the necessary number of bits are available.
Parameter
The following table shows the parameters of the instruction:
Parameter Declara- Data type Memory area Description
tion
S7-1200 S7-1500
ARRAY[*] of ARRAY, STRUCT or
BOOL, PLC data type, the
ARRAY[*]
STRUCT or bits of which are
of BOOL,
PLC data I, Q, M, D, merged into a bit se-
IN Input STRUCT
type L quence.
or PLC da-
ta type
*: 8, 16, 32 or The values must not
64 elements be located in the I/O
GATHER: Merge individual bits into a bit sequence (S7-1200, S7-1500)
*: 8, 16, 32
area or in the DB of a
or 64 ele-
technology object.
ments
BYTE,
BYTE,
WORD, I, Q, M, D, Merged bit sequence,
OUT Output WORD,
DWORD, L saved in a tag
DWORD
LWORD
You can find additional information on valid data types under "See also".
Example with an ARRAY
Create the following tags in the block interface:
Tag Section Data type
SourceArray Input ARRAY[0..15] of BOOL
DestinationWord Output WORD
The following example shows how the instruction works:
SCL
GATHER(IN := #SourceArray,
OUT => #DestinationWord);
The following table shows how the instruction works using specific operand values:
Parameter Operand Data type
The operand "SourceArray"
is of the data type AR-
RAY[0..15] of BOOL. It con-
IN SourceArray sists of 16 elements and is
therefore just as large as the
WORD in which the bits are
to be merged.
OUT DestinationWord WORD (16 bits)
The bits of the #SourceArray operand are merged into a WORD.
Example with a PLC data type (UDT)
Create the following PLC data type "myBits":
GATHER: Merge individual bits into a bit sequence (S7-1200, S7-1500)
Create the following tags in the block interface:
Tag Section Data type
SourceUDT Input "myBits"
DestinationWord Output WORD
The following example shows how the instruction works:
SCL
GATHER(IN := #SourceUDT,
OUT => #DestinationWord);
The following table shows how the instruction works using specific operand values:
Parameter Operand Data type
The "SourceUDT" operand
has the PLC data type
(UDT). It consists of 16 ele-
IN SourceUDT ments and is therefore just
as large as the WORD in
which the bits are to be
merged.
OUT DestinationWord WORD (16 bits)
The bits of the #SourceUDT operand are merged into a WORD.
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)