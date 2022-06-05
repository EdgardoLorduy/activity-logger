//https://stackoverflow.com/questions/11625595/read-last-line-of-text-file

using ActivityLogger.Interfaces;

namespace ActivityLogger.Services
{
    internal class TextFileActivityLogger : IActivityLogger
    {
        readonly string _path;

        public TextFileActivityLogger(string path)
        {
            _path = path;
        }

        public void Add(DateTime dateTime, string activityName, string? description)
        {
            if (FileEnsured(dateTime, activityName, description))
            {
                return;
            }

            UpdateLastLine(dateTime);
            AddNewLine(dateTime, activityName, description);
        }

        bool FileEnsured(DateTime dateTime, string activityName, string? description)
        {

            if (File.Exists(_path))
            {
                return false;
            }

            AddNewLine(dateTime, activityName, description);

            return true;
        }

        string GetLastLine()
        {
            var lastLine = File.ReadLines(_path).Last();

            return lastLine;
        }

        void AddNewLine(DateTime dateTime, string activityName, string? description)
        {
            using (var writer = File.AppendText(_path))
            {
                writer.WriteLine(TextFileLineSerializer.SerializeLine(dateTime, endDateTime: null, activityName, description));
            }
        }

        void UpdateLastLine(DateTime endDateTime)
        {
            //lee la ultima linea del archivo
            var lastLine = TextFileLineSerializer.DeserializeLine(GetLastLine());

            //elimina la ultima linea del archivo
            var lines = File.ReadAllLines(_path);
            File.WriteAllLines(_path, lines.Take(lines.Length - 1).ToArray());

            //agrega la ultima linea actualizada al archivo
            using (var writer = File.AppendText(_path))
            {
                writer.WriteLine(TextFileLineSerializer.SerializeLine(lastLine.StartDateTime, endDateTime, lastLine.ActivityName, lastLine.Description));
            }
        }
    }
}
