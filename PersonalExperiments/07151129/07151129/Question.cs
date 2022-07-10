namespace _07151129
{
    public class Question
    {
        public string Title { get; set; }
        public string[] Choices { get; set; }
        public int AnswerIndex { get; set; } = -1;
        public Action? RunBefore { get; set; }

        public Question() : this("")
        { }

        public Question(string title) : this(title, Array.Empty<string>())
        { }

        public Question(string title, string choices) : this(title, choices.Split(':', StringSplitOptions.TrimEntries))
        { }

        public Question(string title, string[] choices)
        {
            this.Title = title;
            this.Choices = choices;
        }

        public Question(string title, string choices, int answerIndex) : this(title, choices.Split(':', StringSplitOptions.TrimEntries), answerIndex)
        { }

        public Question(string title, string[] choices, int answerIndex) : this(title, choices)
        { this.AnswerIndex = answerIndex; }

        public Question(string title, string choices, int answerIndex, Action runBefore) : this(title, choices, answerIndex)
        { this.RunBefore = runBefore; }

        public Question(string title, string[] choices, int answerIndex, Action runBefore) : this(title, choices, answerIndex)
        { this.RunBefore = runBefore; }

        public Question WithTitle(string title)
        {
            this.Title = title;
            return this;
        }

        public Question WithChoices(string[] choices)
        {
            this.Choices = choices;
            return this;
        }

        public Question WithAnswerIndex(int answerIndex)
        {
            this.AnswerIndex = answerIndex;
            return this;
        }

        public Question WithRunBefore(Action runBefore)
        {
            this.RunBefore = runBefore;
            return this;
        }
    }
}