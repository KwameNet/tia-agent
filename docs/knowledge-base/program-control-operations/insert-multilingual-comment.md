# (/*...*/): Insert multilingual comment

**Category:** Program control operations  
**Source:** SCL_help_instructions_s7_1200_1500.pdf, page 272  

---

(/*...*/): Insert multilingual comment (S7-1200, S7-1500)
(/*...*/): Insert multilingual comment
Description
With the instruction "Insert multilingual comment" you insert a comment that you can trans-
late into other project languages. A multilingual comment is started with "(/*" and ended
with "*/)" and forms a unit. This means you can always only mark or select the entire com-
ment and not parts of it. Multilingual comments cannot be nested into one another but you
can use them within line comments and comment sections. By contrast, you cannot use
line comments or simple comment sections within multilingual comments because every-
thing between "(/*" and "*/)" is interpreted as normal text.
Example
The following example shows how the instruction works:
SCL
(/*This is a comment that can be translated into other project languages.*/)
See also
Inserting comments