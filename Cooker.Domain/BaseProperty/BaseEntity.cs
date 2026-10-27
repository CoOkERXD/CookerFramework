using Cooker.Domain.BaseProperty;

namespace Cooker.Domain
{
    public abstract class BaseEntity<T> : IHasCurrentTime, IHasGuid, IHasDeleted, IHasVersion
    {
        public T Id { get; set; }
        public long Version { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public string Guid { get; set; } = System.Guid.NewGuid().ToString("N");
        public int IsDeleted { get; set; }
    }
}