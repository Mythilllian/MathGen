using System.Text.Json;

namespace MathProblemGenerator
{
    public static class Serializer
    {
        public static Problem? LoadProblemFromJson(string json)
        {
            try
            {
                return (Problem?)JsonSerializer.Deserialize(json, typeof(Problem));
            }
            catch { return null; }
        }

        public static Problem? LoadProblemFromPath(string path)
        {
            if(File.Exists(path) && Path.GetExtension(path) == ".json")
            {
                return LoadProblemFromJson(File.ReadAllText(path));
            }
            return null;
        }

        public static Problem[] LoadProblemFolder(string path)
        {
            List<Problem> problems = new List<Problem>();

            foreach(var file in Directory.GetFiles(path, "*.json"))
            {
                try
                {
                    problems.Add(LoadProblemFromPath(file));
                }
                catch { }
            }

            return problems.ToArray();
        }

        public static void SaveProblem(Problem problem)
        {
            JsonSerializer.Serialize(problem);
        }
    }
}
