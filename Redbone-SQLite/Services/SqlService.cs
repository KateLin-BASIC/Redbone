using Microsoft.Data.Sqlite;
using Redbone.Models;
using SqlKata.Compilers;
using SqlKata.Execution;
using Redbone.Helpers;

namespace Redbone.Services;

public class SqlService
{
    private readonly string _connectionString;

    public SqlService(IConfiguration configuration)
    {
        _connectionString = $"Data Source={configuration.GetValue<string>("DatabaseFileName")}";
        
        string currentPath = Directory.GetCurrentDirectory();
        string databaseFileName = configuration.GetValue<string>("DatabaseFileName");
        string filePath = @$"{currentPath}\{databaseFileName}";
        
        if (!File.Exists(filePath))
            SqlUtilities.InitializeDatabase(databaseFileName);
    }

    public int GetAvailablePostId()
    {
        var connection = new SqliteConnection(_connectionString);
        var db = new QueryFactory(connection, new SqliteCompiler());

        int lastPostId = db.Query("post").Exists() ? db.Query("post").Get<Post>().Last().Id : 0;
        
        connection.Close();

        return lastPostId + 1;
    }

    public IEnumerable<Post> GetAllPosts()
    {
        var connection = new SqliteConnection(_connectionString);
        var db = new QueryFactory(connection, new SqliteCompiler());

        var posts = db.Query("post")
            .Get<Post>();

        connection.Close();
        
        return posts;
    }

    public Post GetPostById(int postId)
    {
        var connection = new SqliteConnection(_connectionString);
        var db = new QueryFactory(connection, new SqliteCompiler());

        Post post = null;
        
        if (IsPostExist(postId))
        {
            post = db.Query("post")
                .Where("id", postId)
                .First<Post>();   
        }

        connection.Close();

        return post;
    }

    public void IncreaseViewCountByPostId(int postId)
    {
        var connection = new SqliteConnection(_connectionString);
        var db = new QueryFactory(connection, new SqliteCompiler());

        int currentViewCount = db.Query("post").Where("id", postId).Select("view").First<int>();

        db.Query("post").Where("id", postId).Update(new
        {
            View = currentViewCount + 1
        });
        
        connection.Close();
    }

    public int CreatePost(Post post)
    {
        var connection = new SqliteConnection(_connectionString);
        var db = new QueryFactory(connection, new SqliteCompiler());

        int id = db.Query("post").InsertGetId<int>(post);
        
        connection.Close();

        return id;
    }
    
    public void UpdatePost(int id, Post post)
    {
        var connection = new SqliteConnection(_connectionString);
        var db = new QueryFactory(connection, new SqliteCompiler());

        db.Query("post").Where("id", id).Update(post);
        
        connection.Close();
    }

    public void DeletePost(int postId)
    {
        var connection = new SqliteConnection(_connectionString);
        var db = new QueryFactory(connection, new SqliteCompiler());

        db.Query("post").Where("id", postId).Delete();
        
        connection.Close();
    }

    public bool IsPostExist(int postId)
    {
        var connection = new SqliteConnection(_connectionString);
        var db = new QueryFactory(connection, new SqliteCompiler());
        
        bool exists = db.Query("post").Where("id", postId).Exists();

        connection.Close();
        
        return exists;
    }
    
    public int GetAvailableImageId()
    {
        var connection = new SqliteConnection(_connectionString);
        var db = new QueryFactory(connection, new SqliteCompiler());

        int lastPostId = db.Query("image").Exists() ? db.Query("image").Get<Image>().Last().Id : 0;
        
        connection.Close();

        return lastPostId + 1;
    }
    
    public int CreateImageData(Image image)
    {
        var connection = new SqliteConnection(_connectionString);
        var db = new QueryFactory(connection, new SqliteCompiler());

        int id = db.Query("image").InsertGetId<int>(image);
        
        connection.Close();

        return id;
    }
    
    public IEnumerable<Image> GetAllImageData()
    {
        var connection = new SqliteConnection(_connectionString);
        var db = new QueryFactory(connection, new SqliteCompiler());

        var images = db.Query("image")
            .Get<Image>();

        connection.Close();
        
        return images;
    }
    
    public bool IsImageDataExist(int imageId)
    {
        var connection = new SqliteConnection(_connectionString);
        var db = new QueryFactory(connection, new SqliteCompiler());
        
        bool exists = db.Query("image").Where("id", imageId).Exists();

        connection.Close();
        
        return exists;
    }
    
    public void DeleteImageData(int imageId)
    {
        var connection = new SqliteConnection(_connectionString);
        var db = new QueryFactory(connection, new SqliteCompiler());

        db.Query("image").Where("id", imageId).Delete();
        
        connection.Close();
    }
    
    public Image GetImageDataById(int imageId)
    {
        var connection = new SqliteConnection(_connectionString);
        var db = new QueryFactory(connection, new SqliteCompiler());

        Image imageData = null;
        
        if (IsImageDataExist(imageId))
        {
            imageData = db.Query("image")
                .Where("id", imageId)
                .First<Image>();   
        }

        connection.Close();

        return imageData;
    }
}