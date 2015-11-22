using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLCompiler.Commands {
    public class OutputCommand : ICommand {
        public void Execute (ISplRuntime runtime) {
            if (runtime.Current == null) {
                runtime.ShowErrorMessage ("Error: NULL Value Used");
            }
            if (runtime.Current is int) {
                runtime.Output.Write (runtime.Current + " ");
            } else {
                runtime.Output.Write (runtime.Current);
            }
        }
    }
}
