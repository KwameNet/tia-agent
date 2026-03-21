# System data type ResolvedSymbol

**Category:** Symbolic move  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 193  

---

System data type ResolvedSymbol (S7-1500)
System data type ResolvedSymbol
Description
The system data type "ResolvedSymbol" is required to execute the "Symbolic access dur-
ing runtime" function. It saves references to tags in the PLC program as well as status in-
formation for generating the references. The structure can be copied but access to the indi-
vidual elements is in read-only mode.
Note
Query data type of a referenced tag
You can use the "TypeOf" instruction to query the data type of a tag that is referenced by
"ResolvedSymbol".
See also: TypeOf: Check data type of a VARIANT or ResolvedSymbol tag
Structure of the system data type
The system data type "ResolvedSymbol" has the following visible elements:
Parameter name Data type Description
resolved BOOL Provide information on whether the symbol was
successfully resolved:
• resolved = FALSE + status = 0
=> No attempt has been made yet to resolve
the symbol.
• resolved = FALSE + status <> 0
=> The resolution of the symbol failed
The value of the status parameter indicates
the structure level within the tag that could not
be resolved.
A negative value means that the symbol could
status INT
not be resolved, e.g. due to range violations.
A positive value means that the tag is not ac-
cessible or not writable by HMI/OPC UA or
Web API.
• resolved = TRUE + status = 0
=> Symbol was successfully resolved.
• resolved = TRUE + status = 0
=> Symbol was successfully resolved but the
DB was overwritten by a subsequent loading
in "RUN".
The element "resolved" indicates whether a symbol was successfully resolved. The status
parameter has the value "0" and the "ResolvedSymbol" structure contains reliable informa-
tion only if the symbol has been successfully resolved.
In addition to these two parameters, the system data type contains internal parameters that
save the information on data type, length and address of the tags. However, you cannot
access these parameters.
If the resolution of the symbol was faulty, the "status" parameter returns the following val-
ues:
System data type ResolvedSymbol (S7-1500)
Error code Explanation
(W#16#...)
8021 The WSTRING for the symbolic tag name is empty.
8022 The WSTRING for the symbolic tag name contains invalid characters.
8023 The data type declaration of the referenced tag is missing.
The reference points to a tag that no longer exists. It may have been
8024
overwritten by a loading in "RUN".
8054 The data type of the referenced tag is not supported.
80B4 The project is inconsistent.
Invalid references in the system data type "ResolvedSymbol"
The references in the SDT "ResolvedSymbol" can become invalid if the referenced tags
are overwritten by a loading in "RUN". The references may point to tags that no longer ex-
ist. An error code at the "status" parameter indicates invalid references.
The following example shows how you can interrupt the execution of a Move instruction in
case of an error and resolve the symbol once again:
Before calling the „MoveResolvedSymbolsToBuffer“ instruction, a check is performed to de-
termine whether the symbol was resolved successfully and „MoveResolvedSymbolsToBuff-
er“ can be executed.
Even if a symbol was resolved successfully, errors can occur when executing „MoveResol-
vedSymbolToBuffer“, for example, when a tag is overwritten by a loading in RUN. In this
case, the return value „err“ provides the number of failed copy processes.
If a failed copy process is recognized, the subsequent IF instruction sets "EnableMove" to
FALSE so that "MoveResolvedSymbolsToBuffer" is no longer executed.
System data type ResolvedSymbol (S7-1500)
Afterwards, a check is performed with a FOR instruction to determine which symbols sup-
ply an error code. For these symbols, the error code is copied to the "status" parameter.
At the same time, the „resolved“ parameter is set to FALSE. Now you have to resolve the
symbol with the asynchronously operating „ResolveSymbols“ instruction again.
See also
Symbolic access during runtime (S7-1500)