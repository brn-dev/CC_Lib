from cclib.trees.binary_tree_node import BinaryTreeNode
from cclib.trees.tree_node_utils import get_descendant_layer

def main():
    root = BinaryTreeNode('r')
    one = BinaryTreeNode('one')
    two = BinaryTreeNode('two')
    root.left = one
    root.right = two
    print(root)
    print(get_descendant_layer(root, 0))

if __name__ == '__main__':
    main()
