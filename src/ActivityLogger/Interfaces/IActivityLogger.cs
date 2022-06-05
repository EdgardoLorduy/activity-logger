namespace ActivityLogger.Interfaces
{
    internal interface IActivityLogger
    {
        void Add(DateTime dateTime, string activityName, string? description);
    }
}
