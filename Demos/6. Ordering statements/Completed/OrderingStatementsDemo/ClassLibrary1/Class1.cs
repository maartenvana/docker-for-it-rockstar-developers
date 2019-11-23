using Newtonsoft.Json;

namespace ClassLibrary1
{
    public static class Class1
    {
        public static void JustDoSomething()
        {
            JsonConvert.SerializeObject(new
            {
                asdfasdf = "asdfasdf"
            });
        }
    }
}