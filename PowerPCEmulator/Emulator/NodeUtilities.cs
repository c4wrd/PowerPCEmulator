using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Interpreter.Ast;
using Irony.Interpreter;

namespace Bionware.PowerPC
{
    static class NodeUtilities
    {
        public static int UnwrapRegister(Irony.Parsing.ParseTreeNode registerNode)
        {
            return int.Parse(registerNode.ChildNodes[1].FindTokenAndGetText()); //Register->int
        }
    }
}
