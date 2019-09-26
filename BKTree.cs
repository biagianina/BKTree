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
            List<string> result = new List<string>();
            Match(rootNode, word, tolerance, result);
            return result;
        }

        private void AddToChildren(BKTreeNode node, string value)
        {
            int levenstheinDist = GetLevenstheinDistance(node.Value, value);
            if (!node.Children.ContainsKey(levenstheinDist))
            {
                node.Children.Add(levenstheinDist, new BKTreeNode(value));
            }
            else
            {
                node.Children.TryGetValue(levenstheinDist, out BKTreeNode current);
                AddToChildren(current, value);
            }
        }

        private void Match(BKTreeNode node, string word, int tolerance, ICollection<string> result)
        {
            int dist = GetLevenstheinDistance(node.Value, word);

            if (dist <= tolerance)
            {
                result.Add(node.Value);
            }

            int lowerDist = dist - tolerance > 0 ? dist - tolerance : 1;
            int upperDist = dist + tolerance;

            for (int i = upperDist; i >= lowerDist; i--)
            {
                if (node.Children.TryGetValue(i, out BKTreeNode child))
                {
                    Match(child, word, tolerance, result);
                }
            }
        }

        private int GetLevenstheinDistance(string first, string second)
        {
            int[,] distance;
            int n = first.Length;
            int m = second.Length;
            int cost;
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            distance = new int[n + 1, m + 1];
            for (int i = 0; i <= n; i++)
            {
                distance[i, 0] = i;
            }

            for (int j = 0; j <= m; j++)
            {
                distance[0, j] = j;
            }

            for (int i = 1; i <= n; i++)
            {
                char firstValueChar = first[i - 1];
                for (int j = 1; j <= m; j++)
                {
                    char secondValueChar = second[j - 1];
                    cost = firstValueChar == secondValueChar ? 0 : 1;

                    distance[i, j] = Math.Min(
                        Math.Min(
                            distance[i - 1, j] + 1,
                            distance[i, j - 1] + 1),
                        distance[i - 1, j - 1] + cost);
                }
            }

            return distance[n, m];
        }
    }
}
