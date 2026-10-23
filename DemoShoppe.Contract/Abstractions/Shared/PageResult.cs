namespace DemoShoppe.Contract.Abstractions.Shared
{
    public class PageResult<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
    }
}
