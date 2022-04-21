﻿using committed.defaults.stacks;

namespace committed.defaults.history
{
    public class FixedSizeThrowingCommitHistory : CommitHistory
    {
        // The redo stack can never be larger than the undo stack, therefore only the undo stack needs to be unbounded
        public FixedSizeThrowingCommitHistory(uint maxSize) : base(new FixedSizeThrowingActionStack(maxSize), new UnboundedActionStack())
        {

        }
    }
}