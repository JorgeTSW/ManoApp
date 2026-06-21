using ManoApp.Domain.Models;

namespace ManoApp.Domain.Interfaces
{
    public interface IGestureLogRepository
    {
        void Save(GestureLogEntry entry);
        List<GestureLogEntry> GetAll();
    }
}