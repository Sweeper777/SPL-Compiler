using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLCompiler.Commands {
    public class InputCommand : ICommand {
        public void Execute (ISplRuntime runtime) {
            char c;
            int i;
            if (ReadFromInput (runtime, out c)) {
                runtime.Current = c;
            } else if (ReadFromInput (runtime, out i)) {
                runtime.Current = i;
            } else if (runtime.Input.Peek() == -1) {
                runtime.Stopped = true;
            } else {
                runtime.ShowErrorMessage ("Error: Input Invalid");
                runtime.Stopped = true;
            }
        }

        /// <summary>
        /// Reads a character from the runtime input.
        /// </summary>
        /// <param name="runtime">The provided runtime for providing input</param>
        /// <param name="c">The character read</param>
        /// <returns>True if the method successfully reads a character, false otherwise</returns>
        private bool ReadFromInput (ISplRuntime runtime, out char c) {
            char stuffRead = (char)runtime.Input.Peek ();
            if (char.IsLetter (stuffRead)) {
                c = (char)runtime.Input.Read ();
                return true;
            } /*else if (stuffRead == 65535) {

            }*/ else {
                c = '\0';
                return false;
            }
        }

        private bool ReadFromInput (ISplRuntime runtime, out int i) {
            char stuffRead = (char)runtime.Input.Peek ();
            if (stuffRead == ' ') {
                i = 0;
                runtime.Input.Read ();
                return true;
            }

            if (char.IsNumber (stuffRead)) {
                string numberString = "";
                while (char.IsNumber (stuffRead)) {
                    stuffRead = (char)runtime.Input.Read ();
                    numberString += stuffRead;
                }
                
                i = Convert.ToInt32 (numberString.Replace(unchecked((char)-1), ' ').Trim());
                return true;
            } else {
                i = 0;
                return false;
            }
        }
    }
}
