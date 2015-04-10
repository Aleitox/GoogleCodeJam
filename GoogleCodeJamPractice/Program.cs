using GoogleCodeJam.Base;
using GoogleCodeJam.FileIO;
using GoogleCodeJam.Interpreter;
using System.Collections.Generic;

namespace GoogleCodeJamPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            var aProblem = ProblemFactory.Get(ProblemEnmu.Rotate, SizeEnum.Small);

            var fileManger = new FileManager(aProblem.FileName);
            var input = fileManger.ReadFile();

            var initializer = new ObjectInitializer(aProblem);

            dynamic inputProblems = typeof(ObjectInitializer)
                .GetMethod("InitializeObject")
                .MakeGenericMethod(aProblem.GetType())
                .Invoke(initializer, new object[] { aProblem, input });

            var printSolutions = new List<string>();
            for (var index = 0; index < inputProblems.Cases; index++)
                printSolutions.Add(string.Format("Case #{0}: {1}", (index + 1).ToString(), inputProblems.Problems[index].PrintSolution()));

            fileManger.WriteFile(printSolutions);
        }
    }
}
