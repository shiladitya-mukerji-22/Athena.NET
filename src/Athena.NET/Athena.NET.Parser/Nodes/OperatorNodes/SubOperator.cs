﻿using Athena.NET.Athena.NET.Lexer.Structures;

namespace Athena.NET.Athena.NET.Parser.Nodes.OperatorNodes
{
    internal sealed class SubOperator : OperatorNode
    {
        public override int OperatorWeight { get; } = 3;
        public override TokenIndentificator NodeToken { get; } = TokenIndentificator.Sub;

        public SubOperator(ReadOnlyMemory<Token> tokens, int nodeIndex) : base(tokens, nodeIndex)
        {

        }

        protected override int CalculateData(int firstData, int secondData) =>
            firstData - secondData;
    }
}