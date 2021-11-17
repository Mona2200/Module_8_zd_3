using System;
using System.IO;

namespace ZD3
{
   class Program
   {
      static void Main(string[] args)
      {
         SizeAndDelete(@"C:\Users\Алёна\Desktop\NewDir");
      }

      static void SizeAndDelete(string Path)
      {
         if (Directory.Exists(Path))
         {
            long size = DirectorySize(Path);
            Console.WriteLine($"Исходныи рамер папки: {size} баит");

            DeleteDir(Path);

            long freesize = DirectorySize(Path);
            Console.WriteLine($"Освобождено: {size - freesize} баит");

            Console.WriteLine($"Текущии размер папки: {freesize} баит");
         }
      }

      static void DeleteDir(string Path) //метод из задания 1
      {
         if (Directory.Exists(Path))
         {

            try
            {
               string[] dirs = Directory.GetDirectories(Path);

               foreach (string d in dirs)
               {
                  DirectoryInfo NowDir = new DirectoryInfo(d);

                  if (DateTime.Now.Subtract(NowDir.LastWriteTime) > TimeSpan.FromMinutes(30))
                     NowDir.Delete(true);

               }

               string[] files = Directory.GetFiles(Path);

               foreach (string s in files)
               {
                  FileInfo NowFile = new FileInfo(s);

                  if (DateTime.Now.Subtract(NowFile.LastWriteTime) > TimeSpan.FromMinutes(30))
                     NowFile.Delete();
               }
            }

            catch (Exception ex)
            {
               Console.WriteLine(ex.Message);
            }
         }

      }

      static long DirectorySize(string Path) //метод из задания 2
      {
         if (Directory.Exists(Path))
         {

            try
            {

               long size = 0;

               string[] files = Directory.GetFiles(Path);

               foreach (var f in files)
               {
                  size += f.Length;
               }

               string[] dirs = Directory.GetDirectories(Path);

               foreach (string d in dirs)
               {
                  DirectoryInfo NowDir = new DirectoryInfo(d);

                  size += DirectorySize(d);

               }
               return size;

            }

            catch (Exception ex)
            {
               Console.WriteLine(ex.Message);
            }

         }
         return 0;
      }

   }
}
