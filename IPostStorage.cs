namespace Posts
{
    // Interfacet använder metoderna för att spara och ladda inläggen
    public interface IPostStorage
    {
        List<Post> LoadPosts();
        void SavePost(List<Post> post);
    }
}
