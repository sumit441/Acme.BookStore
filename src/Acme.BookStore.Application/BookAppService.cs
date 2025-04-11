using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acme.BookStore.Books;
using Acme.BookStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace Acme.BookStore;

[Authorize]
public class BookAppService : ApplicationService, ITransientDependency
{
    private readonly IRepository<Book, Guid> _bookRepository;
    private readonly IGuidGenerator _guidGenerator;

    public BookAppService(IRepository<Book, Guid> bookRepository, IGuidGenerator guidGenerator)
    {
        _bookRepository = bookRepository;
        _guidGenerator = guidGenerator;
    }

    [Authorize(BookStorePermissions.Books.Create)]
    [HttpPost("api/books")]
    public async Task<BookDto> CreateAsync(BookDto book)
    {
        try
        {
            var newBook = new Book(_guidGenerator.Create(), book.Name, book.Type, book.PublishDate, book.Price);
            await _bookRepository.InsertAsync(newBook);
            return new BookDto
            {
                Id = newBook.Id,
                Name = newBook.Name,
                Type = newBook.Type,
                PublishDate = newBook.PublishDate,
                Price = newBook.Price
            };
        }
        catch (UserFriendlyException ex)
        {
            Logger.LogException(ex);
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogException(ex);
            throw new UserFriendlyException("An error occurred while creating the book.");
        }
    }

    [Authorize(BookStorePermissions.Books.Delete)]
    [HttpDelete("api/books/{id}")]
    public async Task DeleteAsync(Guid id)
    {
        try
        {
            var book = await _bookRepository.GetAsync(id);
            if (book == null)
            {
                throw new UserFriendlyException("Book not found.");
            }
            await _bookRepository.DeleteAsync(book);
        }
        catch (UserFriendlyException ex)
        {
            Logger.LogException(ex);
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogException(ex);
            throw new UserFriendlyException("An error occurred while deleting the book.");
        }
    }

    [Authorize(BookStorePermissions.Books.Get)]
    [HttpGet("api/books/{id}")]
    public async Task<BookDto> GetAsync(Guid id)
    {
        try
        {
            var book = await _bookRepository.GetAsync(id);
            if (book == null)
            {
                throw new UserFriendlyException("Book not found.");
            }
            return new BookDto
            {
                Id = book.Id,
                Name = book.Name,
                Type = book.Type,
                PublishDate = book.PublishDate,
                Price = book.Price
            };
        }
        catch (UserFriendlyException ex)
        {
            Logger.LogException(ex);
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogException(ex);
            throw new UserFriendlyException("An error occurred while retrieving the book.");
        }
    }

    [Authorize(BookStorePermissions.Books.GetList)]
    [HttpGet("api/books")]
    public async Task<List<BookDto>> GetListAsync()
    {
        try
        {
            var books = await _bookRepository.GetListAsync();
            var bookDtos = new List<BookDto>();
            foreach (var book in books)
            {
                bookDtos.Add(new BookDto
                {
                    Id = book.Id,
                    Name = book.Name,
                    Type = book.Type,
                    PublishDate = book.PublishDate,
                    Price = book.Price
                });
            }
            return bookDtos;
        }
        catch (UserFriendlyException ex)
        {
            Logger.LogException(ex);
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogException(ex);
            throw new UserFriendlyException("An error occurred while retrieving the book list.");
        }
    }

    [Authorize(BookStorePermissions.Books.Update)]
    [HttpPut("api/books/{id}")]
    public async Task UpdateAsync(Guid id, BookDto book)
    {
        try
        {
            var existingBook = await _bookRepository.GetAsync(id);
            if (existingBook == null)
            {
                throw new UserFriendlyException("Book not found.");
            }
            existingBook.Name = book.Name;
            existingBook.Type = book.Type;
            existingBook.PublishDate = book.PublishDate;
            existingBook.Price = book.Price;
            await _bookRepository.UpdateAsync(existingBook);
        }
        catch (UserFriendlyException ex)
        {
            Logger.LogException(ex);
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogException(ex);
            throw new UserFriendlyException("An error occurred while updating the book.");
        }
    }
}