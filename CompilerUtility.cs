using SPLCompiler.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLCompiler {
    public static class CompilerUtility {
        private static Dictionary<string, Type> commandTypeMap =
            new Dictionary<string, Type> (StringComparer.OrdinalIgnoreCase) {
                { "INPUT", typeof(InputCommand) },
                { "OUTPUT", typeof(OutputCommand) },
                { "TAKE", typeof(TakeCommand) },
                { "PUT", typeof(PutCommand) },
                { "ADD", typeof(AddCommand) },
                { "SUB", typeof(SubCommand) },
                { "INC", typeof(IncCommand) },
                { "DEC", typeof(DecCommand) },
                { "GOTO_IF_0", typeof(GotoIf0Command) },
                { "GOTO", typeof(GotoCommand) },
                { "GOTO_IF_NEG", typeof(GotoIfNegCommand) },
                { "SET", typeof(SetCommand) }
            };

        public static Dictionary<string, Type> CommandTypeMap {
            get {
                return commandTypeMap;
            }
        }
    }
}
