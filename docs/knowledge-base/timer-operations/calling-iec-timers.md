# Calling IEC timers

**Category:** Timer operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 6  

---

Calling IEC timers (S7-1200, S7-1500)
Calling IEC timers
Description
You have the option of declaring the IEC timer as single or multiple instance and calling it
in the program code.
The following syntax options are available for the declaration of an IEC timer as multi-in-
stance within a structure in the block interface:
IEC timer as ARRAY element
Declaration in the block interface:
Program code:
SCL
#MyARRAY[1].TOF(IN := <Operand>, PT := <Operand>)
Declaration in the block interface:
Program code:
SCL
#MyARRAY[#index](IN := <Operand>, PT := <Operand>)
IEC timer within an anonymous structure
Declaration in the block interface:
Program code:
Calling IEC timers (S7-1200, S7-1500)
SCL
#MyStruct.FirstTime.TOF(IN := <Operand>, PT := <Operand>)
Declaration in the block interface:
Program code:
SCL
#MyStruct.FirstTime(IN := <Operand>, PT := <Operand>)
IEC timer in the global data block
Declaration in the data block:
Program code:
SCL
"MyGlobalDB".Timer.FirstTime.TOF(IN := <Operand>, PT := <Operand>)
Declaration in the data block:
Program code:
Calling IEC timers (S7-1200, S7-1500)
SCL
"MyGlobalDB".Timer.SecondTime(IN := <Operand>, PT := <Operand>)
IEC timer as elements in the block interface
Declaration in the block interface:
Program code:
SCL
#Timer.FirstTime.TOF(IN := <Operand>, PT := <Operand>)
Declaration in the block interface:
Program code:
SCL
#Timer.SecondTime(IN := <Operand>, PT := <Operand>)
IEC timer within an ARRAY DB
Declaration in the ARRAY DB:
Program code:
Calling IEC timers (S7-1200, S7-1500)
SCL
"MyARRAYDB"."THIS"[0].FirstTime.TOF(IN := <Operand>, PT := <Operand>)
Declaration in the ARRAY DB:
Program code:
SCL
"MyARRAYDB"."THIS"[0].SecondTime(IN := <Operand>, PT := <Operand>)