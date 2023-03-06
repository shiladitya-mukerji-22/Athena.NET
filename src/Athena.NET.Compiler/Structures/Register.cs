﻿using Athena.NET.Compiler;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Athena.NET.Athena.NET.Compiler.Structures
{
    internal sealed class Register : IDisposable
    {
        private NativeMemoryList<MemoryData> memoryData;
        private int lastOffset = 0;

        public int TypeSize { get; }
        public OperatorCodes RegisterCode { get; }

        public Register(OperatorCodes registerCode, Type type)
        {
            RegisterCode = registerCode;
            TypeSize = Marshal.SizeOf(type) * 8;
            memoryData = new NativeMemoryList<MemoryData>();
        }

        //TODO: Make sure, that data value
        //is need to be an a generic unmanged
        //type, maybe consider implementing
        //a INumber interface
        public void AddRegisterData(ReadOnlyMemory<char> identificatorName, int data)
        {
            int dataSize = CalculateByteSize(data);
            memoryData.Add(new MemoryData(identificatorName, lastOffset, dataSize));
            lastOffset += dataSize;
        }

        public bool TryGetMemoryData([NotNullWhen(true)]out MemoryData resultData, ReadOnlyMemory<char> identificatorName) 
        {
            var memoryDataSpan = memoryData.Span;
            for (int i = 0; i < memoryDataSpan.Length; i++)
            {
                MemoryData currentData = memoryDataSpan[i];
                if (identificatorName.Span.
                    SequenceEqual(currentData.IdentifierName.Span)) 
                {
                    resultData = currentData;
                    return true;
                }
            }
            resultData = default;
            return false;
        }

        private int CalculateByteSize(int data) 
        {
            int currentOffset = 0;
            while (currentOffset != TypeSize) 
            {
                int offsetValue = (data >> (currentOffset)) & 255;
                if (offsetValue == 0)
                    return currentOffset;
                currentOffset += 4;
            }
            return -1;
        }

        public void Dispose() 
        {
            memoryData.Dispose();
        }
    }
}
