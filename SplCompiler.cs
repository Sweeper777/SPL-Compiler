﻿using SPLCompiler.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLCompiler {
    public static class SplCompiler {
        public static SplProgram Compile (string code) {
            var lines = code.Split (new[] { Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries);
            var commands = new ICommand[lines.Length];
            var labels = new List<SplLabel> ();
            var commentLines = new List<int> ();
            FindCommentsAndLabels (lines, labels, commentLines);

            var labelLines = from label in labels
                             select label.LineNumber;

            RegisterCommentsAndLabels (commands, labels, commentLines, labelLines);

            for (int i = 0 ; i < lines.Length ; i++) {
                if (labelLines.Contains (i + 1) || commentLines.Contains (i)) {
                    continue;
                }
                string line = lines[i];
                string[] tokens;
                var tokenCount = GetTokenCount (line, out tokens);
                var commandName = tokens[0];
                if (!CompilerUtility.CommandTypeMap.ContainsKey (commandName)) {
                    throw new CompilerErrorException ($"The command \"{commandName}\" does not exist", i + 1);
                }
                var commandType = CompilerUtility.CommandTypeMap[commandName];
                ICommand commandOfThisLine;
                commandOfThisLine = ProcessCommand (labels, i, tokens, tokenCount, commandType);
                commands[i] = commandOfThisLine;
            }

            return new SplProgram (commands);
        }

        private static ICommand ProcessCommand (List<SplLabel> labels, int i, string[] tokens, int tokenCount, Type commandType) {
            ICommand commandOfThisLine;
            if (!commandType.GetInterfaces().Contains (typeof (IParameterCommand)) &&
      !commandType.GetInterfaces ().Contains (typeof (IGotoCommand)) &&
      commandType != typeof(SetCommand)) {
                if (tokenCount > 1)
                    throw new CompilerErrorException ($"Unexpected token \"{tokens[1]}\"", i + 1);
                commandOfThisLine = Activator.CreateInstance (commandType) as ICommand;
            } else {
                if (commandType.GetInterfaces ().Contains (typeof (IGotoCommand))) {
                    if (tokenCount > 2)
                        throw new CompilerErrorException ($"Unexpected token \"{tokens[2]}\"", i + 1);
                    commandOfThisLine = ProcessGotoCommands (labels, tokens, commandType);
                } else {
                    if (tokenCount > 3)
                        throw new CompilerErrorException ($"Unexpected token \"{tokens[3]}\"", i + 1);
                    commandOfThisLine = ProcessParameterCommands (i, tokens, commandType);
                }

            }

            return commandOfThisLine;
        }

        private static ICommand ProcessParameterCommands (int i, string[] tokens, Type commandType) {
            int parseResult;
            bool parseSucceeded = int.TryParse (tokens[1], out parseResult);
            if (!parseSucceeded) {
                throw new CompilerErrorException ($"Unexpected token \"{tokens[1]}\"", i + 1);
            } else {
                if (tokens.Length > 2) {
                    if (tokens[2] != "*") {
                        if (commandType == typeof (SetCommand)) {
                            int valueToSet;
                            parseSucceeded = int.TryParse (tokens[2], out valueToSet);
                            if (!parseSucceeded && tokens[2].Length > 1) {
                                throw new CompilerErrorException ($"Unexpected token \"{tokens[2]}\"", i + 1);
                            }

                            return CreateInstance (commandType, new Type[] { typeof (int), typeof (int) },
                                new object[] { parseResult, parseSucceeded ? valueToSet : tokens[2].Single () }) as ICommand;
                        }
                        throw new CompilerErrorException ($"Unexpected token \"{tokens[2]}\"", i + 1);
                    } else {
                        return CreateInstance (commandType, new[] { typeof (SplParameter) }, new object[] { new SplParameter (parseResult, true) }) as ICommand;
                    }
                }
                return CreateInstance (commandType, new[] { typeof (SplParameter) }, new object[] { new SplParameter (parseResult, false) }) as ICommand;
            }
        }

        private static ICommand ProcessGotoCommands (List<SplLabel> labels, string[] tokens, Type commandType) {
            ICommand commandOfThisLine;
            var query = from label in labels
                        where label.Name == tokens[1]
                        select label;
            if (query.Count () > 1) {
                throw new CompilerErrorException ($"Duplicate label name \"{query.First ().Name}\"", query.Last ().LineNumber);
            }
            commandOfThisLine = CreateInstance (commandType, new[] { typeof (SplLabel) }, new object[] { query.Single () }) as ICommand;
            return commandOfThisLine;
        }

        private static void RegisterCommentsAndLabels (ICommand[] commands, List<SplLabel> labels, List<int> commentLines, IEnumerable<int> labelLines) {
            for (int i = 0 ; i < labelLines.Count () ; i++) {
                commands[labelLines.ElementAt (i) - 1] = labels[i];
            }

            for (int i = 0 ; i < commentLines.Count ; i++) {
                commands[commentLines[i] - 1] = new Comment ();
            }
        }

        private static void FindCommentsAndLabels (string[] lines, List<SplLabel> labels, List<int> commentLines) {
            for (int i = 0 ; i < lines.Length ; i++) {
                var line = lines[i];
                if (line.Trim ().EndsWith (":")) {
                    if (GetTokenCount (line) == 1)
                        labels.Add (new SplLabel (i + 1, line.Substring (0, line.Length - 1)));
                    else
                        throw new CompilerErrorException ("Labels cannot contain spaces", i + 1);
                }

                if (line.StartsWith ("//")) {
                    commentLines.Add (i + 1);
                }
            }
        }

        private static int GetTokenCount (string s) {
            var tokens = s.Split (new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return tokens.Length;
        }

        private static int GetTokenCount (string s, out string[] tokens) {
            tokens = s.Split (new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return tokens.Length;
        }

        private static object CreateInstance(Type type, Type[] parameterTypes, params object[] parameters) {
            var constructor = type.GetConstructor (parameterTypes);
            var returnVal = constructor.Invoke (parameters);
            return returnVal;
        }
    }
}
