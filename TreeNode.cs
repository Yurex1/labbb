using System.Runtime.Serialization;

namespace BinaryTree;

[DataContract]

public class TreeNode<TKey, TValue> : Node<TKey, TValue> where TKey : IComparable<TKey>
{
	public TreeNode(TKey key, TValue value) : base(key, value) { }

	public TreeNode(TKey key) : this(key, default!) { }

	public TreeNode() : this(default!, default!) { }

	/// <summary>
	/// Check if left tree is a subtree of a right tree
	/// </summary>
	public override bool IsSubtreeOf(Node<TKey, TValue>? node1, Node<TKey, TValue>? node2)
	{
		if (node2 is null)
			return true;

		if (node1 is null)
			throw new NullReferenceException();

		if (TreesEqualWithValues(node1, node2))
			return true;
		
		return IsSubtreeOf(node1, node2.Left) || IsSubtreeOf(node1, node2.Right);
	}
	/// <summary>
	/// Compare two trees with keys and values
	/// </summary>
	public static bool TreesEqualWithValues(Node<TKey, TValue>? node1, Node<TKey, TValue>? node2)
	{
		if (node1 is null && node2 is null)
			return true;

		if (node1 is null || node2 is null)
			return false;

		if (!node1.Key.Equals(node2.Key) || !node1.Value.Equals(node2.Value))
			return false;

		return TreesEqualWithValues(node1.Left, node2.Left) && TreesEqualWithValues(node1.Right, node2.Right);
	}

	/// <summary>
	/// Serializing TreeNode
	/// </summary>
	public void SerializeXml(string filePath)
	{
		using (var writer = new FileStream(filePath, FileMode.Create))
		{
			var serializer = new DataContractSerializer(typeof(TreeNode<TKey, TValue>));
			serializer.WriteObject(writer, this);
		}
	}

	/// <summary>
	/// Deserializing TreeNode
	/// </summary>
	public static TreeNode<TKey, TValue>? DeserializeXml(string filePath)
	{
		using (var reader = new FileStream(filePath, FileMode.Open))
		{
			var serializer = new DataContractSerializer(typeof(TreeNode<TKey, TValue>));
			return (TreeNode<TKey, TValue>?)serializer.ReadObject(reader);
		}
	}
}