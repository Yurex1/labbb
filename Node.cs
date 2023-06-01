using System.Runtime.Serialization;

namespace BinaryTree;

[DataContract]
public abstract class Node<TKey, TValue> : IEquatable<Node<TKey, TValue>> where TKey : IComparable<TKey>
{
	[DataMember]
	public TKey Key { get; set; }
	[DataMember]
	public TValue Value { get; set; }
	[DataMember]
	public Node<TKey, TValue>? Left { get; set; }
	[DataMember]
	public Node<TKey, TValue>? Right { get; set; }

	public Node(TKey key, TValue value)
	{
		Key = key;
		Value = value;
	}

	/// <summary>
	/// Check if nodes are equal
	/// </summary>
	public override bool Equals(object? obj)
	{
		return Equals(obj as Node<TKey, TValue>);
	}

	/// <summary>
	/// Check if nodes are equal
	/// </summary>
	public bool Equals(Node<TKey, TValue>? other)
	{
		if (other is null) return false;
		return Key.Equals(other.Key) && Value.Equals(other.Value);
	}

	/// <summary>
	/// Gets a hash code
	/// </summary>
	public override int GetHashCode()
	{
		return HashCode.Combine(Key, Value);
	}

	/// <summary>
	/// Check if two nodes are equal
	/// </summary>
	public static bool operator ==(Node<TKey, TValue> left, Node<TKey, TValue>? right)
	{
		return left.Equals(right);
	}

	/// <summary>
	/// Check if nodes are not equal
	/// </summary>
	public static bool operator !=(Node<TKey, TValue> left, Node<TKey, TValue> right)
	{
		return !(left == right);
	}

	/// <summary>
	/// Convert a node into a string format "key, value"
	/// </summary>
	public override string ToString()
	{
		return $"Key: {Key}, Value: {Value}";
	}
	/// <summary>
	/// Check if node1 and it's children is subtree to node2 and it's children
	/// </summary>
	public abstract bool IsSubtreeOf(Node<TKey, TValue>? node1, Node<TKey, TValue>? node2);
}