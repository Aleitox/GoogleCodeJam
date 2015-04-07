using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaSigma.FileFixIt
{
    public class Problem
    {
        public int AmountOfPaths { get; set; }

        public int AmountOfPathsToAdd { get; set; }

        public List<string> Paths { get; set; }

        public List<string> PathsToAdd { get; set; }

        public int Solve()
        {

            var acum = 0;

            foreach (var pathToAdd in PathsToAdd)
            {
                acum += AmountOfNewFoldersFor(pathToAdd);
                Paths.Add(pathToAdd);
            }

            return acum;
        }

        private int AmountOfNewFoldersFor(string pathToAdd)
        {
            var aux = pathToAdd;
            var foldersInPath = pathToAdd.Count(x => x == '/');
            var newFolders = 0;
            while (newFolders < foldersInPath)
            {
                if (Paths.Any(p => p.StartsWith(aux)))
                    break;

                aux = aux.Substring(0, aux.LastIndexOf('/'));
                newFolders++;
            }
            return newFolders;
        }
    }
}
