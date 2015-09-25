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
            var comma = ",";

            var li = new NTerminal("li");
            var add = new NTerminal("add");
            var addi = new NTerminal("addi");
            var addc = new NTerminal("addc");
            var and = new NTerminal("and");
            var andc = new NTerminal("andc");
            var andi = new NTerminal("andi");
            var andis = new NTerminal("andis");
            var cmp = new NTerminal("cmp");
            var sub = new NTerminal("sub");
            var subi = new NTerminal("subi");
            var print = new NTerminal("print");

            li.Rule = li.term + register + comma + number;
            add.Rule = add.term + register + comma + register + comma + register;
            addi.Rule = addi.term + register + comma + register + comma + number;
            addc.Rule = addc.term + register + comma + register + comma + register;
            and.Rule = and.term + register + comma + register + comma + register;
            andc.Rule = andc.term + register + comma + register + comma + register;
            andi.Rule = andi.term + register + comma + register + comma + number;
            andis.Rule = andis.term + register + comma + register + comma + number;
            cmp.Rule = cmp.term + register + comma + register;
            sub.Rule = sub.term + register + comma + register + comma + register;
            subi.Rule = subi.term + register + comma + register + comma + number;


            print.Rule = print.term | print.term + register | print.term + register + "..." + register;

            program.Rule = statementList;
            statementList.Rule = MakeStarRule(statementList, NewLine, statement);

            this.MarkPunctuation(comma, ";", "...");

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
