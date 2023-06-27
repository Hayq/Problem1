using Problem.Infrastructure.Domain;
using System.Runtime.CompilerServices;

namespace Problem.Infrastructure
{
    public class FileDataContext : IWriteData
    {
        private readonly string _path = "output.txt";
        private readonly StreamWriter _fileWriter;

        public FileDataContext()
        {
            _fileWriter = File.AppendText(_path);
            _fileWriter.AutoFlush = true;
        }

        public void Dispose()
        {
            _fileWriter.Dispose();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Write(int value)
        {
            var stringValue = $"{value},";
            _fileWriter.Write(stringValue);
        }
    }
}