using SPLCompiler.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLCompiler {
    public class SplProgram {
        public ICommand[] Commands {
            get;
            private set;
        }

        public int CurrentLineNumber {
            get;
            private set;
        }

        public ICommand CurrentCommand => Commands[CurrentLineNumber];

        public void Run (ISplRuntime runtime) {
            runtime.Stopped = false;
            while (!runtime.Stopped) {
                StepOver (runtime);
            }
        }

        public void StepOver (ISplRuntime runtime) {
            if (runtime.Stopped)
                return;

            try {
                CurrentCommand.Execute (runtime);

                if (CurrentCommand is IGotoCommand) {
                    IGotoCommand gotoCommand = CurrentCommand as IGotoCommand;
                    if (gotoCommand.GotoCondition (runtime)) {
                        CurrentLineNumber = gotoCommand.JumpTo.LineNumber - 1;
                        return;
                    }
                }
                CurrentLineNumber++;
            } catch (IndexOutOfRangeException) {
                runtime.Stopped = true;
            }
        }

        public SplProgram (ICommand[] commands) {
            Commands = commands;
            CurrentLineNumber = 0;
        }
    }
}
