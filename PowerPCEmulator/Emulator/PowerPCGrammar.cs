using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irony.Parsing;
using Irony.Interpreter.Ast;
using Irony.Interpreter;

namespace Bionware.PowerPC
{

    public class PowerPCGrammar : Grammar
    {

        private class TermDictionary<T1,T2> : Dictionary<string, KeyTerm>
        {
            public void Add(string key)
            {
                base.Add(key, PowerPCGrammar.CurrentGrammar.ToTerm(key));
            }

            public void Register(string[] keys)
            {
                foreach (string key in keys)
                {
                    base.Add(key, PowerPCGrammar.CurrentGrammar.ToTerm(key));
                }
            }
        }

        private TermDictionary<string, KeyTerm> TermDict = new TermDictionary<string, KeyTerm>();

        public PowerPCGrammar()
            : base(false)
        {
            var number = new NumberLiteral("number", NumberOptions.Default);
            var REGCHAR = ToTerm("r");
            REGCHAR.AllowAlphaAfterKeyword = true;
            var register = new NonTerminal("register");
            register.Rule = REGCHAR + number;

            var program = new NonTerminal("program");
            var statementList = new NonTerminal("statementList");
            var statement = new NonTerminal("statement");
            var printStatement = new NonTerminal("printStatement");
            var opCodeStatement = new NonTerminal("opCodeStatement");

            //opcode nonterminals
            var _add = new NonTerminal("add");
            var _addi = new NonTerminal("addi");
            var _sub = new NonTerminal("sub");
            var _subi = new NonTerminal("subi");
            var _li = new NonTerminal("li");

            TermDict.Register(new string[] {
                "add",
                "addi",
                "sub",
                "subi",
                "print",
                "li"
            });

            //begin teh rulez
            program.Rule = statementList;
            statementList.Rule = MakeStarRule(statementList, NewLine, statement);
            statement.Rule = printStatement | _add | _addi | _sub | _subi | _li;
            printStatement.Rule = TermDict["print"] | TermDict["print"] + register | TermDict["print"] + register + "..." + register;

            //opcode rules
            _add.Rule = TermDict["add"] + register + "," + register + "," + register;
            _addi.Rule = TermDict["addi"] + register + "," + register + "," + number;
            _sub.Rule = TermDict["sub"] + register + "," + register + "," + register;
            _subi.Rule = TermDict["subi"] + register + "," + register + "," + number;
            _li.Rule = TermDict["li"] + register + "," + number;

            this.MarkPunctuation(",", ";", "...");
            foreach (KeyValuePair<String, KeyTerm> pair in TermDict)
            {
                MarkPunctuation(pair.Value);
            }
            this.Root = program;
        }
    }
}
