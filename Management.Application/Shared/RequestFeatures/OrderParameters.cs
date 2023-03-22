namespace Management.Application.Shared.RequestFeatures
{
    public class OrderParameters
    {
        public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Now.AddMonths(-1));
        public DateOnly EndDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
