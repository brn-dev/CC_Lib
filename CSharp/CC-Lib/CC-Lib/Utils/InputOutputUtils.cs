using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CC_Lib.Utils
{
    public class InputDirectoryNotFoundException : DirectoryNotFoundException
    {
        public InputDirectoryNotFoundException(string inputDir) : base($"input directory not found ({inputDir})") { }
    }

    public class OutputDirectoryNotFoundException : DirectoryNotFoundException
    {
        public OutputDirectoryNotFoundException(string output) : base($"output directory not found ({output})") { }
    }

    public class InputFileNotFoundException : FileNotFoundException
    {
        public InputFileNotFoundException(string inputFile) : base($"input file not found ({inputFile})") { }
    }

    public static class InputOutputUtils
    {

        private static void CheckInputFile(string inputFile)
        {
            if (!File.Exists(inputFile))
            {
                throw new InputFileNotFoundException(inputFile);
            }
        }

        private static void CheckInputDir(string inputDir)
        {
            if (!Directory.Exists(inputDir))
            {
                throw new InputDirectoryNotFoundException(inputDir);
            }
        }

        private static void CheckOutputDir(string outputDir)
        {
            if (!Directory.Exists(outputDir))
            {
                throw new OutputDirectoryNotFoundException(outputDir);
            }
        }

        private static void CheckInputOutputDir(string inputDir, string outputDir)
        {
            CheckInputDir(inputDir);
            CheckOutputDir(outputDir);
        }

        private static void CheckBaseDir(ref string baseDir, string level, out string inputDir, out string outputDir)
        {
            if (!baseDir.EndsWith(@"\"))
            {
                baseDir += @"\";
            }

            inputDir = $"{baseDir}input\\{level}";
            outputDir = $"{baseDir}output\\{level}";

            CheckInputOutputDir(inputDir, outputDir);
        }

        /// <summary>
        /// Executes the given action for each file in the given folder.
        /// </summary>
        /// <param name="inputDirPath">The path to the folder</param>
        /// <param name="func">The function which shall be executed with the input of each file. 
        /// This function has to have one parameter namely a string array - 
        /// representing the content of the input file</param>
        public static void ExecOnInputDir(string inputDirPath, Action<string[]> func)
            // ReSharper restore UnusedMember.Global
        {
            CheckInputDir(inputDirPath);

            var files = Directory.GetFiles(inputDirPath);
            foreach (var file in files)
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
            CheckInputFile(inputFilePath);
                
            var lines = File.ReadAllLines(inputFilePath);
            foreach (var line in lines)
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
             CheckInputOutputDir(inputDirPath, outputDirPath);

            _ExecOnInputDirWithOutputDir(inputDirPath, outputDirPath, func);
        }

        private static void _ExecOnInputDirWithOutputDir(string inputDirPath, string outputDirPath,
            Func<string[], string[]> func)
        {
            var files = Directory.GetFiles(inputDirPath);
            foreach (var file in files)
            {
                var output = func(File.ReadAllLines(file));

                using (var streamWriter =
                    File.CreateText(outputDirPath + "\\" + file.Split('\\').LastOrDefault() + "-output.txt"))
                {
                    foreach (string line in output)
                    {
                        streamWriter.WriteLine(line);
                    }
                }
            }
        }

        private static void _ExecOnInputDirWithOutputDir(string inputDirPath, string outputDirPath, ExecLevelWithListDelegate func)
        {
            var files = Directory.GetFiles(inputDirPath);
            foreach (var file in files)
            {
                var list = new List<string>();
                func(File.ReadAllLines(file), list);

                using (var streamWriter =
                    File.CreateText(outputDirPath + "\\" + file.Split('\\').LastOrDefault() + "-output.txt"))
                {
                    foreach (var line in list)
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
            CheckInputDir(inputDirPath);

            var files = Directory.GetFiles(inputDirPath);
            var stringBuilder = new StringBuilder();
            foreach (var file in files)
            {
                var output = func(File.ReadAllLines(file));
                var fileName = file.Split('\\').LastOrDefault();
                stringBuilder.AppendLine("<" + fileName + ">");
                stringBuilder.AppendLine(output);
                stringBuilder.AppendLine("</" + fileName + ">");
                stringBuilder.AppendLine("\n");
            }
            using (var streamWriter = File.CreateText(outputFilePath))
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
                var input = Console.ReadLine();
                if (input == null || input.ToLower() == "esc")
                {
                    return;
                }
                var output = func(input);
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
            CheckInputFile(inputFilePath);

            var builder = new StringBuilder();

            using (var reader = new StreamReader(new FileStream(inputFilePath, FileMode.Open)))
            {
                var counter = 1;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var output = func(line);
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
            CheckBaseDir(ref baseDir, level, out var inputDir, out var outputDir);

            _ExecOnInputDirWithOutputDir(inputDir, outputDir, func);
        }

        public delegate void ExecLevelWithListDelegate(string[] input, IList<string> output);

        public static void ExecLevel(string baseDir, string level, ExecLevelWithListDelegate func)
        {
            CheckBaseDir(ref baseDir, level, out var inputDir, out var outputDir);

            _ExecOnInputDirWithOutputDir(inputDir, outputDir, func);
        }
    }
}
