using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLCompiler.Commands {
    public class IncCommand : IParameterCommand {
        public int Parameter {
            get;
            private set;
        }

        public void Execute (ISplRuntime runtime) {
            if (Parameter >= runtime.Memory.Length) {
                runtime.ShowErrorMessage ("Error: Memory Index Out Of Bounds");
                runtime.Stopped = true;
            }

            if (runtime.Memory[Parameter] == null) {
                runtime.ShowErrorMessage ("Error: NULL Value In Memory");
                runtime.Stopped = true;
            }
            
            int data = Convert.ToInt32 (runtime.Memory[Parameter]);
            data++;
            runtime.Memory[Parameter] = data;
        }

        public IncCommand (int param) {
            Parameter = param;
        }
    }
}
