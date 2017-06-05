using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CSharpViaTest.Collections._10_EnumerablePractices
{
    /* 
     * Description
     * ===========
     * 
     * This test is a slightly difficult practice to manually create a IEnumerator<T>
     * To flatten a tree. In this practice you have to reserve some state in order
     * to complete the tree transversal. Please complete "SkippedEnumeratorPractice"
     * first.
     * 
     * Difficulty: Super Hard
     * 
     * Requirement
     * ===========
     * 
     * - No LINQ method is allowed to use in this test.
     */
    public class TreeEnumeratorPractice
    {
        class TreeNode
        {
            public TreeNode(string id, TreeNode[] children = null)
            {
                Id = id;
                Children = children ?? Array.Empty<TreeNode>();
            }

            public string Id { get; }
            public TreeNode[] Children { get; }
        }

        class Tree : IEnumerable<TreeNode>
        {
            readonly TreeNode root;

            public Tree(TreeNode root)
            {
                this.root = root;
            }

            public IEnumerator<TreeNode> GetEnumerator()
            {
                return new TreeNodeEnumerator(root);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        #region Please modifies the code to pass the test

        class TreeNodeEnumerator : IEnumerator<TreeNode>
        {
            public TreeNodeEnumerator(TreeNode root)
            {
                throw new NotImplementedException();
            }

            public bool MoveNext()
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }

            public TreeNode Current { get; }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        [Fact]
        public void should_flatten_tree_structure()
        {
            var tree = new Tree(
                new TreeNode(
                    "1",
                    new[]
                    {
                        new TreeNode(
                            "1/1",
                            new[]
                            {
                                new TreeNode("1/1/1"),
                                new TreeNode("1/1/2")
                            }),
                        new TreeNode("1/2"),
                        new TreeNode(
                            "1/3",
                            new[]
                            {
                                new TreeNode("1/3/1"),
                                new TreeNode("1/3/2"),
                                new TreeNode("1/3/3")
                            })
                    }));

            string[] treeNodes = tree.OrderBy(node => node.Id).Select(node => node.Id).ToArray();
            Assert.Equal(
                new[] {"1", "1/1", "1/1/1", "1/1/2", "1/2", "1/3", "1/3/1", "1/3/2", "1/3/3"},
                treeNodes);
        }
    }
}