using System;
using System.Collections.Generic;

namespace YouTubeTracking
{
    // Comment class
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

    // Video class
    public class Video
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Length { get; set; }  // Length in seconds
        private List<Comment> Comments { get; set; }

        public Video(string title, string author, int length)
        {
            Title = title;
            Author = author;
            Length = length;
            Comments = new List<Comment>();
        }

        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }

        public int GetNumberOfComments()
        {
            return Comments.Count;
        }

        public List<Comment> GetComments()
        {
            return Comments;
        }
    }

    // Main Program class
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create video objects
      // Create video objects
        Video video1 = new Video("Learn all about C# in 10min", "Tech Guru", 600);
        Video video2 = new Video("Top 10 vegan food ", "Nutrition", 900);
        Video video3 = new Video("Ton Blockchain explained", "CryptoWorld", 1200);

        // Add comments to video1
        video1.AddComment(new Comment("Folusho", "Thanks for the tutorial!"));
        video1.AddComment(new Comment("Damilola", "Learnt something new, thanks!"));
        video1.AddComment(new Comment("Adekemi", "Wow, great tutorial"));

        // Add comments to video2
        video2.AddComment(new Comment("Kacee", "I am trying to be a vegan"));
        video2.AddComment(new Comment("Seun", "Awesome!"));
        video2.AddComment(new Comment("Godwin", "I tried #05 already"));

        // Add comments to video3
        video3.AddComment(new Comment("Kunmi", "Thanks for this."));
        video3.AddComment(new Comment("Tracy", "Ton blockchain to the moon!"));
        video3.AddComment(new Comment("Chkwudi", "Hopefully you can explain better"));

            // Store the videos in a list
            List<Video> videos = new List<Video> { video1, video2, video3 };

            // Iterate through the videos and display information
            foreach (var video in videos)
            {
                Console.WriteLine($"Title: {video.Title}");
                Console.WriteLine($"Author: {video.Author}");
                Console.WriteLine($"Length: {video.Length} seconds");
                Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");

                // Display each comment for the video
                foreach (var comment in video.GetComments())
                {
                    Console.WriteLine($"Comment by {comment.CommenterName}: {comment.Text}");
                }

                Console.WriteLine();  // Blank line between videos
            }
        }
    }
}
