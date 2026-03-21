# MoveResolvedSymbolsFromBuffer: Read values from buffer and write them into resolved symbols

**Category:** Symbolic move  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 204  

---

MoveResolvedSymbolsFromBuffer: Read values from buffer and write them into resolved symbols (S7-1500)
MoveResolvedSymbolsFromBuffer: Read values
from buffer and write them into resolved symbols
Description
With the "Read values from buffer and write them into resolved symbols" instruction, you
read values from a memory area (Array of BYTE) and write them into the values of several
resolved symbols. In this way, you can process a memory area that you have received
from a communication instruction, such as TRCV.
The "src" parameter is an Array of BYTE. It serves as the source buffer from which the val-
ues are read. The "dst" parameter is an Array of ResolvedSymbol (SDT). It contains refer-
ences to tags that were previously resolved with the instruction "ResolveSymbols". By us-
ing the references, the tags are to be written with the values from the source buffer.
By using the parameters "firstIndex" and "lastIndex", you restrict the selection of tags in the
list of resolved symbols whose values are to be written. If you do not wish to restrict the list,
the value at the "firstIndex" parameter must correspond to the low limit of the list and the
value at the "lastIndex" parameter must correspond to the high limit of the list.
The value at the "mode" parameter defines the memory format at the "src" parameter.
With offsets, you determine from where the value of the resolved symbol is read and cop-
ied in the source buffer. You specify the offsets at the "srcOffsets" (Array of DINT) parame-
ter. Each offset is a bit offset and determines the first bit from which the value is read from
the buffer. You can store several values of the BOOL data type in one byte. Values of all
other data types must start at a bit position divisible by 8.
If the "dst[i]" parameter references a tag of the REAL data type, the value at the "srcOff-
sets[i]" parameter is 88 and 2#1 at the "mode" parameter, for example, then the "Read val-
ues from resolved symbols and write them into buffer" instruction reads the values from
bytes 11 to 14, starting with the least significant byte.
If the "src[i]" parameter references a tag of the BOOL data type, the value at the "srcOff-
sets[i]" parameter is 29, for example, then the "Read values from resolved symbols and
write them into buffer" instruction reads the value at byte 3, offset 5 of the source buffer.
The arrays at "srcOffsets" and "dst" must have the same limits to ensure that "srcOffsets[i]"
contains the offset for "dst[i]".
This allows you to specify exactly which values are copied from the target buffer. However,
note that the instruction does not verify whether the specified offsets overlap. An error is
not signaled in this case and the read value can be random.
The "status" parameter is an Array of INT. It must have the same limits as the "dst" and
"srcOffsets" parameters to ensure that "status[i]" contains the status for "dst[i]".
Note
Invalid references in the SDT "ResolvedSymbol"
The references in the SDT "ResolvedSymbol" can become invalid when the referenced
tags are overwritten by loading in "RUN". The references may point to tags that no lon-
ger exist. An error code at the "status" parameter indicates invalid references.
In this case, resolve the symbols again with the instruction "ResolveSymbols".
Parameters
The following table shows the parameters of the "Read values from buffer and write them
into resolved symbols" instruction:
MoveResolvedSymbolsFromBuffer: Read values from buffer and write them into resolved symbols (S7-1500)
Parameter Declaration Data type Memory area Description
EN Input BOOL I, Q, M, D, L Enable input
ENO Output BOOL I, Q, M, D, L Enable output
Index of the first
resolved symbol
firstIndex Input DINT I, Q, M, D, L in the target buf-
fer that is to be
written.
Index of the last
resolved symbol
lastIndex Input DINT I, Q, M, D, L in the target buf-
fer that is to be
written.
Memory format
• 2#0 = Big-En-
mode Input DWORD I, Q, M, D, L dian
• 2#1 = Little-
Endian
Source buffer
src Input Array of BYTE D, L from which the
values are read
Offsets of the
srcOffsets Input Array of DINT D, L values in the
source buffer
Target buffer
that contains the
Array of Resol-
dst InOut D, L references to
vedSymbol
the resolved
symbols
Contains a copy
status for each
status InOut Array of INT D, L
value to be writ-
ten
Error informa-
Function value (RET_VAL) INT I, Q, M, D, L
tion
You can find additional information on SDT here: System data type ResolvedSymbol
Status parameter
The following table shows the meaning of the values of the Status[i] parameter:
Error code*(W#16#...) Explanation
8020 The symbol was not resolved.
8021 The WSTRING for the symbolic tag name is empty.
8022 The WSTRING for the symbolic tag name contains invalid characters.
8023 The data type declaration of the referenced tag is missing.
The reference points to a tag that no longer exists. It may have been
8024
overwritten by a loading in "RUN".
MoveResolvedSymbolsFromBuffer: Read values from buffer and write them into resolved symbols (S7-1500)
8054 The reference points to a tag with invalid data type.
80B4 The project is inconsistent.
8031 dst[i]: The data block does not exist.
dst[i]: The data block is only available in the load memory and cannot
8033
be addressed by the instruction.
8034 dst[i]: The data block is write-protected.
8054 dst[i]: The data type of the referenced tag is not supported.
srcOffset[i]: The offset is located outside the ARRAY limits of the src
8082
parameter.
8085 srcOffset[i]: The offset does not start at a bit position divisible by 8.
RET_VAL parameter
The following table shows the meaning of the values of the RET_VAL parameter:
Error Explanation
code*(W#16#...)
0000 No error
The value at the firstIndex parameter is greater than the value at the las-
80B3
tIndex parameter.
80B4 The parameters dst, srcOffsets and status have different ARRAY limits.
8182 The value at the firstIndex parameter is outside the limits of the ARRAY.
8282 The value at the lastIndex parameter is outside the limits of the ARRAY.
8431 src: The data block does not exist.
src: The data block is only available in the load memory and cannot be
8433
addressed by the instruction.
8454 src: Invalid data type.
8531 srcOffsets: The data block does not exist.
srcOffsets: The data block is only available in the load memory and can-
8533
not be addressed by the instruction.
8554 srcOffsets: Invalid data type.
8631 dst: The data block does not exist.
dst: The data block is only available in the load memory and cannot be
8633
addressed by the instruction.
8634 dst: The data block is write-protected.
8636 dst: The data block is not in an optimized memory area.
8654 dst: Invalid data type.
8731 status: The data block does not exist.
status: The data block is only available in the load memory and cannot
8733
be addressed by the instruction.
8734 status: The data block is write-protected.
8736 status: The data block is not in an optimized memory area.
8754 status: Invalid data type.
MoveResolvedSymbolsFromBuffer: Read values from buffer and write them into resolved symbols (S7-1500)
Example
The following example shows how the instruction works:
SCL
MoveResolvedSymbolsFromBuffer(firstIndex := 2,
lastIndex := 7,
mode := 2#0,
src := "MySrcDB".Input_Buffer,
srcOffsets := #Input_Offset,
dst := "MyTargetDB".InOut_ResolvedSymbols,
status := #InOut_Status);
The following table shows how the instruction works using specific operand values:
Parameter Operand Value/Data type
firstIndex - 2
lastIndex - 7
mode - 2#0
src Input_Buffer Array[0..99] of Byte
srcOffsets Input_Offset Array[0..99] of Dint
Array[0..99] of Resolved-
dst InOut_ResolvedSymbols
Symbol
status InOut_Status Array[0..99] of Int
If the operand "TagIn" has the signal state "1", the instruction is executed. The values of
the tags from the source buffer "Input_Buffer" are read in Big-Endian format and written to
the resolved symbols via the references in "#InOut_ResolvedSymbols".
The two constants at the "firstIndex" and "lastIndex" parameters restrict the number of tags
in the target buffer whose values are to be written. Based on the offset in the operand "In-
put_Offset", it is determined from where the value in the source buffer is being read.
See also
Symbolic access during runtime (S7-1500)