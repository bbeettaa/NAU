namespace LB_2_Krupina_225
{
    public class Node<T>
    {
        public Node() { }
        public Node(string key, Node<T> parent)
        {
            this.key = key;
            this.parent = parent;
        }
        public Node(string key, Node<T> parent, T typeObj)
        {
            this.key = key;
            this.parent = parent;
            obj = typeObj;
        }
        public void SetValue(string key, Node<T> parent, T typeObj)
        {
            this.key = key;
            this.parent = parent;
            obj = typeObj;
        }

        public T obj = default(T);
        public string key = null;
        public Node<T> parent = null;
        public Node<T> left = null;
        public Node<T> right = null;
    }
}
