# MOVE_BLK_VARIANT: Move block

**Category:** Move operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 112  

---

MOVE_BLK_VARIANT: Move block (S7-1200, S7-1500)
MOVE_BLK_VARIANT: Move block
Description
You use the "Move block" instruction to move the content of a memory area (source range)
to another memory area (target range). You can copy a complete ARRAY or elements of
an ARRAY to another ARRAY of the same data type. The size (number of elements) of
source and destination ARRAY may be different. You can copy multiple or single elements
within an ARRAY.
The number of elements that are to be copied must not exceed the selected source range
or target range.
If you use the instruction at the time the block is being created, the ARRAY does not yet
have to be known, as the source and the target are transferred per VARIANT.
Counting at the parameters SRC_INDEX and DEST_INDEX always begins with the low
limit "0", regardless of the later declaration of the ARRAY.
The instruction is not executed if more data is copied than is made available.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
VARIANT L (The declara-
(which points to tion is possible
an ARRAY or an in the "Input",
Source block from which
SRC Input 2) individual AR- "InOut" and
to copy
RAY element), "Temp" sections
ARRAY of <Da- of the block in-
ta_type> terface.)
Number of elements
which are copied
Assign the value "1" to
COUNT Input UDINT I, Q, M, D, L
the parameter COUNT, if
no ARRAY is specified at
parameter SRC or at pa-
rameter DEST.
Defines the first element
to be copied:
• The SRC_INDEX pa-
rameter is calculated
zero-based. If an AR-
RAY is specified at pa-
SRC_IN- rameter SRC, the inte-
Input DINT I, Q, M, D, L
DEX ger at parameter
SRC_INDEX specifies
the first element within
the source area from
which it is to be copied.
Independent of the de-
clared ARRAY limits.
MOVE_BLK_VARIANT: Move block (S7-1200, S7-1500)
• If no ARRAY is speci-
fied at parameter SRC
or only one single ele-
ment of an ARRAY is
specified, then assign
the value "0" at param-
eter SRC_INDEX.
Defines the start of the
destination memory area:
• The DEST_INDEX pa-
rameter is calculated
zero-based. If an AR-
RAY is specified at pa-
rameter DEST, the in-
teger at parameter
DEST_INDEX speci-
DEST_IN-
Input DINT I, Q, M, D, L fies the first element
DEX
within the target range
that is to be copied in-
to. Independent of the
declared ARRAY limits.
• If no ARRAY is speci-
fied at parameter
DEST, then assign the
value "0" at parameter
DEST_INDEX.
L (The declara-
tion is possible
in the "Input", Destination area into
DEST Output 1) VARIANT "InOut" and which the contents of the
"Temp" sections source block are copied.
of the block in-
terface.)
Function value
INT I, Q, M, D, L Error information
(RET_VAL)
1) The DEST parameter is declared as Output, since the data flow into the tag. However,
the tag itself must be declared as InOut in the block interface.
2) With the SRC parameter, the data types BOOL and ARRAY of BOOL are not permit-
ted.
You can find additional information on valid data types under "See also".
RET_VAL parameter
The following table shows the meaning of the values of the RET_VAL parameter:
Error code* Explanation
(W#16#...)
0000 No error
The data types do not correspond. Instead of an ARRAY of Struct, use an
80B4
ARRAY of PLC data type (UDT).
8151 Access to the SRC parameter is not possible.
MOVE_BLK_VARIANT: Move block (S7-1200, S7-1500)
8152 The operand at the SRC parameter is not typed.
8153 Code generation error at the SRC parameter
8154 The operand at the SRC parameter has the data type BOOL.
8281 The COUNT parameter has an invalid value
8382 The value at the SRC_INDEX parameter is outside the ARRAY low limit.
The sum of the SRC_INDEX and COUNT parameters is outside the ARRAY
8383
low limit.
8482 The value at the DEST_INDEX parameter is outside the ARRAY high limit.
The sum of the DEST_INDEX and COUNT parameters is outside the ARRAY
8483
high limit.
8534 The DEST parameter is write protected
8551 Access to the DEST parameter is not possible.
8552 The operand at the DEST parameter is not typed.
8553 Code generation error at the DEST parameter
8554 The operand at the DEST parameter has the data type BOOL.
*The error codes can be displayed as integer or hexadecimal value in the program editor.
You can find information on switching the display formats under "See also".
Example
The following example shows how the instruction works:
SCL
"Tag_Result" := MOVE_BLK_VARIANT(SRC := #SrcField,
COUNT := "Tag_Count",
SRC_INDEX := "Tag_Src_Index",
DEST_INDEX := "Tag_Dest_Index",
DEST => #DestField);
The following table shows how the instruction works using specific operand values:
Parameters Declaration in the block in- Operand Value
terface
The local operand
#SrcField uses a
PLC data type that
was still unknown at
SRC Input #SrcField
the time when the
block was program-
med. (ARRAY[0..10]
of "MOVE_UDT")
COUNT Input Tag_Count 2
SRC_INDEX Input Tag_Src_Index 3
DEST_INDEX Input Tag_Dest_Index 3
The local operand
DEST InOut #DestField
#DestField uses a
MOVE_BLK_VARIANT: Move block (S7-1200, S7-1500)
PLC data type that
was still unknown at
the time when the
block was program-
med. (AR-
RAY[10..20] of
"MOVE_UDT")
Two elements are moved from the source range, starting from the fourth element of the
ARRAY [0..10] of MOVE_UDT, to the target range. The copies are pasted in the ARRAY
[10..20] of MOVE_UDT starting from the fourth element.
Note
You can find more information on the MOVE_BLK_VARIANT instruction in the fol-
lowing article in the Siemens Industry Online Support:
In STEP 7 (TIA Portal) how do you copy memory areas and structured data from one
data block to another?
https://support.industry.siemens.com/cs/ww/en/view/42603881
See also
Overview of the valid data types
Switching display formats in the program status
VariantGet: Read out VARIANT tag value (S7-1200, S7-1500)
Memory areas (S7-1500)
(Not available)
Basics of SCL
Memory areas (S7-1200)