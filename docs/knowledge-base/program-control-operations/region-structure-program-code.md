# REGION: Structure program code

**Category:** Program control operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 273  

---

REGION: Structure program code (S7-1200, S7-1500)
REGION: Structure program code
Description
You can use the instruction "Structure program code" to structure the program code in an
SCL block by dividing it into different areas.
For additional information on working with areas, please refer to:Working with regions
You can declare the instruction as follows:
REGION <Name>
<Instructions>
END_REGION
Parameters
The following table shows the parameters of the instruction:
Parameters Data type Memory area Description
Keyword with which
REGION - -
the area starts
Text designating the
<Name> - -
REGION
Program code that is
preceded and suc-
<Instructions> - -
ceeded by the RE-
GION
Keyword with which
END_REGION - -
the area ends
Example
The following example shows how the instruction works:
SCL
REGION Feeder System
// Structures the source code for the feeder system
IF "Variable_1" = 0 THEN
"Variable_2" := 10;
END_IF;
END_REGION
The Feeder System area comprises the complete program code.
See also
Overview of the valid data types
Memory areas (S7-1500)
Memory areas (S7-1200)
Basics of SCL
REGION: Structure program code (S7-1200, S7-1500)