﻿using UndoRedo.defaults.stacks;

namespace UndoRedo.defaults
{
    public class FixedSizeRollingCommitHistory : CommitHistory
    {
        public FixedSizeRollingCommitHistory(uint maxSize) : base(new FixedSizeRollingActionStack(maxSize), new UnboundedActionStack())
        {

        }
    }
}