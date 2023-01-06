using System;
using System.Collections.Generic;
using System.Text;

namespace Cviko6.Commands
{
    public interface IAction<T>
    {
        void Execute(T t);
    }
}
