using System;
using System.IO;
using System.Linq;
using System.Text;

namespace CC_Lib.Utils
{
    public static class InputOutput
    {
        /// <summary>
        /// Executes the given action for each file in the given folder.
        /// </summary>
        /// <param name="inputDirPath">The path to the folder</param>
        /// <param name="func">The function which shall be executed with the input of each file. 
        /// This function has to have one parameter namely a string array - 
        /// representing the content of the input file</param>
        public static void ExecOnInputDir(string inputDirPath, Action<string[]> func)
        {
            if (!Directory.Exists(inputDirPath))
            {
                throw new ArgumentException("Input Directory doesn't exist");
            }

            var files = Directory.GetFiles(inputDirPath);
            foreach (string file in files)
            {
                func(File.ReadAllLines(file));
            }
        }

        /// <summary>
        /// Executes the given function for each line in the input file.
        /// </summary>
        /// <param name="inputFilePath">The path to the input file. There has to be one input per line in this file.</param>
        /// <param name="func">The function that will be called for each line in the input file.</param>
        public static void ExecOnInputFile(string inputFilePath, Action<string> func)
        {
            if (!File.Exists(inputFilePath))
            {
                throw new ArgumentException("Input Directory doesn't exist");
            }

            var lines = File.ReadAllLines(inputFilePath);
            foreach (string line in lines)
            {
                func(line);
            }
        }


        /// <summary>
        /// Executes the given function for each file in the given input folder. 
        /// The return value of the function will then be written on a new file in the output dir.
        /// </summary>
        /// <param name="inputDirPath">Where the input files are.</param>
        /// <param name="outputDirPath">Where the output files should be created.</param>
        /// <param name="func">The function that converts the lines of the input file (string[]) 
        /// to a single line output (string) [public string doSmth(string[] input) {...}] </param>
        public static void ExecOnInputDirWithOutputDir(string inputDirPath, string outputDirPath,
            Func<string[], string[]> func)
        {
            if (!Directory.Exists(inputDirPath))
            {
                throw new ArgumentException("Input Directory doesn't exist");
            }

            if (!Directory.Exists(outputDirPath))
            {
                throw new ArgumentException("Ouput Directory doesn't exist");
            }

            var files = Directory.GetFiles(inputDirPath);
            foreach (string file in files)
            {
                string[] output = func(File.ReadAllLines(file));

                using (StreamWriter streamWriter =
                    File.CreateText(outputDirPath + "\\" + file.Split('\\').LastOrDefault() + "-output.txt"))
                {
                    foreach (string line in output)
                    {
                        streamWriter.WriteLine(line);
                    }
                }
            }
        }

        /// <summary>
        /// Executes the given function for each file in the given input folder. 
        /// All return values will then be written into a single output file.
        /// </summary>
        /// <param name="inputDirPath">Where the input files are.</param>
        /// <param name="outputFilePath">Where the output file should be.</param>
        /// <param name="func">Takes string[] as input and returns string</param>
        public static void ExecOnInputDirWithOutputFile(string inputDirPath, string outputFilePath,
            Func<string[], string> func)
        {
            if (!Directory.Exists(inputDirPath))
            {
                throw new ArgumentException("Input Directory doesn't exist");
            }

            var files = Directory.GetFiles(inputDirPath);
            var stringBuilder = new StringBuilder();
            foreach (string file in files)
            {
                string output = func(File.ReadAllLines(file));
                string fileName = file.Split('\\').LastOrDefault();
                stringBuilder.AppendLine("<" + fileName + ">");
                stringBuilder.AppendLine(output);
                stringBuilder.AppendLine("</" + fileName + ">");
                stringBuilder.AppendLine("\n");
            }
            using (StreamWriter streamWriter = File.CreateText(outputFilePath))
            {
                streamWriter.WriteLine(stringBuilder.ToString());
            }
        }

        /// <summary>
        /// Executes the given function with inputs written into the console (only works in console applications). 
        /// The output is written onto the console.
        /// If you want to stop, type "esc" into the input.
        /// </summary>
        /// <param name="func">The function that will be executed for each input.</param>
        public static void ExecConsoleInput(Func<string, string> func)
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (input == null || input.ToLower() == "esc")
                {
                    return;
                }
                string output = func(input);
                Console.WriteLine($"\n{output}\n\n");
            }
        }

        /// <summary>
        /// Executes the given function for each line in the input file and writes it to the output file.
        /// </summary>
        /// <param name="inputFilePath">The path to the input file. There has to be one input per line in this file.</param>
        /// <param name="outputFilePath">The path to the file where the outputs will be written to.</param>
        /// <param name="func">The function that will be executed to convert the input to the output.</param>
        public static void ExecFileInputWithOutputFile(string inputFilePath, string outputFilePath,
            Func<string, string> func)
        {
            if (!File.Exists(inputFilePath))
            {
                throw new ArgumentException("Input File doesn't exist");
            }

            var builder = new StringBuilder();

            using (var reader = new StreamReader(new FileStream(inputFilePath, FileMode.Open)))
            {
                int counter = 1;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string output = func(line);
                    builder.AppendLine($"<{counter}>");
                    builder.AppendLine($"{output}");
                    builder.AppendLine($"</{counter}>");
                    builder.AppendLine();
                    counter++;
                }
            }

            using (var writer = new StreamWriter(new FileStream(outputFilePath, FileMode.Create)))
            {
                writer.WriteLine(builder.ToString());
            }
            
        }

        public static void ExecLevel(string baseDir, string level, Func<string[], string[]> func)
        {
            if (!baseDir.EndsWith(@"\"))
            {
                baseDir += @"\";
            }

            var inputDir = $"{baseDir}input\\{level}";
            var outputDir = $"{baseDir}output\\{level}";

            if (!Directory.Exists(inputDir))
            {
                throw new ArgumentException($"input directory doesn't exist ({inputDir})");
            }

            if (!Directory.Exists(inputDir))
            {
                throw new ArgumentException($"output directory doesn't exist ({outputDir})");
            }

            ExecOnInputDirWithOutputDir(inputDir, outputDir, func);
        }
    }
}
