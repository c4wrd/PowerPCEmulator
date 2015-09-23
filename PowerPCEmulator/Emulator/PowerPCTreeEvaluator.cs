using Irony.Parsing;
using System;

namespace Bionware.PowerPC
{
    internal class PowerPCTreeEvaluator
    {
        private void print(string text)
        {
            Console.WriteLine(text);
        }
        public void Evaluate(ParseTree tree)
        {
            var emulator = Emulator.Instance;
            foreach (var node in tree.Root.ChildNodes[0].ChildNodes)
            {
                var curNode = node.ChildNodes[0];
                switch (curNode.Term.ToString())
                {
                    case "add":
                        {
                            var rd = NodeUtilities.UnwrapRegister(curNode.ChildNodes[0]);
                            var ra = NodeUtilities.UnwrapRegister(curNode.ChildNodes[1]);
                            var rb = NodeUtilities.UnwrapRegister(curNode.ChildNodes[2]);
                            emulator.GPR[rd] = emulator.GPR[ra] + emulator.GPR[rb];
                            break;
                        }
                    case "addi":
                        {
                            var rd = NodeUtilities.UnwrapRegister(curNode.ChildNodes[0]);
                            var ra = NodeUtilities.UnwrapRegister(curNode.ChildNodes[1]);
                            var simm = NodeUtilities.ParseNumber(curNode.ChildNodes[2]);
                            emulator.GPR[rd] = emulator.GPR[ra] + simm;
                            break;
                        }
                    case "printStatement":
                        {
                            if (curNode.ChildNodes.Count == 1)
                            {
                                var reg = NodeUtilities.UnwrapRegister(curNode.ChildNodes[0]);
                                print(String.Format("PRINT: r{0}: {1}", reg, emulator.GPR[reg]));
                            }
                            else if (curNode.ChildNodes.Count == 2)
                            {
                                var reg = NodeUtilities.UnwrapRegister(curNode.ChildNodes[0]);
                                var reg2 = NodeUtilities.UnwrapRegister(curNode.ChildNodes[1]);
                                if (reg2 > reg)
                                {
                                    for (int i = reg; i <= reg2; i++)
                                    {
                                        print(String.Format("PRINT: r{0}: {1}", i, emulator.GPR[i]));
                                    }
                                }
                                else
                                {
                                    print("PRINT: invalid print syntax");
                                }
                            }
                            else
                            {
                                //todo print all registers
                                var regStr = "";
                                for(int i = 0; i < 32; i++)
                                {
                                    regStr += String.Format("r{0}: {1}\r\n", i, emulator.GPR[i]);
                                }
                                Console.Write(
                                    "MACHINE PRINT\r\n{0}",
                                    regStr
                                    );
                            }
                            break;
                        }
                    case "li":
                        {
                            var rd = NodeUtilities.UnwrapRegister(curNode.ChildNodes[0]);
                            var num = NodeUtilities.ParseNumber(curNode.ChildNodes[1]);
                            emulator.GPR[rd] = num;
                            break;
                        }
                    case "sub":
                        {
                            var rd = NodeUtilities.UnwrapRegister(curNode.ChildNodes[0]);
                            var ra = NodeUtilities.UnwrapRegister(curNode.ChildNodes[1]);
                            var rb = NodeUtilities.UnwrapRegister(curNode.ChildNodes[2]);
                            emulator.GPR[rd] = emulator.GPR[ra] - emulator.GPR[rb];
                            break;
                        }
                    case "subi":
                        {
                            var rd = NodeUtilities.UnwrapRegister(curNode.ChildNodes[0]);
                            var ra = NodeUtilities.UnwrapRegister(curNode.ChildNodes[1]);
                            var simm = NodeUtilities.ParseNumber(curNode.ChildNodes[2]);
                            emulator.GPR[rd] = emulator.GPR[ra] - simm;
                            break;
                        }
                    default:
                        break;
                }
            }
        }
    }
}