using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLCompiler.Commands {
    public class SubCommand : IParameterCommand {
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

            if (runtime.Memory[Parameter] == null) {
                runtime.ShowErrorMessage ("Error: NULL Value In Memory");
                runtime.Stopped = true;
            }

            int current = Convert.ToInt32 (runtime.Current);
            int data = Convert.ToInt32 (runtime.Memory[Parameter]);

            runtime.Current = current - data;
        }

        public SubCommand (int param) {
            Parameter = param;
        }
    }
}
