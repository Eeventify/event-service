using DTO_Layer;

namespace Abstraction_Layer
{
    public interface IEventDAL
    {
        bool AddEvent(EventDTO eventDTO);
        EventDTO? GetEvent(int Id);
    }
}