namespace Bionware.PowerPC
{
    internal class NTerminal
    {
        private OpCodeNodes.OpAddNode opAddValueNode;
        private string v;

        public NTerminal(string v, OpCodeNodes.OpAddNode opAddValueNode)
        {
            this.v = v;
            this.opAddValueNode = opAddValueNode;
        }
    }
}