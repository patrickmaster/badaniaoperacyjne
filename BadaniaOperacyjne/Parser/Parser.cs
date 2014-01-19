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
                    //int numPlaces = reader.ReadInt32();

                    //problem.NumPlaces = numPlaces;
                    //for (int i = 0; i < numPlaces; i++)
                    //{
                    //    List<double> list = new List<double>();
                    //    for (int j = 0; j < numPlaces; j++)
                    //    {
                    //        list.Add(reader.ReadDouble());
                    //    }
                    //    problem.Places.Add(list);
                    //}

                    //int petrolPlacesCount = reader.ReadInt32();
                    //for (int i = 0; i < petrolPlacesCount; i++)
                    //{
                    //    problem.PetrolPlaces.Add(reader.ReadInt32());
                    //}
                    problem = ReadInputData(reader);
                }
            }

            return problem;
        }

        public InputData ReadUserProblemFile(string filename)
        {
            // TODO: implementacja metody
            throw new NotImplementedException();
        }

        public SolutionData ReadSolutionFile(string filename)
        {
            InputData input = new InputData();
            OutputData output = new OutputData();
            using (FileStream stream = new FileStream(filename, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    input = ReadInputData(reader);
                    output = ReadOutputData(reader);
                }
            }
            return new SolutionData(input, output);
        }

        public void WriteBinaryProblemFile(string filename, InputData problem)
        {
            using (FileStream stream = new FileStream(filename, FileMode.Create))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    Write(writer, problem);
                }
            }
        }

        public void WriteSolutionFile(string filename, SolutionData solution)
        {
            using (FileStream stream = new FileStream(filename, FileMode.Create))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    Write(writer, solution.Input);
                    Write(writer, solution.Output);
                }
            }
        }

        private static void Write(BinaryWriter writer, InputData problem)
        {
            writer.Write(problem.NumPlaces);
            foreach (List<double> list in problem.Places)
            {
                Write(writer, list, false);
            }

            Write(writer, problem.PetrolPlaces);
        }

        private static InputData ReadInputData(BinaryReader reader)
        {
            InputData result = new InputData();

            result.NumPlaces = reader.ReadInt32();
            for (int i = 0; i < result.NumPlaces; i++)
            {
                result.Places.Add(ReadCollectionDouble(reader, result.NumPlaces).ToList());
            }
            result.PetrolPlaces = ReadCollectionInt(reader).ToList();

            return result;
        }

        private static void Write(BinaryWriter writer, OutputData output)
        {
            writer.Write(output.TotalCost);
            Write(writer, output.Order);
            Write(writer, output.Iterations);
        }

        private static OutputData ReadOutputData(BinaryReader reader)
        {
            OutputData result = new OutputData();
            result.TotalCost = reader.ReadDouble();
            result.Order = ReadCollectionInt(reader).ToList();
            result.Iterations = ReadCollectionIterationBlock(reader).ToList();
            return result;
        }

        private static void Write(BinaryWriter writer, IterationBlock block)
        {
            writer.Write(block.ProgressionCount);
            writer.Write(block.RegressionCount);
            Write(writer, block.Iterations);
        }

        private static IterationBlock ReadIterationBlock(BinaryReader reader)
        {
            IterationBlock result = new IterationBlock();
            result.ProgressionCount = reader.ReadInt32();
            result.RegressionCount = reader.ReadInt32();
            result.Iterations = ReadCollectionIteration(reader).ToList();
            return result;
        }

        private static void Write(BinaryWriter writer, Iteration iteration)
        {
            writer.Write(iteration.IterationNumber);
            writer.Write(iteration.Cost);
        }

        private static Iteration ReadIteration(BinaryReader reader)
        {
            Iteration iteration = new Iteration(reader.ReadInt32());
            iteration.Cost = reader.ReadDouble();
            return iteration;
        }

        private static void Write<T>(BinaryWriter writer, IEnumerable<T> input, bool writeLength = true)
        {
            if (writeLength == true)
            {
                writer.Write(input.Count());
            }

            foreach (T item in input)
            {
                if (typeof(T) == typeof(int))
                    writer.Write((int)(object)item);
                else if (typeof(T) == typeof(double))
                    writer.Write((double)(object)item);
                else if (typeof(T) == typeof(Iteration))
                    Write(writer, (Iteration)(object)item);
                else if (typeof(T) == typeof(IterationBlock))
                    Write(writer, (IterationBlock)(object)item);
                else
                    throw new Exception("Unsupported write-to-file type!");
            }
        }

        private static IEnumerable<IterationBlock> ReadCollectionIterationBlock(BinaryReader reader, int toRead = 0)
        {
            int count;
            if (toRead == 0)
                count = reader.ReadInt32();
            else
                count = toRead;
            List<IterationBlock> result = new List<IterationBlock>();
            for (int i = 0; i < count; i++)
            {
                result.Add(ReadIterationBlock(reader));
            }
            return result;
        }

        private static IEnumerable<Iteration> ReadCollectionIteration(BinaryReader reader, int toRead = 0)
        {
            int count;
            if (toRead == 0)
                count = reader.ReadInt32();
            else
                count = toRead;
            List<Iteration> result = new List<Iteration>();
            for (int i = 0; i < count; i++)
            {
                result.Add(ReadIteration(reader));
            }
            return result;
        }

        private static IEnumerable<int> ReadCollectionInt(BinaryReader reader, int toRead = 0)
        {
            int count;
            if (toRead == 0)
                count = reader.ReadInt32();
            else
                count = toRead;
            List<int> result = new List<int>();
            for (int i = 0; i < count; i++)
            {
                result.Add(reader.ReadInt32());
            }
            return result;
        }

        private static IEnumerable<double> ReadCollectionDouble(BinaryReader reader, int toRead = 0)
        {
            int count;
            if (toRead == 0)
                count = reader.ReadInt32();
            else
                count = toRead;
            List<double> result = new List<double>();
            for (int i = 0; i < count; i++)
            {
                result.Add(reader.ReadDouble());
            }
            return result;
        }
    }
}
