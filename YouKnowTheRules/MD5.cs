using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouKnowTheRules
{
    public class MD5
    {
        private uint A, B, C, D;
        private ProgressBar progressBar;

        public MD5()
        {
            init();
            progressBar = new ProgressBar(100);
        }

        private void init()
        {
            A = 0x67452301;
            B = 0xEFCDAB89;
            C = 0x98BADCFE;
            D = 0x10325476;
        }

        int[] MD5ShiftAmounts = new int[64]
        {
            7, 12, 17, 22, 7, 12, 17, 22, 7, 12, 17, 22, 7, 12, 17, 22,
            5, 9, 14, 20, 5, 9, 14, 20, 5, 9, 14, 20, 5, 9, 14, 20,
            4, 11, 16, 23, 4, 11, 16, 23, 4, 11, 16, 23, 4, 11, 16, 23,
            6, 10, 15, 21, 6, 10, 15, 21, 6, 10, 15, 21, 6, 10, 15, 21
        };

        public byte[] ComputeHash(byte[] input)
        {
            var preparedInput = PrepareMD5Message(input);
            int totalBlocks = preparedInput.Length / 64;
            progressBar = new ProgressBar(totalBlocks); 

            ProcessMessage(preparedInput);

            byte[] hash = new byte[16];
            BitConverter.GetBytes(A).CopyTo(hash, 0);
            BitConverter.GetBytes(B).CopyTo(hash, 4);
            BitConverter.GetBytes(C).CopyTo(hash, 8);
            BitConverter.GetBytes(D).CopyTo(hash, 12);

            progressBar.Complete(); 

            init();
            return hash;
        }

        private byte[] PrepareMD5Message(byte[] input)
        {
            int originalLengthInBits = input.Length * 8;
            int lengthWithOneAppended = originalLengthInBits + 1;
            int lengthMod512 = lengthWithOneAppended % 512;
            int paddingLength = (lengthMod512 < 448) ? 448 - lengthMod512 : 960 - lengthMod512;

            long totalLength = originalLengthInBits + paddingLength + 64; 
            byte[] paddedInput = new byte[totalLength / 8 + 1];

            Buffer.BlockCopy(input, 0, paddedInput, 0, input.Length);
            paddedInput[input.Length] = 0x80;


            byte[] lengthBytes = BitConverter.GetBytes((long)originalLengthInBits);
            Buffer.BlockCopy(lengthBytes, 0, paddedInput, paddedInput.Length - 8, 8);
            return paddedInput;
        }

        private void ProcessMessage(byte[] preparedInput)
        {
            int totalBlocks = preparedInput.Length / 64;
            int updateInterval = 1000; 
            for (int i = 0; i < totalBlocks; i++)
            {
                uint[] block = new uint[16];
                for (int j = 0; j < 16; j++)
                {
                    block[j] = BitConverter.ToUInt32(preparedInput, (i * 64) + (j * 4));
                }

                ProcessBlock(block);

                if (i % updateInterval == 0 || i == totalBlocks - 1)
                {
                    progressBar.Update(i + 1);
                }
            }
        }

        private void ProcessBlock(uint[] block)
        {
            uint AA = A;
            uint BB = B;
            uint CC = C;
            uint DD = D;

            uint[] T = new uint[64];
            for (int i = 0; i < 64; i++)
            {
                T[i] = (uint)(Math.Pow(2, 32) * Math.Abs(Math.Sin(i + 1)));
            }

            for (int i = 0; i < 64; i++)
            {
                uint F, g;
                if (i < 16) { F = (B & C) | (~B & D); g = (uint)i; }
                else if (i < 32) { F = (D & B) | (~D & C); g = (uint)((5 * i + 1) % 16); }
                else if (i < 48) { F = B ^ C ^ D; g = (uint)((3 * i + 5) % 16); }
                else { F = C ^ (B | ~D); g = (uint)((7 * i) % 16); }

                uint temp = D;
                D = C;
                C = B;
                B = B + LeftRotate((A + F + T[i] + block[g]), MD5ShiftAmounts[i]);
                A = temp;
            }

            A += AA;
            B += BB;
            C += CC;
            D += DD;
        }

        private uint LeftRotate(uint x, int n)
        {
            return (x << n) | (x >> (32 - n));
        }

    }

}