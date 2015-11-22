using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLCompiler.Commands {
    public class SetCommand : ICommand {
        public int Parameter1 {
            get;
        }

        public object Parameter2 {
            get;
        }

        public void Execute (ISplRuntime runtime) {
            if (Parameter1 >= runtime.Memory.Length) {
                runtime.ShowErrorMessage ("Error: Memory Index Out Of Bounds");
                runtime.Stopped = true;
                return;
            }
            runtime.Memory[Parameter1] = Parameter2;
        }

        public SetCommand (int param1, object param2) {
            Parameter1 = param1;
            Parameter2 = param2;
        }
    }
}
