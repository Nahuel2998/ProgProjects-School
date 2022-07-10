namespace _07151129
{
    public class Question
    {
        public string Title { get; set; }
        public string[] Choices { get; set; }
        public int AnswerIndex { get; set; } = -1;

        public Question() : this("")
        { }

        public Question(string title) : this(title, Array.Empty<string>())
        { }

        public Question(string title, string[] choices)
        {
            this.Title = title;
            this.Choices = choices;
        }

        public Question(string title, string[] choices, int answerIndex) : this(title, choices)
        { this.AnswerIndex = answerIndex; }
    }

    public static class QuestionExtensions
    {
        public static Question WithTitle(this Question question, string title)
        {
            question.Title = title;
            return question;
        }

        public static Question WithChoices(this Question question, params string[] choices)
        {
            question.Choices = choices;
            return question;
        }

        public static Question WithAnswerIndex(this Question question, int answerIndex)
        {
            question.AnswerIndex = answerIndex;
            return question;
        }
    }
}