using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLCompiler.Commands {
    public class PutCommand : IParameterCommand {
        public SplParameter Parameter {
            get;
            private set;
        }

        public void Execute (ISplRuntime runtime) {
            if (Parameter.Value >= runtime.Memory.Length) {
                runtime.ShowErrorMessage ("Error: Memory Index Out Of Bounds");
                runtime.Stopped = true;
            }

            if (runtime.Current == null) {
                runtime.ShowErrorMessage ("Error: NULL Value In Current");
                runtime.Stopped = true;
            }

            if (Parameter.IsPointer) {
                int pointTo = Convert.ToInt32 (runtime.Memory[Parameter.Value]);
                if (pointTo >= runtime.Memory.Length) {
                    runtime.ShowErrorMessage ("Error: Memory Index Out Of Bounds");
                    runtime.Stopped = true;
                    return;
                }
                runtime.Memory[pointTo] = runtime.Current;
            } else {
                runtime.Memory[Parameter.Value] = runtime.Current;
            }
        }

        public PutCommand (SplParameter param) {
            Parameter = param;
        }
    }
}
