namespace TBDName.Models
{
    public class Question
    {
        // Properties for the question details
        public string QuestionText { get; set; }
        public string Topic { get; set; }
        public int Difficulty { get; set; }
        public string ExpectedAnswer { get; set; } // Expected answer for evaluation

        // Constructor to initialize a question
        public Question(string questionText, string topic, int difficulty, string expectedAnswer)
        {
            QuestionText = questionText;
            Topic = topic;
            Difficulty = difficulty;
            ExpectedAnswer = expectedAnswer;
        }

        // Method to check if a user's answer is correct
        public bool IsAnswerCorrect(string userAnswer)
        {

            return true;
        }

        // Method to get the question's details as a formatted string
        public string GetQuestionDetails()
        {
            return $"{Topic} - {QuestionText} (Difficulty: {Difficulty})";
        }
    }

}
