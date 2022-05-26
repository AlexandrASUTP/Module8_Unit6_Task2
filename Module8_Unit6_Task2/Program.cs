using System;
using System.IO;
namespace TestApplicationForStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите URL:");
            string pathToDirectory = Console.ReadLine();
            double catalogSize = 0;
            catalogSize = sizeOfFolder(pathToDirectory, ref catalogSize); //Вызываем наш рекурсивный метод
            if (catalogSize != 0)
            {
                Console.WriteLine("Размер каталога {0} составляет {1},байт", pathToDirectory, catalogSize);
            }
            else
            {
                Console.WriteLine("Каталог {0} пуст.", pathToDirectory);
            }
            Console.ReadLine();
        }

        static double sizeOfFolder(string folder, ref double catalogSize)
        {
            try
            {
                
                DirectoryInfo di = new DirectoryInfo(folder);
                DirectoryInfo[] diA = di.GetDirectories();
                FileInfo[] fi = di.GetFiles();
                foreach (FileInfo f in fi)
                {
                    //Записываем размер файла в байтах
                    catalogSize = catalogSize + f.Length;
                }
                foreach (DirectoryInfo df in diA)
                {
                    //рекурсивно вызываем наш метод
                    sizeOfFolder(df.FullName, ref catalogSize);
                }
                
                return catalogSize;
            }
            //Начинаем перехватывать ошибки
            //DirectoryNotFoundException - директория не найдена
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("Директория не найдена. Ошибка: " + ex.Message);
                return 0;
            }
            //UnauthorizedAccessException - отсутствует доступ к файлу или папке
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Отсутствует доступ. Ошибка: " + ex.Message);
                return 0;
            }
            //Во всех остальных случаях
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка. Обратитесь к администратору. Ошибка: " + ex.Message);
                return 0;
            }
        }
    }
}
