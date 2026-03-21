# MoveToResolvedSymbol: Write value into resolved symbol

**Category:** Symbolic move  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 196  

---

MoveToResolvedSymbol: Write value into resolved symbol (S7-1500)
MoveToResolvedSymbol: Write value into resolved
symbol
Description
Use the "Write value into resolved symbol" instruction to read the value of a tag and write it
to a target tag that is referenced by a resolved symbol.
The SRC parameter has the data type VARIANT. It points to the tag that is read.
The DST parameter has the data type ResolvedSymbol. It points to a tag in the PLC pro-
gram that is written. The tag must have been resolved before with the "ResolveSymbols"
instruction.
Source and target tag must have the same data type.
Note
Query data type of a referenced tag
Before you execute "Write value into resolved symbol", you can use the instruction
"TypeOf" to query the data type of the tag that is referenced by ResolvedSymbol.
See also: TypeOf: Check data type of a VARIANT or ResolvedSymbol tag
Parameters
The following table shows the parameters of the instruction "Write value to resolved sym-
bol":
Parameter Declaration Data type Memory area Description
SRC Input Variant I, Q, M, D, L Source tag
Target tag that is
ResolvedSym- referenced by
DST Input D, L
bol the resolved
symbol.
Error informa-
Function value (RET_VAL) INT I, Q, M, D, L
tion
Note
Invalid references in the SDT "ResolvedSymbol"
The references in the SDT "ResolvedSymbol" can become invalid when the referenced
tags are overwritten by loading in "RUN". The references may point to tags that no lon-
ger exist. An error code at the "status" parameter indicates invalid references.
In this case, resolve the symbols again with the instruction "ResolveSymbols".
You can find additional information on SDT here: System data type ResolvedSymbol
RET_VAL parameter
The following table shows the meaning of the values of the RET_VAL parameter:
Error Explanation
code*(W#16#...)
MoveToResolvedSymbol: Write value into resolved symbol (S7-1500)
0000 No error
The reference points to a tag that no longer exists. It may have been
8024
overwritten by a loading in "RUN".
The reference points to a data block that does not exist. It may have
8031
been overwritten by a loading in "RUN".
80B4 The data types of source and target tag do not match.
Example
The following example shows how the instruction works:
SCL
MoveToResolvedSymbol(SRC := "MySrcDB".Input_Variant,
DST => "MyTargetDB".Output_ResolvedSymbol);
The following table shows how the instruction works using specific operand values:
Parameter Operand Value/Data type
SRC Input_Variant Variant
DST Output_ResolvedSymbol ResolvedSymbol
The value of the tag at the "SRC" parameter is read and written to the tag that is refer-
enced by the parameter "DST".
See also
Symbolic access during runtime (S7-1500)