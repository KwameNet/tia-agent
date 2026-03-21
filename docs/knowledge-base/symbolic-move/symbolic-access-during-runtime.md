# Symbolic access during runtime

**Category:** Symbolic move  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 186  

---

Symbolic access during runtime (S7-1500)
Symbolic access during runtime
Application
The "Symbolic access during runtime" function gives external applications access to the
tags in the PLC program during runtime. External applications can be, for example, HMI
applications, OPC UA functions or other communication functions. The tags can be read or
written.
Read or write access is not programmed statically when the program is being created. In
fact, access takes place dynamically during runtime. Users enter the symbolic tag names
they wish to access manually or program-controlled during runtime.
The "Symbolic access during runtime" function can process optimized as well as non-opti-
mized data. This means it is more flexible and efficient than an ANY pointer that can only
access non-optimized data.
The function enables, for example, the tracing of tags through external devices or applica-
tions.
Constraints
The following constraints apply to the symbolic access during runtime:
• The symbolic access is only available for S7-1500 as of firmware version V3.0.
• You can access PLC tags (I, Q, M) and elements of data blocks.
• You can access elementary data types and individual elements of a structured data type.
• The tags must have the attribute "Accessible from HMI/OPC UA/Web API" or "Writable
from HMI/OPC UA/Web API".
• The following types of access are not possible:
o Access to structured data types, e.g., ARRAY or STRUCT.
o Access to the data types WSTRING, STRING and DTL
o Access to I/O (tag name:P)
o Access to elements of a technology object
Principle of operation
Two steps are necessary to access tags during runtime:
1. The symbolic tag names that are entered via HMI, for example, must be "resolved".
This means that references to the respective tags are created in the PLC program.
References are typed pointers by which you can address the tags in the PLC pro-
gram. To resolve the symbolic tags, use the asynchronous instruction "ResolveSym-
bols".
2. Special Move instructions are available to read or write the tag values. The Move in-
structions are synchronous instructions. They address the tags using the previously
generated references.
Symbolic access during runtime (S7-1500)
Example of step 1
The following example shows how the symbolic tag names are resolved with the instruction
"ResolveSymbols":
• At the parameter "nameList", specify an Array of WSTRING that contains the tag names
you wish to resolve.
• At the parameter "referenceList", specify an Array of ResolvedSymbol (SDT) in which the
references to the tags are saved.
• The two Arrays must have the same limits.
As a result, you receive a reference for each symbolic tag name at the parameter "refer-
enceList". The reference is contained in a structure of the system data type "ResolvedSym-
bol".
See also:
ResolveSymbols: Resolve several symbols
System data type ResolvedSymbol
Symbolic access during runtime (S7-1500)
Example of step 2
The following example shows you how to read the tag values with the instruction "MoveRe-
solvedSymbolsToBuffer" and write them to a buffer:
• At the parameter "src", specify the Array of ResolvedSymbol (SDT) that contains the ref-
erences to the resolved tags.
• At the parameter "dst", specify an Array of BYTE . It serves as a target buffer to which
the tag values are written.
When you execute the instruction "MoveResolvedSymbolsToBuffer", the tag values are
read using the references and written to the target buffer.
See also:
MoveResolvedSymbolsToBuffer: Read values from resolved symbols and write them
into buffer