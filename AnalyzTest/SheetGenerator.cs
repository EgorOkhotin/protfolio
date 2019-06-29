using System;
using System.Collections.Generic;
using System.Text;
using Analyz;
using System.Linq;

namespace AnalyzTest
{
    class SheetGenerator
    {
        const int ServicesCount = 7;
        decimal[] tempProbabilities = { 0.2m, 0.3m, 0.15m, 0.1m, 0.05m, 0.1m, 0.1m };
        public Sheet Generate()
        {
            decimal[] defaultProbabilities = { 0.2m, 0.3m, 0.15m, 0.1m, 0.05m, 0.1m, 0.1m };
            return new Sheet(GenerateQuestions(), defaultProbabilities);
        }

        private List<Question> GenerateQuestions()
        {
            var result = new List<Question>();
            for (int i = 0; i < 3; i++)
                result.Add(GenerateQuestion(i));
            return result;
        }

        private Question GenerateQuestion(int index)
        {
            var result = new Question();
            result.Id = index + 1;
            result.Text = $"TEST_QUESTION_{index+1}";
            result.Answers = GenerateAnswers();
            return result;
        }

        private IEnumerable<Answer> GenerateAnswers()
        {
            var result = new List<Answer>();
            for(int i=0; i<4; i++)
            {
                result.Add(GenerateAnswer(i));
            }
            return result;
        }

        private Answer GenerateAnswer(int index)
        {
            var result = new Answer();
            result.Probabilities = GenerateProbabilities();
            result.Text = $"ANSWER_{index + 1}";
            return result;
        }

        private IEnumerable<Probability> GenerateProbabilities()
        {
            var result = new List<Probability>(ServicesCount);
            var random = new Random();

            for (int i=0; i<ServicesCount; i++)
            {
                result.Add(new Probability(new Service($"SERVICE{i}"), tempProbabilities[i]));
            }
            tempProbabilities = ShuffleList(tempProbabilities.ToList()).ToArray();
            return result;
        }

        private List<E> ShuffleList<E>(List<E> inputList)
        {
            List<E> randomList = new List<E>();

            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count); //Choose a random object in the list
                randomList.Add(inputList[randomIndex]); //add it to the new, random list
                inputList.RemoveAt(randomIndex); //remove to avoid duplicates
            }

            return randomList; //return the new random list
        }
    }
}
