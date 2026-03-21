# MoveFromResolvedSymbol: Read value from resolved symbol

**Category:** Symbolic move  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 198  

---

MoveFromResolvedSymbol: Read value from resolved symbol (S7-1500)
MoveFromResolvedSymbol: Read value from re-
solved symbol
Description
Use the "Read value from resolved symbol" instruction to read the value of a tag that is
referenced by a resolved symbol and write it to a target tag.
The "SRC" parameter has the data type ResolvedSymbol. It points to a tag in the PLC pro-
gram that is read. The tag must have been resolved before with the "ResolveSymbols" in-
struction.
The "DST" parameter has the data type VARIANT. It points to a tag that is written.
Source and target tag must have the same data type.
Note
Query data type of a referenced tag
Before you execute "Write value into resolved symbol", you can use the instruction
"TypeOf" to query the data type of the tag that is referenced by ResolvedSymbol.
See also: TypeOf: Check data type of a VARIANT or ResolvedSymbol tag
Parameters
The following table shows the parameters of the instruction "Read value from resolved
symbol":
Parameter Declaration Data type Memory area Description
Source tag that
ResolvedSym- is referenced by
SRC Input D, L
bol the resolved
symbol.
DST Output Variant I, Q, M, D, L Target tag:
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
MoveFromResolvedSymbol: Read value from resolved symbol (S7-1500)
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
MoveFromResolvedSymbol(SRC := "MySrcDB".Input_ResolvedSymbol,
DST => "MyTargetDB".Output_Variant);
The following table shows how the instruction works using specific operand values:
Parameter Operand Value/Data type
SRC Input_ResolvedSymbol ResolvedSymbol
DST Output_Variant Variant
The value of the tag that is referenced by the "SRC" parameter is read and written to the
tag at the "DST" parameter.
See also
Symbolic access during runtime (S7-1500)