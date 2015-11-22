using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLCompiler.Commands {
    public interface ICommand {
        void Execute (ISplRuntime runtime);
    }

    public interface IParameterCommand : ICommand {
        SplParameter Parameter {
            get;
        }
    }

    public interface IGotoCommand : ICommand {
        SplLabel JumpTo {
            get;
        }

        Predicate<ISplRuntime> GotoCondition {
            get;
        }
    }
}
