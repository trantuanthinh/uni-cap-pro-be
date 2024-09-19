namespace uni_cap_pro_be.Core.Base.Entity
{
    // DONE
    public class PatchRequest<T>
    {
        public required T Request { get; set; }
        public required ICollection<string> Fields { get; set; } = new List<string>();
    }
}
