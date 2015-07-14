using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irony.Parsing;
using Irony.Interpreter.Ast;
using Irony.Interpreter;

namespace Bionware.PowerPC
{

    public class PowerPCGrammer : InterpretedLanguageGrammar
    {
        private class NTerminal
        {
            public NonTerminal nonTerminal;
            public KeyTerm keyTerm;
            public BnfExpression Rule
            {
                get {
                    return nonTerminal.Rule;
                }
                set
                {
                    nonTerminal.Rule = value;
                }
            }

            public NTerminal(string identifier, Type handler)
            {
                keyTerm = PowerPCGrammer.CurrentGrammar.ToTerm(identifier);
                keyTerm.SetFlag(TermFlags.IsTransient, true);
                nonTerminal = new NonTerminal("n_" + identifier, handler);
            }
        }

        public PowerPCGrammer()
            : base(false)
        {
            var number = new NumberLiteral("number", NumberOptions.Default);
            var comma = ToTerm(",");
            comma.SetFlag(TermFlags.IsTransient, true);
            var REGCHAR = ToTerm("r");
            REGCHAR.AllowAlphaAfterKeyword = true;
            var REGISTER = new NonTerminal("regSymbol");
            REGISTER.Rule = REGCHAR + number;
            REGISTER.SetFlag(TermFlags.NoAstNode, true);

            //OpCodes

            //todo: ALL TEH OPCODES!!

            var n_add = new NTerminal("add", typeof(OpCodeNodes.OpAddNode));
            n_add.Rule = n_add.keyTerm + REGISTER + comma + REGISTER + comma + REGISTER;

            var t_addi = new NTerminal("addi", typeof(OpCodeNodes.OpAddiNode));
            t_addi.Rule = t_addi.keyTerm + REGISTER + comma + REGISTER + comma + number;

            //var t_addi = new NTerminal("addis", typeof(OpCodeNodes.OpAddisNode));
            //t_addi.Rule += REGISTER + comma + REGISTER + comma + number;

            var n_li = new NTerminal("li", typeof(OpCodeNodes.OpLiNode));
            n_li.Rule = n_li.keyTerm + REGISTER + comma + number;

            var n_print = new NTerminal("print", typeof(OpCodeNodes.PrintNode));
            n_print.Rule = n_print.keyTerm + REGISTER;

            var expression = new NonTerminal("expression", typeof(OpCodeNodes.ExpressionNode));
            expression.Rule = n_add.nonTerminal | t_addi.nonTerminal | n_li.nonTerminal | n_print.nonTerminal;

            this.Root = expression;
        }
    }
}
