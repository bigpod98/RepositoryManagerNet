﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

string WatchLocation = "/Watch";
string repoLocation = "/repository";
string codename = Environment.GetEnvironmentVariable("CODENAME");
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

            //reprepro -b repo/ includedeb trusty jake_1.0-7_amd64.deb
            Process p = new Process();

            p.StartInfo.FileName = "reprepro";
            p.StartInfo.Arguments = $"-b {repoLocation}/ includedeb {codename} {i.FullName}";
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.RedirectStandardOutput = false;  
            p.Start();
            p.WaitForExit();

        }
    }

    System.Threading.Thread.Sleep(2000);
}