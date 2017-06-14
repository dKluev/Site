using System.Collections.Generic;
using System.Linq;

namespace Specialist.Entities.Core
{
    public class TagWithEntity: TagWithEntity<object>
    {
        public TagWithEntity(object entity, int weight) : base(entity, weight) {}

        public static TagWithEntity<T> New<T>(T entity, int weight)
        {
            return new TagWithEntity<T>(entity, weight);
        }

        
    }

    public static class TagExtension
    {
        private const int MaxTagSize = 5;
        public static List<TagWithEntity<T>> Normalization<T>(this List<TagWithEntity<T>> tags)
        {
            if (tags.Count > 0)
            {
                var maxWeight = tags.Max(t => t.Weight);
                if (maxWeight == 0)
                    maxWeight = 1;
                foreach (var tag in tags)
                {
                    tag.Weight = (tag.Weight * MaxTagSize) / maxWeight;
                    if (tag.Weight == 0) tag.Weight = 1;
                }
                var average = (tags.Min(t => t.Weight) + tags.Max(t => t.Weight)) / 2;
                var offset = 3 - average;
                foreach (var tag in tags)
                {
                    tag.Weight += offset;
                }
            }
            return tags;
        }
    }

    
    public class TagWithEntity<T>
    {

        public T Entity { get; set; }

        public int Weight { get; set; }

        public TagWithEntity(T entity, int weight)
        {
            Entity = entity;
            Weight = weight;
        }
    }
}