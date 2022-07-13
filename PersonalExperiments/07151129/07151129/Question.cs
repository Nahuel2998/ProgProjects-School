namespace _07151129
{
    public class Question
    {
        public string Title { get; set; }
        public string[] Choices { get; set; }
        public int AnswerIndex { get; set; } = -1;
        public Action? RunBefore { get; set; }
        public Action? RunIfCorrect { get; set; }
        public Tuple<int, Action>? RunIfAnswerIndexIs { get; set; }
        public Func<string, string[], int>? CustomDrawFunc { get; set; }

        public Question() : this("")
        { }

        public Question(string title) : this(title, Array.Empty<string>())
        { }

        public Question(string title, string[] choices, int answerIndex = -1,
        Action? runBefore = null, Action? runIfCorrect = null, 
        Tuple<int, Action>? runIfAnswerIndexIs = null, Func<string, string[], int>? customDrawFunc = null)
        {
            this.Title = title;
            this.Choices = choices;
            this.AnswerIndex = answerIndex;
            this.RunBefore = runBefore;
            this.RunIfCorrect = runIfCorrect;
            this.RunIfAnswerIndexIs = runIfAnswerIndexIs;
            this.CustomDrawFunc = customDrawFunc;
        }

        public Question(string title, string choices, int answerIndex = -1,
        Action? runBefore = null, Action? runIfCorrect = null, 
        Tuple<int, Action>? runIfAnswerIndexIs = null, Func<string, string[], int>? customDrawFunc = null) 
        : this(title, choices.Split(':', StringSplitOptions.TrimEntries), answerIndex, runBefore, runIfCorrect, runIfAnswerIndexIs, customDrawFunc)
        { }

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