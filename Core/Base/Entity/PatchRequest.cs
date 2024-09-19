namespace Core.Base.Entity
{
    public class PatchRequest<T>
    {
        public required T Request { get; set; }
        public required ICollection<string> Fields { get; set; } = new List<string>();
    }
}
