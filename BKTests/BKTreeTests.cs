using System;
using System.Collections.Generic;
using Xunit;

namespace BK
{
    public class BKTreeTests
    {
        [Fact]
        public void PopulateTree()
        {
            var tree = new BKTree();
            tree.Add("help");
            tree.Add("hell");
            tree.Add("helps");
            tree.Add("hello");
            tree.Add("shell");
            tree.Add("helper");
            tree.Add("loop");
            tree.Add("toop");
            Assert.NotNull(tree);
        }

        [Fact]
        public void CheckWord()
        {
            var tree = new BKTree();
            tree.Add("help");
            tree.Add("hell");
            tree.Add("helps");
            tree.Add("hello");
            tree.Add("shell");
            tree.Add("helper");
            tree.Add("loop");
            tree.Add("toop");
            const string word = "oop";
            const int tolerance = 2;

            var result = new List<string> { "loop", "toop" };
            var results = tree.Match(word, tolerance);

            Assert.Equal(result, results);
        }

        [Fact]
        public void CheckAnotherWord()
        {
            var tree = new BKTree();
            tree.Add("help");
            tree.Add("hell");
            tree.Add("shel");
            tree.Add("smell");
            tree.Add("fell");
            tree.Add("felt");
            tree.Add("oops");
            tree.Add("pop");
            tree.Add("oouch");
            tree.Add("halt");
            const string word = "ops";
            const int tolerance = 3;

            var result = new List<string> { "oops", "pop" };
            var results = tree.Match(word, tolerance);

            Assert.Equal(result, results);
        }

        [Fact]
        public void CheckAWordWithMultipleSuggestions()
        {
            var tree = new BKTree();
            tree.Add("help");
            tree.Add("hell");
            tree.Add("shel");
            tree.Add("smell");
            tree.Add("fell");
            tree.Add("felt");
            tree.Add("oops");
            tree.Add("pop");
            tree.Add("oouch");
            tree.Add("halt");
            const string word = "helt";
            const int tolerance = 2;

            var result = new List<string> { "help", "shel", "fell", "halt", "felt", "hell" };
            var results = tree.Match(word, tolerance);

            Assert.Equal(result, results);
        }
    }
}
