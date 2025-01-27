﻿using Athena.NET.Lexer;
using Athena.NET.Parser.Nodes.DataNodes;

namespace Athena.NET.Parser.Nodes
{
    internal sealed class IdentifierNode : DataNode<ReadOnlyMemory<char>>
    {
        private static readonly TokenIndentificator identifierToken =
            TokenIndentificator.Identifier;

        public IdentifierNode(ReadOnlyMemory<char> data) : base(identifierToken, data)
        {
        }
    }
}
