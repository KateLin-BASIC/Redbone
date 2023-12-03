using Redbone.Models;
using SqlKata.Compilers;
using SqlKata.Execution;
using MySql.Data.MySqlClient;

namespace Redbone.Services;

public class SqlService
{
    private readonly string _connectionString = Environment.GetEnvironmentVariable("REDBONE_CONNECTION_STRING");

    public IEnumerable<Post> GetAllPosts()
    {
        var connection = new MySqlConnection(_connectionString);
        var db = new QueryFactory(connection, new MySqlCompiler());

        var posts = db.Query("Post")
            .Get<Post>();

        connection.Close();
        
        return posts;
    }

    public Post GetPostById(int postId)
    {
        var connection = new MySqlConnection(_connectionString);
        var db = new QueryFactory(connection, new MySqlCompiler());

        Post post = null;
        
        if (IsPostExist(postId))
        {
            post = db.Query("Post")
                .Where("ID", postId)
                .First<Post>();   
        }

        connection.Close();

        return post;
    }

    public void IncreaseViewCountByPostId(int postId)
    {
        var connection = new MySqlConnection(_connectionString);
        var db = new QueryFactory(connection, new MySqlCompiler());

        int currentViewCount = db.Query("Post").Where("ID", postId).Select("VIEWS").First<int>();

        db.Query("Post").Where("ID", postId).Update(new
        {
            Views = currentViewCount + 1
        });
        
        connection.Close();
    }

    public int CreatePost(Post post)
    {
        var connection = new MySqlConnection(_connectionString);
        var db = new QueryFactory(connection, new MySqlCompiler());

        int id = db.Query("Post").InsertGetId<int>(post);
        
        connection.Close();

        return id;
    }
    
    public void UpdatePost(int id, Post post)
    {
        var connection = new MySqlConnection(_connectionString);
        var db = new QueryFactory(connection, new MySqlCompiler());

        db.Query("Post").Where("ID", id).Update(post);
        
        connection.Close();
    }

    public void DeletePost(int postId)
    {
        var connection = new MySqlConnection(_connectionString);
        var db = new QueryFactory(connection, new MySqlCompiler());

        db.Query("Post").Where("ID", postId).Delete();
        
        connection.Close();
    }

    public bool IsPostExist(int postId)
    {
        var connection = new MySqlConnection(_connectionString);
        var db = new QueryFactory(connection, new MySqlCompiler());
        
        bool exists = db.Query("Post").Where("ID", postId).Exists();

        connection.Close();
        
        return exists;
    }
    
    public int CreateImageData(Image image)
    {
        var connection = new MySqlConnection(_connectionString);
        var db = new QueryFactory(connection, new MySqlCompiler());

        int id = db.Query("Image").InsertGetId<int>(image);
        
        connection.Close();

        return id;
    }
    
    public IEnumerable<Image> GetAllImageData()
    {
        var connection = new MySqlConnection(_connectionString);
        var db = new QueryFactory(connection, new MySqlCompiler());

        var images = db.Query("Image")
            .Get<Image>();

        connection.Close();
        
        return images;
    }
    
    public bool IsImageDataExist(int imageId)
    {
        var connection = new MySqlConnection(_connectionString);
        var db = new QueryFactory(connection, new MySqlCompiler());
        
        bool exists = db.Query("Image").Where("ID", imageId).Exists();

        connection.Close();
        
        return exists;
    }
    
    public void DeleteImageData(int imageId)
    {
        var connection = new MySqlConnection(_connectionString);
        var db = new QueryFactory(connection, new MySqlCompiler());

        db.Query("Image").Where("ID", imageId).Delete();
        
        connection.Close();
    }
    
    public Image GetImageDataById(int imageId)
    {
        var connection = new MySqlConnection(_connectionString);
        var db = new QueryFactory(connection, new MySqlCompiler());

        Image imageData = null;
        
        if (IsImageDataExist(imageId))
        {
            imageData = db.Query("Image")
                .Where("ID", imageId)
                .First<Image>();   
        }

        connection.Close();

        return imageData;
    }
}