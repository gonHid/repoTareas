using repoTareas.Models;
using repoTareas.Repositories;

namespace repoTareas.Services
{
    public class TareasServices
    {
        private readonly TareasRepos _tareasRepository = new TareasRepos();

        public async Task<List<Tarea>> GetTareas()
        {
            var data = await _tareasRepository.GetTareas();
            return data;
        }

        public async Task<Tarea> GetTareaByID(int id)
        {
            var data = await _tareasRepository.GetTareaByID(id);
            return data;
        }

        public async Task<bool> PostNuevaTarea(Tarea tarea)
        {
            var data = await _tareasRepository.PostNuevaTarea(tarea);
            return data;
        }

        public async Task<bool> AvanzarTarea(int id)
        {
            Tarea tarea = await _tareasRepository.GetTareaByID(id).ConfigureAwait(false);
            switch (tarea.status)
            {
                case "TODO":
                    tarea.status = "INPROGRESS";
                    break;

                case "INPROGRESS":
                    tarea.status = "REVIEWING";
                    break;

                case "REVIEWING":
                    tarea.status = "DONE";
                    break;

                default:
                    return false;
            }

            var data = await _tareasRepository.UpdateTarea(tarea);
            return data;
        }

        public async Task<bool> DeleteTarea(int id)
        {
            var data = await _tareasRepository.DeleteTarea(id);
            return data;
        }

    }
}
