using System;
using System.Collections.Generic;

// Comment class to represent comments on a video
public class Comment
{
    public string CommenterName { get; set; }
    public string Text { get; set; }

    public Comment(string commenterName, string text)
    {
        CommenterName = commenterName;
        Text = text;
    }
}

// Video class to represent YouTube videos
public class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthSeconds { get; set; }
    private List<Comment> Comments { get; set; }

    public Video(string title, string author, int lengthSeconds)
    {
        Title = title;
        Author = author;
        LengthSeconds = lengthSeconds;
        Comments = new List<Comment>();
    }

    // Method to add a comment to the video
    public void AddComment(string commenterName, string commentText)
    {
        Comments.Add(new Comment(commenterName, commentText));
    }

    // Method to get the number of comments on the video
    public int GetNumberOfComments()
    {
        return Comments.Count;
    }

    // Method to display video information and comments
    public void DisplayVideoInfo()
    {
        Console.WriteLine("Title: " + Title);
        Console.WriteLine("Author: " + Author);
        Console.WriteLine("Length: " + LengthSeconds + " seconds");
        Console.WriteLine("Number of Comments: " + GetNumberOfComments());
        Console.WriteLine("Comments:");

        foreach (var comment in Comments)
        {
            Console.WriteLine("\tComment by " + comment.CommenterName + ": " + comment.Text);
        }

        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Creating videos
        var videos = new List<Video>
        {
            new Video("Video 1", "Author 1", 120),
            new Video("Video 2", "Author 2", 180),
            new Video("Video 3", "Author 3", 90)
        };

        // Adding comments to videos
        videos[0].AddComment("User1", "Great video!");
        videos[0].AddComment("User2", "I loved it!");
        videos[0].AddComment("User3", "Very informative.");

        videos[1].AddComment("User4", "Interesting content.");
        videos[1].AddComment("User5", "Could be better.");

        videos[2].AddComment("User6", "Short but sweet.");
        videos[2].AddComment("User7", "I enjoyed it.");

        // Displaying video information and comments
        foreach (var video in videos)
        {
            video.DisplayVideoInfo();
        }
    }
}
