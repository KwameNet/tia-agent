# CONVERT: Convert value

**Category:** Conversion operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 223  

---

CONVERT: Convert value (S7-1200, S7-1500)
CONVERT: Convert value
Description
Use the "Convert value" instruction to program explicit conversions. When the instruction is
inserted, the "CONVERT" dialog opens. You specify the source data type and the destina-
tion data type of the conversion in this dialog. The source value is read and converted to
the specified destination data type.
Conversion options for bit strings
BYTE and WORD bit strings cannot be selected in the instruction box. However, it is possi-
ble to specify an operand of data type DWORD or LWORD at a parameter of the instruction
if the lengths of the input and output operands match. The operand is then interpreted by
the data type of a bit string according to the data type of the input or output parameter and
is implicitly converted. The data type DWORD is for example interpreted as DINT/UDINT
and LWORD as LINT/ULINT. These conversion options are also available to you when
"IEC check" is enabled.
Note
For CPUs of the S7-1500 series: The data types DWORD and LWORD can only be con-
verted to or from data type REAL or LREAL.
During the conversion, the bit pattern of the source value is transferred unchanged, right-
justified, to the target data type. If no errors occur during the conversion, the signal state
of enable output ENO = 1; if an error occurs during processing, the signal state of the
enable output ENO = 0.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
Binary numbers,
integers, float-
ing-point num-
<Source I, Q, M, D, L, P
Input bers, times, date Value to be converted.
type> or constant
and time, char-
acter strings,
BCD16, BCD32
Binary numbers,
integers, float-
ing-point num-
<Destina- I, Q, M, D, L, P
Output bers, times, date Result of the conversion
tion type> or constant
and time, char-
acter strings,
BCD16, BCD32
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
CONVERT: Convert value (S7-1200, S7-1500)
SCL
"Tag_INT" := REAL_TO_INT("Tag_REAL");
The following table shows how the instruction works using specific operand values:
Operand Data type Value
Tag_REAL REAL 20.56
Tag_INT INT 21
During the conversion, the value of the "Tag_REAL" operand will be rounded to the nearest
integer and saved in the "Tag_INT" operand.
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)