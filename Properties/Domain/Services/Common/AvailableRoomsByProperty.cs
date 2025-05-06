using Domain.Entities;

namespace Domain.Services.Common;

public class AvailableRoomsByProperty
{
    public AvailableRoomsByProperty(Property property, List<RoomType> roomTypes)
    {
        Property = property;
        AvailableRoomTypes = roomTypes;
    }

    public Property Property { get; }
    public List<RoomType> AvailableRoomTypes { get; }
}