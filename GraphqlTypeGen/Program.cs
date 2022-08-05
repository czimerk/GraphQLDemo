// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var fileName = "types.cs";
var folder = "c:\\GitRepos\\GraphQLDemo\\GraphQLDemo\\Domain";
File.Delete(Path.Join(folder, fileName));

if (Directory.Exists(folder))
{
    bool first = true;
    var files = Directory.GetFiles(folder);

    var outputLines = new List<string>();
    outputLines.Add("using GraphQL.Types;");
    outputLines.Add("");
    foreach (var file in files) {
        var inputLines = File.ReadAllLines(file);
        foreach (var line in inputLines)
        {
            if (line.Contains("public class"))
            {
                if (first)
                {
                    outputLines.Add(inputLines[0]);
                    outputLines.Add(inputLines[1]);
                    first = false;
                }
                else {
                    outputLines.Add("\t\t}");
                    outputLines.Add("\t}");
                }
                var parts = line.Split(" ");
                var className = parts[parts.Length - 1];

                var outputLine = $"\tpublic class {className}Type : ObjectGraphType<{className}>";
                outputLines.Add(outputLine);
                outputLines.Add("\t{");
                outputLines.Add($"\t\tpublic {className}Type()");
                outputLines.Add("\t\t{");
            }
            else {
                if (!line.Contains("enum") && line.Contains("public"))
                {
                    var parts = line.Split(" ");

                    string typeName = "";
                    string propertyName = "";
                    for (int i = 0; i < parts.Length; i++)
                    {
                        if (parts[i] == "public")
                        {
                            i++;
                            typeName = parts[i];
                            i++;
                            propertyName = parts[i];
                        }
                    }
                  

                    var outputLine = $"\t\t\tField(x => x.{propertyName});";
                    outputLines.Add(outputLine);
                }
            }
         
        }
    }
    outputLines.Add("\t\t}");
    outputLines.Add("\t}");
    outputLines.Add("}");
    File.WriteAllLines(Path.Join(folder,fileName), outputLines);
}
else
{
    Console.WriteLine("Directory does not exist");
}