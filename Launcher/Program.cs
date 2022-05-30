using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Launcher
{
    public static class Program
    {
        private static readonly (string path, string name) _core = ("./Core", "Projet principal");
        private static readonly (string path, string name) _reviewsCollection = ("./HomeProjectTest", "Projet de collecte");
        private static readonly (string path, string name) _reviewsTracking = ("./ReviewsTracking", "Projet de suivi");

        public static void Main(string[] args)
        {

            var projets = new List<(string path, string name)>
            {
                _core,
                _reviewsCollection,
                _reviewsTracking
            };

            RunAll(projets);
        }

        /// <summary>
        /// Méthode de démarrage de tous les projets.
        /// </summary>
        /// <param name="projects">Liste des projets avec le path et le nom informatif de chacun.</param>
        private static void RunAll(List<(string path, string name)> projects)
        {
            foreach(var project in projects)
            {
                Console.WriteLine("Démarrage de {0}...", project.name);

                var startInfos = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    WorkingDirectory = Path.GetFullPath(project.path),
                    Arguments = "run",
                    UseShellExecute = true,
                };

                Process.Start(startInfos);

                Console.WriteLine("Démarré.");

                Thread.Sleep(500);

            }
        }
    }
}
