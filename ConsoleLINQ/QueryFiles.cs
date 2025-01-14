namespace ConsoleLINQ;

internal class QueryFiles
{
    internal static void RunTest()
    {
        // Query for files with a specified attribute or name
        string startFolder = """E:\eDev\HGW\csharp\learning""";
        // Or
        // string startFolder = "/usr/local/share/dotnet/sdk";

        DirectoryInfo dir = new(startFolder);
        var fileList = dir.GetFiles("*.*", SearchOption.AllDirectories);

        var fileQuery = from file in fileList
                        where (file.Extension == ".txt" || file.Extension == ".cs")
                        orderby file.Name
                        select file;

        // Uncomment this block to see the full query
        foreach (FileInfo fi in fileQuery)
        {
            Console.WriteLine(fi.FullName);
        }

        // Get the name of the newest file
        var newestFile = (from file in fileQuery
                          orderby file.CreationTime
                          select new { file.FullName, file.CreationTime })
                          .Last();

        Console.WriteLine($"\r\nThe newest .txt file is {newestFile.FullName}. Creation time: {newestFile.CreationTime}");

        // Get files created or updated in the last 24 hours
        // Get the name of the newest file
        var updatedFiles = from file in fileQuery
                           where file.LastWriteTime > DateTime.Now.AddHours(-12)
                           orderby file.LastWriteTime
                           select file;

        // Uncomment this block to see the full query
        Console.WriteLine("\nFiles created in the last 12 hours:");
        foreach (FileInfo f in updatedFiles)
        {
            Console.WriteLine(f.FullName);
        }

        /////////////////////////////////////////////////////////////
        // How to group files by extension
        int trimLength = startFolder.Length;

        DirectoryInfo dir2 = new(startFolder);

        var fileList2 = dir.GetFiles("*.*", SearchOption.AllDirectories);

        var queryGroupByExt = from file in fileList2
                              group file by file.Extension.ToLower() into fileGroup
                              orderby fileGroup.Count(), fileGroup.Key
                              select fileGroup;

        // Iterate through the outer collection of groups.
        foreach (var filegroup in queryGroupByExt.Take(5))
        {
            Console.WriteLine($"Extension: {filegroup.Key}");
            var resultPage = filegroup.Take(20);

            //Execute the resultPage query
            foreach (var f in resultPage)
            {
                //Console.WriteLine($"\t{f.FullName.Substring(trimLength)}");
                Console.WriteLine($"\t{f.FullName[trimLength..]}");
            }
            Console.WriteLine();
        }

        //////////////////////////////////////////////////////////////////
        // Query for the total number of bytes in a set of folders
        var fileList3 = Directory.GetFiles(startFolder, "*.*", SearchOption.AllDirectories);

        var fileQuery3 = from file in fileList3
                         let fileLen = new FileInfo(file).Length
                         where fileLen > 0
                         select fileLen;

        // Cache the results to avoid multiple trips to the file system.
        long[] fileLengths = fileQuery3.ToArray();

        // Return the size of the largest file
        long largestFile = fileLengths.Max();

        // Return the total number of bytes in all the files under the specified folder.
        long totalBytes = fileLengths.Sum();

        Console.WriteLine($"There are {totalBytes} bytes in {fileList.Length} files under {startFolder}");
        Console.WriteLine($"The largest file is {largestFile} bytes.");

        ///////////////////////////////////////////////////////////////
        //
        // Return the FileInfo object for the largest file
        // by sorting and selecting from beginning of list
        FileInfo longestFile = (from file in fileList
                                where file.Length > 0
                                orderby file.Length descending
                                select file
                                ).First();

        Console.WriteLine($"The largest file under {startFolder} is {longestFile.FullName} with a length of {longestFile.Length} bytes");

        //Return the FileInfo of the smallest file
        FileInfo smallestFile = (from file in fileList
                                 where file.Length > 0
                                 orderby file.Length ascending
                                 select file
                                ).First();

        Console.WriteLine($"The smallest file under {startFolder} is {smallestFile.FullName} with a length of {smallestFile.Length} bytes");

        //Return the FileInfos for the 10 largest files
        var queryTenLargest = (from file in fileList
                               let len = file.Length
                               orderby len descending
                               select file
                              ).Take(10);

        Console.WriteLine($"The 10 largest files under {startFolder} are:");
        foreach (var v in queryTenLargest)
        {
            Console.WriteLine($"{v.FullName}: {v.Length} bytes");
        }

        // Group the files according to their size, leaving out
        // files that are less than 200000 bytes.
        var querySizeGroups = from file in fileList
                              let len = file.Length
                              where len > 0
                              group file by (len / 100000) into fileGroup
                              where fileGroup.Key >= 2
                              orderby fileGroup.Key descending
                              select fileGroup;

        foreach (var filegroup in querySizeGroups)
        {
            Console.WriteLine($"{filegroup.Key}00000");
            foreach (var item in filegroup)
            {
                Console.WriteLine($"\t{item.Name}: {item.Length}");
            }
        }

        ////////////////////////////////////////////////////////////////////////
        // Query for duplicate files in a directory tree

        // used in WriteLine to keep the lines shorter
        int charsToSkip = startFolder.Length;

        // var can be used for convenience with groups.
        var queryDupNames = from file in fileList
                                //group file.FullName.Substring(charsToSkip) by file.Name into fileGroup
                            group file.FullName[charsToSkip..] by file.Name into fileGroup
                            where fileGroup.Count() > 1
                            select fileGroup;

        foreach (var queryDup in queryDupNames.Take(20))
        {
            Console.WriteLine($"Filename = {(queryDup.Key.ToString() == string.Empty ? "[none]" : queryDup.Key.ToString())}");

            foreach (var fileName in queryDup.Take(10))
            {
                Console.WriteLine($"\t{fileName}");
            }
        }

        // Note the use of a compound key. Files that match
        // all three properties belong to the same group.
        // A named type is used to enable the query to be
        // passed to another method. Anonymous types can also be used
        // for composite keys but cannot be passed across method boundaries
        //
        var queryDupFiles = from file in fileList
                            group file.FullName[charsToSkip..] by
                                //(Name: file.Name, LastWriteTime: file.LastWriteTime, Length: file.Length)
                                (file.Name, file.LastWriteTime, file.Length)
                            into fileGroup
                            where fileGroup.Count() > 1
                            select fileGroup;

        foreach (var queryDup in queryDupFiles.Take(20))
        {
            Console.WriteLine($"Filename = {(queryDup.Key.ToString() == string.Empty ? "[none]" : queryDup.Key.ToString())}");

            foreach (var fileName in queryDup)
            {
                Console.WriteLine($"\t{fileName}");
            }
        }


    }
}
