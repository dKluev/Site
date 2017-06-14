using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecialistTest.Web.Core.Mvc {
    public class TagList: List<object> {
        public TagList() {
        }

        public TagList(int capacity) : base(capacity) {
        }

        public TagList(IEnumerable<object> collection) : base(collection) {
        }

        public override string ToString()
        {
            return GetString(this);
        }
    	private static string GetString(object o) {
    		var list = o as IEnumerable<object>;
			if(list == null)
				return o.ToString();
    		return GetString(list);
    	}

    	private static string GetString(IEnumerable<object> list) {
    		var buffer = new StringBuilder();
    		foreach (var o in list) {
				if(o == null)
					continue;
    			buffer.AppendLine(GetString(o));
    		}
    		return buffer.ToString();
    	}
    }
}