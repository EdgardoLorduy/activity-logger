namespace ActivityLogger.Services
{
    internal static class TextFileLineSerializer
    {
        const char SEPARATOR = '|';

        public static string SerializeLine(DateTime startDateTime, DateTime? endDateTime, string activityName, string? description)
            => $"{startDateTime.ToString("yyyy-MM-dd HH:mm")}{SEPARATOR}{endDateTime?.ToString("yyyy-MM-dd HH:mm")}{SEPARATOR}{activityName}{SEPARATOR}{description}";

        public static (DateTime StartDateTime, DateTime? EndDateTime, string ActivityName, string? Description) DeserializeLine(string line)
        { 
            var positions = line.Split(SEPARATOR);
            DateTime startDateTime = Convert.ToDateTime(positions[0]);
            DateTime? endDateTime = string.IsNullOrWhiteSpace(positions[1]) ? null : Convert.ToDateTime(positions[1]);
            var activityName = positions[2];
            var description = positions[3];

            return (startDateTime, endDateTime, activityName, description);
        }
    }
}
