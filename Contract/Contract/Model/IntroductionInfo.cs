using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Model
{
    public class IntroductionInfo 
    {
        public bool ShowButton { get; set; }
        public string ImagePath { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }

        public IntroductionInfo(string imagePath)
        {
            ImagePath = imagePath;
        }

        public IntroductionInfo()
        {

        }
    }
}
