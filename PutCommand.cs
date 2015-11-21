using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLCompiler.Commands {
    public class PutCommand : IParameterCommand {
        public int Parameter {
            get;
            private set;
        }

        public void Execute (ISplRuntime runtime) {
            if (Parameter >= runtime.Memory.Length) {
                runtime.ShowErrorMessage ("Error: Memory Index Out Of Bounds");
                runtime.Stopped = true;
            }

            if (runtime.Current == null) {
                runtime.ShowErrorMessage ("Error: NULL Value In Current");
                runtime.Stopped = true;
            }
            runtime.Memory[Parameter] = runtime.Current;
        }

        public PutCommand (int param) {
            Parameter = param;
        }
    }
}
