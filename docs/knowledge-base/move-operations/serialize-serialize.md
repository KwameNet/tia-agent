# Serialize: Serialize

**Category:** Move operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 105  

---

Serialize: Serialize (S7-1200, S7-1500)
Serialize: Serialize
Description
You can use the "Serialize" instruction to convert several PLC data types (UDT), STRUCT
or ARRAY of <data type> to a sequential representation without losing parts of their struc-
ture.
You use the instruction to temporarily save multiple structured data items from your pro-
gram in a buffer, which should preferably be in a global data block, and send them to an-
other CPU. The memory area in which the converted data is stored must have the ARRAY
of BYTE or ARRAY of CHAR data type and be declared with standard access in version
1.0. Optimized data is also permitted as of version 2.0. Fill data of the source data area is
undefined in the target array. These can be fill bytes or fill bits of a data area (e.g. ARRAY,
STRUCT or PLC data type (UDT)) as well as the characters of a string currently not in use.
The capacity of the standard memory area is 64 KB. Structures that are larger than 64 KB
in accordance with the rules for standard memory areas cannot be serialized if the operand
at the DEST_ARRAY parameter is located in a standard memory area.
It is recommended to define the low limit of the ARRAY with "0", because then the index
within ARRAY corresponds to the value of the POS parameter, e.g. ARRAY[0] = POS 0.
The description and example below have been formed based on this.
The operand at the POS parameter contains information about the number of bytes used
by the converted data.
If you want to send a single PLC data type (UDT), STRUCT or ARRAY of <data type>, you
can directly call the instruction "TSEND: Send data via communication connection".
Note
Comparison of structures
To compare structures, they have to be serialized first. Use a comparison expression in-
stead.
You can find additional information here: Relational expressions
Size of the memory area
The alignment rules mean that simple structures in the optimized memory area do not con-
tain filling bytes. This results in a structure in the optimized memory area being smaller
than in the standard memory area. ARRAYs of structures and nested structures contain fill-
ing bytes. There is no general rule as to the memory area in which a composed structure
requires more space.
Applies to CPUs of the S7-1500 series:
For a block with the block property "Optimized block access", the length of the data type
BOOL depends on the data type that follows. This means if a BYTE follows, for example,
the data type BOOL has a length of 1 byte. When a WORD follows, for example, the data
type BOOL has a length of 2 bytes. A structure that consists largely of the data type BOOL
can therefore be larger in the optimized memory area than in the standard memory area.
Composed structures with a low proportion of BOOL data types are smaller in the opti-
mized memory area than in the standard memory area.
We therefore recommend that the source data area for the serialization starts with the large
data types and ends with Boolean elements. This greatly reduces the filling with filling bits.
Serialize: Serialize (S7-1200, S7-1500)
Note
Serialize several structures on a CPU S7-1200
If you want to serialize several structures in a buffer on a CPU S7-1200 and want to
communicate with them (for example, with a control system or a CPU S7-1500), then
you must check whether the return value (index at the POS parameter) is even. If this is
not the case, then you must increment the return value by 1 before serializing the sec-
ond structure because the first structure is not filled with a filling byte.
Example:
Structure consisting of 1 DWORD and 1 BYTE
The start address for the next serialized structure in the buffer is 5. Add +1 so that the
start address is even.
Optimized memory area
To serialize larger structures, you can declare the memory area with optimized access for a
CPU of the S7-1200 series as of firmware version >= 4.2, and for a CPU of the S7-1500
series as of firmware version >= 2.0. The sequential representation remains unchanged, as
for a standard memory area.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
Tag to be serialized.
I, Q, D, block in- S7-1500:
SRC_VARI- terface of an FB,
Input all data types For optimum perform-
ABLE
No I/O data ance, do not provide the
parameter with a VAR-
IANT pointer.
ARRAY in which the gen-
I, Q, D, block in- erated data stream is
terface of an FB stored.
(the sections In-
ARRAY of BYTE
DEST_AR- put, Output, S7-1500:
InOut or ARRAY of
RAY Static and Temp
CHAR For optimum perform-
are possible).
ance, do not provide the
No I/O data parameter with a VAR-
IANT pointer.
The operand at the POS
parameter stores the in-
dex of the first byte
based on the total num-
POS InOut DINT I, Q, M, D, L ber of bytes that the con-
verted customer data has
occupied. The POS pa-
rameter is calculated
zero-based.
Function value INT I, Q, M, D, L Error information
Serialize: Serialize (S7-1200, S7-1500)
RET_VAL parameter
The following table shows the meaning of the values of the RET_VAL parameter:
Error code* Explanation
(W#16#...)
0000 No error
The memory areas for the SRC_VARIABLE and DEST_ARRAY parameters
80B0
overlap.
The VARIANT data type at the SRC_VARIABLE parameter contains a ZERO
8150
pointer.
8151 No valid reference at the SRC_VARIABLE parameter
8236 The tag at the SRC_ARRAY parameter is not in a block with standard access.
8250 A NULL pointer was transferred at parameter DEST_ARRAY.
8251 No valid reference at the DEST_ARRAY parameter
The tag at the parameter DEST_ARRAY does not provide enough memory
space for the content of the tag at the parameter SRC_VARIABLE. Due to the
8253 input value of the tag at parameter POS the available memory space is re-
duced. The input value of the parameter POS decides where to begin within
the tag at the parameter DEST_ARRAY.
8254 Invalid data type at the DEST_ARRAY parameter
8382 The value at the POS parameter is outside the limits of the array.
* The error codes can be displayed as integer or hexadecimal values in the program edi-
tor. You can find information on switching the display formats under "See also".
Special features as of firmware version 4.2 (S7-1200) and 2.0 (S7-1500):
The following error code has a different meaning:
Error code* Explanation
(W#16#...)
8236 Access to the memory area at the DEST_ARRAY parameter is invalid.
* The error codes can be displayed as integer or hexadecimal values in the program edi-
tor. You can find information on switching the display formats under "See also".
Special features as of firmware version 2.8 (S7-1500):
To improve the performance of the "Serialize" instruction (Version 2.1), do not provide the
SRC_VARIABLE and DEST_ARRAY parameters with a VARIANT pointer but with a specif-
ic data type instead.
Note that the error response to the instruction changes as a result: In individual error sce-
narios, the CPU does not output any error codes but changes to STOP with an access er-
ror. To avoid this, use the local error handling with the instructions "GET_ERROR" and
"GET_ERR_ID".
Special features as of firmware version 2.2 (S7-1200/S7-1500):
With the "Serialize" instruction (version 2.2), it is no longer permissible to interconnect an
element of a technology object (e.g. TO_SpeedAxis.Statusword) to input or output parame-
ters (SRC_VARIABLE/DEST_ARRAY).
Special features as of firmware version 2.1 (S7-1200/S7-1500):
Serialize: Serialize (S7-1200, S7-1500)
The optimized version of the "Serialize" instruction (as of version V2.1) requires more work
memory than its predecessor version due to the complexity of the processed data.
Example
The following example shows how the instruction works:
SCL
#Tag_RetVal := Serialize(SRC_VARIABLE := "Source".Client,
DEST_ARRAY := "Buffer".Field,
POS := #BufferPos);
#Label := STRING_TO_WSTRING('arti');
#Tag_RetVal := Serialize(SRC_VARIABLE := #Label,
DEST_ARRAY := "Buffer".Field,
POS := #BufferPos);
#Tag_RetVal := Serialize(SRC_VARIABLE := "Source".Article[#DeliverPos],
DEST_ARRAY := "Buffer".Field,
POS := #BufferPos);
The "Serialize" instruction serializes the customer data from the "Source" tag and writes
the sequential representation to the "Buffer" tag. The index of the next unwritten byte of the
"Buffer".Field operand is saved in the #BufferPos operand.
A kind of separator sheet is inserted now to make it easier to deserialize the sequential
representation later. The "Move character string" instruction moves the "arti" characters to
the #Label operand. The "Serialize" instruction writes these characters after the customer
data to the "Buffer" tag. The value of the #BufferPos operand is incremented accordingly.
The "Serialize" instruction serializes the data of a specific article, which is calculated in run-
time, from the "Source" tag and writes it in sequential representation to the "Buffer" tag af-
ter the "arti" characters.
The following table shows the declaration of the operands:
Operand Data type Declaration
In the "Input" section of the
DeliverPos INT
block interface
In the "Temp" section of the
BufferPos DINT
block interface
In the "Temp" section of the
Error INT
block interface
In the "Temp" section of the
Label STRING[4]
block interface
The following table shows the declarations of the PLC data types:
Name of the PLC data types Name Data type
Article Number DINT
Serialize: Serialize (S7-1200, S7-1500)
Declaration STRING
Colli INT
Title INT
Client First name STRING[10]
Surname STRING[10]
The following table shows the declaration of the data blocks:
Name of the data blocks Name Data type
Client "Client" (PLC data type)
Source
Array[0..10] of "Article" (PLC
Article
data type)
Buffer Field Array[0..294] of BYTE
See also
Overview of the valid data types
Switching display formats in the program status
Basics of PLC data types (UDT)
Structure of an ARRAY tag
Structure of a STRUCT tag
Structure of a STRING tag
Padding bytes when using structured data types
Memory areas (S7-1500)
(Not available)
Basics of SCL
Memory areas (S7-1200)