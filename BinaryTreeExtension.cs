namespace BinaryTree;

public static class BinaryTreeExtension
{
    /// <summary>
    /// Find a maximum node in a given tree
    /// </summary>
    public static Node<TKey, TValue> FindMaxNode<TKey, TValue>(this BinaryTree<TKey, TValue> tree, Node<TKey, TValue>? node) where TKey : IComparable<TKey>
    {
        if (node.Right is null)
        {
            return node;
        }
        
        return FindMaxNode(tree, node.Right);
    }

    /// <summary>
    /// Find a minimum node in a given tree
    /// </summary>
    public static Node<TKey, TValue> FindMinNode<TKey, TValue>(this BinaryTree<TKey, TValue> tree, Node<TKey, TValue>? node) where TKey : IComparable<TKey>
    {
        if (node.Left is null)
        {
            return node;
        }
        
        return FindMinNode(tree, node.Left);
    }
    
}