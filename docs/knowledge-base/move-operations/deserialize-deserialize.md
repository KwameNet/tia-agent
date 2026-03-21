# Deserialize: Deserialize

**Category:** Move operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 100  

---

Deserialize: Deserialize (S7-1200, S7-1500)
Deserialize: Deserialize
Description
You can use the "Deserialize" instruction to convert back the sequential representation of a
PLC data type (UDT), STRUCT or ARRAY of <data type> and to fill its entire contents. You
can use the instruction to convert multiple serialized data areas back to their deserialized
representation form.
If you only want to convert back a single sequential representation of a PLC data type
(UDT), STRUCT or ARRAY of <data type>, you can also directly use the instruction
"TRCV: Receive data via communication connection".
The memory area "SRC_ARRAY" in which the sequential representation of a PLC data
type (UDT), STRUCT or ARRAY of <data type> is located must have the ARRAY of BYTE
or ARRAY of CHAR data type and be declared with standard access in version 1.0. The
capacity of the standard memory area is 64 KB. Make sure that there is enough memory
space prior to the conversion. Optimized memory areas are also permitted as of version
2.0.
When the "SRC_ARRAY" memory area is filled using the "Serialize" instruction, any re-
quired filling bytes are automatically inserted. If you fill the "SRC_ARRAY" memory area by
other means, you need to insert any required filling bytes yourself. Filling bytes are ignored
during deserialization, regardless of whether "SRC_ARRAY" is in an optimized or a stand-
ard memory area.
It is recommended to define the low limit of the ARRAY with "0", because then the index
within ARRAY corresponds to the value of the POS parameter, e.g. ARRAY[0] = POS 0.
The description and example below have been formed based on this.
Size of the memory area
The alignment rules mean that simple structures in the optimized memory area do not con-
tain filling bytes. This results in a structure in the optimized memory area being smaller
than in the standard memory area. ARRAYs of structures and nested structures contain fill-
ing bytes. There is no general rule as to the memory area in which a composed structure
requires more space.
Applies to CPUs of the S7-1500 series:
For a block with the block property "Optimized block access", the BOOL has a length of 1
byte. A structure that consists largely of the data type BOOL can therefore be larger in the
optimized memory area than in the standard memory area. Composed structures with a
low proportion of BOOL data types are smaller in the optimized memory area than in the
standard memory area.
Note
Serialize several structures on a CPU S7-1200
If you want to serialize several structures in a buffer on a CPU S7-1200 and want to
communicate with them (for example, with a control system or a CPU S7-1500), then
you must check whether the return value (index at the POS parameter) is even. If this is
not the case, then you must increment the return value by 1 before serializing the sec-
ond structure because the first structure is not filled with a filling byte.
Example:
Structure consisting of 1 DWORD and 1 BYTE
Deserialize: Deserialize (S7-1200, S7-1500)
The start address for the next serialized structure in the buffer is 5. Add +1 so that the
start address is even.
Optimized memory area
To deserialize larger structures, you can declare the memory area for sequential represen-
tation with optimized access for a CPU of the S7-1200 series as of firmware version >=
4.2, and for a CPU of the S7-1500 series as of firmware version >= 2.0. The content of the
sequential representation remains unchanged, as for a standard memory area. Only sym-
bolic access to the bytes in the ARRAY is possible.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
ARRAY of BYTE or AR-
RAY of CHAR in which
I, Q, D, block in- the data stream which is
ARRAY[*] of
terface of an FB to be deserialized is
BYTE 1) (the sections In- saved.
SRC_AR- put, Output,
Input or
RAY Static and Temp S7-1500:
ARRAY of are possible).
For optimum perform-
CHAR
No I/O data ance, do not provide the
parameter with a VAR-
IANT pointer.
Tag to which the deserial-
ized data is to be written.
I, Q, D, block in-
S7-1500:
DEST_VAR terface of an FB
InOut all data types
IABLE For optimum perform-
No I/O data
ance, do not provide the
parameter with a VAR-
IANT pointer.
The operand at the POS
parameter stores the in-
dex of the first byte
based on the number of
POS InOut DINT I, Q, M, D, L
bytes occupied by the
converted customer data.
The POS parameter is
calculated zero-based.
Function value INT I, Q, M, D, L Error information
1) Possible for a CPU of the S7-1200 series as of firmware version >= 4.2, and for a CPU
of the S7-1500 series as of firmware version >= 2.0.
RET_VAL parameter
The following table shows the meaning of the values of the RET_VAL parameter:
Error code* Explanation
(W#16#...)
Deserialize: Deserialize (S7-1200, S7-1500)
0000 No error
The memory areas for the SRC_ARRAY and DEST_VARIABLE parameters
80B0
overlap.
8136 The tag at the SRC_ARRAY parameter is not in a block with standard access.
The VARIANT data type at the SRC_ARRAY parameter contains a ZERO
8150
pointer.
8151 No valid reference at the SRC_ARRAY parameter
8153 There is not enough free memory available at the SRC_ARRAY parameter.
8154 Invalid data type at the SRC_ARRAY parameter
8250 A NULL pointer was transferred at parameter DEST_ARRAY.
8251 No valid reference at the DEST_VARIABLE parameter
8382 The value at the POS parameter is outside the limits of the array.
* The error codes can be displayed as integer or hexadecimal values in the program edi-
tor. You can find information on switching the display formats under "See also".
Special features as of firmware version 4.2 (S7-1200) and 2.0 (S7-1500):
The following error code has a different meaning:
Error code* Explanation
(W#16#...)
8136 Access to the memory area at the SRC_ARRAY parameter is invalid.
* The error codes can be displayed as integer or hexadecimal values in the program edi-
tor. You can find information on switching the display formats under "See also".
Special features as of firmware version 2.8 (S7-1500):
To improve the performance of the "Deserialize" instruction (Version 2.1), do not provide
the SRC_ARRAY and DEST_VARIABLE parameters with a VARIANT pointer but with a
specific data type instead.
Note that the error response to the instruction changes as a result: In individual error sce-
narios, the CPU does not output any error codes but changes to STOP with an access er-
ror. To avoid this, use the local error handling with the instructions "GET_ERROR" and
"GET_ERR_ID".
Special features as of firmware version 2.2 (S7-1200/S7-1500):
With the "Deserialize" instruction (version 2.2), it is no longer permissible to interconnect an
element of a technology object (e.g. TO_SpeedAxis.Statusword) to input or output parame-
ters (SRC_ARRAY/DEST_VARIABLE).
Special features as of firmware version 2.1 (S7-1200/S7-1500):
The optimized version of the "Deserialize" instruction (as of version V2.1) requires more
work memory than its predecessor version due to the complexity of the processed data.
Example
The following table shows the declaration of the operands:
Operand Data type Declaration
Deserialize: Deserialize (S7-1200, S7-1500)
In the "Input" section of the
DeliverPos INT block interface of an FB or
FC.
BufferPos DINT
In the "Temp" section of the
Error INT block interface of an FB or
FC.
Label STRING[4]
The following table shows the declarations of the PLC data types:
Name of the PLC data types Name Data type
Number DINT
Article Declaration STRING
Colli INT
Title INT
Client First name STRING[10]
Surname STRING[10]
The following table shows the declaration of the data blocks:
Name of the data blocks Name Data type
Client "Client" (PLC data type)
Array[0..10] of "Article" (PLC
Target Article
data type)
Bill Array[0..10] of INT
Buffer Field Array[0..294] of BYTE
The following example shows how the instruction works:
SCL
#Tag_RetVal := Deserialize(SRC_ARRAY := "Buffer".Field,
DEST_VARIABLE := "Target".Client,
POS := #BufferPos);
#Tag_RetVal := Deserialize(SRC_ARRAY := "Buffer".Field,
DEST_VARIABLE := #Label,
POS := #BufferPos);
IF #Label = 'arti' THEN
#Tag_RetVal := Deserialize(SRC_ARRAY := "Buffer".Field,
DEST_VARIABLE := "Target".Article[#DeliverPos],
POS := #BufferPos);
ELSIF #Label = 'Bill' THEN
#Tag_RetVal := Deserialize(SRC_ARRAY := "Buffer".Field,
DEST_VARIABLE := "Target".Bill[#DeliverPos],
POS := #BufferPos);
Deserialize: Deserialize (S7-1200, S7-1500)
;
ELSE
;
END_IF;
The "Deserialize" instruction deserializes the sequential representation of the customer da-
ta from the "Buffer" tag and writes it to the "Target" tag. The #BufferPos operand stores the
index of the first byte based on the number of bytes occupied by the converted customer
data.
The "Deserialize" instruction deserializes the sequential representation of the separator
sheet, which was stored in the sequential representation after the customer data, from the
"Buffer" tag and writes the characters to the #Label operand. The characters are compared
using comparison instructions "arti" and "Bill". If the comparison for "arti" = TRUE, these
are article data that have been deserialized and written to the "Target" tag. If the compari-
son for "Bill" = TRUE, these are billing data that have been deserialized and written to the
"Target" tag.
See also
Overview of the valid data types
Switching display formats in the program status
Basics of PLC data types (UDT)
Structure of an ARRAY tag
Structure of a STRUCT tag
Structure of a STRING tag
Padding bytes when using structured data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)
(Not available)