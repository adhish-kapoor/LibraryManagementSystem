using LibraryData;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryServices
{
    public class LibraryAssetService : ILibraryAsset
    {
        private LibraryContext _context;

        public LibraryAssetService(LibraryContext context)
        {
            _context = context;
        }
        public void Add(LibraryAsset newAsset)
        {
            _context.Add(newAsset);
            _context.SaveChanges();

            //throw new NotImplementedException();
        }

        public IEnumerable<LibraryAsset> GetAll()
        {
            return _context.LibraryAssets
                .Include(asset => asset.Status)
                .Include(asset => asset.Location);
           // throw new NotImplementedException();
        }

        public LibraryAsset GetById(int id)
        {
            return _context.LibraryAssets                             //or return GetAll().FirstOrDefault(a=>a.Id == id)
                .Include(asset => asset.Status)
                .Include(asset => asset.Location)
                .FirstOrDefault(asset => asset.Id == id);
            //throw new NotImplementedException();
        }

        public LibraryBranch GetCurrentLocation(int id)
        {
            return GetById(id).Location;
            //throw new NotImplementedException();
        }

        public string GetDeweyIndex(int id)
        {
            //Discriminator
            if (_context.Books.Any(book => book.Id == id))
            {
                return _context.Books
                    .FirstOrDefault(book => book.Id == id).DeweyIndex;
            }
            else
                return "";
            //throw new NotImplementedException();
        }

        public string GetIsbn(int id)
        {
            if (_context.Books.Any(book => book.Id == id))
            {
                return _context.Books
                    .FirstOrDefault(book => book.Id == id).ISBN;
            }
            else
                return "";
            //throw new NotImplementedException();
        }

        public string GetTitle(int id)
        {
            return _context.LibraryAssets
                .FirstOrDefault(book => book.Id == id).Title;
            //throw new NotImplementedException();
        }

        public string GetType(int id)
        {
            var book = _context.LibraryAssets.OfType<Book>()
                .Where(b => b.Id == id);

            return book.Any() ? "Book" : "Video";
            //throw new NotImplementedException();
        }
        public string GetAuthorOrDirector(int id)
        {
            var isBook = _context.LibraryAssets.OfType<Book>()
                .Where(asset => asset.Id == id).Any();

            var isVideo = _context.LibraryAssets.OfType<Video>()
                .Where(asset => asset.Id == id).Any();
            return isBook ? _context.Books.FirstOrDefault(book => book.Id == id).Author :
                          _context.Videos.FirstOrDefault(video => video.Id == id).Director
                          ?? "Unknown";                                                      //a??b return a if it's not null,else return b

            //throw new NotImplementedException();
        }
    }
}
