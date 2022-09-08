using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpeechText
{
    public interface IDictation
    {
        Task<string> Dictate();
    }
}
