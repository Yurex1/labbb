namespace BinaryTree;

public interface IMyBinaryTree<TKey, TValue>
{
	void Add(TKey key, TValue value);
	void Remove(TKey key);
	int Count { get; }
	TValue Value { get; }
	int Height { get; }
}