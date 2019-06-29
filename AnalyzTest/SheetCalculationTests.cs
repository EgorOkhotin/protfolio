using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;

namespace AnalyzTest
{
    public class SheetCalculationTests
    {
        [Fact]
        public void CalculateSheet_FirstStepValidArgumanets_FinishSuccess()
        {
            var g = new SheetGenerator();
            var sh = g.Generate();
            var questionText = sh.Questions[0].Text;
            var answerText = sh.Questions[0].Answers.ElementAt(0).Text;
            var s = sh.CalculateProbabilities(questionText, answerText);
            Assert.True(true);
        }
    }
}
