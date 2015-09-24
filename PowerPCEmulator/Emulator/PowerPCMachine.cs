using Irony.Interpreter;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bionware.PowerPC
{
    public class Emulator
    {
        public PowerPCMachine Instance;
        private static LanguageData language = new LanguageData(new Bionware.PowerPC.PowerPCGrammar());
        private static Parser parser = new Parser(language);

        public Emulator()
        {
             Instance = new PowerPCMachine();
        }

        public Dictionary<string, Object> Evaluate(String text, int numInstructions)
        {
            ParseTree parseTree = parser.Parse(text);
            if (parseTree.Root != null)
            {
                Console.WriteLine("Time To Parse {0} instructions: {1} ms", numInstructions, parseTree.ParseTimeMilliseconds);
                PowerPCTreeEvaluator app = new PowerPCTreeEvaluator();
                var startTime = System.Environment.TickCount;
                app.Evaluate(parseTree, Instance);
                var endTime = System.Environment.TickCount - startTime;
                Console.WriteLine("Time to execute {0} instructions: {1} ms", numInstructions, endTime);
                return new Dictionary<string, object> {
                    { "parse_time", parseTree.ParseTimeMilliseconds },
                    { "execution_time", endTime },
                    { "status", "ok" }
                };
            }
            var errorToken = parseTree.Tokens.Where(n => n.Terminal.Name == "SYNTAX_ERROR").FirstOrDefault();
            return new Dictionary<string, object> {
                { "status", "error" },
                { "error_message", parseTree.ParserMessages[0] },
                { "error_location", errorToken.Location }
            };
        }

        public long getRegister(int reg)
        {
            return Instance.GPR[reg];
        }
    }

    public class PowerPCMachine
    {
        public class CONDITION_REGISTER
        {
            bool LT;
            bool GT;
            bool EQ;
            bool OVERVIEW;

            public CONDITION_REGISTER()
            {
                LT = false;
                GT = false;
                EQ = false;
                OVERVIEW = false;
            }

            public bool getFlag(int index)
            {
                switch(index){
                    case 0:
                        return LT;
                    case 1:
                        return GT;
                    case 2:
                        return EQ;
                    case 3:
                        return OVERVIEW;
                }
                return false;
            }
        }

        public long[] GPR;
        public long[] FPR;
        public CONDITION_REGISTER[] CR;
        public int CTR;
        public int LR;
        public int INSTRUCTION_INDEX;

        public PowerPCMachine()
        {
            GPR = new long[32];
            FPR = new long[32];
            CR = new CONDITION_REGISTER[8];
            CTR = 0;
            LR = 0;
            INSTRUCTION_INDEX = 0;
        }
    }
}
