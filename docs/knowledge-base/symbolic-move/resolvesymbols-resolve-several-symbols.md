# ResolveSymbols: Resolve several symbols

**Category:** Symbolic move  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 189  

---

ResolveSymbols: Resolve several symbols (S7-1500)
ResolveSymbols: Resolve several symbols
Description
With the "Resolve several symbols" instruction, you resolve multiple symbolic tag names.
As a result, you receive references to the tags. References are typed pointers that give you
read or write access to the tags.
At the parameter "nameList", specify an Array of WSTRING . Here, the tag names that are
to be resolved are transferred during runtime. At the parameter "referenceList", specify an
Array of ResolvedSymbol (SDT) in which the references are to be saved. The two Arrays
must have the same limits.
The symbolic tag names are transferred in the format WSTRING and must not exceed a
length of 254 UTF-16 characters. Enter the fully qualified name including the namespace.
The qualifier # for local tags is not supported. Elements in arrays are supported but they
must specify a fixed index for access to an element. Accesses with variable index, e.g.,
myArray[myIndexTag], are not supported.
Example of a fully-qualified name:
myNamespace.mySubnamespace.myDataBlock.myStructured-
Tag.myTagMember
Using the "firstIndex" and "lastIndex" parameters, you can also only resolve some of the
symbols from the list. If you wish to resolve the entire list of symbols, the "firstIndex" pa-
rameter can specify the low limit and the "lastIndex" parameter can specify the high limit.
The instruction is executed asynchronously. The execution starts with a rising signal edge
at the "execute" parameter. You cannot make any changes at the "nameList" and "refer-
enceList" parameters while the instruction is running (Busy = 1).
Before the symbols are resolved, the values are reset in the Array at the "referenceList"
parameter within the specified Array limits ("firstIndex" and "lastIndex").
After the instruction is complete, the "done" parameter has the value = 1 for one cycle. The
array at the "referenceList" parameter is filled with the references to the tags.
After a falling signal edge at the "execute" parameter, the instance of "ResolveSymbol" is
no longer considered active.
Note
Configuration limits
A maximum of 10 "ResolveSymbols" instructions can be active at the same time. In total,
all active instances of the instruction can resolve a maximum of 2,000 symbols.
Parameters
The following table shows the parameters of the "Resolve several symbols" instruction:
Parameter Declaration Data type Memory area Description
I, Q, M, D, L or
EN Input BOOL Enable input
constant
ENO Output BOOL I, Q, M, D, L Enable output
ResolveSymbols: Resolve several symbols (S7-1500)
With a rising sig-
nal edge, execu-
tion of the in-
execute Input BOOL I, Q, M, D, L
struction is star-
ted.
Index of the first
firstIndex Input DINT I, Q, M, D, L tag name to be
resolved.
Index of the last
lastIndex Input DINT I, Q, M, D, L tag name to be
resolved.
Done = 1
done Output BOOL I, Q, M, D, L Execution of the
instruction is
complete.
Busy = 1
busy Output BOOL I, Q, M, D, L The instruction
is currently be-
ing executed.
ERROR = 1
Error occurred
during process-
error Output BOOL I, Q, M, D, L
ing. The error
message is out-
put at the STA-
TUS parameter.
Block status /
error number
status Output INT I, Q, M, D, L
(see "STATUS
parameter")
List of tag
Array of
nameList InOut D names to be re-
WSTRING
solved.
Array of Resol- List of referen-
referenceList InOut D
vedSymbol ces
You can find additional information on SDT here: System data type ResolvedSymbol
STATUS parameter
The following table shows the meaning of the values of the "STATUS" parameter:
Error code (W#16#...) Description
0000 No error occurred.
7000 No job processing active.
7001 Start of asynchronous job processing. Parameter BUSY = 1, DONE = 0
7002 Intermediate call: Instruction already active; BUSY = 1.
The value at the "firstIndex" parameter is greater than the value at the
80B3
"lastIndex" parameter.
ResolveSymbols: Resolve several symbols (S7-1500)
The ARRAY limits of "referenceList" and "nameList" have different lim-
80B4
its.
The maximum possible number of simultaneously active "ResolveSym-
80C3 bols" instructions has already been reached, or the maximum possible
number of 2000 simultaneously resolvable symbols has been reached.
The value at the "firstIndex" parameter is outside the limits of the AR-
8282
RAY.
The value at the "lastIndex" parameter is outside the limits of the AR-
8382
RAY.
The ARRAY of WSTRING that you specified as the actual parameter
8831
for "nameList" does not exist.
The ARRAY of WSTRING that you specified as the actual parameter
8833 for "nameList" is only in the load memory and cannot be addressed by
the instruction.
The ARRAY of WSTRING that you specified as the actual parameter
8836
for "nameList" is not in an optimized global data block.
The actual parameter for "nameList" is not of the data type "ARRAY of
8854
WSTRING".
The ARRAY of ResolvedSymbol that you specified as the actual param-
8931
eter for "referenceList" does not exist.
The ARRAY of ResolvedSymbol that you specified as the actual param-
8933 eter for "referenceList" is only in the load memory and cannot be ad-
dressed by the instruction.
The ARRAY of ResolvedSymbol that you specified as the actual param-
8934
eter for "referenceList" is write-protected.
The ARRAY of ResolvedSymbol that you specified as the actual param-
8936
eter for "referenceList" is not in an optimized global data block.
The actual parameter for "referenceList" is not of the data type "ARRAY
8954
of WSTRING".
Example
The following example shows how the instruction works:
SCL
"ResolveSymbols_DB"(execute := #Input_Execute,
firstIndex := 0,
lastIndex := 9,
done => #Output_Done,
busy => #Output_Busy,
error => _bool_out_,
status => _int_out_,
nameList := "MySrcDB".InOut_Symbols,
referenceList := "MyTargetDB".InOut_ResolvedSymbols);
The following table shows how the instruction works using specific operand values:
ResolveSymbols: Resolve several symbols (S7-1500)
Parameter Operand Value/Data type
nameList InOut_Symbols Array[0..99] of WSTRING
Array[0..99] of Resolved-
referenceList InOut_ResolvedSymbols
Symbol
firstIndex - 0
lastIndex - 9
execute Input_Execute BOOL
busy Output_Busy BOOL
done Output_Done BOOL
If the "TagIn" and #Input_Execute operands have signal state "1", the execution of the in-
struction is started. The tag names at the parameter "nameList" are resolved and the refer-
ences to the tags are written in the operand "#InOut_ResolvedSymbols" at the parameter
"referenceList". After the instruction is complete, the "done" parameter has the value = 1
for one cycle.
See also
Symbolic access during runtime (S7-1500)