# MOVE_BLK: Move block

**Category:** Move operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 110  

---

MOVE_BLK: Move block (S7-1200, S7-1500)
MOVE_BLK: Move block
Description
You can use the "Move block" instruction to move the content of a memory area (source
range) to another memory area (target range). The number of elements to be moved to the
target range is specified with the COUNT parameter. The width of the elements to be
moved is defined by the width of the first element in the source area.
The instruction can only be executed if the source range and the target range have the
same data type.
The value of the OUT output is invalid if the following condition is met:
• More data is moved than is made available at the IN parameter or OUT parameter.
Note
Use of ARRAYs
The instruction copies the contents from the defined element n elements (n = depending
on the value at the parameter COUNT) from the source range to the target range, start-
ing at the specified index.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
S7-1200 S7-1500
Binary
numbers,
Binary num-
integers,
bers, inte-
floating-
gers, floating- The first ele-
point num-
point num- ment of the
bers,
IN 1) Input bers, timers, D, L source area
timers,
DATE, that is being
DATE,
CHAR, copied
CHAR,
WCHAR,
WCHAR,
TOD
TOD,
LTOD
Number of el-
USINT, ements to be
USINT, UINT, UINT, I, Q, M, D, L, copied from
COUNT Input
UDINT UDINT, P the source
ULINT range to the
target range
Binary num- Binary
The first ele-
bers, inte- numbers,
ment of the
gers, floating- integers,
target range
OUT 1) Output point num- floating- D, L
to which the
bers, timers, point num-
contents of
DATE, bers,
the source
CHAR, timers,
MOVE_BLK: Move block (S7-1200, S7-1500)
DATE,
CHAR,
WCHAR, range are be-
WCHAR,
TOD ing copied
TOD,
LTOD
1) The specified data types can only be used as elements of an ARRAY structure.
For additional information on valid data types, refer to "See also".
Example
The following example shows how the instruction works:
SCL
MOVE_BLK(IN := #a_array[2],
COUNT := "Tag_Count",
OUT => #b_array[1]);
The following table shows how the instruction works using specific operand values:
Parameters Operand Value
The data type of the "a_ar-
ray" operand is ARRAY[0..5]
IN a_array[2]
of INT. It consists of six ele-
ments of data type INT.
COUNT Tag_Count 3
The data type of the "b_ar-
ray" operand is ARRAY[0..6]
OUT b_array[1]
of INT. It consists of seven
elements of data type INT.
Starting from the third element, the instruction selects three INT elements from the #a_ar-
ray tag and copies their contents to the #b_array output tag, beginning with the second ele-
ment.
Note
You can find more information on the MOVE_BLK instruction in the following arti-
cle in the Siemens Industry Online Support:
In STEP 7 (TIA Portal) how do you copy memory areas and structured data from one
data block to another?
https://support.industry.siemens.com/cs/ww/en/view/42603881
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)