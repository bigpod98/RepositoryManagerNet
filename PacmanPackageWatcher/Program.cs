using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

string WatchLocation = "/Watch";
string repoLocation = "/repository";
DirectoryInfo WatchDir = new DirectoryInfo(WatchLocation);
while (true)
{
    List<FileInfo> files = WatchDir.EnumerateFiles().ToList();

    if (files.Count == 0)
    {
        continue;
    }
    if (files.Count > 0)
    {
        foreach (FileInfo i in files)
        {
            Console.WriteLine(i.FullName);
            //run repoadd
            File.Copy(i.FullName, $"{repoLocation}/{i.Name}");

        }
    }

    System.Threading.Thread.Sleep(2000);
}