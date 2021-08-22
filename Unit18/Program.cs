using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Unit18.Commands;

namespace Unit18
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ConsoleKey response;
            do
            {
                Console.Write("Введите url youtube-видео: ");
                string url = Console.ReadLine();
                if (string.IsNullOrEmpty(url)) 
                { 
                    response = ConsoleKey.R; 
                    continue; 
                }

                Console.Write("Инфо/Скачать? i/s ");
                response = Console.ReadKey(false).Key;
                Console.WriteLine();

                await ExecuteCommands(response, url);

                Console.Write("Нажмите R для повтора или любую клавишу для завершения: ");
                response = Console.ReadKey(false).Key;
                Console.WriteLine();

            } while (response == ConsoleKey.R);
        }

        private static async Task ExecuteCommands(ConsoleKey response, string url)
        {
            var reciver = new YoutubeReceiver();
            var sender = new YoutubeInvoker();
            ICommand command;

            switch (response)
            {
                case ConsoleKey.I:
                    command = new GetInfoCommand(reciver, url);
                    sender.SetCommand(command);
                    await sender.ExecuteAsync();
                    break;
                case ConsoleKey.S:
                    string dirPath = GetPath();
                    if (string.IsNullOrEmpty(dirPath)) { return; }

                    command = new DownloadCommand(reciver, url, dirPath);
                    sender.SetCommand(command);

                    Task task = new Task(() =>
                    {
                        try
                        {
                            sender.ExecuteAsync().GetAwaiter().GetResult();
                            Console.WriteLine("Загрузка завершена");
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Загрузка прервана");
                        }
                    });
                    task.Start();

                    Console.WriteLine("Нажмите любую клавишу для прерывания загрузки");
                    while (!task.IsCompleted)
                    {
                        if (Console.KeyAvailable) 
                        { 
                            Console.ReadKey(); 
                            sender.Cancel(); 
                        }
                    }

                    break;
                default:
                    break;
            }
        }

        private static string GetPath()
        {
            Console.Write("Введите путь к папке для сохранения видео: ");
            string dirPath = Console.ReadLine();
            if (string.IsNullOrEmpty(dirPath)) { return null; }

            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);

            if (!dirInfo.Exists)
            {
                Console.WriteLine("Папка по заданному адресу не существует");
                return null;
            }

            return dirPath;
        }
    }
}
