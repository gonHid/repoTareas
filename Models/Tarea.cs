namespace repoTareas.Models
{
    public class Tarea
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int idUser { get; set; }
        public string user { get; set; }
        public string status { get; set; }
        public int importance { get; set; }
    }
}
