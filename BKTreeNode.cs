using System;
using System.Collections.Generic;
using System.Text;

namespace BK
{
    class BKTreeNode
    {
        public Dictionary<int, BKTreeNode> Children;

        public BKTreeNode(string value)
        {
            this.Value = value;
            Children = new Dictionary<int, BKTreeNode>();
        }

        public string Value { get; }

        
        public BKTreeNode GetChild(int key)
        {
            Children.TryGetValue(key, out BKTreeNode current);
            return current;
        }
    }
}
