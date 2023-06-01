using System.Collections;
using System.Runtime.Serialization;

namespace BinaryTree;

[DataContract]

public class BinaryTree<TKey, TValue> : IMyBinaryTree<TKey, TValue>, IEquatable<BinaryTree<TKey, TValue>>, IEnumerable<Node<TKey, TValue>>, ICloneable
	where TKey : IComparable<TKey>
{
	public BinaryTree(TKey key, TValue value)
	{
		_root = new TreeNode<TKey, TValue>(key, value);
	}
    
	public BinaryTree() {}
    
	public BinaryTree(TKey key, TValue value, params (TKey keys, TValue values)[] fill) : this(key, value)
	{
		for(int i = 0; i < fill.Length; i++)
		{
			Add(fill[i].keys, fill[i].values);
		}
	}

	[DataMember]
	private Node<TKey, TValue>? _root;

	public Node<TKey, TValue>? Root => _root;

	private int _count;

	public int Count => _count;
	public TValue Value => _root.Value;

	public int Height => GetHeight(_root);
	/// <summary>
	/// Add a specific node into a tree
	/// </summary>

	public void Add(TKey key, TValue value)
	{
		if (_root is null)
		{
			_root = new TreeNode<TKey, TValue>(key, value);
			_count++;
		}
		else
		{
			Insert(_root, key, value);
		}
	}

	/// <summary>
	/// Find a place for adding a node in a tree
	/// </summary>
	private void Insert(Node<TKey, TValue>? node, TKey key, TValue value)
	{
		
		if (key.CompareTo(node.Key) < 0)
		{
			if (node.Left is null)
			{
				node.Left = new TreeNode<TKey, TValue>(key, value);
				_count++;
			}
			else
			{
				Insert(node.Left, key, value);
			}
		}
		else if (key.CompareTo(node.Key) > 0)
		{
			if (node.Right is null)
			{
				node.Right = new TreeNode<TKey, TValue>(key, value);
				_count++;
			}
			else
			{
				Insert(node.Right, key, value);
			}
		}
		else
		{
			node.Value = value;
		}
	}

   
	/// <summary>
	/// Can take a value using given key as Tree[key]
	/// </summary>
	public TValue this[TKey key]
	{
		get
		{
			var node = Find(_root, key);
			if (node is not null)
				return node.Value;
			throw new KeyNotFoundException("Key not found ");
		}
        
	}
	/// <summary>
	/// Search a value using specific key
	/// </summary>
	public TValue Search(TKey key)
	{
		var node = Find(_root, key);
		if (node is null)
			throw new NullReferenceException();
        
		return node.Value;
	}
	/// <summary>
	/// Find a node in tree and returns it
	/// </summary>
	private Node<TKey, TValue>? Find(Node<TKey, TValue>? node, TKey key)
	{
		if (node is null)
			return null;

		if (key.CompareTo(node.Key) < 0)
			return Find(node.Left, key);

		if (key.CompareTo(node.Key) > 0)
			return Find(node.Right, key);

		return node;
	}
	/// <summary>
	/// Remove specific key and it's value from a tree
	/// </summary>
	public void Remove(TKey key)
	{
		ArgumentNullException.ThrowIfNull(key);
		ArgumentNullException.ThrowIfNull(_root);

		_root = RemoveNode(_root, key);
		_count--;

	}

	/// <summary>
	/// Find a node with specific key and remove it
	/// </summary>
	private Node<TKey, TValue>? RemoveNode(Node<TKey, TValue>? node, TKey key)
	{
		if (node is null)
			throw new KeyNotFoundException("Key not found");

		int compareResult = key.CompareTo(node.Key);

		if (compareResult < 0)
		{
			node.Left = RemoveNode(node.Left, key);
		}
		else if (compareResult > 0)
		{
			node.Right = RemoveNode(node.Right, key);
		}
		else
		{
			if (node.Left is null)
			{
				return node.Right;
			}
			else if (node.Right is null)
			{
				return node.Left;
			}
			else
			{
				Node<TKey, TValue> minRightNode = this.FindMinNode(node.Right);
				node.Key = minRightNode.Key;
				node.Value = minRightNode.Value;
				node.Right = RemoveNode(node.Right, minRightNode.Key);
			}
		}

		return node;
	}



	/// <summary>
	/// Gets a height of a tree
	/// </summary>
	private int GetHeight(Node<TKey, TValue>? node)
	{
		if (node is null)
		{
			return 0;
		}

		int leftHeight = GetHeight(node.Left);
		int rightHeight = GetHeight(node.Right);
		return Math.Max(leftHeight, rightHeight) + 1;
	}

	/// <summary>
	/// Check if given tree's keys are similar to this
	/// </summary>
	public override bool Equals(object? obj)
	{
		if (obj is null || GetType() != obj.GetType())
			return false;

		Console.WriteLine("111");
		return Equals((BinaryTree<TKey, TValue>)obj);
	}

	/// <summary>
	/// Check for nulls and return if tree's keys are equal 
	/// </summary>
	public bool Equals(BinaryTree<TKey, TValue>? other)
	{
		if (_root is null)
		{
			throw new NullReferenceException(("aaa"));
		}

		if (other is null)
			return false;

		return CompareOnlyKeys(other);
	}

    

	/// <summary>
	/// Operator returns true if tree's are equal with keys and values
	/// </summary>
	public static bool operator == (BinaryTree<TKey, TValue>? left, BinaryTree<TKey, TValue>? right)
	{
		if (ReferenceEquals(left, right))
			return true;
		if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
			throw new NullReferenceException();

		if (left._root is null)
		{
			throw new NullReferenceException();
		}

		if (right._root is null)
			throw new NullReferenceException();

		return TreeNode<TKey, TValue>.TreesEqualWithValues(left._root, right._root);
	}


	/// <summary>
	/// Operator returns true if tree's aren't equal with keys and/or values
	/// </summary>
	public static bool operator !=(BinaryTree<TKey, TValue> left, BinaryTree<TKey, TValue> right)
	{
		return !(left == right);
	}

    
	/// <summary>
	/// Return a count of nodes  in which we can get from this node
	/// </summary>
	private static int GetSize(Node<TKey, TValue>? node)
	{
		if (node is null)
			return 0;

		return 1 + GetSize(node.Left) + GetSize(node.Right);
	}


	/// <summary>
	/// Check if left tree is a subtree for a right tree
	/// </summary>
	public static bool operator <= (BinaryTree<TKey, TValue>? left, BinaryTree<TKey, TValue>? right)
	{
		int leftSize = GetSize(left._root);
		int rightSize = GetSize(right._root);
        
		return leftSize <= rightSize && right._root.IsSubtreeOf(right._root, left._root);
	}

	/// <summary>
	/// check if the right tree is a subtree for a left tree
	/// </summary>
	public static bool operator >=(BinaryTree<TKey, TValue> left, BinaryTree<TKey, TValue> right)
	{
		int leftSize = GetSize(left._root);
		int rightSize = GetSize(right._root);
		return leftSize >= rightSize && left._root.IsSubtreeOf(left._root,right._root);
	}
    
	/// <summary>
	/// Gets a hash code
	/// </summary>
	public override int GetHashCode()
	{
		int hash = 17;

		if (_root is not null)
		{
			foreach (var node in InOrderTraversal(_root))
			{
				hash = hash * 31 + node.GetHashCode();
			}
		}

		return hash;
	}


	/// <summary>
	/// Return a string in format "root, count, height" from given tree
	/// </summary>
	public override string ToString()
	{
		if (_root is null)
		{
			throw new NullReferenceException();
		}
		return $"root: {Value}, Count: {_count}, Height: {Height}";
	}

	/// <summary>
	/// Implementing a enumarating with foreach for a tree, generic function
	/// </summary>
	public IEnumerator<Node<TKey, TValue>> GetEnumerator()
	{
		return InOrderTraversal(_root).GetEnumerator();
	}

	/// <summary>
	/// 
	/// </summary>
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
	/// <summary>
	/// In-order traversal of the binary search tree
	/// </summary>
	private IEnumerable<Node<TKey, TValue>> InOrderTraversal(Node<TKey, TValue>? node)
	{
		if (node is not null)
		{
			foreach (var leftChild in InOrderTraversal(node.Left))
			{
				yield return leftChild;
			}

			yield return node;

			foreach (var rightChild in InOrderTraversal(node.Right))
			{
				yield return rightChild;
			}
		}
	}

	/// <summary>
	/// Implements a cloning of given tree
	/// </summary>
	public object Clone()
	{
		var clonedTree = new BinaryTree<TKey, TValue>();

		if (_root is not null)
		{
			clonedTree._root = new TreeNode<TKey, TValue>
			(
				_root.Key,
				_root.Value
			);
			CloneNodes(clonedTree._root, _root);
		}

		return clonedTree;
	}

	/// <summary>
	/// Recursive cloning nodes
	/// </summary>
	private void CloneNodes(Node<TKey, TValue> clonedNode, Node<TKey, TValue> originalNode)
	{
		if (originalNode.Left is not null)
		{
			clonedNode.Left = new TreeNode<TKey, TValue>
			(
				originalNode.Left.Key,
				originalNode.Left.Value
			);
			CloneNodes(clonedNode.Left, originalNode.Left);
		}
		if (originalNode.Right is not null)
		{
			clonedNode.Right = new TreeNode<TKey, TValue>
			(
				originalNode.Right.Key,
				originalNode.Right.Value
			);
			CloneNodes(clonedNode.Right, originalNode.Right);
		}
	}
	/// <summary>
	/// Check if tree's are equal with keys
	/// </summary>
	private bool CompareOnlyKeys(BinaryTree<TKey, TValue> otherTree)
	{
		if (ReferenceEquals(this, otherTree))
			return true;
        
		return CompareKeys(_root, otherTree._root);
	}

	/// <summary>
	/// Recursive checking if nodes of trees are equal
	/// </summary>
	private bool CompareKeys(Node<TKey, TValue>? node1, Node<TKey, TValue>? node2)
	{
		if (node1 is null && node2 is null)
			return true;

		if (node1 is not null && node2 is not null)
		{
			return node1.Key.Equals(node2.Key) &&
			       CompareKeys(node1.Left, node2.Left) &&
			       CompareKeys(node1.Right, node2.Right);
		}

		return false;
	}

	/// <summary>
	/// Serializing object in Xml
	/// </summary>
	public void SerializeXml(string filePath)
	{
		using (var writer = new FileStream(filePath, FileMode.Create))
		{
			var knownTypes = new List<Type> { typeof(TreeNode<TKey, TValue>) };
			var serializer = new DataContractSerializer(typeof(BinaryTree<TKey, TValue>), knownTypes);
			serializer.WriteObject(writer, this);
		}
	}
	/// <summary>
	/// Deserializing object from Xml
	/// </summary>
	public static BinaryTree<TKey, TValue>? DeserializeXml(string filePath)
	{
		using (var reader = new FileStream(filePath, FileMode.Open))
		{
			var knownTypes = new List<Type> { typeof(TreeNode<TKey, TValue>) };
			var serializer = new DataContractSerializer(typeof(BinaryTree<TKey, TValue>), knownTypes);
			return (BinaryTree<TKey, TValue>?)serializer.ReadObject(reader);
		}
	}
}