using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using WebApp.Models.Olympics;

namespace WebApp.Controllers;
[Route("api/person")]
[ApiController]
public class OlympicsPersonApiController : ControllerBase
{
    private readonly OlympicsContext _context;

    public OlympicsPersonApiController(OlympicsContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetFiltered(string filter)
    {
        return Ok(_context.People
            .Where(o => o.FullName.ToLower().Contains(filter.ToLower()))
            .OrderBy(o => o.FullName)
            .AsNoTracking()
        );
    }

    [HttpGet]
    public IActionResult GetPaged(int page = 1, int size = 10)
    {
        int totalCount = _context.People.Count();
        
        var peoplePaged = _context.People
            .OrderBy(o => o.FullName)
            .Skip((page - 1) * size)
            .Take(size)
            .AsNoTracking()
            .ToList();
        return Ok(peoplePaged);
    }
    
}

public class PagingListAsync<T>
{
    public IAsyncEnumerable<T> Data { get;  }
    public int TotalItems { get; }
    public int TotalPages { get; }
    public int Page { get; }
    public int Size { get; }
    public bool IsFirst { get; }
    public bool IsLast { get; }
    public bool IsNext { get; }
    public bool IsPrevious { get; }
    private PagingListAsync(Func<int, int, IAsyncEnumerable<T>> dataGenerator, int totalItems, int page, int size)
    {
        TotalItems = totalItems;
        Page = page;
        Size = size;
        TotalPages = CalcTotalPages(Page, TotalItems, Size);
        IsFirst = Page <= 1;
        IsLast = Page >= TotalPages;
        IsNext = !IsLast;
        IsPrevious = !IsFirst;
        Data = dataGenerator(Page, size);
    }
    public static PagingListAsync<T> Create(Func<int, int, IAsyncEnumerable<T>> data, int totalItems, int number, int size)
    {
        return new PagingListAsync<T>( data, totalItems, ClipPage(number, totalItems, size), Math.Abs(size));
    }

    private static int CalcTotalPages(int page, int totalItems, int size)
    {
        return totalItems / size + (totalItems % size > 0 ? 1 : 0);
    }
    private static int ClipPage(int page, int totalItems, int size)
    {
        int totalPages = CalcTotalPages(page, totalItems, size);
        if (page > totalPages)
        {
            return totalPages;
        }
        if (page < 1)
        {
            return 1;
        }
        return page;
    } 
}

