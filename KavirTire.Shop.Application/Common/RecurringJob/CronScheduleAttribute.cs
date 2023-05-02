
namespace KavirTire.Shop.Application.Common.RecurringJob;

[AttributeUsage(AttributeTargets.Class)]
public class CronScheduleAttribute : Attribute
{
    public string CronExpression { get; }

    public CronScheduleAttribute(string cronExpression)
    {
        CronExpression = cronExpression;
    }
}