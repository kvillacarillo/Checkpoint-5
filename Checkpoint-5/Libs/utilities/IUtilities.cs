using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint_5.Libs.utilities
{
    internal interface IUtilities
    {
        public T InputHandler<T>(String inputName, out T value) where T : IConvertible;
    }
}
