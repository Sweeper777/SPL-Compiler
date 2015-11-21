using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLCompiler.Commands {
    public class GotoCommand : IGotoCommand {
        public virtual Predicate<ISplRuntime> GotoCondition {
            get {
                return x => true;
            }
        }

        public SplLabel JumpTo {
            get;
            private set;
        }

        public void Execute (ISplRuntime runtime) {
            
        }

        public GotoCommand (SplLabel jumpTo) {
            JumpTo = jumpTo;
        }
    }

    public class GotoIf0Command : GotoCommand {
        public GotoIf0Command (SplLabel jumpTo) : base (jumpTo) { }

        public override Predicate<ISplRuntime> GotoCondition {
            get {
                return x => x.Current is int && (int)x.Current == 0;
            }
        }
    }

    public class GotoIfNegCommand : GotoCommand {
        public GotoIfNegCommand (SplLabel jumpTo) : base (jumpTo) {
        }

        public override Predicate<ISplRuntime> GotoCondition {
            get {
                return x => x.Current is int && (int)x.Current < 0;
            }
        }
    }
}
