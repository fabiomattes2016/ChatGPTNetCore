namespace ChatGPTNetCore.Models
{
    public class ChatGptInputModel
    {
        public ChatGptInputModel(string _prompt)
        {
            prompt = $"Top 5 medicamentos para: {_prompt}.";
            model = "text-davinci-003";
            //model = "gpt-3.5-turbo";
            max_tokens = 1500;
            temperature = 0.8m;
        }

        public string model { get; set; }
        public string prompt { get; set; }
        public int max_tokens { get; set; }
        public decimal temperature { get; set; }
    }
}
