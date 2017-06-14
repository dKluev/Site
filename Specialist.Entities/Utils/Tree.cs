using System.Collections.Generic;
using SimpleUtils.Collections.Extensions;
using System.Linq;

namespace SimpleUtils.Common
{
    public class Tree<T>
    {
        public T Value { get; set; }

	    public int Deep {
		    get {
			    if (!Nodes.Any()) {
				    return 0;
			    }
			    return 1 + Nodes.Max(x => x.Deep);
		    }
	    }

        public bool IsLeaf { get
        {
            return Nodes.IsEmpty();
        }}

        public Tree(T value)
        {
            Value = value;
            Nodes = new List<Tree<T>>();
        }

        public Tree<T> AddNode(params Tree<T>[] tree)
        {
        	foreach (var node in tree) {
	            Nodes.Add(node);
        	}
            return this;
        }

        public Tree(): this(default(T)) {}

        public List<Tree<T>> Nodes { get; set; }
    }

    public static class Tree
    {
        public static Tree<T> New<T>(T value)
        {
            return new Tree<T>(value);
        }
    }
}