﻿namespace DesafioDvD.Core.EventBus.Events
{
    public record DirectorUpdatedEvent(string Id, string FullName, DateTime UpdatedAt);
}