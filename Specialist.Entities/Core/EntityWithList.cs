using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Core
{
    public class EntityWithList
    {
        public static EntityWithList<T, K> New<T, K>(T entity, IEnumerable<K> list)
        {
            return new EntityWithList<T, K>(entity, 
				list == null ? new List<K>() : list.ToList());
        }

        public static EntityWithList<T, K, K2> New<T, K, K2>(T entity, IEnumerable<K> list
            ,IEnumerable<K2> list2)
        {
            return new EntityWithList<T, K, K2>(entity, list.ToList(), list2.ToList());
        }

        public static EntityWithList<T, K, K2, K3> New<T, K, K2, K3>(T entity, 
            IEnumerable<K> list, IEnumerable<K2> list2, IEnumerable<K3> list3)
        {
            return new EntityWithList<T, K, K2, K3>(entity, list.ToList(), list2.ToList(),
                list3.ToList());
        }
    }
    public class EntityWithList<T, K>:IGrouping<T,K> {
        public T Entity { get; set; }

        public List<K> List {get; set; }

        public EntityWithList(T entity, List<K> list)
        {
            Entity = entity;
            List = list;
        }


	    IEnumerator<K> IEnumerable<K>.GetEnumerator() {
		    return List.GetEnumerator();
	    }

	    IEnumerator IEnumerable.GetEnumerator() {
		    return List.GetEnumerator();
	    }

	    T IGrouping<T, K>.Key { get { return Entity; } }
    }

    public class EntityWithList<T, K, K2>: EntityWithList<T, K>
    {
        public List<K2> List2 { get; set; }

        public EntityWithList(T entity, List<K> list, List<K2> list2) : base(entity, list)
        {
            List2 = list2;
        }
    }

    public class EntityWithList<T, K, K2, K3> : EntityWithList<T, K, K2>
    {
        public List<K3> List3 { get; set; }

        public EntityWithList(T entity, List<K> list, List<K2> list2, List<K3> list3) : base(entity, list, list2)
        {
            List3 = list3;
        }
    }
}