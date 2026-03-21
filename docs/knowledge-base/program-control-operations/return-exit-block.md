# RETURN: Exit block

**Category:** Program control operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 270  

---

RETURN: Exit block (S7-1200, S7-1500)
RETURN: Exit block
Description
The instruction "Exit block" exits the program execution in the currently edited block and
continues in the calling block.
The instruction can be omitted at the end of the block.
Example
The following example shows how the instruction works:
SCL
IF "Tag_Error" <>0 THEN RETURN;
END_IF;
If the signal state of the "Tag_Error" operand is not zero, execution of the program ends in
the block currently being processed.
See also
Overview of the valid data types
Memory areas (S7-1500)
Basics of SCL
Memory areas (S7-1200)