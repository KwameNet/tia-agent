# UFILL_BLK: Fill block uninterruptible

**Category:** Move operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 122  

---

UFILL_BLK: Fill block uninterruptible (S7-1200, S7-1500)
UFILL_BLK: Fill block uninterruptible
Description
You can use the "Fill block uninterruptible" instruction to fill a memory area (target range)
with the value of the IN input. The instruction cannot be interrupted. The target range is fil-
led beginning with the address specified at the OUT output. The number of repeated copy
operations is specified with the COUNT parameter. When the instruction is executed, the
value at the input IN is moved to the target range as often as specified by the value of the
COUNT parameter.
The instruction can only be executed if the source range and the target range have the
same data type.
Note
The move operation cannot be interrupted by other operating system activities. This is
why the alarm reaction times of the CPU increase during the execution of the "Fill block
uninterruptible" instruction.
The maximum number of elements changed is the number of elements in the ARRAY or
structure. If you copy more data than there are elements at the OUT output, you will get an
unintended result.
Note
Use of ARRAYs
The instruction reads the content from the selected element from the source range and
copies this content n-times (n = depending on the value at the parameter COUNT) to the
destination, starting at the specified index.
You use the "Fill block uninterruptible" instruction to move a maximum of 16 KB. Note the
CPU-specific restrictions for this.
Filling structures
As well as the elements of an ARRAY, you can also fill multiple elements of a structure
(STRUCT, PLC data type) with the same value. The structure whose elements you want to
fill must only contain individual elements of the same elementary data type. The structure
itself can, however, be embedded in another structure.
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
S7-1200 S7-1500
Binary num- Binary num-
bers, inte- bers, integers,
gers, float- floating-point
Element used to
ing-point numbers, I, Q, M, D,
IN Input fill the target
numbers, timers, DATE, L, P
range
timers, CHAR,
DATE, WCHAR,
CHAR, TOD, LTOD
UFILL_BLK: Fill block uninterruptible (S7-1200, S7-1500)
WCHAR,
TOD
Number of re-
USINT, UINT, USINT, UINT, I, Q, M, D,
COUNT Input peated move
UDINT UDINT, ULINT L, P
operations
Binary num-
Binary num-
bers, inte-
bers, integers,
gers, float-
floating-point Address in tar-
ing-point
numbers, get range from
OUT Output numbers, D, L
timers, DATE, which filling
timers, TOD,
CHAR, starts
DATE,
WCHAR,
CHAR,
TOD, LTOD
WCHAR
For additional information on valid data types, refer to "See also".
Example with an ARRAY
The following example shows how the instruction works when you want to fill an ARRAY:
SCL
UFILL_BLK(IN := #FillValue,
COUNT := "Tag_Count",
OUT => #TargetArea[1]);
The following table shows how the instruction works using specific operand values:
Parameters Operand Value
The data type of the oper-
IN FillValue
and is INT.
COUNT Tag_Count 3
The data type of the oper-
and TargetArea is AR-
OUT TargetArea RAY[1..5] of INT. It consists
of five elements of data type
INT.
The instruction copies the value of the operand #FillValue to the #TargetArea output tag
three times, starting with the first element. The move operation cannot be interrupted by
other operating system activities.
Examples with a structure
The following examples show how the instruction works when you want to fill a structure:
Create a global data block with the following elements:
Data_block_1 Data type
MyStruct1 STRUCT
Member_1 INT
Member_2 INT
UFILL_BLK: Fill block uninterruptible (S7-1200, S7-1500)
Member_3 INT
Member_4 INT
MyStruct2 STRUCT
SubArray ARRAY[1..2] of STRUCT
SubArray[1] STRUCT
NestedStruct STRUCT
Member_1 INT
Member_2 INT
Member_3 INT
Member_4 INT
SubArray[2] STRUCT
Nested-
STRUCT
Struct
Member_1 INT
Member_2 INT
Member_3 INT
Member_4 INT
Generate the following program code to address the MyStruct1 tag:
SCL
UFILL_BLK(IN := 10,
COUNT := 2,
OUT => "Data_block_1".MyStruct1.Member_2);
Generate the following program code to address the MyStruct2 tag:
SCL
UFILL_BLK(IN := 10,
COUNT := 2,
OUT => "Data_block_1".MyStruct2.SubArray[1].NestedStruct.Member_2);
In each of the two examples, the value 10 of the IN parameter is copied twice to the oper-
and at the OUT parameter, starting with the element Member_2. This means that the value
10 is copied to the elements Member_2 and Member_3. The other two elements, Mem-
ber_1 and Member_4, are not changed.
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)