# REF: Create a reference to a tag

**Category:** Conversion operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 238  

---

REF: Create a reference to a tag (S7-1500)
REF: Create a reference to a tag
Description
The keyword "REF()" is used to specify the tag to which a previously declared reference
will point. As parameter, specify the tag to be referenced.
Note
Declaration of references
Before you use the keyword "REF()", you must first declare a reference in the block inter-
face.
See also:
Declaring references
Parameter
The following table shows the parameters of the instruction:
Parameter Declaration Data type Memory area Description
Bit sequences,
except BOOL,
Integers,
Floating-point
numbers,
DB tag (opti-
Character mized),
Tag to which the
strings,
<Expression> Input reference will
Block interface
PLC data types of an FB (opti- point
(UDT), mized)
System data
types (SDT),
ARRAYs of the
named data
types
The data type of Block interface
the function val- of an FC: Input,
ue corresponds Output, Temp Address of the
Function value
to that of the de- referenced tag
clared reference Block interface
tag. of an FB: Temp
You will find additional information on valid data types under "See also".
Rules
The following rules apply to referencing:
REF: Create a reference to a tag (S7-1500)
• The data type of the tag that you specify in brackets must correspond exactly to the data
type of the declared reference. This means that a reference with data type "REF_TO Int"
can only point to a tag of data type "Int". No data type conversion is made.
• The tag that you specify as the parameter for "REF()" must be located in an optimized
memory area.
• A reference must not refer to the following data:
o Temporary data (TEMP)
o Global tags from the PLC tag table
o Block parameters
o Constants
o Write-protected tags
The following applies to references to arrays:
• The array limits and dimensions of the reference and the referenced tag must be identi-
cal.
• Array[*] is not supported.
• References to ARRAY DBs that are based on a PLC data type must be created as fol-
lows:
REF("my_ArrayDB_UDT".THIS)
REF("my_ArrayDB_UDT"."THIS"[i])
Example
In the interface of the block the temporary tags "#myRefInt", "#myRefType" and "#myRe-
fArray" were declared.
In the program code these references are assigned concrete tags:
SCL
#myRefInt := REF(#a);
#myRefType := REF("myDB".myUDT);
#myRefType := REF("myArrayDB_UDT"."THIS"[1]);
#myRefARRAY := REF("myDB".myArray);
REF: Create a reference to a tag (S7-1500)
See also
Basic information about references (S7-1500)
Overview of the valid data types
Memory areas (S7-1500)
Memory areas (S7-1200)
Basics of SCL