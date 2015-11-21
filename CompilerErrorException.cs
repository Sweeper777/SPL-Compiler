using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLCompiler {
    public class CompilerErrorException : Exception{
        public CompilerErrorException (string message, int lineNumber) :
            base ($"Compiler Error at line {lineNumber}:\n" + message) {

        }
    }
}
