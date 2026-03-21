# CountOfElements: Get number of ARRAY elements

**Category:** VARIANT  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 183  

---

CountOfElements: Get number of ARRAY elements (S7-1200, S7-1500)
CountOfElements: Get number of ARRAY elements
Description
You use the "Get number of ARRAY elements" instruction to query how many ARRAY ele-
ments a tag to which VARIANT points has.
If it is a single-dimensional ARRAY, the number of ARRAY elements is output as the result.
(The difference between the high and low limit +1). If it is a multi-dimensional ARRAY, the
number of all dimensions is output as the result.
If you want to query the elements of an ARRAY DB, you should use the instructions "Read-
FromArrayDB" or "WriteFromArrayDB", because here a more precise error evaluation for
the number of elements is possible.
Note
Instances
The VARIANT pointer can point to no instance and therefore also to no multi-instance or
an ARRAY of multi-instances.
Note
ARRAY within a data block
If you want to query the number of the elements of an ARRAY which is located in a data
block, the block attribute "Data block write-protected in the device" must not be activated
in this block. Otherwise, the RET_VAL parameter returns the result "0", irrespective of
how many elements the ARRAY contains.
The result is also "0" if the VARIANT tag is not an ARRAY.
If the VARIANT points to an ARRAY of BOOL, the fill elements are included in the count.
(For example, 8 is returned for an ARRAY[0..1] of BOOL).
Parameters
The following table shows the parameters of the instruction:
Parameters Declaration Data type Memory area Description
L (The declara-
tion is possible
in the "Input",
<Operand> Input VARIANT "InOut" and Tag to be queried
"Temp" sections
of the block in-
terface.)
Function value UDINT I, Q, M, D, L Result of the instruction
You can find additional information on valid data types under "See also".
Example
The following example shows how the instruction works:
CountOfElements: Get number of ARRAY elements (S7-1200, S7-1500)
SCL
IF IS_ARRAY(#Tag_VARIANTToArray) THEN
"Tag_Result" := CountOfElements(#Tag_VARIANTToArray);
END_IF;
If the tag to which the VARIANT points is an ARRAY, the number of ARRAY elements is
output.
See also
Overview of the valid data types
Memory areas (S7-1500)
(Not available)
Basics of SCL
Memory areas (S7-1200)