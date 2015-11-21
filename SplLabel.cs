using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLCompiler.Commands {
    public class SplLabel : ICommand{
        public int LineNumber {
            get;
        }

        public string Name {
            get;
        }

        public SplLabel (int lineNumber, string name) {
            LineNumber = lineNumber;
            Name = name;
        }

        public void Execute (ISplRuntime runtime) {
            
        }
    }
}
