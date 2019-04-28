using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using EFcore.Context;
using EFcore.Model;
using Microsoft.AspNetCore.Mvc;
using EFCore.Web.Models;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account.Manage;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly EFContext _eFContext;
        private readonly EFContext _eFContext2;
        private readonly EFContext _eFContext3;

        public HomeController(EFContext eFContext, EFContext eFContext2, EFContext eFContext3)
        {
            _eFContext = eFContext;
            _eFContext2 = eFContext2;
            _eFContext3 = eFContext3;
        }

        private int InitializeDatabase()
        {
            _eFContext.Set<Blog>().Add(new Blog
            {
                Url = "http://sample.com",
                Posts = new List<Post>
                {
                    new Post {Title = "Saving Data with EF"},
                    new Post {Title = "Cascade Delete with EF"},
                    new Post {Title = "走着瞧"}
                },
                 Bytes = DateTime.Now
            });

            return _eFContext.SaveChanges();
        }

        void sapp(Blog blog, IList<Post> posts)
        {
            var blogEntry = _eFContext.Entry(blog);
            Console.WriteLine($"    Blog '{blog.BlogId}' is in state {blogEntry.State} with {posts.Count} posts referenced.");
            foreach (var post in posts)
            {
                var postEntry = _eFContext.Entry(post);

                Console.WriteLine(
                    $"      Post '{post.PostId}' is in state {postEntry.State} " +
                    $"with FK '{post.BlogId?.ToString() ?? "null"}' and {(post.Blog == null ? "no reference to a blog." : $"reference to blog '{post.BlogId}'.")}");
            }
        }
        public IActionResult Index()
        {
            #region add
            var ad = InitializeDatabase();
            //var blog = _eFContext.Set<Blog>().Include(b => b.Posts).First();
            //var posts = blog.Posts.ToList();
            //sapp(blog, posts);

            //// var s = _eFContext.Remove(blog);

            //blog.Posts.Add(new Post()
            //{
            //     Title = "as"
            //});
            //blog.Posts.ForEach(_ =>
            //{
            //    int s = 1;
            //    _.Title = s++.ToString();
            //});
            //sapp(blog, posts);
            ////  blog.Posts.Clear();
            //var t = _eFContext.SaveChanges();
            //sapp(blog, posts);
            #endregion
            IsItNew();
            var e = _eFContext.Set<Blog>().SingleOrDefault(m => m.Url == "http://samaple.com");
            var b = new Blog()
            {
                Url = "da",
                Posts = new List<Post>()
              {
                  new Post()
                  {
                      Title = "12"
                  }
              }
            };
            //b = new Blog()
            //{
            //    BlogId = 2,
            //    Url = "da",
            //    Posts = new List<Post>()
            //    {
            //        new Post()
            //        {
            //            Title = "12"
            //        }
            //    }
            //};

            _eFContext.Add(b);
            var r = _eFContext.SaveChanges();
            var ex = _eFContext.Set<Blog>().Find(b.BlogId);
            if (ex == null)
            {
                _eFContext.Add(b);
            }
            else
            {
                //_eFContext.Update(b);
                _eFContext.Entry(ex).CurrentValues.SetValues(b);
            }
            var xes = _eFContext.Set<Blog>().Include(m => m.Posts).Where(s => s.Posts.Count() > 0).ToList();
            var xe = xes.First();
            //if (xe!=null)
            //{
            //    int i = 1;
            //    xe.Posts.ForEach(m => m.Content = "1" + i++.ToString());
            //}

            // _eFContext.Update(xe);
            // var ra = _eFContext.SaveChanges();



            // var blog = b;
            // blog.Url = "dsadasd";


            // var existingBlog = _eFContext.Blogs
            //     .Include(bs => bs.Posts)
            //     .FirstOrDefault(ba => ba.BlogId == blog.BlogId);

            // if (existingBlog == null)
            // {
            //     _eFContext.Add(blog);
            // }
            // else
            // {
            //     _eFContext.Entry(existingBlog).CurrentValues.SetValues(blog);
            //     foreach (var post in blog.Posts)
            //     {
            //         var existingPost = existingBlog.Posts
            //             .FirstOrDefault(p => p.PostId == post.PostId);

            //         if (existingPost == null)
            //         {
            //             existingBlog.Posts.Add(post);
            //         }
            //         else
            //         {
            //             _eFContext.Entry(existingPost).CurrentValues.SetValues(post);
            //         }
            //     }

            //     foreach (var post in existingBlog.Posts)
            //     {
            //         if (!blog.Posts.Any(p => p.PostId == post.PostId))
            //         {
            //             _eFContext.Remove(post);
            //         }
            //     }
            // }

            // _eFContext.SaveChanges();

            // xe.Url = "sad";

            // SaveAnnotatedGraph(_eFContext2, xe);

            // //var sp=_eFContext.Entry<Blog>().Collections(MetadataProvider=>)

            // var sws = _eFContext.Blogs.Find(5);
            //  _eFContext.Entry(sws).Collection(m=>m.Posts).Load();
            //var st=  _eFContext.Entry(sws).State;
            // foreach (var item in sws.Posts)
            // {

            // }


            // _eFContext.ChangeTracker.LazyLoadingEnabled = false;
            // var sw = _eFContext.Blogs.Find(5);
            // var swaq= _eFContext.Blogs.Where(n=>n.BlogId==5).AsNoTracking().ToList();
            // #region strategy
            // var stategy = _eFContext.Database.CreateExecutionStrategy();
            // stategy.Execute(() =>
            // {
            //     #region ManualTransaction
            //     using (var transaction = _eFContext2.Database.BeginTransaction())
            //     {
            //         try
            //         {
            //             var see = new Blog { Url = "http://blogs.msdn.com/dotnet" };
            //             _eFContext2.Blogs.Add(see);
            //             //    _eFContext2.SaveChanges();

            //             _eFContext2.Blogs.Add(new Blog { Url = "http://blogs.msdn.com/visualstudio" });
            //             _eFContext2.SaveChanges();

            //             transaction.Commit();
            //         }
            //         catch (Exception sae)
            //         {
            //             Console.WriteLine(sae.Message);
            //             transaction.Rollback();
            //         }
            //     }


            //     #endregion
            // });


            // #endregion


            #region Concurrency

            try
            {
                var da1 = _eFContext.Blogs.Find(1);
                Console.WriteLine(da1.RowVersionStr);

                var da2 = _eFContext2.Blogs.Find(1);
                Console.WriteLine(da2.RowVersionStr);
                da2.Url = "2";
                da1.Url = "1";
                var sa = _eFContext.SaveChanges();
                //  var sqq = _eFContext2.Blogs.Remove(da2);
                var sasd = _eFContext2.SaveChanges();
            }
            catch (DbUpdateConcurrencyException aseq)
            {
                var tracking = aseq.Entries.Single();
                var or = tracking.OriginalValues.ToObject();
                var cu = tracking.CurrentValues.ToObject();
              
            }
            var da3 = _eFContext3.Blogs.Find(1);
            Console.WriteLine(da3.RowVersionStr);
            Console.WriteLine(da3.Url);


            #endregion

            return View();
        }
        public void SaveAnnotatedGraph(DbContext context, object rootEntity)
        {
            context.ChangeTracker.TrackGraph(rootEntity, n => { n.Entry.State = EntityState.Deleted; });

            context.SaveChanges();

        }
        private void IsItNew()
        {
            Console.WriteLine();
            Console.WriteLine("Show entity-specific check for key set:");
            var context = _eFContext;

            var blog = new Blog { Url = "http://sample.com" };

            // Key is not set for a new entity
            Console.WriteLine($"  Blog entity is {(IsItNew(blog) ? "new" : "existing")}.");

            context.Add(blog);
            context.SaveChanges();

            // Key is now set
            Console.WriteLine($"  Blog entity is {(IsItNew(blog) ? "new" : "existing")}.");


            Console.WriteLine();
            Console.WriteLine("Show general IsKeySet:");

            blog = new Blog { Url = "http://asample.com" };

            // Key is not set for a new entity
            Console.WriteLine($"  Blog entity is {(IsItNew(context, blog) ? "new" : "existing")}.");

            context.Add(blog);
            context.SaveChanges();

            // Key is now set
            Console.WriteLine($"  Blog entity is {(IsItNew(context, blog) ? "new" : "existing")}.");

            Console.WriteLine();
            Console.WriteLine("Show key set on Add:");

            blog = new Blog { Url = "http://ssample.com" };

            // Key is not set for a new entity
            Console.WriteLine($"  Blog entity is {(IsItNew(context, blog) ? "new" : "existing")}.");

            context.Add(blog);

            // Key is set as soon as Add assigns a key, even if it is temporary
            Console.WriteLine($"  Blog entity is {(IsItNew(context, blog) ? "new" : "existing")}.");


            Console.WriteLine();
            Console.WriteLine("Show using query to check for new entity:");

            blog = new Blog { Url = "http://qsample.com" };

            Console.WriteLine($"  Blog entity is {(IsItNew(context, blog) ? "new" : "existing")}.");

            context.Add(blog);
            context.SaveChanges();

            Console.WriteLine($"  Blog entity is {(IsItNew(context, blog) ? "new" : "existing")}.");

        }
        public bool IsItNew(Blog blog)
            => blog.BlogId == 0;
        public bool IsItNew(EFContext context, Blog blog)
            => context.Set<Blog>().Find(blog.BlogId) == null;
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
