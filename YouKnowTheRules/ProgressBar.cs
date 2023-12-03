using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouKnowTheRules
{
    public class ProgressBar
    {
        private int totalSteps;
        private int currentStep;

        public ProgressBar(int totalSteps)
        {
            this.totalSteps = totalSteps;
            this.currentStep = 0;
        }

        public void Update(int currentStep)
        {
            this.currentStep = currentStep;
            Draw();
        }

        private void Draw()
        {
            int percentage = currentStep * 100 / totalSteps;

            Console.Write(" Progress: [");
            for (int i = 0; i < 22; i++)
            {
                if (i < percentage * 22 / 100)
                {
                    Console.Write("#");
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.Write($"] {percentage}%\r");
        }

        public void Complete()
        {
            Update(totalSteps);
            Console.WriteLine(); 
        }
    }
}