using System;

namespace GitFlowWithPR.VS
{
    public class CommandOutputEventArgs : EventArgs
    {
        public CommandOutputEventArgs(string output)
        {
            Output = output;
        }
        public string Output { get; set; }
    }
}