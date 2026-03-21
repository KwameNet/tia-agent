# AssignmentAttempt: Attempt assignment to a reference

**Category:** Move operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 140  

---

AssignmentAttempt: Attempt assignment to a reference (S7-1500)
AssignmentAttempt: Attempt assignment to a refer-
ence
Description
With the "AssignmentAttempt" instruction, you attempt an assignment to a reference tag.
The following assignment attempts are possible:
• Assignment attempt of a VARIANT to a reference
• Assignment attempt of a DB_ANY to the reference of a technology object
Note
SCL: Assignment attempt with NULL
In SCL, you can also assign NULL in the assignment attempt to specifically set the refer-
ence to NULL:
#myReference ?= NULL; // set #myReference to NULL in
any case
Assignment attempt of a VARIANT to a reference
The data type of a reference tag is determined at the time of declaration. However, the da-
ta type of a VARIANT tag is determined during runtime. An implicit data type conversion is
not permitted with reference tags. To assign the two data types to each other therefore, use
the assignment attempt. With the assignment attempt, a check is made at runtime to find
out whether the assigned tag is of the correct data type. If this is the case, the instruction is
executed. If the instruction is executed successfully, there is a valid reference in the target
tag, otherwise a NULL.
After the assignment attempt, you can check if the attempt was successful and, depending
on the result, continue processing the program. In LAD and FBD, you can use the enable
output "ENO" for the check. "ENO" returns the signal state "1" if the assignment attempt
was successful. Only then are the subsequent instructions executed in the network.
In STL and SCL, you can use the instructions "IS_NULL" or "NOT_NULL", for example, to
check the success of an assignment attempt. See the examples below.
The following rules apply to the assignment attempt of VARIANT. VARIANT tags that do not
match these rules return the value "NULL" at runtime.
• The VARIANT must point to an address in an optimized memory area.
• The VARIANT must not point to an address in a temporary memory area.
• If you want to assign a VARIANT to a reference to an ARRAY, the following rules apply:
o The VARIANT tag must point to an ARRAY whose limits exactly match those of the
declared reference. A VARIANT tag which points to an ARRAY [0..9] does not
match a tag REF_TO ARRAY[1..10].
o Moreover, you should compile the blocks that form the value of the VARIANT tag
once in a CPU of the S7-1500 series, firmware version V2.5.
• The assignment attempt of a VARIANT to the reference of a technology object is not
possible.
• In SCL, assignment attempts cannot be used in multiple assignments (a := b := c;).
Assignment attempt of a DB_ANY to the reference of a technology object
AssignmentAttempt: Attempt assignment to a reference (S7-1500)
A reference to a technology object always points to a specific technology object, e.g.
REF_TO TO_SpeedAxis. If you want to assign a technology object via a tag of the type
DB_ANY during runtime, you need to check whether the technology object matches the
declared reference. To do this, use the assignment attempt. With the assignment attempt,
a check is made during runtime to find out whether the technology object has the declared
type. If this is the case, the assignment is performed. If this is executed successfully, there
is a valid reference in the target tag, otherwise a NULL.
The following rules apply to the assignment attempt of a DB_ANY to the reference of a
technology object:
• The DB_ANY must point to a technology object in an optimized memory area.
• Two technology objects of the same type can be assigned to each other.
• A derived type can be assigned to its base type.
• A basic type cannot be assigned to its derived type
Parameter
The following table shows the parameters of the instruction:
Parameter Declaration Data type Memory area Description
• Block inter-
face of an FC:
VARIANT Input, Output, Pointer to the
InOut, Temp source tag
SRC Input DB_ANY • Block inter- whose address
will be read out
NULL face of an FB:
or NULL
Input, InOut,
Temp
With SRC =
VARIANT, refer-
ence to:
• Bit sequen-
ces, except
BOOL,
• Integers,
• Block inter-
• Floating-point
face of an FC:
numbers, Reference to
• Character Input, Output, which the ad-
Temp
DST Output strings, dress of the
• PLC data • Block inter- source tag will
face of an FB: be transferred
types (UDT),
• System data Temp
types (SDT),
• ARRAYs of
the named
data types
With SRC =
DB_ANY, refer-
ence to:
AssignmentAttempt: Attempt assignment to a reference (S7-1500)
• Technology
object
You can select the data type of the instruction from the "???" drop-down list of the instruc-
tion box.
You can find additional information on valid data types under "See also".
Example
The following example shows how the instruction works:
The interface of the block was programmed as follows:
In the program code an attempt is made to assign "myVariant" to the reference tag "myRe-
ference". If "myVariant" has the data type "Int" at runtime, "myReference" contains a valid
reference to the source tag of the VARIANT, otherwise NULL. If the next query for
"NOT_NULL" is true, it means that the assignment was successful and the value "10" can
be written to the target tag.
An attempt is also made to assign "myDB" to the "myReferenceTO" reference tag. If
"myDB" points to a technology object of the "Positioning Axis" type in runtime, "myReferen-
ceTO" contains a valid reference, otherwise a NULL. When the assignment was success-
ful, the enable output "ENO" has the signal state "1" and the value "1" can be written to the
target tag within the technology object.
See also
AssignmentAttempt: Attempt assignment to a reference (S7-1500)
Basic information about references (S7-1500)
Assignment attempt to a reference (S7-1500)
Overview of the valid data types
Basic information on the status word (S7-1500)
Memory areas (S7-1500)