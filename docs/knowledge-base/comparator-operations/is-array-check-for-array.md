# IS_ARRAY: Check for ARRAY

**Category:** Comparator operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 74  

---

IS_ARRAY: Check for ARRAY (S7-1200, S7-1500)
IS_ARRAY: Check for ARRAY
Description
You can use the "Check for ARRAY" instruction to query whether the VARIANT points to a
tag of the ARRAY data type.
You can only use the "Check for ARRAY" instruction within an IF instruction.
Parameters
The following table shows the parameters of the "Check for ARRAY" instruction:
Parameters Declaration Data type Memory area Description
L (The declara-
tion is possible
in the "Input",
Operand that is queried
<operand> Input VARIANT "InOut" and
for ARRAY
"Temp" sections
of the block in-
terface.)
Function value UDINT I, Q, M, D, L Result of the instruction
For additional information on valid data types, refer to "See also".
Note
Checking an ARRAY data block
If you use the IS_ARRAY instruction with an ArrayDB and generate the VARIANT input
parameter via DB_ANY_TO_VARIANT , a symbolic use of the ArrayDB must be present
elsewhere in the program as an actual parameter of a formal parameter of the data type
VARIANT. To work correctly, it is sufficient if the point of use is downloaded. It is not nec-
essary to execute it.
Example
The following example shows how the instruction works:
SCL
IF IS_ARRAY(#Tag_VARIANTToArray) THEN
"Tag_Result" := CountOfElements(#Tag_VARIANTToArray);
END_IF;
If the tag to which the VARIANT points is an ARRAY, the number of ARRAY elements is
output.
See also
Overview of the valid data types
Switching display formats in the program status
Basic information on VARIANT (S7-1200, S7-1500)
Memory areas (S7-1500)
Basics of SCL
IS_ARRAY: Check for ARRAY (S7-1200, S7-1500)
Memory areas (S7-1200)
Example of programming a queue (FIFO) (S7-1200, S7-1500)