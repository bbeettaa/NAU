using System;
using System.Collections;

namespace LB_2_Krupina_225
{
    class BinaryTree<T> where T : IComparable<T>
        , IEnumerable
    {
        Node<T> root = new Node<T>();
        public void Insert(T obj, string key)
        {
            AddObjectToNode(root, key, obj);
        }
        private void AddObjectToNode(Node<T> node, string value, T obj)
        {
            if (node.key == null)
                node.SetValue(value, node, obj);

            else
            if (node.obj.CompareTo(obj) == 1)
            {
                if (node.left == null)
                    node.left = new Node<T>(value, node, obj);
                else
                    AddObjectToNode(node.left, value, obj);
            }
            else
            {
                if (node.right == null)
                    node.right = new Node<T>(value, node, obj);
                else
                    AddObjectToNode(node.right, value, obj);
            }

        }
        public void Insert(string value)
        {
            AddStringValue(root, value);
        }
        private void AddStringValue(Node<T> node, string value)
        {
            if (node.key == null)
                node.key = value;
            else
            if (node.key.CompareTo(value) == 1)
            {
                if (node.left == null)
                    node.left = new Node<T>(value, node);
                else
                    AddStringValue(node.left, value);
            }
            else
            {
                if (node.right == null)
                    node.right = new Node<T>(value, node);
                else
                    AddStringValue(node.right, value);
            }

        }
        public void Insert(Node<T> node)
        {
            AddNodeToTree(root, node);
        }
        private void AddNodeToTree(Node<T> root, Node<T> node)
        {
            if (root == null)
                root = node;
            else
            {
                if (root.key.CompareTo(node.key) == 1)
                {
                    if (root.left == null)
                    {
                        root.left = node;
                        node.parent = root;
                    }
                    else
                        AddNodeToTree(root.left, node);
                }
                else
                {
                    if (root.right == null)
                    {
                        root.right = node;
                        node.parent = root;
                    }
                    else
                        AddNodeToTree(root.right, node);
                }
            }
        }


        public void Delete(string value)
        {
            Node<T> current = FindNode(root, value);
            if (current.parent == null)
            {
                current.right.parent = null;
                current.left.parent = null;
                this.root = current.right;
                Insert(current.left);
                return;
            }
            DeleteNode(current);
        }
        private void DeleteNode(Node<T> current)
        {
            if (current.left == null && current.right == null)
            {
                current.parent.left = null;
                return;
            }

            if (current.parent.left == current)
                current.parent.left = current.right;
            if (current.parent.right == current)
                current.parent.right = current.right;

            current.right.parent = current.parent;

            if (current.left != null)
                Insert(current.left);
        }


        public Queue GetObjArr(string key) // Реалізації черги
        {
            Queue queue = new Queue();
            queue = GetObjArr(root, key, queue);
            return queue;
        }
        private Queue GetObjArr(Node<T> node, string key, Queue queue)
        {
            if (node.left != null)
                queue = GetObjArr(node.left, key, queue);
            if (node.right != null)
                queue = GetObjArr(node.right, key, queue);

            if (node != null)
                if (node.key.Contains(key))
                { 
                    queue.Enqueue(node);
                }

            return queue;
        }


        public void ChangeNodeObj(string key,T obj)
        {
            ChangeNodeObj(root,key,obj);
        }
        private void ChangeNodeObj(Node<T> node, string key, T obj)
        {
            if (node.key == key)
                node.obj = obj;

            else
               if (node.obj.CompareTo(obj) == 1)
            {
                if (node.left == null)
                    node.left = new Node<T>(key, node, obj);
                else
                    ChangeNodeObj(node.left, key, obj);
            }
            else
            {
                if (node.right == null)
                    node.right = new Node<T>(key, node, obj);
                else
                    ChangeNodeObj(node.right, key, obj);
            }
        }


        private Node<T> FindNode(Node<T> current, string value)
        {
            Node<T> returnValue = null;
            if (current.key.CompareTo(value) == 0)
                return returnValue = current;

            if (current.left != null)
                returnValue = FindNode(current.left, value);
            if (returnValue != null)
                return returnValue;

            if (current.right != null)
                returnValue = FindNode(current.right, value);
            if (returnValue != null)
                return returnValue;


            return returnValue;
        }
        public void Clear()
        {
            root.obj = default(T);
            root.key = null;
            root.left = null;
            root.right = null;
        }


        public IEnumerator GetEnumerator()
        {
            foreach (object o in GetObjArr(""))
            {
                if (o == null)
                {
                    break;
                }
                yield return o;
            }
        }
    }

}
