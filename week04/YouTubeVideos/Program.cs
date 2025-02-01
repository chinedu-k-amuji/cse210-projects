using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

public class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthInSeconds { get; set; }
    public List<Comment> Comments { get; set; }

    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
        Comments = new List<Comment>();
    }

    public int GetCommentCount()
    {
        return Comments.Count;
    }
}

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

public class Program
{
    public static void Main(string[] args)
    {
        // Create a list of videos
        List<Video> videos = new List<Video>();

        // Create and add videos to the list
        videos.Add(new Video("Funny Dog Videos", "Dog Lovers Nigeria", 120)
        {
            Comments = 
            {
                new Comment("Jane Smith", "This is hilarious!"),
                new Comment("Chinedu K. Amuji", "So cute!"),
                new Comment("Chimsimdi Ekezie", "I love dogs!")
            }
        });

        videos.Add(new Video("Space Documentary", "NASA", 3600)
        {
            Comments = 
            {
                new Comment("Doris Amuji", "Amazing footage!"),
                new Comment("David Lee", "Very informative."),
                new Comment("Sarah Williams", "Mind-blowing!") 
            }
        });

        videos.Add(new Video("Nedu Phone Accessories Limited", "Chinedu K. Amuji", 500)
        {
            Comments = 
            {
                new Comment("Michael Brown", "Good presentation!"),
                new Comment("Ifeanyi Okolo", "Wow! this is a business with solution!"),
                new Comment("Christopher White", "Loved the Business!")
            }
        });

        // Iterate through the list and display video information
        foreach (var video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.LengthInSeconds} seconds");
            Console.WriteLine($"Number of Comments: {video.GetCommentCount()}");
            Console.WriteLine("Comments:");

            foreach (var comment in video.Comments)
            {
                Console.WriteLine($"- {comment.CommenterName}: {comment.Text}");
            }

            Console.WriteLine(); // Empty line for better readability
        }
    }
}