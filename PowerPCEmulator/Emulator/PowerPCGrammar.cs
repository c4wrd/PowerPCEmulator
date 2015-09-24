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

        private class NTerminal
        {
            public static List<NTerminal> termList = new List<NTerminal>();
            public NonTerminal keyTerm;
            public KeyTerm term;
            public BnfExpression Rule
            {
                get
                {
                    return keyTerm.Rule;
                }
                set
                {
                    keyTerm.Rule = value;
                }
            }
            public NTerminal(string key)
            {
                this.keyTerm = new NonTerminal(key);
                this.term = PowerPCGrammar.CurrentGrammar.ToTerm(key);
                termList.Add(this);
            }
        }

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

            var li = new NTerminal("li");
            var add = new NTerminal("add");
            var addi = new NTerminal("addi");
            var sub = new NTerminal("sub");
            var subi = new NTerminal("subi");
            var print = new NTerminal("print");

            li.Rule = li.term + register + "," + number;
            add.Rule = add.term + register + "," + register + "," + register;
            addi.Rule = addi.term + register + "," + register + "," + number;
            sub.Rule = sub.term + register + "," + register + "," + register;
            subi.Rule = subi.term + register + "," + register + "," + number;
            print.Rule = print.term | print.term + register | print.term + register + "..." + register;

            program.Rule = statementList;
            statementList.Rule = MakeStarRule(statementList, NewLine, statement);

            this.MarkPunctuation(",", ";", "...");

            foreach (NTerminal n in NTerminal.termList)
            {
                if (statement.Rule == null)
                    statement.Rule = n.keyTerm;
                else
                    statement.Rule = statement.Rule | n.keyTerm;
                MarkPunctuation(n.term);
            }

            this.Root = program;
        }
    }
}
