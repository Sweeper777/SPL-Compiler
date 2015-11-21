using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLCompiler {
    public interface ISplRuntime {
        object Current {
            get;
            set;
        }

        object[] Memory {
            get;
        }

        TextReader Input {
            get;
        }

        TextWriter Output {
            get;
        }

        bool Stopped {
            get;
            set;
        }

        void ShowErrorMessage (string error);
    }

    public delegate void NextCommandEventHandler (int lineNumber);
}
