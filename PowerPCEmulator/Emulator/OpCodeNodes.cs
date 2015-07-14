using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Interpreter.Ast;

namespace Bionware.PowerPC
{
    public class OpCodeNodes
    {
        // REGISTER_REGISTER.Rule = REGISTER + COMMA + REGISTER;
        //n_add.Rule = t_add + REGISTER + comma + REGISTER + comma + REGISTER;

        public class ExpressionNode : AstNode
        {
            private AstNode InnerExpression;

            public override void Init(Irony.Ast.AstContext context, Irony.Parsing.ParseTreeNode treeNode)
            {
                base.Init(context, treeNode);

                InnerExpression = AddChild("Inner", treeNode.ChildNodes[0]);
            }

            protected override object DoEvaluate(Irony.Interpreter.ScriptThread thread)
            {
                return InnerExpression.Evaluate(thread);
            }
        }

        public class OpAddValueNode : AstNode
        {
            int destination_register;
            int op_register_one;
            int op_register_two;

            public override void Init(Irony.Ast.AstContext context, Irony.Parsing.ParseTreeNode treeNode)
            {
                base.Init(context, treeNode);
                destination_register = NodeUtilities.UnwrapRegister(treeNode.ChildNodes[0]);
                op_register_one = NodeUtilities.UnwrapRegister(treeNode.ChildNodes[1]);
                op_register_two = NodeUtilities.UnwrapRegister(treeNode.ChildNodes[2]);
            }

            protected override object DoEvaluate(Irony.Interpreter.ScriptThread thread)
            {
                Emulator.Instance.GPR[destination_register] = Emulator.Instance.GPR[op_register_one] + Emulator.Instance.GPR[op_register_two];
                return true;
            }
        }

        public class OpLiNode : AstNode
        {
            int destination_register;
            int value;

            public override void Init(Irony.Ast.AstContext context, Irony.Parsing.ParseTreeNode treeNode)
            {
                base.Init(context, treeNode);
                destination_register = NodeUtilities.UnwrapRegister(treeNode.ChildNodes[0]);
                value = int.Parse(treeNode.ChildNodes[1].FindTokenAndGetText());
            }

            protected override object DoEvaluate(Irony.Interpreter.ScriptThread thread)
            {
                Emulator.Instance.GPR[destination_register] = value;
                return true;
            }
        }

        public class PrintNode : AstNode
        {
            int destination_register;

            public override void Init(Irony.Ast.AstContext context, Irony.Parsing.ParseTreeNode treeNode)
            {
                base.Init(context, treeNode);
                destination_register = NodeUtilities.UnwrapRegister(treeNode.ChildNodes[0]);
            }

            protected override object DoEvaluate(Irony.Interpreter.ScriptThread thread)
            {
                Console.WriteLine(Emulator.Instance.GPR[destination_register]);
                return true;
            }
        }
    }
}
