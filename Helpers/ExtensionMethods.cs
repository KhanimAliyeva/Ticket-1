

using System.Threading.Tasks;

namespace Simulation_1Mpa201.Helpers
{
    public static  class ExtensionMethods
    {
        public static bool  CheckSize(this IFormFile file,int mb)
        {
            return file.Length <= 1024 * 1024 * mb;
        }

        public static bool CheckType(this IFormFile file, string type)
        {
            return file.ContentType.Contains(type);
        }


        public static async Task<string> SaveFile(this IFormFile file,string folderPath)
        {
            string filePath = Guid.NewGuid().ToString() + file.FileName;
            string path = Path.Combine(folderPath, filePath);
            using FileStream stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);
            return filePath;

        }
    }
}
