﻿using Athena.NET.Athena.NET.Parser.Interfaces;
using Athena.NET.ParseViewer.Interfaces;
using Athena.NET.ParseViewer.NodeElements;
using System.Drawing;

namespace Athena.NET.ParseViewer
{
    public sealed class NodeViewer : IDisposable
    {
        private Bitmap nodeBitmap;
        private Graphics nodeGraphics;

        private readonly ReadOnlyMemory<INodeDrawer> drawElements =
            new INodeDrawer[]
            {
                new NodeGraphElement(15)
            };
        private Size originalSize;

        public ReadOnlyMemory<INode> RenderNodes { get; }
        public Size ImageSize { get; }

        public NodeViewer(ReadOnlyMemory<INode> nodes, Size imageSize)
        {
            RenderNodes = nodes;
            originalSize = imageSize;

            int width = imageSize.Width * (nodes.Length / 2);
            int height = imageSize.Height * (nodes.Length);
            ImageSize = new(width, height);

            //TODO: Create better solutions that
            //will be multiplatform and not just
            //for windows
            nodeBitmap = new Bitmap(ImageSize.Width, ImageSize.Height)!;
            nodeGraphics = Graphics.FromImage(nodeBitmap);
        }

        public Image CreateImage() 
        {
            var nodesSpan = RenderNodes.Span;
            int nodesLenght = nodesSpan.Length;
            for (int i = 0; i < nodesLenght; i++)
            {
                var currentPosition = new Point(ImageSize.Width / 3, ImageSize.Height - (originalSize.Height * i));
                DrawNodeElements(nodesSpan[i], currentPosition);
            }
            return nodeBitmap;
        }

        private void DrawNodeElements(INode node, Point position)
        {
            var nodeElementsSpan = drawElements.Span;
            for (int i = 0; i < nodeElementsSpan.Length; i++)
            {
                var currentElement = nodeElementsSpan[i];
                currentElement.OnDraw(node, nodeGraphics, position);
            }
        }

        public void Dispose() 
        {
            nodeBitmap.Dispose();
            nodeGraphics.Dispose();
        }
    }
}
