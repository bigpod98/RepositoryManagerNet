using System;
using System.Linq;
using System.Collections.Generic;

string WatchLocation = "/Watch";
string repoLocation = "/repository";
System.IO.DirectoryInfo WatchDir = new System.IO.DirectoryInfo(WatchLocation);
while (true)
{
    List<System.IO.FileInfo> files = WatchDir.EnumerateFiles().ToList();

    if (files.Count == 0)
    {
        continue;
    }
    if (files.Count > 0)
    {
        foreach (System.IO.FileInfo i in files)
        {
            Console.WriteLine(i.FullName);
            //code for adding to repo
        }
    }

    System.Threading.Thread.Sleep(2000);
}