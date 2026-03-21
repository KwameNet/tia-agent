# SCL Knowledge Base

Auto-generated from SCL_help_instructions_s7_1200_1500.pdf


**131 instructions** across **19 categories**

## SCL


## Bit logic operations

- [R_TRIG: Detect positive signal edge](bit-logic-operations/r-trig-detect-positive-signal-edge.md)
- [F_TRIG: Detect negative signal edge](bit-logic-operations/f-trig-detect-negative-signal-edge.md)

## Timer operations

- [Calling IEC timers](timer-operations/calling-iec-timers.md)
- [TP: Generate pulse](timer-operations/tp-generate-pulse.md)
- [TON: Generate on-delay](timer-operations/ton-generate-on-delay.md)
- [TOF: Generate off-delay](timer-operations/tof-generate-off-delay.md)
- [TONR: Time accumulator](timer-operations/tonr-time-accumulator.md)
- [RESET_TIMER: Reset timer](timer-operations/reset-timer-reset-timer.md)
- [PRESET_TIMER: Load time duration](timer-operations/preset-timer-load-time-duration.md)

## Legacy

- [S_PULSE: Assign pulse timer parameters and start](legacy/s-pulse-assign-pulse-timer-parameters-and-start.md)
- [S_PEXT: Assign extended pulse timer parameters and start](legacy/s-pext-assign-extended-pulse-timer-parameters-and-start.md)
- [S_ODT: Assign on-delay timer parameters and start](legacy/s-odt-assign-on-delay-timer-parameters-and-start.md)
- [S_ODTS: Assign retentive on-delay timer parameters and start](legacy/s-odts-assign-retentive-on-delay-timer-parameters-and-start.md)
- [S_OFFDT: Assign off-delay timer parameters and start](legacy/s-offdt-assign-off-delay-timer-parameters-and-start.md)
- [S_CU: Assign parameters and count up](legacy/s-cu-assign-parameters-and-count-up.md)
- [S_CD: Assign parameters and count down](legacy/s-cd-assign-parameters-and-count-down.md)
- [S_CUD: Assign parameters and count up / down](legacy/s-cud-assign-parameters-and-count-up-down.md)
- [BLKMOV: Move block](legacy/blkmov-move-block.md)
- [UBLKMOV: Move block uninterruptible](legacy/ublkmov-move-block-uninterruptible.md)
- [FILL: Fill block](legacy/fill-fill-block.md)
- [SCALE: Scale](legacy/scale-scale.md)
- [UNSCALE: Unscale](legacy/unscale-unscale.md)
- [DRUM: Implement sequencer](legacy/drum-implement-sequencer.md)
- [DCAT: Discrete control-timer alarm](legacy/dcat-discrete-control-timer-alarm.md)
- [MCAT: Motor control-timer alarm](legacy/mcat-motor-control-timer-alarm.md)
- [IMC: Compare input bits with the bits of a mask](legacy/imc-compare-input-bits-with-the-bits-of-a-mask.md)
- [SMC: Compare scan matrix](legacy/smc-compare-scan-matrix.md)
- [LEAD_LAG: Lead and lag algorithm](legacy/lead-lag-lead-and-lag-algorithm.md)
- [SEG: Create bit pattern for seven-segment display](legacy/seg-create-bit-pattern-for-seven-segment-display.md)
- [BCDCPL: Create tens complement](legacy/bcdcpl-create-tens-complement.md)
- [BITSUM: Count number of set bits](legacy/bitsum-count-number-of-set-bits.md)

## Counter operations

- [Calling IEC counters](counter-operations/calling-iec-counters.md)
- [CTU: Count up](counter-operations/ctu-count-up.md)
- [CTD: Count down](counter-operations/ctd-count-down.md)
- [CTUD: Count up and down](counter-operations/ctud-count-up-and-down.md)

## Comparator operations

- [TypeOf: Check data type of a VARIANT or ResolvedSymbol tag](comparator-operations/typeof-check-data-type-of-a-variant-or-resolvedsymbol-tag.md)
- [TypeOfElements: Check data type of an ARRAY element of a VARIANT tag](comparator-operations/typeofelements-check-data-type-of-an-array-element-of-a-variant-tag.md)
- [IS_ARRAY: Check for ARRAY](comparator-operations/is-array-check-for-array.md)
- [TypeOfDB: Query data type of a DB](comparator-operations/typeofdb-query-data-type-of-a-db.md)

## Math functions

- [ABS: Form absolute value](math-functions/abs-form-absolute-value.md)
- [MIN: Get minimum](math-functions/min-get-minimum.md)
- [MAX: Get maximum](math-functions/max-get-maximum.md)
- [LIMIT: Set limit value](math-functions/limit-set-limit-value.md)
- [SQR: Form square](math-functions/sqr-form-square.md)
- [SQRT: Form square root](math-functions/sqrt-form-square-root.md)
- [LN: Form natural logarithm](math-functions/ln-form-natural-logarithm.md)
- [EXP: Form exponential value](math-functions/exp-form-exponential-value.md)
- [SIN: Form sine value](math-functions/sin-form-sine-value.md)
- [COS: Form cosine value](math-functions/cos-form-cosine-value.md)
- [TAN: Form tangent value](math-functions/tan-form-tangent-value.md)
- [ASIN: Form arcsine value](math-functions/asin-form-arcsine-value.md)
- [ACOS: Form arccosine value](math-functions/acos-form-arccosine-value.md)
- [ATAN: Form arctangent value](math-functions/atan-form-arctangent-value.md)
- [FRAC: Return fraction](math-functions/frac-return-fraction.md)

## Move operations

- [Deserialize: Deserialize](move-operations/deserialize-deserialize.md)
- [Serialize: Serialize](move-operations/serialize-serialize.md)
- [MOVE_BLK: Move block](move-operations/move-blk-move-block.md)
- [MOVE_BLK_VARIANT: Move block](move-operations/move-blk-variant-move-block.md)
- [UMOVE_BLK: Move block uninterruptible](move-operations/umove-blk-move-block-uninterruptible.md)
- [FILL_BLK: Fill block](move-operations/fill-blk-fill-block.md)
- [UFILL_BLK: Fill block uninterruptible](move-operations/ufill-blk-fill-block-uninterruptible.md)
- [SCATTER: Parse the bit sequence into individual bits](move-operations/scatter-parse-the-bit-sequence-into-individual-bits.md)
- [SCATTER_BLK: Parse elements of an ARRAY of bit sequence into individual bits](move-operations/scatter-blk-parse-elements-of-an-array-of-bit-sequence-into-individual-bits.md)
- [GATHER: Merge individual bits into a bit sequence](move-operations/gather-merge-individual-bits-into-a-bit-sequence.md)
- [GATHER_BLK: Merge individual bits into multiple elements of an ARRAY of bit sequence](move-operations/gather-blk-merge-individual-bits-into-multiple-elements-of-an-array-of-bit-sequence.md)
- [AssignmentAttempt: Attempt assignment to a reference](move-operations/assignmentattempt-attempt-assignment-to-a-reference.md)
- [SWAP: Swap](move-operations/swap-swap.md)

## ARRAY DB

- [ReadFromArrayDB: Read from array data block](array-db/readfromarraydb-read-from-array-data-block.md)
- [WriteToArrayDB: Write to array data block](array-db/writetoarraydb-write-to-array-data-block.md)
- [ReadFromArrayDBL: Read from array data block in load memory](array-db/readfromarraydbl-read-from-array-data-block-in-load-memory.md)
- [WriteToArrayDBL: Write to array data block in load memory](array-db/writetoarraydbl-write-to-array-data-block-in-load-memory.md)

## Read/write access

- [PEEK: Read memory address](read-write-access/peek-read-memory-address.md)
- [PEEK_BOOL: Read memory bit](read-write-access/peek-bool-read-memory-bit.md)
- [POKE: Write memory address](read-write-access/poke-write-memory-address.md)
- [POKE_BOOL: Write memory bit](read-write-access/poke-bool-write-memory-bit.md)
- [POKE_BLK: Write memory area](read-write-access/poke-blk-write-memory-area.md)
- [READ_LITTLE: Read data in little endian format](read-write-access/read-little-read-data-in-little-endian-format.md)
- [WRITE_LITTLE: Write data in little endian format](read-write-access/write-little-write-data-in-little-endian-format.md)
- [READ_BIG: Read data in big endian format](read-write-access/read-big-read-data-in-big-endian-format.md)
- [WRITE_BIG: Write data in big endian format](read-write-access/write-big-write-data-in-big-endian-format.md)

## VARIANT

- [VariantGet: Read out VARIANT tag value](variant/variantget-read-out-variant-tag-value.md)
- [VariantPut: Write VARIANT tag value](variant/variantput-write-variant-tag-value.md)
- [CountOfElements: Get number of ARRAY elements](variant/countofelements-get-number-of-array-elements.md)
- [VARIANT_TO_DB_ANY: Convert VARIANT to DB_ANY](variant/variant-to-db-any-convert-variant-to-db-any.md)
- [DB_ANY_TO_VARIANT: Convert DB_ANY to VARIANT](variant/db-any-to-variant-convert-db-any-to-variant.md)

## Symbolic move

- [Symbolic access during runtime](symbolic-move/symbolic-access-during-runtime.md)
- [ResolveSymbols: Resolve several symbols](symbolic-move/resolvesymbols-resolve-several-symbols.md)
- [System data type ResolvedSymbol](symbolic-move/system-data-type-resolvedsymbol.md)
- [MoveToResolvedSymbol: Write value into resolved symbol](symbolic-move/movetoresolvedsymbol-write-value-into-resolved-symbol.md)
- [MoveFromResolvedSymbol: Read value from resolved symbol](symbolic-move/movefromresolvedsymbol-read-value-from-resolved-symbol.md)
- [MoveResolvedSymbolsToBuffer: Read values from resolved symbols and write them into buffer](symbolic-move/moveresolvedsymbolstobuffer-read-values-from-resolved-symbols-and-write-them-into-buffer.md)
- [MoveResolvedSymbolsFromBuffer: Read values from buffer and write them into resolved symbols](symbolic-move/moveresolvedsymbolsfrombuffer-read-values-from-buffer-and-write-them-into-resolved-symbols.md)

## ARRAY[*]

- [LOWER_BOUND: Read out low ARRAY limit](array/lower-bound-read-out-low-array-limit.md)
- [UPPER_BOUND: Read out high ARRAY limit](array/upper-bound-read-out-high-array-limit.md)

## Conversion operations

- [CONVERT: Convert value](conversion-operations/convert-convert-value.md)
- [ROUND: Round numerical value](conversion-operations/round-round-numerical-value.md)
- [CEIL: Generate next higher integer from floating-point number](conversion-operations/ceil-generate-next-higher-integer-from-floating-point-number.md)
- [FLOOR: Generate next lower integer from floating-point number](conversion-operations/floor-generate-next-lower-integer-from-floating-point-number.md)
- [TRUNC: Truncate numerical value](conversion-operations/trunc-truncate-numerical-value.md)
- [SCALE_X: Scale](conversion-operations/scale-x-scale.md)
- [NORM_X: Normalize](conversion-operations/norm-x-normalize.md)
- [REF: Create a reference to a tag](conversion-operations/ref-create-a-reference-to-a-tag.md)

## Program control operations

- [IF: Run conditionally](program-control-operations/if-run-conditionally.md)
- [CASE: Create multiway branch](program-control-operations/case-create-multiway-branch.md)
- [FOR: Run in counting loop](program-control-operations/for-run-in-counting-loop.md)
- [WHILE: Run if condition is met](program-control-operations/while-run-if-condition-is-met.md)
- [REPEAT: Run if condition is not met](program-control-operations/repeat-run-if-condition-is-not-met.md)
- [CONTINUE: Recheck loop condition](program-control-operations/continue-recheck-loop-condition.md)
- [EXIT: Exit loop immediately](program-control-operations/exit-exit-loop-immediately.md)
- [GOTO: Jump](program-control-operations/goto-jump.md)
- [RETURN: Exit block](program-control-operations/return-exit-block.md)
- [(*...*): Insert a comment section](program-control-operations/insert-a-comment-section.md)
- [(/*...*/): Insert multilingual comment](program-control-operations/insert-multilingual-comment.md)
- [REGION: Structure program code](program-control-operations/region-structure-program-code.md)

## Runtime control

- [ENDIS_PW: Limit and enable password legitimation](runtime-control/endis-pw-limit-and-enable-password-legitimation.md)

## PC systems

- [RE_TRIGR: Restart cycle monitoring time](pc-systems/re-trigr-restart-cycle-monitoring-time.md)
- [STP: Exit program](pc-systems/stp-exit-program.md)
- [GET_ERROR: Get error locally](pc-systems/get-error-get-error-locally.md)
- [GET_ERR_ID: Get error ID locally](pc-systems/get-err-id-get-error-id-locally.md)
- [INIT_RD: Initialize all retain data](pc-systems/init-rd-initialize-all-retain-data.md)
- [WAIT: Configure time delay](pc-systems/wait-configure-time-delay.md)
- [RUNTIME: Measure program runtime](pc-systems/runtime-measure-program-runtime.md)

## Word logic operations

- [DECO: Decode](word-logic-operations/deco-decode.md)
- [ENCO: Encode](word-logic-operations/enco-encode.md)
- [SEL: Select](word-logic-operations/sel-select.md)
- [MUX: Multiplex](word-logic-operations/mux-multiplex.md)
- [DEMUX: Demultiplex](word-logic-operations/demux-demultiplex.md)

## Shift and Rotate

- [SHR: Shift right](shift-and-rotate/shr-shift-right.md)
- [SHL: Shift left](shift-and-rotate/shl-shift-left.md)
- [ROR: Rotate right](shift-and-rotate/ror-rotate-right.md)
- [ROL: Rotate left](shift-and-rotate/rol-rotate-left.md)
