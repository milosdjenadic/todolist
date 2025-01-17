﻿using System.ComponentModel.DataAnnotations;

namespace Assignment.Domain.Entities;

public class TodoList : BaseAuditableEntity
{
    [StringLength(200)]
    public string? Title { get; set; }

    public Colour Colour { get; set; } = Colour.White;

    public IList<TodoItem> Items { get; private set; } = new List<TodoItem>();
}
