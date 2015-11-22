using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLCompiler.Commands {
    public class IncCommand : IParameterCommand {
        public SplParameter Parameter {
            get;
            private set;
        }

        public void Execute (ISplRuntime runtime) {
            if (Parameter.Value >= runtime.Memory.Length) {
                runtime.ShowErrorMessage ("Error: Memory Index Out Of Bounds");
                runtime.Stopped = true;
            }

            if (runtime.Memory[Parameter.Value] == null) {
                runtime.ShowErrorMessage ("Error: NULL Value In Memory");
                runtime.Stopped = true;
            }

            int data;
            if (Parameter.IsPointer) {
                int pointTo = Convert.ToInt32 (runtime.Memory[Parameter.Value]);
                if (pointTo >= runtime.Memory.Length) {
                    runtime.ShowErrorMessage ("Error: Memory Index Out Of Bounds");
                    runtime.Stopped = true;
                    return;
                }

                if (runtime.Memory[pointTo] == null) {
                    runtime.ShowErrorMessage ("Error: NULL Value In Memory");
                    runtime.Stopped = true;
                    return;
                }
                data = Convert.ToInt32 (runtime.Memory[pointTo]);
                data++;
                runtime.Memory[pointTo] = data;
                runtime.Current = data;
            } else {
                data = Convert.ToInt32 (runtime.Memory[Parameter.Value]);
                data++;
                runtime.Memory[Parameter.Value] = data;
                runtime.Current = data;
            }
        }

        public IncCommand (SplParameter param) {
            Parameter = param;
        }
    }
}
