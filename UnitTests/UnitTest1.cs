using System.Collections;
using BinaryTree;

namespace UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

 
        
        [Test]
        public void IsSearchTheSameAsTakingByIndexer()
        {
            int[] arr = { 1, 2, 3 };
            BinaryTree<string, int[]> tree = new()
            {
                { "first", arr }
            };
            Assert.That(arr, Is.EqualTo(tree["first"]));
            Assert.That(arr, Is.EqualTo(tree.Search("first")));
        }

        [Test]
        public void CanCompareDifferentTrees()
        {
            BinaryTree<int, int>? firstTree = new()
            {
                { 4, 4 },
                { 2, 2 },
                { 1, 1 },
                { 3, 3 },
            };

            BinaryTree<int, int> secondTree = new()
            {
                { 1, 1 },
                { 3, 3 },
                { 2, 2 },
                { 4, 4 },
            };

            Assert.IsFalse(firstTree == secondTree);

            firstTree = new()
            {
                { 4, 4 },
                { 2, 2 },
                { 1, 1 },
                { 3, 3 },
            };
            secondTree = new()
            {
                { 4, 4 },
                { 2, 2 },
                { 3, 3 },
                { 1, 2 },
            };

            Assert.IsFalse(firstTree == secondTree);

            firstTree = new()
            {
                { 1, 2 },
                { 2, 3 },
                { 3, 4 },
            };
            secondTree = new()
            {
                { 1, 3 },
                { 2, 4 },
                { 3, 5 },
            };
            Assert.That(firstTree != secondTree);
            Assert.That(firstTree.Equals(secondTree));

            firstTree = null;
            bool? x;
            Assert.Throws<NullReferenceException>(() => x = firstTree == secondTree);
            Assert.That(firstTree == firstTree);
            firstTree = new();
            Assert.Throws<NullReferenceException>(() => x = firstTree == secondTree);
            Assert.Throws<NullReferenceException>(() => x = secondTree == firstTree);
        }

        [Test]
        public void CanCompareSameTrees()
        {
            BinaryTree<int, int> firstTree = new()
            {
                { 4, 4 },
                { 2, 2 },
                { 1, 1 },
                { 3, 3 },
            };
            BinaryTree<int, int> secondTree = new()
            {
                { 4, 4 },
                { 2, 2 },
                { 1, 1 },
                { 3, 3 },
            };

            Assert.That(firstTree == secondTree);
        }

        [Test]
        public void CanCompareLowerEqualOrUpperTrees()
        {
            BinaryTree<int, int> firstTree = new()
            {
                { 4, 4 },
                { 2, 2 },
                { 1, 1 },
                { 3, 3 },
            };
            BinaryTree<int, int> secondTree = new()
            {
                { 2, 2 },
                { 1, 1 },
                { 3, 3 },
            };

            Assert.IsFalse(firstTree <= secondTree);
            Assert.IsTrue(firstTree >= secondTree);
            Assert.IsFalse(firstTree == secondTree);
            Assert.IsTrue(secondTree <= firstTree);
        }

        [Test]
        public void CanCompareHeights()
        {
            BinaryTree<int, int> firstTree = new()
            {
                { 4, 4 },
                { 2, 2 },
                { 1, 1 },
                { 3, 3 },
            };
            BinaryTree<int, int> secondTree = new()
            {
                { 2, 2 },
                { 1, 1 },
                { 3, 3 },
            };

            Assert.That(firstTree.Height != secondTree.Height);

            secondTree.Add(4, 4);

            Assert.That(firstTree.Height == secondTree.Height);
        }

        [Test]
        public void CanCompareValues()
        {
            BinaryTree<int, int[]> firstTree = new();
            BinaryTree<int, int[]> secondTree = new();

            int[] testArray = { 1, 2, 3 };
            int[] ints = { 1, 2 };

            firstTree.Add(4, testArray);
            firstTree.Add(2, ints);
            firstTree.Add(1, ints);
            firstTree.Add(3, ints);

            secondTree.Add(4, ints);
            secondTree.Add(1, testArray);
            secondTree.Add(3, testArray);

            Assert.That(firstTree.Value != secondTree.Value);

            BinaryTree<int, int[]> thirdTree = new()
            {
                { 4, testArray },
                { 2, ints },
            };

            Assert.That(thirdTree.Value == firstTree.Value);
            Assert.That(thirdTree.Value != secondTree.Value);

            BinaryTree<int, int> tree1 = new()
            {
                { 1, 1 },
                { 2, 2 },
                { 4, 4 },
                { 5, 6 },
            };
            BinaryTree<int, int> tree2 = new()
            {
                { 1, 1 },
                { 2, 2 },
                { 4, 4 },
            };
            Assert.That(tree1 != tree2);
        }

        [Test]
        public void CanCompareCounts()
        {
            BinaryTree<int, int[]> firstTree = new();
            BinaryTree<int, int[]> secondTree = new();

            int[] testArray = { 1, 2, 3 };
            int[] ints = { 1, 2 };

            firstTree.Add(4, testArray);
            firstTree.Add(2, ints);
            firstTree.Add(1, ints);
            firstTree.Add(3, ints);

            secondTree.Add(4, ints);
            secondTree.Add(1, testArray);
            secondTree.Add(3, testArray);

            Assert.That(firstTree.Count != secondTree.Count);
            Assert.That(firstTree.Count > secondTree.Count);
            Assert.That(secondTree.Count < firstTree.Count);

            secondTree.Add(5, ints);

            Assert.That(firstTree.Count == secondTree.Count);
            Assert.That(firstTree.Count >= secondTree.Count);
        }

        [Test]
        public void CheckIClonable()
        {
            BinaryTree<int, int> firstTree = new()
            {
                { 1, 2 },
                { 3, 4 },
                { 4, 5 }
            };
            var clonedTree = firstTree.Clone() as BinaryTree<int, int>;

            Assert.That(clonedTree, Is.Not.SameAs(firstTree));
            Assert.That(firstTree == clonedTree);

            firstTree = new()
            {
                { 1, 1 },
                { -1, 1 },
                { -5, 10 },
                { -3, 101 },
            };
            clonedTree = firstTree.Clone() as BinaryTree<int, int>;
            Assert.That(firstTree == clonedTree);
        }

        [Test]
        public void CheckIEnumerable()
        {
            BinaryTree<int, string> firstTree = new()
            {
                { 1, "1" },
                { 2, "2" },
                { 3, "33" }
            };
            List<string> list = new()
            {
                "1",
                "2",
                "33"
            };
            List<string> enumeratedList = new();
            foreach (var item in firstTree)
            {
                enumeratedList.Add(item.Value);
            }

            Assert.That(list.SequenceEqual(enumeratedList));
        }

        [Test]
        public void CheckIfExceptionThrow()
        {
            BinaryTree<int, int> myTree = new();

            int? b;
            string? c;

            Assert.Throws<NullReferenceException>(() => b = myTree.Value);
            Assert.Throws<NullReferenceException>(() => b = myTree.Search(2));
            Assert.Throws<NullReferenceException>(() => c = myTree.ToString());

            Assert.That(myTree.Count == 0);
        }

        [Test]
        public void CheckToString()
        {
            BinaryTree<int, int> myTree = new()
            {
                { 5, 6 },
            };

            string result = "root: 6, Count: 1, Height: 1";

            Assert.That(string.Equals(myTree.ToString(), result));

            myTree.Remove(5);
            string? x;

            Assert.Throws<NullReferenceException>(() => x = myTree.ToString());
        }

        [Test]
        public void CheckEquals()
        {
            BinaryTree<int, int> myTree = new BinaryTree<int, int>()
            {
                { 1, 2 }
            };
            BinaryTree<int, int> mySecondTree = new()
            {
                { 2, 1 }
            };

            Assert.That(!myTree.Equals(mySecondTree));

            mySecondTree.Remove(2);

            Assert.Throws<NullReferenceException>(() => mySecondTree.Equals(myTree));

            Assert.That(!myTree.Equals(mySecondTree));

            int x = 1;
            object? obj = x;

            Assert.That(!myTree.Equals(obj));

            obj = null;
            BinaryTree<int, int>? nullTree = null;

            Assert.That(!myTree.Equals(nullTree));

            Assert.That(myTree.Equals(myTree));

            object myObj = myTree;
            Assert.That(myTree.Equals(myObj));
        }

        [Test]
        public void CheckTakingByIndex()
        {
            BinaryTree<int, int> myTree = new()
            {
                { 2, 5 },
            };
            int? res;

            Assert.That(myTree[2] == 5);
            Assert.Throws<KeyNotFoundException>(() => res = myTree[5]);
        }

        [Test]
        public void CheckEqualsAndOperator()
        {
            BinaryTree<int, int> firstTree = new()
            {
                { 1, 1 },
                { 2, 1 },
                { 3, 2 },
            };
            BinaryTree<int, int> secondTree = new()
            {
                { 1, 2 },
                { 2, 2 },
                { 3, 2 },
            };

            Assert.That(firstTree != secondTree);
            Assert.That(firstTree.Equals(secondTree));
        }

        [Test]
        public void CheckRemovingByKey()
        {
            BinaryTree<int, int> tree = new();

            Assert.Throws<ArgumentNullException>(() => tree.Remove(3));

            tree.Add(1, 3);

            Assert.Throws<KeyNotFoundException>(() => tree.Remove(3));

            tree = new()
            {
                { 1, 2 },
                { 3, 2 },
                { 4, 6 },
                { 6, 7 },
                { 5, 3 },
                { -2, 5 },
            };
            tree.Remove(4);

            BinaryTree<int, int> resultTree = new()
            {
                { 1, 2 },
                { 3, 2 },
                { 6, 7 },
                { 5, 3 },
                { -2, 5 },
            };

            Assert.That(tree == resultTree);
            Assert.That(tree.ToString() == resultTree.ToString());

            tree = new()
            {
                { 1, 1 },
                { -1, 2 },
                { 2, 2 },
                { 9, 8 },
                { 10, 10 },
                { 11, 10 },
            };
            resultTree = new()
            {
                { 1, 1 },
                { -1, 2 },
                { 2, 2 },
                { 9, 8 },
                { 11, 10 },
            };
            tree.Remove(10);
            Assert.That(tree.ToString() == resultTree.ToString());
            Assert.That(resultTree == tree);

            tree = new()
            {
                { 1, 1 },
                { -1, 2 },
                { -5, 3 },
                { -3, 2 },
                { -30, 10 },
                { -10, 5 },
            };
            resultTree = new()
            {
                { 1, 1 },
                { -1, 2 },
                { -5, 3 },
                { -30, 10 },
                { -10, 5 },
            };
            tree.Remove(-3);
            Assert.That(resultTree == tree);
            Assert.That(resultTree.Equals(tree));

            tree = new()
            {
                { 1, 1 },
                { -3, 3 },
                { -5, 5 },
                { -2, 3 },
                { -10, 5 },
                { -8, 0 },
            };
            resultTree = new()
            {
                { 1, 1 },
                { -3, 3 },
                { -5, 5 },
                { -2, 3 },
                { -8, 0 },
            };
            tree.Remove(-10);
            Assert.That(resultTree == tree);

            tree = new()
            {
                { 1, 1 },
                { -3, 3 },
                { -5, 5 },
                { -2, 3 },
                { -10, 5 },
                { -8, 0 },
            };
            resultTree = new()
            {
                { -3, 3 },
                { -5, 5 },
                { -2, 3 },
                { -10, 5 },
                { -8, 0 },
            };
            tree.Remove(1);
            Assert.That(resultTree == tree);

            tree = new()
            {
                { 1, 1 },
                { -1, 1 },
                { -5, 10 },
                { -3, 5 },
            };
            resultTree = new()
            {
                { 1, 1 },
                { -5, 10 },
                { -3, 5 },
            };
            tree.Remove(-1);
            Assert.That(resultTree == tree);

            tree = new()
            {
                { 1, 1 },
                { -1, 1 },
                { -5, 4 },
                { -4, 10 },
                { -20, 5 },
            };
            resultTree = new()
            {
                { 1, 1 },
                { -1, 1 },
                { -4, 10 },
                { -20, 5 },
            };
            tree.Remove(-5);
            Assert.That(tree == resultTree);
        }

        [Test]
        public void CheckSubtree()
        {
            BinaryTree<int, int>? tree = new()
            {
                { 1, 2 },
            };
            BinaryTree<int, int>? secondTree = null;

            bool x;

            Assert.Throws<NullReferenceException>(() => x = secondTree <= tree);

            tree = null;
            secondTree = new()
            {
                { 1, 2 },
            };

            Assert.Throws<NullReferenceException>(() => x = secondTree <= tree);
            Assert.Throws<NullReferenceException>(() => x = tree <= null);
            Assert.Throws<NullReferenceException>(() => x = null <= tree);

            BinaryTree<int, string> tree1 = new()
            {
                { 5, "Node 5" },
                { 3, "Node 3" },
                { 7, "Node 7" },
            };

            BinaryTree<int, string> tree2 = new()
            {
                { 5, "Node 5" },
                { 3, "Node 3" },
                { 7, "Node 7" },
            };

            Assert.True(tree2 <= tree1);

            tree1 = new()
            {
                { 5, "Node 5" },
                { 3, "Node 3" },
                { 7, "Node 7" },
                { 2, "Node 2" },
                { 4, "Node 4" },
            };

            tree2 = new()
            {
                { 3, "Node 3" },
                { 2, "Node 2" },
                { 4, "Node 4" }
            };

            Assert.True(tree1 >= tree2);

            tree1 = new()
            {
                { 5, "Node 5" },
                { 3, "Node 3" },
                { 7, "Node 7" },
                { 2, "Node 2" },
                { 4, "Node 4" },
            };

            tree2 = new()
            {
                { 6, "Node 6" },
                { 3, "Node 3" },
                { 8, "Node 8" }
            };

            Assert.False(tree1 <= tree2);
        }


        [Test]
        public void CheckingEqualsForNodes()
        {
            Node<int, string> node1 = new TreeNode<int, string>(1, "Node 1");
            Node<int, string> node2 = new TreeNode<int, string>(1, "Node 1");

            Assert.IsTrue(node1.Equals(node2));

            node1 = new TreeNode<int, string>(1, "Node 1");
            node2 = new TreeNode<int, string>(2, "Node 2");

            Assert.IsFalse(node1.Equals(node2));

            node1 = new TreeNode<int, string>(1, "Node 1");
            node2 = new TreeNode<int, string>(2, "Node 2");

            Assert.IsFalse(node1.Equals(node2));
            Node<int, string>? nullableNode = null;

            Assert.IsFalse(node2.Equals(nullableNode));
            Assert.Throws<NullReferenceException>(() => nullableNode.Equals(node2));
        }

        [Test]
        public void CheckOperatorsForNode()
        {
            Node<int, string> node1 = new TreeNode<int, string>(1, "Node 1");
            Node<int, string> node2 = new TreeNode<int, string>(1, "Node 1");

            Assert.IsTrue(node1 == node2);

            node1 = new TreeNode<int, string>(1, "Node 1");
            node2 = new TreeNode<int, string>(2, "Node 2");

            Assert.IsFalse(node1 == node2);

            node1 = new TreeNode<int, string>(1, "Node 1");
            node2 = new TreeNode<int, string>(2, "Node 2");

            Assert.IsTrue(node1 != node2);

            node1 = new TreeNode<int, string>(1, "Node 1");
            node2 = new TreeNode<int, string>(1, "Node 1");

            Assert.IsFalse(node1 != node2);
        }


        [Test]
        public void ToString_ReturnsFormattedString()
        {
            Node<int, string> node = new TreeNode<int, string>(1, "Node 1");
            string expectedString = "Key: 1, Value: Node 1";

            string actualString = node.ToString();

            Assert.That(expectedString == actualString);
        }

        [Test]
        public void SearchSpecificNodeInTree()
        {
            var fillArray = new[]
            {
                (3, "Node 3"),
                (7, "Node 7"),
                (2, "Node 2"),
                (4, "Node 4")
            };

            BinaryTree<int, string> tree = new BinaryTree<int, string>(5, "Node 5", fillArray);

            string? node3Value = tree.Search(3);
            string? node7Value = tree.Search(7);
            string? node2Value = tree.Search(2);
            string? node4Value = tree.Search(4);

            Assert.That("Node 3" == node3Value);
            Assert.That("Node 7" == node7Value);
            Assert.That("Node 2" == node2Value);
            Assert.That("Node 4" == node4Value);
        }

        [Test]
        public void CheckMinMaxNodes()
        {
            BinaryTree<int, string> myTree = new()
            {
                { 5, "123" },
                { 10, "1" },
                { 3, "-1" },
                { 10, "12" },
            };
            Node<int, string> minNode = new TreeNode<int, string>(3, "-1");
            Node<int, string> maxNode = new TreeNode<int, string>(10, "12");

            Assert.That(myTree.FindMinNode(myTree.Root) == minNode);
            Assert.That(myTree.FindMaxNode(myTree.Root) == maxNode);
        }

        [Test]
        public void CheckRoot()
        {
            Node<int, int> myRoot = new TreeNode<int, int>(1, 1);
            BinaryTree<int, int> myTree = new()
            {
                { 1, 1 },
                { 2, 2 },
                { -2, 5 },
                { 5, 10 },
            };

            BinaryTree<int, int> tree1 = new()
            {
                { 1, 2 },
                { 3, 4 },
                { 4, 5 },
                { -4, 10 },
                { -5, 10 },
            };
            BinaryTree<int, int> tree2 = new()
            {
                { 1, 2 },
                { 2, 42 },
                { 3, 53 },
                { -2, 120 },
                { -6, 110 },
            };
            Assert.That(myRoot == myTree.Root!);
            Assert.That(tree1.Root! == tree2.Root!);
        }


        [Test]
        public void TestTreeNodeSerialization()
        {
            TreeNode<int, string> treeNode = new TreeNode<int, string>(1, "One");

            string filePath = "treeNode.xml";
            treeNode.SerializeXml(filePath);

            TreeNode<int, string>? deserializedTreeNode = TreeNode<int, string>.DeserializeXml(filePath);

            Assert.IsTrue(TreeNode<int, string>.TreesEqualWithValues(treeNode, deserializedTreeNode));
        }

        [Test]
        public void TestBinaryTreeSerialization()
        {
            BinaryTree<int, string> binaryTree = new()
            {
                { 5, "E" },
                { 3, "C" },
                { 7, "G" },
                { 2, "B" },
                { 4, "D" },
                { 6, "F" },
                { 8, "H" },
            };
            binaryTree.SerializeXml("binary_tree_data.xml");

            var deserializedTree = BinaryTree<int, string>.DeserializeXml("binary_tree_data.xml");

            Assert.That(binaryTree, Is.EqualTo(deserializedTree));
        }

        [Test]
        public void CheckConstructorsForTreeNode()
        {
            TreeNode<int, int> resultNode = new(1, 2);
            TreeNode<int, int> node1 = new(1);
            TreeNode<int, int> node2 = new();


            Assert.That(resultNode.Key == node1.Key);

            node2 = new(1, 2);
            Assert.That(resultNode == node2);
        }

        [Test]
        public void CheckHashCodeForNode()
        {
            Node<int, int> node1 = new TreeNode<int, int>(1, 1);
            Node<int, int> node2 = new TreeNode<int, int>(1, 1);
            Assert.That(node1.GetHashCode() == node2.GetHashCode());
            node2 = new TreeNode<int, int>(2, 1);
            Assert.That(node1 != node2);
        }

        [Test]
        public void CheckHashCodeForBinaryTree()
        {
            BinaryTree<int, int> tree1 = new()
            {
                { 1, 1 },
                { 2, 2 },
                { 3, 3 },
                { 4, 2 },
                { 5, 5 },
            };
            BinaryTree<int, int> tree2 = new()
            {
                { 1, 1 },
                { 2, 2 },
                { 3, 3 },
                { 4, 2 },
                { 5, 5 },
            };
            Assert.That(tree1.GetHashCode() == tree2.GetHashCode());
            tree1 = new()
            {
                { 1, 1 },
                { 2, 2 },
                { 3, 3 },
                { 4, 2 },
            };

            int hashCode1 = tree1.GetHashCode();
            int hashCode2 = tree2.GetHashCode();
            Assert.That(hashCode1 != hashCode2);
        }

        [Test]
        public void ChickGettingEnumerator()
        {
            BinaryTree<int, int> tree = new()
            {
                { 1, 1 },
                { 2, 2 },
                { 3, 3 },
            };

            IEnumerator<Node<int, int>> treeEnumerator = tree.GetEnumerator();
            IEnumerator interfaceEnumerator = tree.GetEnumerator();

            while (treeEnumerator.MoveNext() && interfaceEnumerator.MoveNext())
            {
                Node<int, int> treeCurrent = treeEnumerator.Current;
                object? interfaceCurrent = interfaceEnumerator.Current;

                Assert.That(treeCurrent == interfaceCurrent as Node<int, int>);
            }

            Assert.IsFalse(treeEnumerator.MoveNext());
            Assert.IsFalse(interfaceEnumerator.MoveNext());

            tree = new()
            {
                { 1, 1 },
                { -1, 1 },
                { -2, 5 },
                { -10, 50 },
            };
            treeEnumerator = tree.GetEnumerator();
            interfaceEnumerator = tree.GetEnumerator();

            while (treeEnumerator.MoveNext() && interfaceEnumerator.MoveNext())
            {
                Node<int, int> treeCurrent = treeEnumerator.Current;
                object? interfaceCurrent = interfaceEnumerator.Current;

                Assert.That(treeCurrent == interfaceCurrent as Node<int, int>);
            }

            Assert.IsFalse(treeEnumerator.MoveNext());
            Assert.IsFalse(interfaceEnumerator.MoveNext());

            BinaryTree<int, int> binaryTree = new()
            {
                { 1, 4 },
                { 2, 2 },
                { 3, 6 },
            };

            var enumerator = ((IEnumerable)binaryTree).GetEnumerator();

            Assert.IsTrue(enumerator.MoveNext());
            Assert.That(new TreeNode<int, int>(1, 4), Is.EqualTo(enumerator.Current));

            Assert.IsTrue(enumerator.MoveNext());
            Assert.That(new TreeNode<int, int>(2, 2), Is.EqualTo(enumerator.Current));

            Assert.IsTrue(enumerator.MoveNext());
            Assert.That(new TreeNode<int, int>(3, 6), Is.EqualTo(enumerator.Current));

            Assert.IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void EqualsForObjectInNodes()
        {
            TreeNode<int, string> node1 = new(1, "One");
            TreeNode<int, string> node2 = new(1, "One");
            object obj = node2;

            Assert.IsTrue(node1.Equals(obj));
        }


        [Test]
        public void CheckIsSubtree()
        {
            BinaryTree<int, int> tree1 = new()
            {
                { 1, 2 },
                { -1, 1 },
                { -5, 3 },
                { -20, 3 },
                { -10, 5 },
            };
            TreeNode<int, int> node1 = new(-10, 5);
            Assert.That(node1.IsSubtreeOf(node1, tree1.Root));
            Assert.Throws<NullReferenceException>(() => node1.IsSubtreeOf(null, tree1.Root));
        }
    }
}