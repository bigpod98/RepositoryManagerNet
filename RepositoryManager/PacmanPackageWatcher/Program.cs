using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

string WatchLocation = "/Watch";
string repoLocation = "/repository";
string name = Environment.GetEnvironmentVariable("REPONAME");
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

            Process p = new Process();

            File.Copy(i.FullName, $"{repoLocation}/{i.Name}");

            p.StartInfo.FileName = "repo-add";
            p.StartInfo.Arguments = $"{repoLocation}/{name}.db.tar.gz /{repoLocation}/{i.Name}";

        }
    }

    System.Threading.Thread.Sleep(2000);
}