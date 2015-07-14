namespace Bionware.PowerPC
{
    internal class NTerminal
    {
        private OpCodeNodes.OpAddValueNode opAddValueNode;
        private string v;

        public NTerminal(string v, OpCodeNodes.OpAddValueNode opAddValueNode)
        {
            this.v = v;
            this.opAddValueNode = opAddValueNode;
        }
    }
}