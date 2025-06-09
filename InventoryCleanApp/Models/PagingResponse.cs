﻿namespace InventoryCleanApp.Models;

public class PagingResponse<T> where T : class
{
    public List<T> Items { get; set; }
    public MetaData MetaData { get; set; }
}

public class MetaData
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;
}
