# Calling IEC counters

**Category:** Counter operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 48  

---

Calling IEC counters (S7-1200, S7-1500)
Calling IEC counters
Description
You have the option of declaring the IEC counter as single or multiple instance and calling
it in the program code.
The following syntax options are available for the declaration of an IEC counter as multi-
instance within a structure in the block interface:
IEC counter as ARRAY element
Declaration in the block interface:
Program code:
SCL
#MyARRAY[1].CTU(CU := <Operand>, PV := <Operand>)
Declaration in the block interface:
Program code:
SCL
#MyARRAY[#index](CU := <Operand>, PV := <Operand>)
IEC counter within an anonymous structure
Declaration in the block interface:
Calling IEC counters (S7-1200, S7-1500)
Program code:
SCL
#MyStruct.FirstTime.CTU(CU := <Operand>, PV := <Operand>)
Declaration in the block interface:
Program code:
SCL
#MyStruct.FirstTime(CU := <Operand>, PV := <Operand>)
IEC counter in the global data block
Declaration in the data block:
Program code:
SCL
"MyGlobalDB".Counter.FirstCount.CTU(CU := <Operand>, PV := <Operand>)
Declaration in the data block:
Calling IEC counters (S7-1200, S7-1500)
Program code:
SCL
"MyGlobalDB".Counter.SecondCount(CU := <Operand>, PV := <Operand>)
IEC counter as element in the block interface
Declaration in the block interface:
Program code:
SCL
#Counter.FirstCount.CTU(CU := <Operand>, PV := <Operand>)
Declaration in the block interface:
Calling IEC counters (S7-1200, S7-1500)
Program code:
SCL
#Counter.SecondCount(CU := <Operand>, PV := <Operand>)
IEC counter within an ARRAY DB
Declaration in the ARRAY DB:
Program code:
SCL
"MyARRAYDB"."THIS"[0].FirstCount.CTU(CU := <Operand>, PV := <Operand>)
Declaration in the ARRAY DB:
Calling IEC counters (S7-1200, S7-1500)
Program code:
SCL
"MyARRAYDB"."THIS"[0].SecondCount(CU := <Operand>, PV := <Operand>)