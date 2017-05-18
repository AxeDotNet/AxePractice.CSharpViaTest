using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CSharpViaTest.Collections.Annotations;
using Xunit;

namespace CSharpViaTest.Collections._10_EnumerablePractices
{
    [SuperHard]
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

        // Attention
        // 
        // * No LINQ method is allowed to use.

        class TreeNodeEnumerator : IEnumerator<TreeNode>
        {
            readonly TreeNode root;
            readonly Stack<TreeNode> unvisitedBranch = new Stack<TreeNode>();

            TreeNode current;

            public TreeNodeEnumerator(TreeNode root)
            {
                this.root = root;
                current = CreateBeginNode(root);
            }

            static TreeNode CreateBeginNode(TreeNode root)
            {
                return new TreeNode(null, new []{root});
            }

            public bool MoveNext()
            {
                foreach (TreeNode child in current.Children)
                {
                    unvisitedBranch.Push(child);
                }

                if (unvisitedBranch.Count <= 0)
                {
                    return false;
                }

                current = unvisitedBranch.Pop();
                return true;
            }

            public void Reset()
            {
                current = CreateBeginNode(root);
            }

            public TreeNode Current => current;

            object IEnumerator.Current => Current;

            public void Dispose()
            {
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