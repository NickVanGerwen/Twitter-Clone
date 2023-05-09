using twitter_post_service.Models;
namespace twitter_post_service.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        public static void SeedData(AppDbContext context)
        {
            //if (!context.Posts.Any())
            //{
            //    Console.WriteLine("--> Seeding Data...");

            //    List<Post> Posts = new List<Post>
            //    {
            //        new Post() {Id =1 , Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc eu lorem in libero imperdiet pharetra. Ut augue elit, hendrerit sed erat nec, pulvinar venenatis turpis. Praesent luctus tellus ut odio dictum, ac ullamcorper felis pellentesque. Integer eu sagittis urna. Cras vel lectus accumsan justo vulputate aliquet. Aliquam risus enim, aliquet eget tempor nec, porta in dui. Curabitur quis tincidunt diam. In ac pellentesque nibh. Donec laoreet mi at urna bibendum,", Author = "John Doe", Date = DateTime.Now, Likes =2},
            //        new Post() {Id =2 , Message = "in feugiat urna hendrerit. Nam cursus risus id sem aliquam, vel commodo purus euismod. Quisque consectetur nec neque id sagittis. Nam at bibendum arcu. Nam rutrum vulputate nibh eget auctor. Vestibulum maximus blandit lectus, a tristique sem venenatis in. Fusce placerat leo sed maximus consequat.\r\n\r\n", Author = "John Doe", Date = DateTime.Now, Likes =11},
            //        new Post() {Id =3 , Message = "Ut iaculis tincidunt fermentum. Pellentesque rhoncus quam in mi venenatis interdum. Sed vestibulum suscipit diam ut molestie. In hac habitasse platea dictumst. ", Author = "John Doe", Date = DateTime.Now, Likes =223},
            //        new Post() {Id =4 , Message = "ac egestas ligula. Quisque eget iaculis justo, in congue turpis. Nam scelerisque viverra lectus non lobortis. Suspendisse dapibus tellus at egestas egestas. Integer at mauris non sapien molestie pulvinar. Aenean dictum pharetra leo vitae feugiat. Proin non pulvinar sapien.", Author = "John Doe", Date = DateTime.Now, Likes =223},
            //        new Post() {Id =5 , Message = "Praesent eget nisi sed velit ullamcorper cursus. Praesent eu nunc in lorem bibendum finibus sit amet in orci. Nullam in consequat odio, elementum laoreet ex. ", Author = "Jane Doe", Date = DateTime.Now, Likes =432 },
            //        new Post() {Id =6 , Message = "Etiam ac odio sed ipsum congue suscipit a non turpis. Maecenas dictum in tellus ut rhoncus. Maecenas et nibh ut lectus rutrum commodo consectetur sollicitudin nisi. ", Author = "Jane Doe", Date = DateTime.Now, Likes =312}
            //    };

            //    context.Posts.AddRange(Posts);
            //    context.SaveChanges();
            //}
            //else
            //{
            //    Console.WriteLine("--> We already have data");
            //}
        }
    }
}
