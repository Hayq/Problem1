using System.Runtime.CompilerServices;

namespace Problem1.Services
{
    public interface IFileWrite : IDisposable
    {
        void Append(int value);
    }

    public class FileService : IFileWrite
    {
        private string _path = "output.txt";
        private StreamWriter _writer;

        public FileService()
        {
            _writer = File.AppendText(_path);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Append(int value)
        {
            _writer.Write($"{value},");
        }

        public void Dispose()
        {
            _writer.Dispose();
        }
    }
}