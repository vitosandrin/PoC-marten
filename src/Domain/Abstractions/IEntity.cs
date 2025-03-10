﻿namespace Domain.Abstractions;

public interface IEntity
{
    Guid Id { get; }
    DateTime CreatedAt { get; }
}
