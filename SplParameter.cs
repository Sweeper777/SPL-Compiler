using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLCompiler {
    public struct SplParameter {
        public int Value {
            get;
            private set;
        }

        public bool IsPointer {
            get;
            private set;
        }

        public SplParameter (int value, bool isPointer) {
            Value = value;
            IsPointer = isPointer;
        }

        public static implicit operator SplParameter (int i) {
            return new SplParameter (i, false);
        }
    }
}
