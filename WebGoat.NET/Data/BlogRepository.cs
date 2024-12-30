// Unless explicitly stated otherwise all files in this repository are licensed
// under the Apache License Version 2.0.
// This product includes software developed at Datadog (https://www.datadoghq.com/)
// Copyright 2025-present Datadog, Inc.

using Microsoft.EntityFrameworkCore;
using WebGoat.NET.Models;

namespace WebGoat.NET.Data;

public class BlogRepository(ApplicationDbContext context)
{
    public List<BlogPost> GetPosts(int limit = 5)
    {
        return context.BlogPosts.OrderByDescending(p => p.CreatedAt).Take(limit).ToList();
    }
    
    public BlogPost? GetPostById(int id)
    {
        return context.BlogPosts.Find(id);
    }
    
    public List<BlogComment> GetCommentsForPost(int postId, int? limit)
    {
        if (limit == null)
        {
            return context.BlogComments.Where(c => c.PostId == postId).OrderBy(c => c.CreatedAt).ToList();
        }

        return context.BlogComments.Where(c => c.PostId == postId).OrderBy(c => c.CreatedAt).Take(limit.Value).ToList();
    }
    
    public void CreateComment(int postId, string authorName, string content)
    {
        var sql = "INSERT INTO BlogComments (" +
            "PostId, AuthorName, Content, CreatedAt" +
            ") VALUES (" +
            $"'{postId}','{authorName}','{content}','{DateTime.Now:yyyy-MM-dd HH:mm:ss}')";
        
        using var command = context.Database.GetDbConnection().CreateCommand();
        command.CommandText = sql;
        context.Database.OpenConnection();
        command.ExecuteNonQuery();
    }
}