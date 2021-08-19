using System;
using System.Collections.Generic;
using System.Text;

namespace calc
{
    public interface ISoundEffect
    {
        void HitSound();
        void CorrectSound();
        void IncorrectSound();
        void ResultSound();
    }
}
