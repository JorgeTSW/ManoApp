using ManoApp.Domain.Interfaces;
using ManoApp.Domain.Models;
using System.Text;

namespace ManoApp.Infrastructure.Persistence
{
    public class CsvGestureLogRepository : IGestureLogRepository
    {
        private readonly string _logsFolder;

        public CsvGestureLogRepository(string logsFolder)
        {
            _logsFolder = logsFolder;

            if (!Directory.Exists(_logsFolder))
            {
                Directory.CreateDirectory(_logsFolder);
            }
        }

        public void Save(GestureLogEntry entry)
        {
            var filePath = GetFilePathForDate(entry.Timestamp);
            var fileExists = File.Exists(filePath);

            using (var writer = new StreamWriter(filePath, append: true, encoding: Encoding.UTF8))
            {
                if (!fileExists)
                {
                    writer.WriteLine("Timestamp,RequestId,Handedness,FingerCount,GestureName");
                }

                var line = $"{entry.Timestamp:O},{entry.RequestId},{entry.Handedness},{entry.FingerCount},{entry.GestureName}";
                writer.WriteLine(line);
            }
        }

        public List<GestureLogEntry> GetAll()
        {
            var results = new List<GestureLogEntry>();

            if (!Directory.Exists(_logsFolder))
            {
                return results;
            }

            var files = Directory.GetFiles(_logsFolder, "gestures-log-*.csv");

            foreach (var file in files)
            {
                var lines = File.ReadAllLines(file);

                for (int i = 1; i < lines.Length; i++)
                {
                    var parts = lines[i].Split(',');

                    if (parts.Length < 5)
                    {
                        continue;
                    }

                    results.Add(new GestureLogEntry
                    {
                        Timestamp = DateTime.Parse(parts[0]),
                        RequestId = parts[1],
                        Handedness = parts[2],
                        FingerCount = int.Parse(parts[3]),
                        GestureName = parts[4]
                    });
                }
            }

            return results;
        }

        private string GetFilePathForDate(DateTime date)
        {
            var fileName = $"gestures-log-{date:yyyy-MM-dd}.csv";
            return Path.Combine(_logsFolder, fileName);
        }
    }
}