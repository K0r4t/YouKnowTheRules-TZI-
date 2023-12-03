using System;
using System.Linq;

namespace YouKnowTheRules
{
    public class RC5
    {
        private const int W = 32;
        private const int R = 20;
        private const int K = 8;

        private uint[] S;
        Lab1Math genmath = new Lab1Math();
        private ProgressBar progressBar;

        public RC5(byte[] key)
        {
            if (key.Length != K)
            {
                throw new ArgumentException("Key size must be 16 bytes.");
            }

            Initialize(key);
            progressBar = new ProgressBar(100);
        }

        private void Initialize(byte[] key)
        {
            int c = key.Length / (W / 8);
            int t = 2 * (R + 1);

            S = new uint[t];

            uint Pw = 0xb7e15163;
            uint Qw = 0x9e3779b9;

            S[0] = Pw;
            for (int kk = 1; kk < t; kk++)
            {
                S[kk] = S[kk - 1] + Qw;
            }

            int iA = 0;
            int iB = 0;
            uint[] L = new uint[c * 3];
            for (int k = 0; k < c * 3; k++)
            {
                uint A = BitConverter.ToUInt32(key, iA);
                uint B = BitConverter.ToUInt32(key, iB);

                L[k] = A + B;
                iA = (iA + 4) % key.Length;
                iB = (iB + 4) % key.Length;
            }

            int i = 0, j = 0;
            for (int k = 0; k < 3 * Math.Max(t, c); k++)
            {
                S[i] = RotateLeft((S[i] + L[j]), 3);
                i = (i + 1) % t;
                j = (j + 1) % c;
            }
        }

        private uint RotateLeft(uint value, int shift)
        {
            return (value << shift) | (value >> (W - shift));
        }

        private uint RotateRight(uint value, int shift)
        {
            return (value >> shift) | (value << (W - shift));
        }

        public byte[] Encrypt(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentException("Input data must be non-null.");
            }

            int paddedLength = (data.Length % 8 == 0) ? data.Length : data.Length + (8 - (data.Length % 8));
            byte[] paddedData = new byte[paddedLength];
            Array.Copy(data, paddedData, data.Length);

            byte[] iv = genmath.genIv(8, 256, 1103515245, 12345); 

            byte[] encryptedData = new byte[paddedLength + 8]; 
            iv.CopyTo(encryptedData, 0); 
            byte[] previousBlock = new byte[8];
            iv.CopyTo(previousBlock, 0);

            int totalBlocks = paddedLength / 8;
            int updateInterval = 1000;

            for (int i = 0; i < paddedLength; i += 8)
            {
                byte[] block = new byte[8];
                Array.Copy(paddedData, i, block, 0, 8);

                for (int j = 0; j < 8; j++)
                {
                    block[j] ^= previousBlock[j];
                }

                byte[] encryptedBlock = EncryptBlock(block);
                encryptedBlock.CopyTo(previousBlock, 0);
                encryptedBlock.CopyTo(encryptedData, i + 8);

                //if (i % updateInterval == 0 || i == paddedLength - 1)
                //{
                //    progressBar.Update((i) + 1);
                //}
            }

            //progressBar.Complete();
            return encryptedData;
        }

        public byte[] Decrypt(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentException("Input data must be non-null.");
            }

            if (data.Length % 8 != 0)
            {
                throw new ArgumentException("Input data length must be a multiple of 8.");
            }

            byte[] iv = new byte[8];
            Array.Copy(data, 0, iv, 0, 8); 

            byte[] decryptedData = new byte[data.Length - 8]; 
            byte[] previousBlock = iv;

            for (int i = 8; i < data.Length; i += 8) 
            {
                byte[] block = new byte[8];
                Array.Copy(data, i, block, 0, 8);

                byte[] decryptedBlock = DecryptBlock(block);

                for (int j = 0; j < 8; j++)
                {
                    decryptedBlock[j] ^= previousBlock[j];
                }

                block.CopyTo(previousBlock, 0);

                decryptedBlock.CopyTo(decryptedData, i - 8);

                int progress = ((i - 8) / (data.Length - 8)) * 100;
                progressBar.Update(progress);
            }

            progressBar.Complete();
            return decryptedData;
        }

        private byte[] EncryptBlock(byte[] block)
        {
            uint A = BitConverter.ToUInt32(block, 0);
            uint B = BitConverter.ToUInt32(block, 4);

            A += S[0];
            B += S[1];

            for (int i = 1; i <= R; i++)
            {
                A = RotateLeft((A ^ B), (int)B) + S[2 * i];
                B = RotateLeft((B ^ A), (int)A) + S[2 * i + 1];
            }

            byte[] encryptedBlock = new byte[8];
            BitConverter.GetBytes(A).CopyTo(encryptedBlock, 0);
            BitConverter.GetBytes(B).CopyTo(encryptedBlock, 4);

            return encryptedBlock;
        }

        private byte[] DecryptBlock(byte[] block)
        {
            uint A = BitConverter.ToUInt32(block, 0);
            uint B = BitConverter.ToUInt32(block, 4);

            for (int i = R; i > 0; i--)
            {
                B = (RotateRight(B - S[2 * i + 1], (int)A) ^ A);
                A = (RotateRight(A - S[2 * i], (int)B) ^ B);
            }

            B -= S[1];
            A -= S[0];

            byte[] decryptedBlock = new byte[8];
            BitConverter.GetBytes(A).CopyTo(decryptedBlock, 0);
            BitConverter.GetBytes(B).CopyTo(decryptedBlock, 4);

            return decryptedBlock;
        }
    }
}