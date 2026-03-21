# MoveResolvedSymbolsToBuffer: Read values from resolved symbols and write them into buffer

**Category:** Symbolic move  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 200  

---

MoveResolvedSymbolsToBuffer: Read values from resolved symbols and write them into buffer (S7-1500)
MoveResolvedSymbolsToBuffer: Read values from
resolved symbols and write them into buffer
Description
With the "Read values from resolved symbols and write them into buffer" instruction, you
read values from several resolved symbols and write them into a memory area (Array of
BYTE). In this way, you can prepare values of resolved symbols for further use, such as
sending using communication instructions, e.g. TSEND.
The "src" parameter is an Array of ResolvedSymbol (SDT). It contains references to tags
that were previously resolved with the instruction "ResolveSymbols". At the parameter
"dst", specify an Array of BYTE. It serves as a target buffer to which the tag values are writ-
ten.
Use the parameters "firstIndex" and "lastIndex" to restrict the list of values that are to be
copied during this call of the instruction. During a later call, you can copy the values of oth-
er resolved symbols. If you do not wish to restrict the list, the value at the "firstIndex" pa-
rameter must correspond to the low limit of the list and the value at the "lastIndex" parame-
ter must correspond to the high limit of the list.
The value at the "mode" parameter defines the memory format at the "dst" parameter.
With offsets, you determine where the values of the resolved symbols are stored in the
destination buffer. You specify the offsets at the "dstOffsets" (Array of DINT) parameter.
Each offset is a bit offset and determines the first bit from which the value is written to the
buffer. You can store several values of the BOOL data type in one byte. Values of all other
data types must start at a bit position divisible by 8.
If the "src[i]" parameter references a tag of the REAL data type, the value at the "dstOff-
sets[i]" parameter is 88 and 2#1 at the "mode" parameter, for example, then the "Read val-
ues from resolved symbols and write them into buffer" instruction copies the value into the
bytes 11 to 14, starting with the least significant byte.
If the "src[i]" parameter references a tag of the BOOL data type and the value at the
"dstOffsets[i]" parameter is 29, for example, then the "Read values from resolved symbols
and write them into buffer" instruction copies the value into byte 3, offset 5 of the destina-
tion memory.
The arrays at "dstOffsets" and "src" must have the same limits to ensure that "dstOffsets[i]"
contains the offset for "src[i]".
This way, you can define the structure of the target buffer very specifically. However, note
that the instruction does not verify whether the specified offsets overlap. An error is not sig-
naled in this case and the content of the target buffer is indeterminate.
The "status" parameter is an Array of INT. It must have the same limits as the "src" and
"dstOffsets" parameters to ensure that "status[i]" contains the status for "src[i]".
Note
Invalid references in the SDT "ResolvedSymbol"
The references in the SDT "ResolvedSymbol" can become invalid when the referenced
tags are overwritten by loading in "RUN". The references may point to tags that no lon-
ger exist. An error code at the "status" parameter indicates invalid references.
In this case, resolve the symbols again with the instruction "ResolveSymbols".
Parameters
MoveResolvedSymbolsToBuffer: Read values from resolved symbols and write them into buffer (S7-1500)
The following table shows the parameters of the "Read values from resolved symbols and
write them into buffer" instruction:
Parameter Declaration Data type Memory area Description
EN Input BOOL I, Q, M, D, L Enable input
ENO Output BOOL I, Q, M, D, L Enable output
Index of the first
firstIndex Input DINT I, Q, M, D, L resolved symbol
to be copied.
Index of the last
lastIndex Input DINT I, Q, M, D, L resolved symbol
to be copied.
Array of Resol- List of resolved
src Input D, L
vedSymbol symbols
Contains a bit
offset for each
dstOffsets Input Array of DINT I, Q, M, D, L element in the
destination buf-
fer
Memory format:
• 2#0 = Big-En-
mode Input DWORD I, Q, M, D, L dian
• 2#1 = Little-
Endian
Target buffer to
which the values
dst InOut Array of BYTE D, L of the resolved
symbols are
copied
Contains a copy
status InOut Array of INT D, L status for each
resolved symbol
Error informa-
Function value (RET_VAL) INT I, Q, M, D, L
tion
You can find additional information on SDT here: System data type ResolvedSymbol
Status parameter
The following table shows the meaning of the values of the Status[i] parameter:
Error code*(W#16#...) Explanation
8020 The symbol was not resolved.
The reference points to a tag that no longer exists. It may have been
8024
overwritten by a loading in "RUN".
8031 src[i]: The data block does not exist.
src[i]: The data block is only available in the load memory and cannot
8033
be addressed by the instruction.
8054 src[i]: The data type of the referenced tag is not supported.
MoveResolvedSymbolsToBuffer: Read values from resolved symbols and write them into buffer (S7-1500)
dstOffsets[i]: The offset is located outside the ARRAY limits of the dst
8082
parameter.
8085 dstOffsets[i]: The offset does not start at a bit position divisible by 8.
RET_VAL parameter
The following table shows the meaning of the values of the RET_VAL parameter:
Error Explanation
code*(W#16#...)
0000 No error
The value at the "firstIndex" parameter is greater than the value at the
80B3
"lastIndex" parameter.
The parameters "src", "dstOffset" and "status" have different ARRAY
80B4
limits.
The value at the "firstIndex" parameter is outside the limits of the AR-
8182
RAY.
The value at the "lastIndex" parameter is outside the limits of the AR-
8282
RAY.
8331 src: The data block does not exist.
src: The data block is only available in the load memory and cannot be
8333
addressed by the instruction.
8354 scr: Invalid data type.
8431 dstOffsets: The data block does not exist.
dstOffsets: The data block is only available in the load memory and can-
8433
not be addressed by the instruction.
8436 dstOffsets: The ARRAY is not in an optimized memory area.
8454 dstOffsets: Invalid data type.
8631 dst: The data block does not exist.
dst: The data block is only available in the load memory and cannot be
8633
addressed by the instruction.
8634 dst: The data block is write-protected.
8636 dst: The data block is not in an optimized memory area.
8653 dst: The memory area addressed by VARIANT is too small.
8654 dst: Invalid data type.
8731 status: The data block does not exist.
status: The data block is only available in the load memory and cannot
8733
be addressed by the instruction.
8734 status: The data block is write-protected.
8754 status: Invalid data type.
Example
The following example shows how the instruction works:
MoveResolvedSymbolsToBuffer: Read values from resolved symbols and write them into buffer (S7-1500)
SCL
MoveResolvedSymbolsToBuffer(firstIndex := 2,
lastIndex := 7,
src := "MySrcDB".Input_ResolvedSymbols,
dstOffsets := #Input_Offset,
mode := 2#0
dst := "MyTargetDB".InOut_Buffer,
status := #InOut_Status);
The following table shows how the instruction works using specific operand values:
Parameter Operand Value/Data type
firstIndex - 2
lastIndex - 7
Array[0..99] of Resolved-
src Input_ResolvedSymbols
Symbol
dstOffsets Input_Offset Array[0..99] of Dint
mode - 2#0
dst InOut_Buffer Array[0..99] of Byte
status InOut_Status Array[0..99] of Int
If the operand "TagIn" has the signal state "1", the instruction is executed. The values of
the resolved symbols in the array "Input_ResolvedSymbols" are written in Big-Endian for-
mat to the target buffer "#InOut_Buffer" at the "dst" parameter.
Using the two constants at the "firstIndex" and "lastIndex" parameters, you restrict the
number of tags whose values are to be copied.
The operand "Input_Offset" contains an offset for each value to be written. Based on the
offset, it is determined where the value of the resolved symbol is written in the target buffer.
See also
Symbolic access during runtime (S7-1500)