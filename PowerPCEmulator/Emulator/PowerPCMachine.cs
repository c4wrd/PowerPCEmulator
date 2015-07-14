using Irony.Interpreter;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bionware.PowerPC
{
    public static class Emulator
    {
        public static PowerPCMachine Instance = new PowerPCMachine();
        private static LanguageData language = new LanguageData(new Bionware.PowerPC.PowerPCGrammer());
        private static Parser parser = new Parser(language);

        public static void Evaluate(String text)
        {
            ParseTree parseTree = parser.Parse(text);
            if (parseTree.Root != null)
            {
                ScriptApp app = new ScriptApp(language);
                ScriptThread thread = new ScriptThread(app);

                app.Evaluate(parseTree);
            }
        }

        public static long getRegister(int reg)
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
