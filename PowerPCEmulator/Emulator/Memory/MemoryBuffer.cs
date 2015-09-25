using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPCEmulator.Memory
{
    class MemoryBuffer
    {
        private int size;
        private byte[] buffer;

        public MemoryBuffer(int size = 0x2000000)
        {
            buffer = new byte[size];
        }

        /*
        Translates an Xbox 360 address to an index in our memory buffer
        */

        public void LoadData(byte[] data)
        {
            this.buffer = data;
        }

        public long TranslateAddress(long address)
        {
            if(address >= 0x82000000 && address <= 0x83FFF000) //game address space
            {
                return address - 0x82000000;
            }
            return address;
        }

        public void set(long address, byte[] data)
        {
            Buffer.BlockCopy(data, 0, buffer, (int)(address > 0x82000000 ? TranslateAddress(address) : address), data.Length);
        }

        public byte[] get(long address, int size)
        {
            return buffer.Skip((int)(address > 0x82000000 ? TranslateAddress(address) : address)).Take(size).ToArray();
        }
    }
}
