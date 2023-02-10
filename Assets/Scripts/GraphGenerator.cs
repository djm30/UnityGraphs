using System;
using System.Collections.Generic;
using System.IO;

namespace CSRGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the number of vertices:");
            int numVertices = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the maximum out-degree:");
            int maxOutDegree = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the filename:");
            string filename = Console.ReadLine();
            int[][] csr = GenerateCSRRepresentation(numVertices, maxOutDegree);
            SaveCSRToFile(csr, filename);
        }

        static int[][] GenerateCSRRepresentation(int numVertices, int maxOutDegree)
        {
            int[][] csr = new int[numVertices][];
            Random random = new Random();
            for (int i = 0; i < numVertices; i++)
            {
                int outDegree = Math.Min(maxOutDegree, numVertices - 1);
                csr[i] = new int[outDegree + 1];
                csr[i][0] = i;
                List<int> selectedNodes = new List<int>(outDegree);
                for (int j = 1; j <= outDegree; j++)
                {
                    int destinationNode;
                    do
                    {
                        destinationNode = random.Next(0, numVertices);
                    } while (destinationNode == i || selectedNodes.Contains(destinationNode));
                    selectedNodes.Add(destinationNode);
                    csr[i][j] = destinationNode;
                }
            }
            return csr;
        }

        static void SaveCSRToFile(int[][] csr, string filename)
        {
            string directory = "../Graphs/";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            string filePath = Path.Combine(directory, filename + ".csr");
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (int[] row in csr)
                {
                    writer.WriteLine(string.Join(" ", row));
                }
            }
        }
    }
}
