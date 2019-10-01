using System;
using System.Collections.Generic;
using System.Text;

namespace BK
{
    public class BKTree
    {
        private BKTreeNode rootNode;

        public void Add(string value)
        {
            if (rootNode != null)
            {
                AddToChildren(rootNode, value);
            }
            else
            {
                rootNode = new BKTreeNode(value);
            }
        }

        public List<string> Match(string word, int tolerance)
        {
           return Match(rootNode, word, tolerance);
        }

        private void AddToChildren(BKTreeNode node, string value)
        {
            int levenstheinDist = GetLevenstheinDistance(node.Value, value);

            if (node.GetChild(levenstheinDist) == null)
            {
                node.Add(levenstheinDist, value);
            }
            else
            {
                AddToChildren(node.GetChild(levenstheinDist), value);
            }
        }

        private List<string> Match(BKTreeNode node, string word, int tolerance)
        {
            List<string> result = new List<string>();
            int dist = GetLevenstheinDistance(node.Value, word);

            if (dist <= tolerance)
            {
                result.Add(node.Value);
            }

            int lowerDist = dist - tolerance > 0 ? dist - tolerance : 1;
            int upperDist = dist + tolerance;

            for (int i = upperDist; i >= lowerDist; i--)
            {
                if (node.GetChild(i) != null)
                {
                   var nodeResult = Match(node.GetChild(i), word, tolerance);
                   foreach (var match in nodeResult)
                   {
                       result.Add(match);
                   }
                }
            }

            return result;
        }

        private int GetLevenstheinDistance(string first, string second)
        {
            int firstLength = first.Length;
            int secondLength = second.Length;
            int[,] levDist = new int[firstLength + 1, secondLength + 1];

            if (firstLength == 0)
            {
                return secondLength;
            }

            if (secondLength == 0)
            {
                return firstLength;
            }

            for (int i = 1; i <= firstLength; i++)
            {
                levDist[i, 0] = i;
            }
            
            for (int j = 1; j <= secondLength; j++)
            {
                levDist[0, j] = j;
            }
            
            for (int i = 1; i <= firstLength; i++)
            {
                char firstChar = first[i - 1];
                for (int j = 1; j <= secondLength; j++)
                {
                    int substituionCost = firstChar == second[j - 1] ? 0 : 1;

                    levDist[i, j] = Math.Min(Math.Min(levDist[i - 1, j] + 1,
                        levDist[i, j - 1] + 1),
                        levDist[i - 1, j - 1] + substituionCost);
                }
            }

            return levDist[firstLength, secondLength];
        }
    }
}
