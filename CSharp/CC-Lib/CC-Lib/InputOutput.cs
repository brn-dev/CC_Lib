using System;
using System.IO;
using System.Linq;
using System.Text;

namespace CC_Lib
{
    public static class InputOutput
    {
        /// <summary>
        /// Executes the given action for each file in the given folder.
        /// </summary>
        /// <param name="inputDirPath">The path to the folder</param>
        /// <param name="func">The fucntion which shall be executed with the input of each file. 
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
        /// Executes the given function for each file in the given input folder. 
        /// The return value of the function will then be written on a new file in the output dir.
        /// </summary>
        /// <param name="inputDirPath">Where the input files are.</param>
        /// <param name="outputDirPath">Where the output files should be created.</param>
        /// <param name="func">The function that converts the lines of the input file (string[]) 
        /// to a single line output (string) [public string doSmth(string[] input) {...}] </param>
        public static void ExecOnInputDirWithOutputDir(string inputDirPath, string outputDirPath,
            Func<string[], string> func)
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
                string output = func(File.ReadAllLines(file));

                using (StreamWriter streamWriter =
                    File.CreateText(outputDirPath + "\\" + file.Split('\\').LastOrDefault() + "-output.txt"))
                {
                    streamWriter.WriteLine(output);
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
    }
}
