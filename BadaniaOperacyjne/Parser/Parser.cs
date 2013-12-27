using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BadaniaOperacyjne.DataType;
using System.IO;

namespace BadaniaOperacyjne.Parser
{
    public class Parser : IParser
    {

        public InputData ReadBinaryProblemFile(string filename)
        {
            InputData problem = new InputData();

            using (FileStream stream = new FileStream(filename, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    int numPlaces = reader.ReadInt32();

                    problem.NumPlaces = numPlaces;
                    for (int i = 0; i < numPlaces; i++)
                    {
                        List<double> list = new List<double>();
                        for (int j = 0; j < numPlaces; j++)
                        {
                            list.Add(reader.ReadDouble());
                        }
                        problem.Places.Add(list);
                    }

                    int petrolPlacesCount = reader.ReadInt32();
                    for (int i = 0; i < petrolPlacesCount; i++)
                    {
                        problem.PetrolPlaces.Add(reader.ReadInt32());
                    }
                }
            }

            return problem;
        }

        public InputData ReadUserProblemFile(string filename)
        {
            throw new NotImplementedException();
        }

        public Solution ReadSolutionFile(string filename)
        {
            throw new NotImplementedException();
        }

        public void WriteBinaryProblemFile(string filename, InputData problem)
        {
            using (FileStream stream = new FileStream(filename, FileMode.Create))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(problem.NumPlaces);
                    foreach (List<double> list in problem.Places)
                    {
                        foreach (double value in list)
                        {
                            writer.Write(value);
                        }
                    }

                    writer.Write(problem.PetrolPlaces.Count);
                    foreach (int value in problem.PetrolPlaces)
                    {
                        writer.Write(value);
                    }
                }
            }
        }

        public void WriteSolutionFile(string filename, Solution solution)
        {
            throw new NotImplementedException();
        }
    }
}
